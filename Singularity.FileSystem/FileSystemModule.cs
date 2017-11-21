using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using EventLogger;
using Microsoft.Practices.ServiceLocation;
using Prism.Modularity;
using Singularity.UI.FileSystem.Global.TabModels;
using Singularity.UI.FileSystem.ViewModels;
using SingularityForensic.Modules.MainPage.Global.Services;
using SingularityForensic.Modules.Shell.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static CDFCUIContracts.Helpers.ApplicationHelper;
using static CDFCCultures.Managers.ManagerLocator;
using CDFCMessageBoxes.MessageBoxes;
using System.Threading;
using SingularityForensic.Modules.Shell.Global.Services;
using Singularity.UI.MessageBoxes.Models;
using SingularityForensic.Helpers;
using SingularityForensic.Modules.MainPage.Global.Events;
using Singularity.UI.FileSystem.Models;
using CDFCUIContracts.Models;
using Singularity.Interfaces;
using CDFC.Parse.Local.DeviceObjects;
using CDFC.Parse.Signature.DeviceObjects;
using CDFC.Parse.Signature.Pictures;
using Singularity.UI.Case.Contracts;
using Singularity.UI.Case;
using Prism.Mef.Modularity;
using System.ComponentModel.Composition;
using Singularity.UI.FileSystem.Global.Services;

namespace Singularity.UI.FileSystem {
    [ModuleExport(typeof(FileSystemModule))]
    public class FileSystemModule : IModule {
        [Import]
        IFSNodeService fsNodeService;

        public void Initialize() {
            RegisterEvents();
        }
        
