using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.TreeView;
using SingularityForensic.Contracts.TreeView.Events;
using SingularityForensic.FileExplorer.Helpers;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;

namespace SingularityForensic.FileExplorer {
    /// <summary>
    /// 文件系统资源管理器UI响应单位;
    /// </summary>
    [Export(typeof(IFileExplorerUIReactService))]
    class FileExplorerUIReactServiceImpl : IFileExplorerUIReactService {
        public void Initialize() {
            _uiServiceForDevice.Initialize();
            RegisterEvents();
        }
        
        [Import]
        private FileExplorerUIReactServiceForDevice _uiServiceForDevice;
        
        private void RegisterEvents() {
            //加入文件系统节点响应(左键);
            PubEventHelper.GetEvent<TreeUnitSelectedChangedEvent>().Subscribe(OnFileSystemUnitSelectedChanged);
            //为设备/分区案件文件节点加入文件系统子节点;
            PubEventHelper.GetEvent<TreeUnitAddedEvent>().Subscribe(OnTreeUnitAddedToAddFileSystemUnit);
            //为设备/分区案件文件节点加入右键菜单;
            PubEventHelper.GetEvent<TreeUnitAddedEvent>().Subscribe(OnTreeUnitAddedOnContextCommands);
            //为设备/分区节点加入时加入右键菜单;
            PubEventHelper.GetEvent<TreeUnitAddedEvent>().Subscribe(OnTreeUnitAddedOnBlockStreamedFile);
        }
        
        /// <summary>
        /// 点击了文件系统节点时响应;
        /// </summary>
        /// <param name="unit"></param>
        private void OnFileSystemUnitSelectedChanged((ITreeUnit unit, ITreeService treeService) tuple) {
            if (tuple.unit == null) {
                return;
            }

            if(tuple.treeService != Contracts.MainPage.MainTreeService.Current) {
                return;
            }

            if(tuple.unit.TypeGuid != Contracts.FileExplorer.Constants.TreeUnitType_FileSystem) {
                return;
            }

            var file = tuple.unit.GetIntance<IFile>(Contracts.FileExplorer.Constants.TreeUnitTag_FileSystem_File);
            if (file != null) {
                FileExplorerUIHelper.GetOrAddFileDocument(file);
            }
        }
        
        private struct AddUnitStack {
            public ITreeUnit ParentUnit { get; set; }
            public ITreeUnit ChildUnit { get; set; }
        }

