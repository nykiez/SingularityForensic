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
using System.Windows.Input;
using Singularity.UI.MessageBoxes.Models;
using Singularity.Interfaces;
using Singularity.UI.Case.Contracts;
using Singularity.UI.FileSystem.MessageBoxes;
using CDFC.Parse.Contracts;
using System.Linq;
using Singularity.UI.Case.MessageBoxes;
using Singularity.UI.FileSystem.Global.Services;
using CDFC.Parse.Signature.DeviceObjects;
using CDFC.Parse.Signature.Pictures;
using System.Collections.Generic;
using CDFC.Parse.Signature.Contracts;
using CDFC.Parse.Android.DeviceObjects;

namespace Singularity.UI.FileSystem {
    [Export(typeof(CommandItem<(DirectoriesBrowserViewModel, FileRow)>))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ComputeHashCommandItem: CommandItem<(DirectoriesBrowserViewModel Dvm, FileRow Row)> {
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
                        if (data.Row.File is RegularFile regFile) {
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
            Device device = null;
            LoadRes loadRes = LoadRes.Init;
            string msgInfo = string.Empty;

            msg.DoWork += (sender, e) => {
                try {
                    device = AndroidDevice.LoadFromPath(path, isreadonly,tuple=> {
                        if (tuple.allSize != 0L && tuple.thePartSize != 0L) {
                            msg.ReportProgress((int)(tuple.curSize * 100L / tuple.allSize), (int)(tuple.curPartSize * 100L / tuple.thePartSize),
                            FindResourceString("LoadingImg"), $"{FindResourceString("LoadingPartition")}{tuple.curPart}/{tuple.allPart}" );
                        }
                    }, () => msg.CancellationPending);
                    if (device == null) {
                        device = UnKnownDevice.LoadFromPath(path, isreadonly);
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
                else if(loadRes == LoadRes.Succeed) {
                    if(device is AndroidDevice adDevice) {
                        ServiceLocator.Current.GetInstance<ICaseService>()?.AddNewCaseFile(new AndroidDeviceCaseFile(adDevice,path,DateTime.Now));
                    }
                    else if(device is UnKnownDevice unDevice) {
                        ServiceLocator.Current.GetInstance<ICaseService>()?.AddNewCaseFile(new UnknownDeviceCaseFile(unDevice,path, DateTime.Now));
                    }
                }
            };

            msg.ShowDialog();
        }
    }

    public static class CommandDefinitions {
        public const string DeviceNodeContextCommand = nameof(DeviceNodeContextCommand);

        private static INodeService _nodeService;
        private static INodeService NodeService => _nodeService ?? (_nodeService = ServiceLocator.Current.GetInstance<INodeService>());

        
        public static readonly DelegateCommand RecompositeSignCommand = new DelegateCommand(
                () => {
                    ServiceLocator.Current.GetInstance<IShellService>()?.ChangeLoadState(true, string.Empty);
                    if (NodeService?.SelectedNode is IHaveData<ICaseFile> csFUnit && csFUnit.Data is IHaveData<Device> dcsFile) {
                        try {
                            RecoverSign(dcsFile.Data, true);
                        }
                        catch (Exception ex) {
                            Logger.WriteCallerLine(ex.Message);
                        }
                    }
                    ServiceLocator.Current.GetInstance<IShellService>()?.ChangeLoadState(false, string.Empty);
                },
                () => (NodeService?.SelectedNode is IHaveData<ICaseFile> csFUnit) && (csFUnit.Data is IHaveData<Device>)
            );

        [Export(DeviceNodeContextCommand)]
        public static readonly ICommandItem RecompositeSignCMI = new CommandItem {
            Command = RecompositeSignCommand,
            CommandName = FindResourceString("MobileRecompositeBySign")
        };

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
        

        //显示文件系统信息;
        public static readonly DelegateCommand ShowFileSystemInfoCommand = new DelegateCommand(() => {
                var device = ((NodeService?.SelectedNode as IHaveData<ICaseFile>).Data as IHaveData<Device>).Data;
                BlockDeviceFSInfoMessageBox.Show(device);
            }, 
            () => NodeService?.SelectedNode is IHaveData<ICaseFile> csFUnit && csFUnit.Data is IHaveData<Device>);

        [Export(DeviceNodeContextCommand)]
        public static readonly ICommandItem ShowFileSystemInfoMI = new CommandItem {
            Command = ShowFileSystemInfoCommand,
            CommandName = FindResourceString("FileSystemInfo")
        };
        
        public static readonly DelegateCommand CustomSSearchCommand = new DelegateCommand(
                () => {
                    ServiceLocator.Current.GetInstance<IShellService>()?.ChangeLoadState(true, string.Empty);
                    try {
                        var msg = new SignSearchMessageBox();
                        var setting = msg.Show();

                        if (setting != null) {
                            if (NodeService?.SelectedNode is IHaveData<ICaseFile> csFile && csFile.Data is IHaveData<Device> dcsFile) {
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
                () => NodeService?.SelectedNode is IHaveData<ICaseFile> csFUnit && csFUnit.Data is IHaveData<Device>
            );
        [Export(DeviceNodeContextCommand)]
        public static readonly ICommandItem CustomSSearchMI = new CommandItem {
            Command = CustomSSearchCommand,
            CommandName = FindResourceString("CustomSignSearch")
        };


        /// <summary>
        /// 签名搜索;
        /// </summary>
        /// <param name="blDevice"></param>
        /// <param name="setting"></param>
        private static void SignSearch(BlockDeviceFile blDevice, SignSearchSetting setting) {
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
                    ServiceLocator.Current.GetInstance<IFSNodeService>()?.AddShowingFile(part);
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
        private static void RecoverSign(BlockDeviceFile blDevice, bool isReComposite = false) {
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
                        Logger.WriteCallerLine(ex.Message);
                    }
                    finally {

                        searcher.Dispose();
                    }
                };
                dialog.RunWorkerCompleted += (sender, e) => {
                    ServiceLocator.Current.GetInstance<IFSNodeService>()?.AddShowingFile(part);

                    ServiceLocator.Current.GetInstance<IShellService>()?.Focus();
                };

                dialog.ShowDialog();
            }

        }

        private static DelegateCommand unAvailebleCommand = new DelegateCommand(() => { }, () => false);
    }
}
