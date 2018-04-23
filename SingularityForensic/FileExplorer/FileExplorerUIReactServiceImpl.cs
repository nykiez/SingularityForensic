using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.TreeView;
using SingularityForensic.Contracts.TreeView.Events;
using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.FileExplorer {
    /// <summary>
    /// 文件系统资源管理器UI响应单位;
    /// </summary>
    [Export(typeof(IFileExplorerUIReactService))]
    class FileExplorerUIReactServiceImpl : IFileExplorerUIReactService {
        public void Initialize() {
            _uiServiceForDevice.Initialize();
            _uiServiceForPartition.Initialize();
            RegisterEvents();
        }
        
        [Import]
        private FileExplorerUIReactServiceForDevice _uiServiceForDevice;

        [Import]
        private FileExplorerUIReactServiceForPartition _uiServiceForPartition;

        private void RegisterEvents() {
            //加入文件系统节点响应(左键);
            PubEventHelper.GetEvent<TreeUnitSelectedChangedEvent>().Subscribe(OnFileSystemUnitSelectedChanged);
            //为设备/分区案件文件节点加入文件系统子节点;
            PubEventHelper.GetEvent<TreeUnitAddedEvent>().Subscribe(OnTreeUnitAddedToAddFileSystemUnit);
            //为设备/分区案件文件节点加入文件系统子节点;
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

            if(tuple.unit.TypeGuid != Constants.TreeUnitGUID_FileSystem) {
                return;
            }

            var file = tuple.unit.GetIntance<IFile>(Contracts.FileExplorer.Constants.TreeUnitTag_FileSystem_File);
            if (file != null) {
                FileExplorerUIHelper.GetOrAddFileDocument(file);
            }
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

            if (!(tuple.unit.TypeGuid == Contracts.Casing.Constants.CaseEvidenceUnit)) {
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

            var fsUnit = TreeUnitFactory.CreateNew(Constants.TreeUnitGUID_FileSystem);
            fsUnit.Icon = IconResources.FileSystemIcon;
            fsUnit.Label = LanguageService.Current?.FindResourceString(Constants.TreeUnitLabel_FileSystem);
            fsUnit.SetInstance(fileTuple.Value.file, Contracts.FileExplorer.Constants.TreeUnitTag_FileSystem_File);
            
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
                    tuple.treeService.AddUnit(tUnit, cUnit);
                }
            }

            if (fileTuple.Value.file is IHaveFileCollection haveCollection2) {
                TraverseAddChildren(fsUnit, haveCollection2);
            }
            
            tuple.unit.Children.Add(fsUnit);
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
