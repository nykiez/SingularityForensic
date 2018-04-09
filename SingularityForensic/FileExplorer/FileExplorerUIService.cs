using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Document.Events;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Contracts.MainPage.Events;
using SingularityForensic.Contracts.Previewers;
using SingularityForensic.Contracts.TreeView;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.IO;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.FileExplorer.ViewModels;

namespace SingularityForensic.FileExplorer {
    /// <summary>
    /// 文件系统资源管理器UI响应单位;
    /// </summary>
    [Export]
    public class FileExplorerUIService {
        [ImportingConstructor]
        public FileExplorerUIService([ImportMany]IEnumerable<IPreviewProvider> previewerProviders) {
            this._previewerProviders = previewerProviders.OrderBy(p => p.Order);
        }

        private IEnumerable<IPreviewProvider> _previewerProviders;
        
        //保存设备文档-十六进制关联状态的列表;
        private List<(Device device, IHexDataContext deviceHexContext, IHexDataContext partHexContext)?>
            _hexTuplesForDeviceDocs = new List<(Device device, IHexDataContext deviceHexContext, IHexDataContext partHexContext)?>();

        //保存分区文档-十六进制关联状态的列表;
        private List<(Partition part, IHexDataContext partHexContext, IHexDataContext fileHexContext)?>
            _hexTuplesForPartDocs = new List<(Partition part, IHexDataContext partHexContext, IHexDataContext fileHexContext)?>();

