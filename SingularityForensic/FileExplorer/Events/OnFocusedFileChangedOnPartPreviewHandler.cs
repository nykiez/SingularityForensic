using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Previewers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;

namespace SingularityForensic.FileExplorer.Events {
    [Export(typeof(IFocusedFileRowChangedEventHandler))]
    class OnFocusedFileChangedOnPartPreviewHandler : IFocusedFileRowChangedEventHandler {
        [ImportingConstructor]
        public OnFocusedFileChangedOnPartPreviewHandler([ImportMany]IEnumerable<IPreviewProvider> previewerProviders) {
            this._previewerProviders = previewerProviders.OrderBy(p => p.Order).ToArray();
        }

        private IEnumerable<IPreviewProvider> _previewerProviders;
        public int Sort => 0;

        public bool IsEnabled => true;

        public void Handle((IFolderBrowserViewModel owner, IFileRow file) tuple) {
            if (!(tuple.owner is IFolderBrowserViewModel folderBrowserVM)) {
                return;
            }

            var tab = DocumentService.MainDocumentService.CurrentDocuments.
                FirstOrDefault(p => p.GetIntance<IFile>(Contracts.FileExplorer.Constants.DocumentTag_File) == folderBrowserVM.HaveFileCollection);
            if (tab == null) {
                return;
            }

            var previewerDoc = tab.GetIntance<IDocument>(Constants.Document_FilePreviewer);
            if (previewerDoc == null) {
                LoggerService.WriteCallerLine($"{nameof(previewerDoc)} can't be null.");
            }

            if (!(tuple.file.File is IBlockGroupedFile blockGrouped)) {
                return;
            }

            var fileBaseStream = blockGrouped.GetInputStream();
            if (fileBaseStream == null) {
                LoggerService.WriteCallerLine($"{nameof(fileBaseStream)} can't be null.");
                return;
            }

            if (_previewerProviders == null) {
                LoggerService.WriteCallerLine($"{nameof(_previewerProviders)} can't be null.");
                return;
            }

            var tempDirectory = $"{Environment.CurrentDirectory}/{Constants.TempDirectoryName}/";
            var tempFileName = tempDirectory + Path.GetRandomFileName();
            //是否已经保存到本地;
            var savedToLocal = false;
            IPreviewer previewer = null;
            //是否为本地预览,若为本地预览,则InputStream即可在本方法执行后释放;
            //否则InputStream将在Previewer释放时被释放;
            var isLocalPreviewer = false;

            foreach (var provider in _previewerProviders) {
                try {
                    //若要求保存到本地,则临时保存;
                    if (provider.NeedSaveLocal && !savedToLocal) {
                        //创建临时文件夹;
                        if (!System.IO.Directory.Exists(tempDirectory)) {
                            System.IO.Directory.CreateDirectory(tempDirectory);
                        }

                        using (var tempFs = File.Create(tempFileName)) {
                            fileBaseStream.CopyTo(tempFs);
                            savedToLocal = true;
                        }
                    }

                    if (provider.NeedSaveLocal && savedToLocal) {
                        previewer = provider.CreatePreviewer(tempFileName, tuple.file.File.Name);
                        isLocalPreviewer = previewer != null;
                    }
                    else {
                        previewer = provider.CreatePreviewer(fileBaseStream, tuple.file.File.Name);
                    }

                    if (previewer != null) {
                        break;
                    }
                }
                catch (Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            }

            if (previewer == null) {
                return;
            }

            try {
                previewerDoc.GetIntance<IPreviewer>(Constants.DocumentTag_FilePreviewer)?.Dispose();
                previewerDoc.SetInstance(previewer, Constants.DocumentTag_FilePreviewer);
                previewerDoc.UIObject = previewer.View;
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }

            if (isLocalPreviewer) {
                fileBaseStream.Dispose();
            }

        }
    }
}
