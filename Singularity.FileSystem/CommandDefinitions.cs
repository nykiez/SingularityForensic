using CDFCMessageBoxes.MessageBoxes;
using Prism.Commands;
using Singularity.UI.MessageBoxes.MessageBoxes;
using System.ComponentModel.Composition;
using static CDFCCultures.Managers.ManagerLocator;
using Microsoft.Practices.ServiceLocation;
using EventLogger;
using Ookii.Dialogs.Wpf;
using System;
using System.IO;
using System.Windows;
using Singularity.UI.Case;
using CDFC.Parse.Contracts;
using Singularity.Contracts.Case;
using Singularity.Contracts.FileSystem;
using Singularity.Contracts.Shell;
using Singularity.Contracts.Contracts.MainMenu;
using Singularity.Contracts.MainMenu;
using Singularity.Contracts.Common;

namespace Singularity.UI.FileSystem {
    public static class MenuItemDefinitions {
        private static DelegateCommand _addImgCommand;
        
        public static DelegateCommand AddImgCommand => _addImgCommand ??
            (_addImgCommand = new DelegateCommand(
                () => {
                    var csService = ServiceProvider.Current.GetInstance<ICaseService>();
                    if (csService == null || !csService.ConfirmCaseLoaded()) {
                        return;
                    }

                    if (SingularityCase.Current == null) {
                        Logger.WriteCallerLine("null case entity!", "AddImgCommand");
                        return;
                    }
                    var vistaOpenFileDialog = new VistaOpenFileDialog();
                    vistaOpenFileDialog.Multiselect = false;
                    if (vistaOpenFileDialog.ShowDialog() == true) {
                        var shellService = ServiceProvider.Current.GetInstance<IShellService>();
                        shellService?.ChangeLoadState(true, string.Empty);
                        
                        try {
                            LoadImgFromPath(vistaOpenFileDialog.FileName, false);
                        }
                        catch (Exception ex) {
                            Logger.WriteCallerLine(ex.Message, "AddImgCommand");
                        }
                        finally {
                            shellService?.ChangeLoadState(false, null);
                        }
                    }
                }
            ));
        
        [Export]
        public static readonly MenuButtonItemModel AddImgMenuItem = new MenuButtonItemModel(MenuConstants.MenuMainGroup,
            FindResourceString("AddImg"), 4) {
            Command = AddImgCommand,
            IconSource = IconSources.AddImgIcon
        };
        
        private enum LoadRes {
            Init,
            IOError,
            UnknownErr,
            Succeed
        }

        public static void LoadImgFromPath(string path, bool isreadonly = true) {
            var msg = new DoubleProcessMessageBox {
                Title = FindResourceString("AddingImg")
            };
            IFile file = null;
            LoadRes loadRes = LoadRes.Init;
            string msgInfo = string.Empty;
            IFileSystemServiceProvider fsProvider = null;

            msg.DoWork += (sender, e) => {
                try {
                    var serviceProviders = ServiceProvider.Current.GetAllInstances<IFileSystemServiceProvider>();
                    var stream = File.Open(path, FileMode.Open, isreadonly ? FileAccess.Read : FileAccess.ReadWrite, FileShare.ReadWrite);
                    foreach (var provider in serviceProviders) {
                        if (provider.StreamFileParser.CheckIsValid(stream)) {
                            fsProvider = provider;
                            file = provider.StreamFileParser.ParseStream(stream, tuple => {
                                msg.ReportProgress(tuple.totalPro, tuple.detailPro, tuple.desc, tuple.word);
                                //if (tuple.allSize != 0L && tuple.thePartSize != 0L) {
                                //    msg.ReportProgress((int)(tuple.curSize * 100L / tuple.allSize), (int)(tuple.curPartSize * 100L / tuple.thePartSize),
                                //    FindResourceString("LoadingImg"), $"{FindResourceString("LoadingPartition")}{tuple.curPart}/{tuple.allPart}");
                                //}
                            },() => msg.CancellationPending);
                            //device = AndroidDevice.LoadFromPath(path, isreadonly, tuple => {
                            //    if (tuple.allSize != 0L && tuple.thePartSize != 0L) {
                            //        msg.ReportProgress((int)(tuple.curSize * 100L / tuple.allSize), (int)(tuple.curPartSize * 100L / tuple.thePartSize),
                            //        FindResourceString("LoadingImg"), $"{FindResourceString("LoadingPartition")}{tuple.curPart}/{tuple.allPart}");
                            //    }
                            //}, () => msg.CancellationPending);
                            //if (device == null) {
                            //    device = UnKnownDevice.LoadFromPath(path, isreadonly);
                            //}
                            //break;
                            break;
                        }
                    }
                    
                    if(file == null) {
                        file = DefaultFileSystemProvider.StaticInstance.StreamFileParser.ParseStream(stream, tuple => { }, () => msg.CancellationPending);
                        fsProvider = DefaultFileSystemProvider.StaticInstance;
                    }

                    loadRes = LoadRes.Succeed;
                }
                catch(IOException ex) {
                    Logger.WriteCallerLine(ex.Message, "LoadImgFromPath");
                    msgInfo = ex.Message;
                    loadRes = LoadRes.IOError;
                }
                catch(Exception ex) {
                    Logger.WriteCallerLine(ex.Message, "LoadImgFromPath");
                    msgInfo = ex.Message;
                    loadRes = LoadRes.UnknownErr;
                }
            };
            msg.RunWorkerCompleted += (sender, e) => {
                if(loadRes == LoadRes.IOError) {
                    if (!isreadonly) {
                        if (CDFCMessageBox.Show(FindResourceString("ConfirmToOpenReadonly"), 
                            FindResourceString("FailedToOpen"), MessageBoxButton.YesNo) != MessageBoxResult.Yes) {
                            return;
                        }
                        try {
                            LoadImgFromPath(path, true);
                            return;
                        }
                        catch (Exception) {
                            RemainingMessageBox.Tell($"{FindResourceString("FailedToOpenFile")}:{msgInfo}");
                            return;
                        }
                    }
                }
                else if(loadRes == LoadRes.UnknownErr) {
                    RemainingMessageBox.Tell(string.Format("{0}:{1}", FindResourceString("FailedToOpenFile"), msgInfo));
                    return;
                }
                else if(loadRes == LoadRes.Succeed && fsProvider != null) {
                    try {
                        fsProvider.AddNewCaseFile(file, path);
                    }
                    catch(Exception ex) {
                        Logger.WriteCallerLine(ex.Message);
                        RemainingMessageBox.Tell(ex.Message);
                    }
                    
                    //if(device is AndroidDevice adDevice) {
                    //   ServiceProvider.Current.GetInstance<ICaseService>()?.AddNewCaseFile(new AndroidDeviceCaseFile(adDevice,path,DateTime.Now));
                    //}
                    //else if(device is UnKnownDevice unDevice) {
                    //   ServiceProvider.Current.GetInstance<ICaseService>()?.AddNewCaseFile(new UnknownDeviceCaseFile(unDevice,path, DateTime.Now));
                    //}
                }
            };

            msg.ShowDialog();
        }
    }

    
}
