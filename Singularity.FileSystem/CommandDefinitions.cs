using CDFC.Hasher;
using CDFC.Hasher.Interfaces;
using CDFC.Parse.Abstracts;
using CDFC.Util.IO;
using CDFCCultures.Helpers;
using CDFCMessageBoxes.MessageBoxes;
using CDFCUIContracts.Commands;
using Prism.Commands;
using Singularity.UI.MessageBoxes.MessageBoxes;
using Singularity.UI.FileSystem.Models;
using Singularity.UI.FileSystem.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Threading;
using static CDFCCultures.Managers.ManagerLocator;
using SingularityForensic.Modules.MainMenu.Models;
using SingularityForensic.Modules.MainPage;
using Singularity.UI.FileSystem.Resources;
using Microsoft.Practices.ServiceLocation;
using EventLogger;
using Ookii.Dialogs.Wpf;
using SingularityForensic.Modules.Shell.Global.Services;
using System;
using System.IO;
using System.Windows;
using Singularity.UI.Case.Global.Services;
using Singularity.UI.Case;
using SingularityForensic.Modules.MainPage.Global.Services;
using Singularity.Interfaces;
using Singularity.UI.Case.Contracts;
using CDFC.Parse.Contracts;
using System.Linq;
using Singularity.UI.Case.MessageBoxes;
using Singularity.UI.FileSystem.Interfaces;

namespace Singularity.UI.FileSystem {
    [Export(typeof(CommandItem<(DirectoriesBrowserViewModel, IFileRow)>))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ComputeHashCommandItem: CommandItem<(DirectoriesBrowserViewModel Dvm, IFileRow Row)> {
        public ComputeHashCommandItem() {
            CommandName = FindResourceString("ComputeHashCommandItem");
            LoadChildren();
        } 

        private void LoadChildren() {
            Children = new ObservableCollection<ICommandItem>();
            AddHasher(MD5Hasher.StaticInstance,FindResourceString("MD5HashCommandItem"));
            AddHasher(SHA1Hasher.StaticInstance, FindResourceString("SHA1HashCommandItem"));
            AddHasher(SHA256Hasher.StaticInstance, FindResourceString("SHA256HashCommandItem"));
            AddHasher(SHA512Hasher.StaticInstance, FindResourceString("SHA512HashCommandItem"));
        }

        private void AddHasher(IHasher hasher,string commandName) {
            if(Children == null) {
                return;
            }

            var comm = new DelegateCommand(
                () => {
                    if (GetData != null) {
                        var data = GetData();
                        if (data.Row is IFileRow<RegularFile> regFRow) {
                            var regFile = regFRow.File;
                            using (var stream = regFile.GetStream()) {
                                if(stream.Length == 0) {
                                    return;
                                }

                                var msg = new ProgressMessageBox {
                                    WindowTitle = CommandName
                                };

                                var canceld = false;
                                var done = false;
                                var operatebleStream = new OperatebleStream(stream);
                                operatebleStream.Position = 0;
                                var res = string.Empty;

                                msg.DoWork += (sender, e) => {
                                    operatebleStream.PositionChanged += (se,pos) => {
                                        msg.ReportProgress((int)( pos * 1000 / operatebleStream.Length));
                                    };

                                    ThreadPool.QueueUserWorkItem(cb => {
                                        var bts = hasher.ComputeStream(operatebleStream);
                                        res = ByteConverterHelper.ByteToHex(bts);
                                        done = true;
                                    });

                                    while (!done) {
                                        if (msg.CancellationPending) {
                                            canceld = true;
                                            operatebleStream.Break();
                                        }
                                        Thread.Sleep(100);
                                    }

                                    
                                };
                                
                                msg.RunWorkerCompleted += (sender, e) => {
                                    if (!canceld) {
                                        InputValueMessageBox.Show(CommandName, string.Empty, res);
                                    }
                                };

                                msg.ShowDialog();
                            }
                                
                        }
                    }
                    
                }
            );

            Children.Add(new CommandItem {
                CommandName = commandName,
                Command = comm
            });
        }
    }

    public static class MenuItemDefinitions {
        private static DelegateCommand _addImgCommand;
        
        public static DelegateCommand AddImgCommand => _addImgCommand ??
            (_addImgCommand = new DelegateCommand(
                () => {
                    var csService = ServiceLocator.Current.GetInstance<ICaseService>();
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
                        var shellService = ServiceLocator.Current.GetInstance<IShellService>();
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
        public static readonly MenuButtonItemModel AddImgMenuItem = new MenuButtonItemModel(MenuGroupDefinitions.MainPageMenuGroup, FindResourceString("AddImg"), 4) {
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
                    var serviceProviders = ServiceLocator.Current.GetAllInstances<IFileSystemServiceProvider>();
                    var stream = File.Open(path, FileMode.Open, isreadonly ? FileAccess.Read : FileAccess.ReadWrite);
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
                        }
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
                    //    ServiceLocator.Current.GetInstance<ICaseService>()?.AddNewCaseFile(new AndroidDeviceCaseFile(adDevice,path,DateTime.Now));
                    //}
                    //else if(device is UnKnownDevice unDevice) {
                    //    ServiceLocator.Current.GetInstance<ICaseService>()?.AddNewCaseFile(new UnknownDeviceCaseFile(unDevice,path, DateTime.Now));
                    //}
                }
            };

            msg.ShowDialog();
        }
    }

    public static class CommandDefinitions {
        private static INodeService _nodeService;
        private static INodeService NodeService => _nodeService ?? (_nodeService = ServiceLocator.Current.GetInstance<INodeService>());
        
        public static readonly DelegateCommand ShowPropertyCommand  = new DelegateCommand(() => {
            if (NodeService?.SelectedNode is StorageTreeUnit stUnit && stUnit.File is BlockDeviceFile) {

                if (SingularityCase.Current.CaseFiles.FirstOrDefault(p =>
                p is IHaveData<IFile> fcsFile && fcsFile.Data == stUnit.File) is IHaveData<IFile> fCsFile) {
                    SingularityCase.Current.Save();
                    var csFile = ShowCaseFilePropertyMessageBox.Show(fCsFile as ICaseFile);
                }
            }
        });
        
        //递归浏览命令;
        public static readonly DelegateCommand ExploreRsvCommand = new DelegateCommand(() => {


            },
            () => (NodeService?.SelectedNode is StorageTreeUnit stUnit) && stUnit.File is IIterableFile);
        

        private static DelegateCommand unAvailebleCommand = new DelegateCommand(() => { }, () => false);
    }
}