        //保存分区文档-预览关联状态的列表;
        private List<(Partition part, IDocument previewDoc, IPreviewer previewer)?>
            _previewTuplesForPartDocs = new List<(Partition part, IDocument previewDoc, IPreviewer previewer)?>();

        
        public void RegisterEvents() {
            //为设备案件文件节点加入文件系统子节点;
            PubEventHelper.GetEvent<TreeNodeAddedEvent>().Subscribe(OnTreeUnitAdded);

            //加入文件系统节点响应(左键);
            PubEventHelper.GetEvent<TreeNodeClickEvent>().Subscribe(OnTreeUnitClickOnFileSystemUnit);
            
            //设备加入文档时的呈现主视图;
            PubEventHelper.GetEvent<DocumentAddedEvent>().Subscribe(OnDocumentAddedEventOnDevice);

            //设备加入文档时呈现十六进制;
            PubEventHelper.GetEvent<DocumentAddedEvent>().Subscribe(OnDocumentAddedEventOnDeviceHex);
            
            //设备选中行发生变更时十六进制变化;
            PubEventHelper.GetEvent<FocusedFileChangedEvent>().Subscribe(OnFocusedFileChangedOnDeviceHex);

            //文档被添加时的响应;
            PubEventHelper.GetEvent<DocumentAddedEvent>().Subscribe(tuple => {
                //分区加入文档时呈现主视图;
                OnDocumentAddedEventOnPartition(tuple);
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
            
            //文档关闭时解除列表引用;
            PubEventHelper.GetEvent<DocumentClosedEvent>().Subscribe(OnDocumentClosed);
        }

        private void OnFocusedFileChangedOnPartPreview((object sender,FileBase file) tuple) {
            //var previewTuple = _previewTuplesForPartDocs.FirstOrDefault(p => p?.part == tuple.parent);
            //if (previewTuple == null) {
            //    LoggerService.WriteCallerLine($"{nameof(previewTuple)} can't be null.");
            //    return;
            //}
            
            //if (!(tuple.file is IBlockGroupedFile blockGrouped)) {
            //    return;
            //}

            //var fileBaseStream = blockGrouped.GetInputStream();
            //if(fileBaseStream == null) {
            //    LoggerService.WriteCallerLine($"{nameof(fileBaseStream)} can't be null.");
            //    return;
            //}

            //if(_previewerProviders == null) {
            //    LoggerService.WriteCallerLine($"{nameof(_previewerProviders)} can't be null.");
            //    return;
            //}

            //var tempDirectory = $"{Environment.CurrentDirectory}/{Constants.TempDirectoryName}/";
            //var tempFileName = tempDirectory + Path.GetRandomFileName();

            //var saved = false;
            //IPreviewer previewer = null;

            //foreach (var provider in _previewerProviders) {
            //    try {
            //        //若要求保存到本地,则临时保存;
            //        if (provider.NeedSaveLocal && !saved) {
            //            //创建临时文件夹;
            //            if (!System.IO.Directory.Exists(tempDirectory)) {
            //                System.IO.Directory.CreateDirectory(tempDirectory);
            //            }

            //            using (var tempFs = File.Create(tempFileName)) {
            //                fileBaseStream.CopyTo(tempFs);
            //                saved = true;
            //            }
            //        }
                    
            //        if (provider.NeedSaveLocal) {
            //            if (saved) {
            //                previewer = provider.CreatePreviewer(tempFileName, tuple.file.Name);
            //            }
            //        }
            //        else {
            //            previewer = provider.CreatePreviewer(fileBaseStream);
            //        }

            //        if(previewer != null) {
            //            break;
            //        }
            //    }
            //    catch (Exception ex) {
            //        LoggerService.WriteCallerLine(ex.Message);
            //    }
            //}
            
            //if(previewer != null) {
            //    var tupleValue = previewTuple.Value;
            //    tupleValue.previewer?.Dispose();
            //    tupleValue.previewer = previewer;
            //    tupleValue.previewDoc.UIObject = previewer.View;

            //    _previewTuplesForPartDocs.Remove(previewTuple);
            //    _previewTuplesForPartDocs.Add(tupleValue);
            //}

            
        }

        /// <summary>
        /// 分区加入文档时呈现预览;
        /// </summary>
        /// <param name="tuple"></param>
        private void OnDocumentAddedEventOnPartPreview((IDocument tab, IDocumentService owner) tuple) {
            if (!(tuple.tab is IEnumerableDocument enumDoc)) {
                return;
            }

            if (!(enumDoc.Tag is Partition part)) {
                return;
            }

            var doc = enumDoc.CreateNewDocument();
            doc.Title = LanguageService.FindResourceString(Constants.DocumentTitle_FilePreview);
            enumDoc.AddDocument(doc);
            _previewTuplesForPartDocs.Add((part, doc,null));
        }

        /// <summary>
        /// 分区选中行发生变更新十六进制变化;
        /// </summary>
        /// <param name="tuple"></param>
        private void OnFocusedFileChangedOnPartHex((object sender, FileBase file) tuple) {
            //var focusedTuple = _hexTuplesForPartDocs.FirstOrDefault(p => p?.part == tuple.parent);
            //if (focusedTuple == null) {
            //    LoggerService.WriteCallerLine($"{nameof(focusedTuple)} can't be null.");
            //    return;
            //}

            //if (!(tuple.file is IBlockGroupedFile blockGrouped)) {
            //    return;
            //}

            //var startLBA = blockGrouped.GetStartLBA();
            //if(startLBA != null) {
            //    focusedTuple.Value.partHexContext.Position = startLBA.Value;
            //}

            //var fileBaseStream = blockGrouped.GetInputStream();
            //focusedTuple.Value.fileHexContext.Stream = fileBaseStream;
            
        }

        /// <summary>
        /// 分区加入文档时呈现十六进制;
        /// </summary>
        /// <param name="obj"></param>
        private void OnDocumentAddedEventOnPartHex((IDocument tab, IDocumentService owner) tuple) {
            if (!(tuple.tab is IEnumerableDocument enumDoc)) {
                return;
            }

            if (!(enumDoc.Tag is Partition part)) {
                return;
            }

            var hexPartTuple = GetBlockedStreamHexDocument(part);
            if (hexPartTuple == null) {
                return;
            }

            hexPartTuple.Value.doc.Title = LanguageService.FindResourceString(Constants.DocumentTitle_HexPartition);

            var hexFileTuple = GetBlockedStreamHexDocument(null);
            if (hexFileTuple == null) {
                return;
            }
            hexFileTuple.Value.doc.Title = LanguageService.FindResourceString(Constants.DocumentTitle_HexFile);

            _hexTuplesForPartDocs.Add((part, hexPartTuple.Value.hexDataContext,hexFileTuple.Value.hexDataContext));

            enumDoc.AddDocument(hexPartTuple.Value.doc);
            enumDoc.AddDocument(hexFileTuple.Value.doc);
        }

        private void OnDocumentClosed((IDocument tab, IDocumentService owner) tuple) {
            
        }

        /// <summary>
        /// 设备选中行发生变更时十六进制变化;
        /// </summary>
        /// <param name="tuple"></param>
        private void OnFocusedFileChangedOnDeviceHex((object sender,FileBase file) tuple) {
            //if(tuple.sender is IFolderBrowserViewModel vm) {

            //}
            //var focusedTuple = _hexTuplesForDeviceDocs.FirstOrDefault(p => p?.device == tuple.parent);
            //if(focusedTuple == null) {
            //    LoggerService.WriteCallerLine($"{nameof(focusedTuple)} can't be null.");
            //    return;
            //}

            //if(tuple.file is Partition part) {
            //    focusedTuple.Value.deviceHexContext.Position = focusedTuple.Value.device.GetStartLBA(part);
            //    focusedTuple.Value.partHexContext.Stream = part.BaseStream;
            //}
        }
        
        /// <summary>
        /// 展现设备及分区十六进制;
        /// </summary>
        /// <param name="tuple"></param>
        private void OnDocumentAddedEventOnDeviceHex((IDocument tab, IDocumentService owner) tuple) {
            if (!(tuple.tab is IEnumerableDocument enumDoc)) {
                return;
            }

            if (!(enumDoc.Tag is Device device)) {
                return;
            }

            var hexDeviceTuple = GetBlockedStreamHexDocument(device);
            if (hexDeviceTuple == null) {
                return;
            }
            hexDeviceTuple.Value.doc.Title =
                LanguageService.FindResourceString(Constants.DocumentTitle_HexDevice);

            //分区表高亮;
            if (device.PartitionEntries != null) {
                int i = 0;
                var hexDataContext = hexDeviceTuple.Value.hexDataContext;
                foreach (var ti in device.PartitionEntries.OrderBy(p => p.StartLBA)) {
                    hexDataContext.CustomBackgroundBlocks?.Add(
                        (
                            ti.StartLBA,
                            ti.Size, i++ % 2 == 0 ? Brushes.LightBlue : Brushes.Chocolate
                        )
                    );
                }
            }
            
            var hexPartTuple = GetBlockedStreamHexDocument(null);
            if(hexPartTuple == null) {
                return;
            }
            hexPartTuple.Value.doc.Title = LanguageService.FindResourceString(Constants.DocumentTitle_HexPartition);

            _hexTuplesForDeviceDocs.Add((device,
                hexDeviceTuple.Value.hexDataContext, 
                hexPartTuple.Value.hexDataContext));

            enumDoc.AddDocument(hexDeviceTuple.Value.doc);
            enumDoc.AddDocument(hexPartTuple.Value.doc);
        }
        
        /// <summary>
        /// 根据流文件创建一个十六进制Tab;
        /// </summary>
        /// <param name="blockedStream"></param>
        /// <returns></returns>
        private (IDocument doc, IHexDataContext hexDataContext)?
            GetBlockedStreamHexDocument(IBlockedStream blockedStream) {

            var mainDocService = DocumentService.MainDocumentService;
            if (mainDocService == null) {
                LoggerService.WriteCallerLine($"{nameof(mainDocService)} can't be null.");
                return null;
            }

            var hexDeviceDoc = mainDocService.CreateNewDocument();
            hexDeviceDoc.Tag = blockedStream;

            var hexService = ServiceProvider.Current.GetInstance<IHexService>();
            if (hexService == null) {
                LoggerService.WriteCallerLine($"{nameof(hexService)} can't be null.");
                return null;
            }

            var hexDataContext = hexService.CreateNewHexDataContext(blockedStream?.BaseStream);

            hexDeviceDoc.UIObject = hexDataContext.UIObject;
            return (hexDeviceDoc, hexDataContext);
        }

        /// <summary>
        /// 设备加入文档时的呈现主视图;
        /// </summary>
        /// <param name="tuple"></param>
        private void OnDocumentAddedEventOnDevice((IDocument tab, IDocumentService owner) tuple) {
            if(!(tuple.tab is IEnumerableDocument enumDoc)) {
                return;
            }

            if(!(enumDoc.Tag is Device device)) {
                return;
            }
            
            var partitionBrowser = ServiceProvider.Current?.
                GetInstance<FrameworkElement>(Constants.PartitionsBrowserView);
            if (partitionBrowser == null) {
                LoggerService.WriteCallerLine($"{nameof(partitionBrowser)} can't be null.");
                return;
            }

            var vm = new PartitionsBrowserViewModel(device);
            partitionBrowser.DataContext = vm;

            enumDoc.MainUIObject = partitionBrowser;
        }

        /// <summary>
        /// 设备加入文档时的呈现主视图;
        /// </summary>
        /// <param name="tuple"></param>
        private void OnDocumentAddedEventOnPartition((IDocument tab, IDocumentService owner) tuple) {
            if (!(tuple.tab is IEnumerableDocument enumDoc)) {
                return;
            }

            if (!(enumDoc.Tag is Partition part)) {
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
        /// 案件文件节点加入时加入文件系统节点;
        /// </summary>
        /// <param name="unit"></param>
        private void OnTreeUnitAdded(TreeUnit unit) {
            if (unit == null) {
                return;
            }

            if (!(unit.TypeGuid == Contracts.Casing.Constants.CaseEvidenceUnit
            && unit.Tag is CaseEvidence csFile)) {
                return;
            }

            var fileTuple = FSService.Current.MountedFiles?.FirstOrDefault(p => p.xElem.GetXElemValue(nameof(CaseEvidence.EvidenceGUID)) == csFile.EvidenceGUID);
            if (fileTuple == null) {
                LoggerService.WriteCallerLine($"{nameof(fileTuple)} can't be null.");
                return;
            }

            var fsUnit = new TreeUnit(Constants.FileSystemTreeUnit, fileTuple.Value.file) {
                Icon = IconResources.FileSystemIcon,
                Label = LanguageService.Current?.FindResourceString("FileSystem")
            };

            var partIndex = 0;
            //递归添加子节点：
            void TraverseAddChildren(TreeUnit tUnit, IHaveFileCollection haveCollection) {
                foreach (var file in haveCollection.Children) {
                    if (!(file is IHaveFileCollection cHaveCollection)) {
                        continue;
                    }

                    if (file is Contracts.FileSystem.Directory dir
                            && (dir.IsBack || dir.IsLocalBackUp)) {
                        continue;
                    }
                    
                    var cUnit = new TreeUnit(string.Empty, file) {
                        Label = file.Name
                    };

                    if (file is Contracts.FileSystem.Directory) {
                        cUnit.Icon = IconResources.DirectoryUnitIcon;
                    }
                    else {
                        cUnit.Icon = IconResources.RegFileUnitIcon;
                    }

                    TraverseAddChildren(cUnit, cHaveCollection);
                    cUnit.MoveToUnit(tUnit);
                    partIndex++;
                    if(file is Partition) {
                        cUnit.Label = $"分区{partIndex}";
                    }
                }
            }

            if (fileTuple.Value.file is IHaveFileCollection haveCollection2) {
                TraverseAddChildren(fsUnit, haveCollection2);
            }
            
            fsUnit.MoveToUnit(unit);
        }

        private void OnTreeUnitClickOnFileSystemUnit(TreeUnit unit) {
            if (unit == null) {
                return;
            }

            if (unit.TypeGuid == Constants.FileSystemTreeUnit) {
                if (
                    unit.Tag is FileBase file &&
                    !CheckTagAddedToDocument(file)
                ) {
                    AddFileToDocument(file);
                    return;
                }
            }

        }

        /// <summary>
        /// 查找文档区域是否已经添加了Tag相关;
        /// </summary>
        /// <param name="tag"></param>
        private bool CheckTagAddedToDocument(object tag) {
            var mainDocService = DocumentService.MainDocumentService;
            if(mainDocService == null) {
                LoggerService.WriteCallerLine($"{nameof(mainDocService)} can't be null.");
                return true;
            }

            var doc = mainDocService.CurrentDocuments.FirstOrDefault(p => p.Tag?.Equals(tag)??false);

            if(doc != null) {
                mainDocService.SelectedDocument = doc;
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// 添加文件(设备/分区)显示到文档区域中;
        /// </summary>
        /// <param name="device"></param>
        private void AddFileToDocument(FileBase file) {
            var mainDocService = DocumentService.MainDocumentService;
            if (mainDocService == null) {
                LoggerService.WriteCallerLine($"{nameof(mainDocService)} can't be null.");
                return;
            }

            var enumDoc = mainDocService.CreateEnumerableDocument();
            enumDoc.Title = file.Name;
            enumDoc.Tag = file;
            mainDocService.AddDocument(enumDoc);
        }

        
    }
}
