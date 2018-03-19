using CDFCMessageBoxes.MessageBoxes;
using EventLogger;
using Prism.Events;
using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.MainPage.Events;
using SingularityForensic.Contracts.TreeView;
using SingularityForensic.Controls.FileExplorer.Models;
using SingularityForensic.Controls.FileExplorer.Services;
using SingularityForensic.FileExplorer.Models;
using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static CDFCCultures.Managers.ManagerLocator;
using static CDFCUIContracts.Helpers.ApplicationHelper;

namespace SingularityForensic.FileExplorer {
    [ModuleExport(typeof(ExplorerModule))]
    public class ExplorerModule:IModule {
        [Import]
        IFSNodeService fsNodeService;

        public void Initialize() {
            RegisterEvents();
        }

        private void RegisterEvents() {
            //为设备案件文件节点加入文件系统子节点;
            PubEventHelper.GetEvent<TreeNodeAddedEvent>().Subscribe(unit => {
                if(unit == null) {
                    return;
                }
                
                if (unit.TypeGuid == Contracts.Casing.Constants.CaseEvidenceUnit
                && unit.Data is CaseEvidence csFile) {

                    var device = FSService.Current.EnumedFiles?.FirstOrDefault(p => p.xElem.GetXElemValue(nameof(CaseEvidence.EvidenceGUID)) == csFile.EvidenceGUID);
                    if(device == null) {
                        Logger.WriteCallerLine($"{nameof(device)} can't be null.");
                        return;
                    }

                    var fsUnit = new TreeUnit(Constants.FileSystemTreeUnit, device) {
                        Icon = IconResources.FileSystemIcon,
                        Label = LanguageService.Current?.FindResourceString("FileSystem")
                    };
                    fsUnit.MoveToUnit(unit);

                }
            });

            //加入文件系统节点响应(左键);
            PubEventHelper.GetEvent<TreeNodeClickEvent>().Subscribe(e => {
                if(e == null) {
                    return;
                }

                if(e.TypeGuid != Constants.FileSystemTreeUnit) {
                    return;
                }

                if(!(e.Data is Device device)) {
                    return;
                }

                fsNodeService?.AddShowingFile(device , UnknownFileExplorerServiceProvider.StaticInstance);

                //if (e is StorageTreeUnit stUnit) {
                //    
                //}
                //else if (e is ICaseEvidenceUnit<PartitionCaseFile> pFileUnit) {
                //    var fsUnit = e.GetParent<FileSystemUnit>();
                //    fsNodeService?.AddShowingFile(pFileUnit.Evidence.Partition, fsUnit.FsExpServiceProvider);
                //}
                //else if (e is FileSystemUnit fsUnit && fsUnit.CaseFile is IHaveData<IFile> dCFile) {
                //    fsNodeService?.AddShowingFile(dCFile.Data, fsUnit.FsExpServiceProvider);
                //}

            },ThreadOption.UIThread);

                ////右键递归响应;
                //PubEventHelper.Subscribe<TreeNodeRightClicked, ITreeUnit>(e => {
                //    if (e is StorageTreeUnit stUnit) {
                //        fsNodeService?.ExpandFile(stUnit.File as IEnumerableFileFile);
                //    }
                //    else if (e is FileSystemUnit fsUnit && fsUnit.CaseFile is IHaveData<IFile> dCFile) {
                //        fsNodeService?.AddShowingFile(dCFile.Data, fsUnit.FsExpServiceProvider);
                //    }

                //});

                ////为设备案件文件节点加入上下文菜单;
                //PubEventHelper.Subscribe<TreeNodeAddedEvent, ITreeUnit>(unit => {
                //    if (unit is ICaseEvidenceUnit<Contracts.Case.CaseEvidence> haveCaseFile && haveCaseFile.Evidence is IHaveData<Device>) {
                //        try {
                //            unit.Icon = IconSources.HardDiskIcon;
                //            var commands = unit.ContextCommands ?? (unit.ContextCommands = new ObservableCollection<ICommandItem>());
                //            if (DeviceNodeCommandItems != null) {
                //                commands.AddRange(DeviceNodeCommandItems);
                //            }
                //        }
                //        catch (Exception ex) {
                //            LoggerService.Current?.WriteCallerLine(ex.Message);

                //        }

                //    }
                //});


                ////为设备案件文件节点加入文件系统子节点;
                //PubEventHelper.Subscribe<TreeNodeAddedEvent, ITreeUnit>(unit => {

                //});
            }

        private void ViewFile(ViewerProgramMessage e) {
            FileStream targetStream = null;
            try {
                var path = ServiceProvider.Current.GetInstance<ICaseService>()?.CurrentCase.Path;
                if (!System.IO.Directory.Exists($"{path}/Temp")) {
                    System.IO.Directory.CreateDirectory($"{path}/Temp");
                }
                var oriStream = e.FStream;
                targetStream = File.Create($"{path}/Temp/{e.FileName}");
                oriStream.CopyTo(targetStream);
                Process.Start(e.ViewerPath, $"{path}/Temp/{ e.FileName}");
            }
            catch (Exception ex) {
                LoggerService.Current?.WriteCallerLine($"{ex.Message}");
                AppInvoke(() => {
                    RemainingMessageBox.Tell($"{FindResourceString("FailedToExtractFile")}:{ex.Message}");
                });
            }
            finally {
                targetStream?.Close();
            }
        }
    }
}
