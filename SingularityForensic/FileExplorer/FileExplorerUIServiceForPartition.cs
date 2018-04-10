using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Document.Events;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Contracts.Previewers;
using SingularityForensic.FileExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.FileExplorer {

    [Export]
    public class FileExplorerUIServiceForPartition {
        [ImportingConstructor]
        public FileExplorerUIServiceForPartition([ImportMany]IEnumerable<IPreviewProvider> previewerProviders) {
            this._previewerProviders = previewerProviders.OrderBy(p => p.Order);
        }

        private IEnumerable<IPreviewProvider> _previewerProviders;

        public void Initialize() {
            RegisterEvents();
        }

        private void RegisterEvents() {
            //文档被添加时的响应;
            PubEventHelper.GetEvent<DocumentAddedEvent>().Subscribe(tuple => {
                //分区加入文档时呈现十六进制;
                OnDocumentAddedEventOnPartHex(tuple);
                //分区加入文档时呈现预览;
                OnDocumentAddedEventOnPartPreview(tuple);
            });

            //分区选中行发生变更时更新响应;
            PubEventHelper.GetEvent<FocusedFileChangedEvent>().Subscribe(tuple => {
                //分区选中行发生变更时,更新十六进制变化;
                OnFocusedFileChangedOnPartHex(tuple);
                //分区选中行发生变更时,更新预览;
                OnFocusedFileChangedOnPartPreview(tuple);
            });

            //文档被添加时的响应;
            PubEventHelper.GetEvent<DocumentAddedEvent>().Subscribe(tuple => {
                //分区加入文档时呈现主视图;
                OnDocumentAddedEventOnPartition(tuple);
            });

            //文档关闭时解除列表引用;
            PubEventHelper.GetEvent<DocumentClosedEvent>().Subscribe(OnDocumentClosed);
        }

        /// <summary>
        /// 分区选中行变化时,预览响应;
        /// </summary>
        /// <param name="tuple"></param>
        private void OnFocusedFileChangedOnPartPreview((object sender, FileBase file) tuple) {
            if (!(tuple.sender is IFolderBrowserViewModel folderBrowserVM)) {
                return;
            }

            var tab = DocumentService.MainDocumentService.CurrentDocuments.
                FirstOrDefault(p => p.GetIntance<FileBase>(Constants.DocumentTag_File) == folderBrowserVM.Part);
            if (tab == null) {
                return;
            }

            var previewerDoc = tab.GetIntance<IDocument>(Constants.Document_FilePreviewer);
            if (previewerDoc == null) {
                LoggerService.WriteCallerLine($"{nameof(previewerDoc)} can't be null.");
            }

            if (!(tuple.file is IBlockGroupedFile blockGrouped)) {
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

            var saved = false;
            IPreviewer previewer = null;

            foreach (var provider in _previewerProviders) {
                try {
                    //若要求保存到本地,则临时保存;
                    if (provider.NeedSaveLocal && !saved) {
                        //创建临时文件夹;
                        if (!System.IO.Directory.Exists(tempDirectory)) {
                            System.IO.Directory.CreateDirectory(tempDirectory);
                        }

                        using (var tempFs = File.Create(tempFileName)) {
                            fileBaseStream.CopyTo(tempFs);
                            saved = true;
                        }
                    }

                    if (provider.NeedSaveLocal) {
                        if (saved) {
                            previewer = provider.CreatePreviewer(tempFileName, tuple.file.Name);
                        }
                    }
                    else {
                        previewer = provider.CreatePreviewer(fileBaseStream);
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
                previewerDoc.GetIntance<IPreviewer>(Constants.Document_FilePreviewer)?.Dispose();
                previewerDoc.SetInstance(previewer, Constants.Document_FilePreviewer);
                previewerDoc.UIObject = previewer.View;
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
        }

        /// <summary>
        /// 分区加入文档时呈现预览;
        /// </summary>
        /// <param name="tuple"></param>
        private void OnDocumentAddedEventOnPartPreview((IDocument tab, IDocumentService owner) tuple) {
            if (!(tuple.tab is IEnumerableDocument enumDoc)) {
                return;
            }

            var part = enumDoc.GetIntance<FileBase>(Constants.DocumentTag_File) as Partition;
            if (part == null) {
                LoggerService.WriteCallerLine($"{nameof(part)} can't be null.");
                return;
            }

            var doc = enumDoc.AddNewDocument();
            doc.Title = LanguageService.FindResourceString(Constants.DocumentTitle_FilePreview);
            enumDoc.AddDocument(doc);
            enumDoc.SetInstance(doc, Constants.Document_FilePreviewer);
        }

        /// <summary>
        /// 分区选中行发生变更新十六进制变化;
        /// </summary>
        /// <param name="tuple"></param>
        private void OnFocusedFileChangedOnPartHex((object sender, FileBase file) tuple) {
            if (!(tuple.sender is IFolderBrowserViewModel folderBrowserVM)) {
                return;
            }

            var tab = DocumentService.MainDocumentService.CurrentDocuments.
                FirstOrDefault(p => p.GetIntance<FileBase>(Constants.DocumentTag_File) == folderBrowserVM.Part);
            if (tab == null) {
                return;
            }

            var partHexDataContext = tab.GetIntance<IHexDataContext>(Constants.HexDataContext_FolderBrowser_Partition);
            var fileHexDataContext = tab.GetIntance<IHexDataContext>(Constants.HexDataContext_FolderBrowser_File);

            if (!(tuple.file is IBlockGroupedFile blockGrouped)) {
                return;
            }

            var startLBA = blockGrouped.GetStartLBA();
            if (startLBA != null && partHexDataContext != null) {
                partHexDataContext.Position = startLBA.Value;
            }

            var fileBaseStream = blockGrouped.GetInputStream();
            if (fileHexDataContext != null) {
                fileHexDataContext.Stream = fileBaseStream;
            }
        }

        /// <summary>
        /// 分区加入文档时呈现十六进制;
        /// </summary>
        /// <param name="obj"></param>
        private void OnDocumentAddedEventOnPartHex((IDocument tab, IDocumentService owner) tuple) {
            if (!(tuple.tab is IEnumerableDocument enumDoc)) {
                return;
            }

            var part = enumDoc.GetIntance<FileBase>(Constants.DocumentTag_File) as Partition;
            if (part == null) {
                return;
            }

            var hexPartTuple = FileExplorerUIHelper.GetBlockedStreamHexDocument(part);
            if (hexPartTuple == null) {
                return;
            }

            hexPartTuple.Value.doc.Title = LanguageService.FindResourceString(Constants.DocumentTitle_HexPartition);

            var hexFileTuple = FileExplorerUIHelper.GetBlockedStreamHexDocument(null);
            if (hexFileTuple == null) {
                return;
            }
            hexFileTuple.Value.doc.Title = LanguageService.FindResourceString(Constants.DocumentTitle_HexFile);

            //_hexTuplesForPartDocs.Add((part, hexPartTuple.Value.hexDataContext,hexFileTuple.Value.hexDataContext));

            enumDoc.AddDocument(hexPartTuple.Value.doc);
            enumDoc.AddDocument(hexFileTuple.Value.doc);
            enumDoc.SelectedDocument = hexPartTuple.Value.doc;

            enumDoc.SetInstance(hexPartTuple.Value.hexDataContext, Constants.HexDataContext_FolderBrowser_Partition);
            enumDoc.SetInstance(hexFileTuple.Value.hexDataContext, Constants.HexDataContext_FolderBrowser_File);
        }

        /// <summary>
        /// 分区加入文档时的呈现主视图;
        /// </summary>
        /// <param name="tuple"></param>
        private void OnDocumentAddedEventOnPartition((IDocument tab, IDocumentService owner) tuple) {
            if (!(tuple.tab is IEnumerableDocument enumDoc)) {
                return;
            }

            var part = enumDoc.GetIntance<FileBase>(Constants.DocumentTag_File) as Partition;
            if (part == null) {
                return;
            }

            var folderBrowser = ServiceProvider.Current?.
                GetInstance<FrameworkElement>(Constants.FolderBrowserView);

            if (folderBrowser == null) {
                LoggerService.WriteCallerLine($"{nameof(folderBrowser)} can't be null.");
                return;
            }

            var vm = new FolderBrowserViewModel(part);
            folderBrowser.DataContext = vm;
            enumDoc.MainUIObject = folderBrowser;
        }

        /// <summary>
        /// 文档关闭时;
        /// </summary>
        /// <param name="tuple"></param>
        private void OnDocumentClosed((IDocument tab, IDocumentService owner) tuple) {

        }
    }
}
