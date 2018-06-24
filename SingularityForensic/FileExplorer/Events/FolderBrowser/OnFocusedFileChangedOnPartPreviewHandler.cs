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
    /// <summary>
    /// 文件选中行发生变化的时候预览变化;
    /// </summary>
    [Export(typeof(IFocusedFileRowChangedEventHandler))]
    class OnFocusedFileChangedOnPartPreviewHandler : IFocusedFileRowChangedEventHandler {
        [ImportingConstructor]
        public OnFocusedFileChangedOnPartPreviewHandler([ImportMany]IEnumerable<IPreviewProvider> previewerProviders) {
            this._previewerProviders = previewerProviders.OrderBy(p => p.Order).ToArray();
        }

        private IEnumerable<IPreviewProvider> _previewerProviders;
        public int Sort => 0;

        public bool IsEnabled => true;
        /// <summary>
        /// 记录优先级;
        /// </summary>
        private long prior = 0;

        public void Handle((IFolderBrowserViewModel owner, IFileRow file) tuple) {
            if (!(tuple.owner is IFolderBrowserViewModel folderBrowserVM)) {
                return;
            }
            var tab = DocumentService.MainDocumentService.CurrentDocuments.
                FirstOrDefault(p => p.GetInstance<IFile>(Contracts.FileExplorer.Constants.DocumentTag_File) == folderBrowserVM.HaveFileCollection);
            if (tab == null) {
                return;
            }

            var previewerDoc = tab.GetInstance<IDocument>(Constants.Document_FilePreviewer);
            if (previewerDoc == null) {
                LoggerService.WriteCallerLine($"{nameof(previewerDoc)} can't be null.");
            }

            previewerDoc.UIObject = null;
            var localPrior = ++prior;
            ThreadInvoker.BackInvoke(() => {
                var previewer = GetPreviewer(tuple.file.File);
                if(previewer == null) {
                    return;
                }

                if (localPrior != prior) {
                    return;
                }
                ThreadInvoker.UIInvoke(() => {
                    try {
                        previewerDoc.GetInstance<IPreviewer>(Constants.DocumentTag_FilePreviewer)?.Dispose();
                        previewerDoc.SetInstance(previewer, Constants.DocumentTag_FilePreviewer);
                        previewerDoc.UIObject = previewer.UIObject;
                    }
                    catch (Exception ex) {
                        LoggerService.WriteCallerLine(ex.Message);
                    }
                });
            });
            
        }

        /// <summary>
        /// 获取预览器;
        /// </summary>
        private IPreviewer GetPreviewer(IFile file) {
            var fileBaseStream = file.GetInputStream();
            if (fileBaseStream == null) {
                LoggerService.WriteCallerLine($"{nameof(fileBaseStream)} can't be null.");
                return null;
            }

            if (fileBaseStream.Length > 1024 * 1024 * 100) {
                fileBaseStream.Dispose();
                return null;
            }

            if (_previewerProviders == null) {
                LoggerService.WriteCallerLine($"{nameof(_previewerProviders)} can't be null.");
                return null;
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
                    if (provider.NeedSaveToLocal && !savedToLocal) {
                        //创建临时文件夹;
                        if (!System.IO.Directory.Exists(tempDirectory)) {
                            System.IO.Directory.CreateDirectory(tempDirectory);
                        }

                        using (var tempFs = File.Create(tempFileName)) {
                            fileBaseStream.CopyTo(tempFs);
                            savedToLocal = true;
                        }
                    }

                    if (provider.NeedSaveToLocal && savedToLocal) {
                        ThreadInvoker.UIInvoke(() => {
                            previewer = provider.CreatePreviewer(tempFileName, file.Name);
                        });
                        
                        isLocalPreviewer = previewer != null;
                    }
                    else {
                        ThreadInvoker.UIInvoke(() => {
                            previewer = provider.CreatePreviewer(fileBaseStream, file.Name);
                        });
                    }

                    if (previewer != null) {
                        break;
                    }
                }
                catch (Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            }
            
            if (isLocalPreviewer) {
                fileBaseStream.Dispose();
            }

            return previewer;
        }
    }
}
