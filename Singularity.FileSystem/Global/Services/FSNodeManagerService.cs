using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using CDFCMessageBoxes.MessageBoxes;
using EventLogger;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Singularity.UI.MessageBoxes.MessageBoxes;
using Singularity.UI.MessageBoxes.Models;
using Singularity.UI.FileSystem.Global.TabModels;
using Singularity.UI.FileSystem.MessageBoxes;
using Singularity.UI.FileSystem.Models;
using Singularity.UI.FileSystem.ViewModels;
using SingularityForensic.Modules.MainPage.Global.Services;
using SingularityForensic.Modules.Shell.Global.Services;
using SingularityForensic.ViewModels.Modules.MainPage.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading;
using static CDFCCultures.Managers.ManagerLocator;
using Singularity.Interfaces;
using CDFC.Parse.Signature.DeviceObjects;
using CDFC.Parse.Signature.Pictures;
using CDFC.Parse.Signature.Contracts;
using CDFC.Parse.Local.DeviceObjects;
using Singularity.UI.Case.Contracts;
using Singularity.UI.Case;
using Singularity.UI.Case.MessageBoxes;
using static CDFCUIContracts.Helpers.ApplicationHelper;

namespace Singularity.UI.FileSystem.Global.Services {
    public interface IFSNodeService {
        //void ShowCaseFileProperty(ICaseFile csFile);
        void ShowFileSystem(IFile file);
        //void SignSearch(BlockDeviceFile blDevice, SignSearchSetting setting);
        //void RecoverSign(BlockDeviceFile blDevice, bool isReComposite = false);
        void AddShowingFile(IFile file);
        void ExpandFile(IIterableFile file);
    }

