using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using CDFCMessageBoxes.MessageBoxes;
using CDFCUIContracts.Models;
using EventLogger;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Singularity.Contracts.Case;
using Singularity.Contracts.Common;
using Singularity.Contracts.FileExplorer;
using Singularity.Contracts.FileSystem;
using Singularity.Contracts.Helpers;
using Singularity.Contracts.MainPage.Events;
using Singularity.UI.FileExplorer.Models;
using SingularityForensic.FileExplorer.Models;
using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
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
            PubEventHelper.Subscribe<TreeNodeAdded, ITreeUnit>(unit => {
                if (unit is ICaseEvidenceUnit<ICaseEvidence> haveCaseFile) {
                    //若为可迭代(设备)案件文件，则添加文件系统节点;
                    if (haveCaseFile.Evidence is IHaveData<Device>) {
                        unit.Children.Add(new FileSystemUnit(haveCaseFile.Evidence, unit,
                            FileExplorerHelper.GetFileExplorerServiceProvider(haveCaseFile.Evidence)) {
                            Icon = IconResources.FileSystemIcon
                        });
                    }
                }
            });

            //加入文件系统节点响应(左键);
            PubEventHelper.Subscribe<TreeNodeClickEvent, ITreeUnit>(e => {
                if (e != null) {
                    if (e is StorageTreeUnit stUnit) {
                        fsNodeService?.AddShowingFile(stUnit.File, stUnit.FSProvider);
                    }
                    else if (e is ICaseEvidenceUnit<PartitionCaseFile> pFileUnit) {
                        var fsUnit = e.GetParent<FileSystemUnit>();
                        fsNodeService?.AddShowingFile(pFileUnit.Evidence.Partition, fsUnit.FsExpServiceProvider);
                    }
                    else if (e is FileSystemUnit fsUnit && fsUnit.CaseFile is IHaveData<IFile> dCFile) {
                        fsNodeService?.AddShowingFile(dCFile.Data, fsUnit.FsExpServiceProvider);
                    }

                }

            });

            //右键递归响应;
            PubEventHelper.Subscribe<TreeNodeRightClicked, ITreeUnit>(e => {
                if (e is StorageTreeUnit stUnit) {
                    fsNodeService?.ExpandFile(stUnit.File as IIterableFile);
                }
                else if (e is FileSystemUnit fsUnit && fsUnit.CaseFile is IHaveData<IFile> dCFile) {
                    fsNodeService?.AddShowingFile(dCFile.Data, fsUnit.FsExpServiceProvider);
                }

            });
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
                Logger.WriteCallerLine($"{ex.Message}");
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