        private void ViewFile(ViewerProgramMessage e) {
            FileStream targetStream = null;
            try {
                var path = SingularityCase.Current.Path;
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
        
        private void RegisterEvents() {
            //加入文件系统节点响应(左键);
            PubEventHelper.Subscribe<TreeNodeClickEvent, ITreeUnit>(e => {
                if (e != null) {
                    if (e is StorageTreeUnit stUnit) {
                        fsNodeService?.AddShowingFile(stUnit.File);
                    }
                    else if (e is IHaveData<ICaseFile> cFileUnit && cFileUnit.Data is IHaveData<IFile> fCFile) {
                        fsNodeService?.AddShowingFile(fCFile.Data);
                    }
                    else if (e is FileSystemUnit fsUnit && fsUnit.CaseFile is IHaveData<IFile> dCFile) {
                        fsNodeService?.AddShowingFile(dCFile.Data);
                    }
                    
                }
                
            });
            //右键递归响应;
            PubEventHelper.Subscribe<TreeNodeRightClicked, ITreeUnit>(e => {
                if(e is StorageTreeUnit stUnit) {
                    fsNodeService?.ExpandFile(stUnit.File as IIterableFile);
                }
                else if(e is FileSystemUnit fsUnit && fsUnit.CaseFile is IHaveData<IFile> dCFile) {
                    fsNodeService?.AddShowingFile(dCFile.Data);
                }
                
            });
            //为设备案件文件节点加入右键菜单,子节点;
            PubEventHelper.Subscribe<TreeNodeAdded, ITreeUnit>(unit => {
                if (unit is IHaveData<ICaseFile> haveCaseFile) {
                    //若为可迭代(设备)案件文件，则添加文件系统节点;
                    if (haveCaseFile.Data is IHaveGroup<ICaseFile>) {
                        unit.Children.Add(new FileSystemUnit(haveCaseFile.Data, unit));
                    }

                }
            });
        }
        
        ///// <summary>
        ///// 签名搜索;
        ///// </summary>
        ///// <param name="blDevice"></param>
        ///// <param name="setting"></param>
        //private static void SignSearch(BlockDeviceFile blDevice, SignSearchSetting setting) {
        //    Device device = null;
        //    long startLBA = 0;
        //    long endLBA = 0;
        //    if (blDevice is Device) {
        //        device = blDevice as Device;
        //        endLBA = device.Size - 1;
        //    }
        //    else if (blDevice is Partition) {
        //        var part = blDevice as Partition;
        //        device = blDevice.GetParent<Device>();
        //        startLBA = part.StartLBA;
        //        endLBA = part.EndLBA;

        //    }

        //    if (device != null) {
        //        var dialog = new ProgressMessageBox();
        //        dialog.WindowTitle = FindResourceString("SignSearch");

        //        var part = new SearcherPartition(device, blDevice, startLBA, endLBA, $"{blDevice.Name}-{FindResourceString("SignSearch")}");

        //        dialog.DoWork += (sender, e) => {
        //            var searcher = new SignSearcher(device.Stream, setting.KeyWord, setting.MaxSize, setting.SectorSize, setting.SecStartLBA);
        //            searcher.AlignToSector = setting.AlignToSec;
        //            searcher.FileExtension = setting.FileExtension;

        //            searcher.CurOffsetChanged += (insender, curOffset) => {
        //                var percentage = (int)((curOffset - startLBA) * 100 / (endLBA - startLBA));
        //                if (percentage >= 0 && percentage <= 100) {
        //                    dialog.ReportProgress(percentage,
        //                    FindResourceString("SearchingSignFile"),
        //                    $"{FindResourceString("RecoveringBySign")}:{percentage}%");
        //                }
        //                if (dialog.CancellationPending) {
        //                    searcher.Stop();
        //                }
        //            };

        //            searcher.SearchStart(startLBA, endLBA);

        //            var fileList = new List<RegularFile>();
        //            var shfileList = new List<RegularFile>();

        //            try {
        //                //遍历获取文件列表;
        //                searcher.FileExtension = setting.FileExtension;
        //                var ndList = searcher.GetFileList(string.Empty);
        //                if (ndList?.Count != 0) {
        //                    shfileList.AddRange(ndList.Select(p => new SearcherFile(part, p)));
        //                }
        //                shfileList.ForEach(p => {
        //                    fileList.Add(p);
        //                });
        //            }
        //            catch (Exception ex) {
        //                Logger.WriteLine($"{nameof(FileSystemModule)} -> {nameof(SignSearch)}:{ex.Message}");
        //            }
        //            finally {
        //                part.Children.AddRange(fileList);
        //                searcher.Dispose();
        //            }
        //        };
        //        dialog.RunWorkerCompleted += (sender, e) => {
        //            AddShowingFile(part);
        //            ServiceLocator.Current.GetInstance<IShellService>()?.Focus();
        //        };
        //        dialog.ShowDialog();
        //    }
        //}
        
        ////递归浏览节点;
        //public static void RecurUnit(ITreeUnit unit) {
        //    PubEventHelper.GetEvent<TreeNodeClickEvent>()?.Publish(unit);
        //    //通知等待;
        //    ServiceLocator.Current.GetInstance<IShellService>()?.ChangeLoadState(true, string.Empty);
        //    ThreadPool.QueueUserWorkItem(callBack => {
        //        try {
        //            IFile file = null;
        //            if (unit is StorageTreeUnit stUnit) {
        //                file = stUnit.File;
        //            }
        //            else if (unit is IHaveData<ICaseFile> cFileUnit) {
        //                if (cFileUnit.Data is IHaveData<IFile> fCFile) {
        //                    file = fCFile.Data;
        //                }
        //            }
        //            if (file is IIterableFile itrFile) {
        //                ExpandFile(itrFile);
        //            }
        //        }
        //        catch (Exception ex) {
        //            Logger.WriteCallerLine($"{ex.Message}");
        //            AppInvoke(() => {
        //                RemainingMessageBox.Tell(ex.Message);
        //            });
        //        }
        //        finally {
        //            //解除等待；
        //            ServiceLocator.Current.GetInstance<IShellService>()?.ChangeLoadState(false, string.Empty);
        //        }
        //    });
        //}
    }
}
