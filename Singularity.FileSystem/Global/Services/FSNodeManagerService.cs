using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using CDFCMessageBoxes.MessageBoxes;
using EventLogger;
using Microsoft.Practices.ServiceLocation;
using Singularity.UI.FileSystem.Global.TabModels;
using Singularity.UI.FileSystem.ViewModels;
using SingularityForensic.Modules.MainPage.Global.Services;
using SingularityForensic.Modules.Shell.Global.Services;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using CDFC.Parse.Local.DeviceObjects;
using static CDFCUIContracts.Helpers.ApplicationHelper;
using Singularity.UI.FileSystem.Interfaces;

namespace Singularity.UI.FileSystem.Global.Services {
    public interface IFSNodeService {
        //void ShowCaseFileProperty(ICaseFile csFile);
        //void ShowFileSystem(IFile file);
        //void SignSearch(BlockDeviceFile blDevice, SignSearchSetting setting);
        //void RecoverSign(BlockDeviceFile blDevice, bool isReComposite = false);
        void AddShowingFile(IFile file, IFileSystemServiceProvider provider);
        void ExpandFile(IIterableFile file);
    }

    //文件系统树形节点管理器服务;
    [Export(typeof(IFSNodeService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FSNodeManagerService : IFSNodeService {
        [Import]
        Lazy<INodeService> nodeService;
        
        ////显示文件系统信息;
        //public void ShowFileSystem(IFile file) {
        //    var device = file as Device;
        //    BlockDeviceFSInfoMessageBox.Show(device);
            
        //}
        

        /// <summary>
        /// 加入新的文件显示;
        /// </summary>
        /// <param name="file"></param>
        public void AddShowingFile(IFile file,IFileSystemServiceProvider provider) {
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
                    newFbVm = new PartitionBrowserViewModel(file as Partition,provider);
                }
                else if (file is Device) {
                    newFbVm = new DeviceBrowserTabModel(file as Device, provider);

                    //添加请求;
                    newFbVm.AddPartTabRequired += (sender, e) => {
                        AddShowingFile(e.Target, provider);
                    };
                }
                else if (file is LocalDirectory) {
                    newFbVm = new LocalDirectoryBrowserViewModel(file,provider);
                }
                else if (file is CDFC.Parse.Abstracts.Directory) {
                    newFbVm = new PartitionBrowserViewModel(file as CDFC.Parse.Abstracts.Directory, provider);
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
