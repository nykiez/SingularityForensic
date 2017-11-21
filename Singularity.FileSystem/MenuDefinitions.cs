using CDFC.Parse.Abstracts;
using CDFC.Parse.Android.DeviceObjects;
using CDFC.Singularity.Forensics.Cases;
using CDFC.Singularity.Forensics.Contracts;
using CDFCMessageBoxes.MessageBoxes;
using EventLogger;
using Microsoft.Practices.ServiceLocation;
using Ookii.Dialogs.Wpf;
using Prism.Commands;
using Singularity.UI.MessageBoxes.MessageBoxes;
using SingularityForensic.Modules.Case.Global;
using SingularityForensic.Modules.FileSystem.Global.Services;
using SingularityForensic.Modules.FileSystem.Models;
using SingularityForensic.Modules.FileSystem.Resources;
using SingularityForensic.Modules.MainMenu;
using SingularityForensic.Modules.MainMenu.Models;
using SingularityForensic.Modules.MainPage;
using SingularityForensic.Modules.Shell.Global.Services;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using static CDFCCultures.Managers.ManagerLocator;
namespace SingularityForensic.Modules.FileSystem {
    
    //菜单项定义;
    public static partial class MenuItemDefinitions {
        [Export]
        public static readonly MenuButtonItemModel AddImgMenuItem =
            new MenuButtonItemModel(
                MenuGroupDefinitions.MainPageMenuGroup, FindResourceString("AddImg"), 4) {
                Command = AddImgCommand,
                IconSource = IconSources.AddImgIcon
            };

        private static DelegateCommand _addImgCommand;
        public static DelegateCommand AddImgCommand =>
            _addImgCommand ?? (_addImgCommand = new DelegateCommand(
                () => {
                    //若尚未装载案件;询问是否新建案件;
                    if (ServiceLocator.Current.GetInstance<ICaseService>()?.ConfirmCaseLoaded() != true) {
                        return;
                    }

                    if (SingularityCase.Current == null) {
                        Logger.WriteCallerLine($"null case entity!");
                        return;
                    }
                    else {
                        var dialog = new VistaOpenFileDialog();
                        dialog.Multiselect = false;
                        if (dialog.ShowDialog() == true) {
                            ServiceLocator.Current.GetInstance<IShellService>()?.ChangeLoadState(true, string.Empty);
                            try {
                                LoadImgFromPath(dialog.FileName, false);
                            }
                            catch (Exception ex) {                               //若打开失败，可能是由于文件被锁定，只能以只读方式打开;
                                Logger.WriteCallerLine(ex.Message);
                            }
                            finally {
                                ServiceLocator.Current.GetInstance<IShellService>()?.ChangeLoadState(false, null);
                            }
                        }
                    }
                }
            ));


        //加载结果;
        private enum LoadRes {
            Init,
            IOError,
            UnknownErr,
            Succeed
        }

        /// <summary>
        /// 从指定源文件位置加载案件文件;
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isreadonly"></param>
        /// <returns></returns>
        public static void LoadImgFromPath(string path, bool isreadonly = true) {
            var msg = new DoubleProcessMessageBox { Title = FindResourceString("AddingImg") };

            Device device = null;

            var loadRes = LoadRes.Init;
            var msgInfo = string.Empty;

            msg.DoWork += delegate {
                try {
                    device = AndroidDevice.LoadFromPath(path, isreadonly, tuple => {
                        if (tuple.allSize != 0 && tuple.thePartSize != 0) {
                            msg.ReportProgress((int)(tuple.curSize * 100 / tuple.allSize),
                            (int)(tuple.curPartSize * 100 / tuple.thePartSize),
                            FindResourceString("LoadingImg"),
                            $"{FindResourceString("LoadingPartition")}{tuple.curPart}/{tuple.allPart}");
                        }
                    }, () => msg.CancellationPending);
                    if (device == null) {
                        device = UnKnownDevice.LoadFromPath(path, isreadonly);
                    }
                    loadRes = LoadRes.Succeed;
                }
                catch (IOException ex) {
                    Logger.WriteCallerLine(ex.Message);
                    msgInfo = ex.Message;
                    loadRes = LoadRes.IOError;
                }
                catch (Exception ex) {
                    Logger.WriteLine(ex.Message);
                    msgInfo = ex.Message;
                    loadRes = LoadRes.UnknownErr;
                }
            };

            msg.RunWorkerCompleted += delegate {
                if (loadRes == LoadRes.IOError) {
                    if (!isreadonly) {
                        if (CDFCMessageBox.Show(FindResourceString("ConfirmToOpenReadonly"),
                        FindResourceString("FailedToOpen"), MessageBoxButton.YesNo)
                        == MessageBoxResult.Yes) {
                            try {
                                LoadImgFromPath(path);
                            }
                            catch (Exception ex2) {
                                RemainingMessageBox.Tell($"{FindResourceString("FailedToOpenFile")}:{nameof(ex2.Message)}");
                                return;
                            }
                        }
                    }
                    else {
                        CDFCMessageBox.Show(FindResourceString("FailedToOpen"), msgInfo, MessageBoxButton.OK);
                    }
                }
                else if (loadRes == LoadRes.UnknownErr) {
                    RemainingMessageBox.Tell($"{FindResourceString("FailedToOpenFile")}:{nameof(msgInfo)}");
                }
                else if (loadRes == LoadRes.Succeed && device != null) {
                    //添加案件文件泛型委托方法;
                    void addCSFile<TCaseFile>(TCaseFile csFile) where TCaseFile : ICaseFile {
                        ServiceLocator.Current.GetInstance<IFSNodeManagerService>()?.LoadCaseFile(csFile);
                        ServiceLocator.Current.GetInstance<ICaseService>()?.AddCaseFile(csFile);
                    }

                    if (device is AndroidDevice adDevice) {
                        var cFile = new AndroidDeviceCaseFile(adDevice, path, DateTime.Now);
                        addCSFile(cFile);
                    }
                    else if (device is UnKnownDevice unDevice) {
                        var cFile = new UnknownDeviceCaseFile(unDevice, path, DateTime.Now);
                        addCSFile(cFile);
                    }
                }
            };

            msg.ShowDialog();

            ServiceLocator.Current.GetInstance<IShellService>()?.ChangeLoadState(false);
        }
    }
}