    //文件系统树形节点管理器服务;
    [Export(typeof(IFSNodeService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FSNodeManagerService : IFSNodeService {
        [Import]
        Lazy<INodeService> nodeService;
        
        //显示文件系统信息;
        public void ShowFileSystem(IFile file) {
            var device = file as Device;
            BlockDeviceFSInfoMessageBox.Show(device);
            
        }

        private DelegateCommand _recompositeSignCommand;
        public DelegateCommand RecompositeSignCommand =>
            _recompositeSignCommand ?? (_recompositeSignCommand = new DelegateCommand(
                () => {
                    ServiceLocator.Current.GetInstance<IShellService>()?.ChangeLoadState(true, string.Empty);
                    if (nodeService?.Value?.SelectedNode is IHaveData<ICaseFile> csFUnit && csFUnit.Data is IHaveData<Device> dcsFile) {
                        try {
                            RecoverSign(dcsFile.Data, true);
                        }
                        catch (Exception ex) {
                            Logger.WriteLine($"{nameof(MainPageNodeManagerViewModel)}->{nameof(RecompositeSignCommand)}:{ex.Message}");
                        }
                    }
                    ServiceLocator.Current.GetInstance<IShellService>()?.ChangeLoadState(false, string.Empty);
                },
                () => (nodeService?.Value?.SelectedNode is IHaveData<ICaseFile> csFUnit) && (csFUnit.Data is IHaveData<Device>)
            ));

        //显示属性命令;
        private DelegateCommand showPropertyCommand;
        public DelegateCommand ShowPropertyCommand =>
            showPropertyCommand ??
             (showPropertyCommand = new DelegateCommand(() => {
                 if (nodeService?.Value?.SelectedNode is StorageTreeUnit stUnit && stUnit.File is BlockDeviceFile) {

                     if (SingularityCase.Current.CaseFiles.FirstOrDefault(p =>
                      p is IHaveData<IFile> fcsFile && fcsFile.Data == stUnit.File) is IHaveData<IFile> fCsFile) {
                         SingularityCase.Current.Save();
                         var csFile = ShowCaseFilePropertyMessageBox.Show(fCsFile as ICaseFile);
                     }
                 }
             }));

        //递归浏览命令;
        private DelegateCommand _exploreRsvCommand;
        public DelegateCommand ExploreRsvCommand =>
            _exploreRsvCommand ?? (_exploreRsvCommand = new DelegateCommand(() => {


            },
            () => (nodeService.Value?.SelectedNode is StorageTreeUnit stUnit) && stUnit.File is IIterableFile));

        //显示文件系统信息;
        private DelegateCommand _showFileSystemInfoCommand;
        public DelegateCommand ShowFileSystemInfoCommand =>
            _showFileSystemInfoCommand ?? (_showFileSystemInfoCommand = new DelegateCommand(() => {
                var device = (nodeService?.Value?.SelectedNode as StorageTreeUnit).File as Device;
                BlockDeviceFSInfoMessageBox.Show(device);
            }, () => nodeService?.Value?.SelectedNode is StorageTreeUnit stUnit && stUnit.File is Device));

        private DelegateCommand _customSSearchCommand;
        public DelegateCommand CustomSSearchCommand =>
            _customSSearchCommand ?? (_customSSearchCommand = new DelegateCommand(
                () => {
                    ServiceLocator.Current.GetInstance<IShellService>()?.ChangeLoadState(true, string.Empty);
                    try {
                        var msg = new SignSearchMessageBox();
                        var setting = msg.Show();

                        if (setting != null) {
                            if (nodeService?.Value?.SelectedNode is IHaveData<ICaseFile> csFile && csFile.Data is IHaveData<Device> dcsFile) {
                                SignSearch(dcsFile.Data, setting);
                            }
                        }
                    }
                    catch (Exception ex) {
                        Logger.WriteLine($"{nameof(IFSNodeService)}-{nameof(CustomSSearchCommand)}:{ex.Message}");
                    }
                    finally {
                        ServiceLocator.Current.GetInstance<IShellService>()?.ChangeLoadState(false, string.Empty);
                    }
                },
                () =>
                nodeService?.Value?.SelectedNode is IHaveData<ICaseFile> csFUnit && csFUnit.Data is IHaveData<Device>
            ));

        /// <summary>
        /// 签名搜索;
        /// </summary>
        /// <param name="blDevice"></param>
        /// <param name="setting"></param>
        private void SignSearch(BlockDeviceFile blDevice, SignSearchSetting setting) {
            Device device = null;
            long startLBA = 0;
            long endLBA = 0;
            if (blDevice is Device) {
                device = blDevice as Device;
                endLBA = device.Size - 1;
            }
            else if (blDevice is Partition) {
                var part = blDevice as Partition;
                device = blDevice.GetParent<Device>();
                startLBA = part.StartLBA;
                endLBA = part.EndLBA;

            }

            if (device != null) {
                var dialog = new ProgressMessageBox();
                dialog.WindowTitle = FindResourceString("SignSearch");

                var part = new SearcherPartition(device, blDevice, startLBA, endLBA, $"{blDevice.Name}-{FindResourceString("SignSearch")}");

                dialog.DoWork += (sender, e) => {
                    var searcher = new SignSearcher(device.Stream, setting.KeyWord, setting.MaxSize, setting.SectorSize, setting.SecStartLBA);
                    searcher.AlignToSector = setting.AlignToSec;
                    searcher.FileExtension = setting.FileExtension;

                    searcher.CurOffsetChanged += (insender, curOffset) => {
                        var percentage = (int)((curOffset - startLBA) * 100 / (endLBA - startLBA));
                        if (percentage >= 0 && percentage <= 100) {
                            dialog.ReportProgress(percentage,
                            FindResourceString("SearchingSignFile"),
                            $"{FindResourceString("RecoveringBySign")}:{percentage}%");
                        }
                        if (dialog.CancellationPending) {
                            searcher.Stop();
                        }
                    };

                    searcher.SearchStart(startLBA, endLBA);

                    var fileList = new List<RegularFile>();
                    var shfileList = new List<RegularFile>();

                    try {
                        //遍历获取文件列表;
                        searcher.FileExtension = setting.FileExtension;
                        var ndList = searcher.GetFileList(string.Empty);
                        if (ndList?.Count != 0) {
                            shfileList.AddRange(ndList.Select(p => new SearcherFile(part, p)));
                        }
                        shfileList.ForEach(p => {
                            fileList.Add(p);
                        });
                    }
                    catch (Exception ex) {
                        Logger.WriteLine($"{nameof(IFSNodeService)} -> {nameof(RecoverSign)}:{ex.Message}");
                    }
                    finally {
                        part.Children.AddRange(fileList);
                        searcher.Dispose();
                    }
                };
                dialog.RunWorkerCompleted += (sender, e) => {
                    AddShowingFile(part);
                    ServiceLocator.Current.GetInstance<IShellService>()?.Focus();
                };
                dialog.ShowDialog();
            }
        }

        /// <summary>
        /// 重组;
        /// </summary>
        /// <param name="blDevice"></param>
        /// <param name="isReComposite"></param>
        private void RecoverSign(BlockDeviceFile blDevice, bool isReComposite = false) {
            Device device = null;
            long startLBA = 0;
            long endLBA = 0;
            if (blDevice is Device) {
                device = blDevice as Device;
                endLBA = device.Size - 1;
            }
            else if (blDevice is Partition) {
                var part = blDevice as Partition;
                device = blDevice.GetParent<Device>();
                startLBA = part.StartLBA;
                endLBA = part.EndLBA;

            }

            if (device != null) {
                var dialog = new ProgressMessageBox {
                    WindowTitle = isReComposite ? FindResourceString("MobileRecompositeBySign") : FindResourceString("RecoveredBySign")
                };

                string[] extensions = null;

                if (isReComposite) {
                    var setting = ReComPreSettingMessageBox.Show();
                    if (setting != null) {
                        extensions = setting.Extesions;
                    }
                    else {
                        return;
                    }
                }

                SearcherPartition part = null;

                dialog.DoWork += (sender, e) => {
                    IFileSearcher searcher = null;
                    if (!isReComposite) {
                        searcher = new PictureSearcher(device, device.SecSize);
                    }
                    else {
                        searcher = new RecompositeSearcher(device, device.SecSize);
                    }

                    bool done = false;
                    ThreadPool.QueueUserWorkItem(callBack => {
                        while (!done) {
                            var percentage = (int)((searcher.CurOffset - startLBA) * 100 / (endLBA - startLBA));
                            if (percentage >= 0 && percentage <= 100) {
                                dialog.ReportProgress(percentage,
                                FindResourceString("SearchingSignFile"),
                                $"{(isReComposite ? FindResourceString("MobileRecompositeBySign") : FindResourceString("RecoveringBySign"))}:{percentage}%");
                            }
                            if (dialog.CancellationPending) {
                                searcher.Stop();
                            }
                            Thread.Sleep(1000);
                        }
                    });

                    searcher.SearchStart(startLBA, endLBA);
                    done = true;

                    try {
                        List<IFileNode> ndList = null;
                        if (isReComposite) {
                            if (extensions != null) {
                                ndList = searcher.GetFileList(string.Empty);
                                ndList.RemoveAll(p => extensions.FirstOrDefault(q =>
                                p.Type == q) == null);
                            }

                        }
                        else {
                            var sr = new StreamReader("Attachments/sign.txt");
                            var line = string.Empty;
                            ndList = new List<IFileNode>();
                            while (!string.IsNullOrEmpty(line = sr.ReadLine()?.Trim())) {
                                ndList.AddRange(searcher.GetFileList(line));
                                dialog.ReportProgress(100, $"{FindResourceString("RestoringData")}",
                                    $"{FindResourceString("Format")}{line}");
                            }
                            sr.Close();
                        }
                        part = SearcherPartition.LoadFromNodeList(blDevice, ndList, $"{blDevice.Name}-{(isReComposite ? FindResourceString("MobileRecompositeBySign") : FindResourceString("RecoveredBySign"))}");
                    }
                    catch (Exception ex) {
                        Logger.WriteLine($"{nameof(IFSNodeService)} -> {nameof(RecoverSign)}:{ex.Message}");
                    }
                    finally {

                        searcher.Dispose();
                    }
                };
                dialog.RunWorkerCompleted += (sender, e) => {
                    AddShowingFile(part);

                    ServiceLocator.Current.GetInstance<IShellService>()?.Focus();
                };

                dialog.ShowDialog();
            }

        }

        private DelegateCommand unAvailebleCommand = new DelegateCommand(() => { }, () => false);

        /// <summary>
        /// 加入新的文件显示;
        /// </summary>
        /// <param name="file"></param>
        public void AddShowingFile(IFile file) {
            var documentService = ServiceLocator.Current.GetInstance<IDocumentTabService>();
            if (documentService == null) {
                EventLogger.Logger.WriteCallerLine($"{nameof(documentService)} can't be null!");
                return;
            }

            //验证该文件是否打开过;
            var preTab = documentService.CurrentTabs.FirstOrDefault(p => {
                if (file.FileType == FileType.BlockDeviceFile) {
                    return (p as FileBrowserTabModel)?.FileBrowserViewModel.OwnerFile == file;
                }
                else if (file is LocalDirectory) {
                    var fbModel = (p as FileBrowserTabModel)?.FileBrowserViewModel;
                    if (fbModel == null) {
                        return false;
                    }
                    var imFile = fbModel.File;
                    var thisFile = file;
                    while (thisFile != null) {
                        if (imFile == thisFile) {
                            return true;
                        }
                        thisFile = thisFile.Parent;
                    }

                    return false;
                }
                else if (file.FileType == FileType.Directory) {
                    var part = file.GetParent<Partition>();
                    return part == (p as FileBrowserTabModel)?.FileBrowserViewModel.OwnerFile;
                }
                return false;
            });

            //如若已打开，将跳转至该文件;
            if (preTab != null) {
                documentService.ChangeSelectedTab(preTab);
                if (preTab is FileBrowserTabModel fbTabModel) {
                    fbTabModel.FileBrowserViewModel.EnterFile(file);
                }
                //CDFCMessageBox.Show($"对象:{file.Name}已经被打开。");
            }
            else {
                //选定新增的Tab;
                FileBrowserViewModel newFbVm = null;
                if (file is Partition) {
                    newFbVm = new PartitionBrowserViewModel(file as Partition);
                }
                else if (file is Device) {
                    newFbVm = new DeviceBrowserTabModel(file as Device);

                    //添加请求;
                    newFbVm.AddPartTabRequired += (sender, e) => {
                        AddShowingFile(e.Target);
                    };
                }
                else if (file is LocalDirectory) {
                    newFbVm = new LocalDirectoryBrowserViewModel(file);
                }
                else if (file is CDFC.Parse.Abstracts.Directory) {
                    newFbVm = new PartitionBrowserViewModel(file as CDFC.Parse.Abstracts.Directory);
                }
                if (newFbVm != null) {
                    ////订阅关闭事件;
                    //WeakEventManager<FileBrowserTabModel, TEventArgs<IClosableTabModel>>.
                    //    AddHandler(newTab, nameof(newTab.TabClosed), (removingSender, tab) => {

                    //        RemoveBrowserItem(tab.Target);
                    //    });

                    //查看程序事件;
                    //WeakEventManager<FileBrowserViewModel, TEventArgs<ViewerProgramMessage>>.
                    //    AddHandler(newFbVm, nameof(newFbVm.WatchRequired), (watchSener, msg) => {
                    //        WatchRequired?.Invoke(this, msg.Target);
                    //    });

                    ////等待要求：
                    //WeakEventManager<FileBrowserViewModel, TEventArgs<bool>>.
                    //    AddHandler(
                    //    newFbVm, nameof(newFbVm.IsLoadingRequired), (loadingSender, arg) => {
                    //        IsLoadingRequired?.Invoke(this, arg.Target);
                    //    });
                    //WeakEventManager<FileBrowserViewModel, EventArgs>.
                    //    AddHandler(
                    //    newFbVm, nameof(newFbVm.CloseAllRequired), (closeSender, arg) => {
                    //        CloseAllItems();
                    //    });
                    var newTab = new FileBrowserTabModel(newFbVm);
                    documentService.AddTab(newTab);

                    documentService.ChangeSelectedTab(newTab);
                }
            }


        }

        //展开某个具有子文件的文件;
        public void ExpandFile(IIterableFile file) {
            var documentService = ServiceLocator.Current.GetInstance<IDocumentTabService>();
            if (documentService == null) {
                EventLogger.Logger.WriteCallerLine($"{nameof(documentService)} can't be null!");
                return;
            }

            ServiceLocator.Current.GetInstance<IShellService>()?.ChangeLoadState(true, string.Empty);
            ThreadPool.QueueUserWorkItem(callBack => {
                try {
                    //验证该文件是否打开过;
                    var preTab = documentService.CurrentTabs.FirstOrDefault(p => {
                        if (file.FileType == FileType.BlockDeviceFile) {
                            return (p is FileBrowserTabModel fbTabModel)
                            && (fbTabModel.FileBrowserViewModel.OwnerFile == file);
                        }
                        else if (file is LocalDirectory) {
                            IFile parent = file;
                            while (parent != null) {
                                if (parent == (p as FileBrowserTabModel)?.FileBrowserViewModel.File) {
                                    return true;
                                }
                                parent = parent.Parent;
                            }
                            return false;
                        }
                        else if (file.FileType == FileType.Directory) {
                            var part = file.GetParent<Partition>();
                            return part == (p as FileBrowserTabModel)?.FileBrowserViewModel.OwnerFile;
                        }
                        return false;
                    });
                    if (preTab != null) {
                        documentService.ChangeSelectedTab(preTab);
                        if (preTab is FileBrowserTabModel fbTabModel) {
                            fbTabModel.FileBrowserViewModel.ExpandFile(file);
                        }
                    }
                }
                catch (Exception ex) {
                    Logger.WriteCallerLine($"{ex.Message}");
                    AppInvoke(() => {
                        RemainingMessageBox.Tell(ex.Message);
                    });
                }
                finally {
                    //解除等待；
                    ServiceLocator.Current.GetInstance<IShellService>()?.ChangeLoadState(false, string.Empty);
                }
            });
            
        }
    }
}