        /// <summary>
        /// 案件文件节点加入时加入文件系统节点;
        /// </summary>
        /// <param name="unit"></param>
        private void OnTreeUnitAddedToAddFileSystemUnit((ITreeUnit unit, ITreeService treeService) tuple) {
            if(tuple.treeService != Contracts.MainPage.MainTreeService.Current) {
                return;
            }

            if (tuple.unit == null) {
                return;
            }

            if (!(tuple.unit.TypeGuid == Contracts.Casing.Constants.TreeUnitType_CaseEvidence)) {
                return;
            }

            var csEvidence = tuple.unit.GetIntance<ICaseEvidence>(Contracts.Casing.Constants.TreeUnitTag_CaseEvidence);
            if(csEvidence == null) {
                LoggerService.WriteCallerLine($"{nameof(csEvidence)} can't be null.");
                return;
            }

            var fileTuple = FileSystemService.Current.MountedFiles?.FirstOrDefault(p => p.xElem.GetXElemValue(nameof(ICaseEvidence.EvidenceGUID)) == csEvidence.EvidenceGUID);
            if (fileTuple == null) {
                LoggerService.WriteCallerLine($"{nameof(fileTuple)} can't be null.");
                return;
            }

            var fsUnit = TreeUnitFactory.CreateNew(Contracts.FileExplorer.Constants.TreeUnitType_FileSystem);
            fsUnit.Icon = IconResources.FileSystemIcon;
            fsUnit.Label = LanguageService.Current?.FindResourceString(Constants.TreeUnitLabel_FileSystem);
            fsUnit.SetInstance(fileTuple.Value.file, Contracts.FileExplorer.Constants.TreeUnitTag_FileSystem_File);

            var bufferLength = 10;
            var bufferStacks = new AddUnitStack[bufferLength];
            var index = 0;
            //递归添加子节点：
            void TraverseAddChildren(ITreeUnit tUnit, IHaveFileCollection haveCollection) {
                foreach (var file in haveCollection.Children) {
                    if (!(file is IHaveFileCollection cHaveCollection)) {
                        continue;
                    }

                    //忽略备份文件夹;
                    if (file is IDirectory dir && (dir.IsBack || dir.IsLocalBackUp)) {
                        continue;
                    }

                    var cUnit = TreeUnitFactory.CreateNew(Contracts.FileExplorer.Constants.TreeUnitType_InnerFile);
                    cUnit.Label = file.Name;
                    cUnit.SetInstance(file, Contracts.FileExplorer.Constants.TreeUnitTag_InnerFile);

                    if (file is IDirectory) {
                        cUnit.Icon = IconResources.DirectoryUnitIcon;
                    }
                    else if (file is IPartition part) {
                        cUnit.Label = FileExtensions.GetPartFixAndName(part);
                        cUnit.Icon = IconResources.PartUnitIcon;
                    }
                    
                    TraverseAddChildren(cUnit, cHaveCollection);

                    if(index == bufferLength) {
                        ThreadInvoker.UIInvoke(() => {
                            foreach (var unit in bufferStacks) {
                                Contracts.MainPage.MainTreeService.Current.AddUnit(unit.ParentUnit,unit.ChildUnit);
                            }
                        });
                        index = 0;
                        Thread.Sleep(1);
                    }

                    bufferStacks[index].ParentUnit = tUnit;
                    bufferStacks[index].ChildUnit = cUnit;

                    index++;
                    //tUnit.Children.Add(cUnit);
                }
            }

            if (fileTuple.Value.file is IHaveFileCollection haveCollection2) {
                TraverseAddChildren(fsUnit, haveCollection2);
                ThreadInvoker.UIInvoke(() => {
                    for (int i = 0; i < index; i++) {
                        Contracts.MainPage.MainTreeService.Current.AddUnit(bufferStacks[i].ParentUnit, bufferStacks[i].ChildUnit);
                    }
                });
            }

            Contracts.MainPage.MainTreeService.Current.AddUnit(tuple.unit, fsUnit);
            
        }

        /// <summary>
        /// 为节点加入时加入右键菜单;
        /// </summary>
        /// <param name="tuple"></param>
        private void OnTreeUnitAddedOnBlockStreamedFile((ITreeUnit unit, ITreeService treeService) tuple) {
            if (tuple.unit == null) {
                return;
            }

            if (!(tuple.unit.TypeGuid == Contracts.FileExplorer.Constants.TreeUnitType_InnerFile)) {
                return;
            }

            var file = tuple.unit.GetIntance<IFile>(Contracts.FileExplorer.Constants.TreeUnitTag_InnerFile);
            if (file == null) {
                LoggerService.WriteCallerLine($"{nameof(file)} can't be null.");
                return;
            }

            if(!(file is IStreamFile streamFile)) {
                return;
            }

            tuple.unit.AddContextCommand(FileExplorerTreeUnitCommandItemFactory.CreateCustomSignSearchCommandItem(streamFile));
        }

        /// <summary>
        /// 为设备/分区案件文件加入右键菜单;
        /// </summary>
        /// <param name="obj"></param>
        private void OnTreeUnitAddedOnContextCommands((ITreeUnit unit, ITreeService treeService) tuple) {
            if (tuple.unit == null) {
                return;
            }

            if (tuple.treeService != Contracts.MainPage.MainTreeService.Current) {
                return;
            }
            
            var csFile = tuple.unit.GetIntance<ICaseEvidence>(Contracts.Casing.Constants.TreeUnitTag_CaseEvidence);
            if (csFile == null) {
                LoggerService.WriteCallerLine($"{nameof(csFile)} can't be null.");
                return;
            }

            var fileTuple = FileSystemService.Current.MountedFiles?.FirstOrDefault(p => p.xElem.GetXElemValue(nameof(ICaseEvidence.EvidenceGUID)) == csFile.EvidenceGUID);
            if (fileTuple == null) {
                LoggerService.WriteCallerLine($"{nameof(fileTuple)} can't be null.");
                return;
            }

            if(!(fileTuple.Value.file is IStreamFile streamFile)) {
                return;
            }

            tuple.unit.AddContextCommand(FileExplorerTreeUnitCommandItemFactory.CreateCustomSignSearchCommandItem(streamFile));
        }
    }

   
}
