using CDFCUIContracts.Abstracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Cflab.DataTransport;
using Cflab.DataTransport.Modules.Transport.Model;
using System.Threading;
using System.IO;
using static CDFCCultures.Helpers.IOPathHelper;
using Prism.Commands;
using CDFC.Info.Adb;
using SingularityForensic.Adb.Models.AdbViewer;
using Cflab.DataTransport.Modules.Backup.Android;
using SingularityForensic.Adb.Contracts;
using SingularityForensic.Adb.Helpers;
using SingularityForensic.Contracts.Info;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.App;

namespace SingularityForensic.Adb.ViewModels.AdbViewer {
    public partial class AdbInfoesCheckedViewModel:PageModelBase,IDisposable {
        public AdbInfoesCheckedViewModel():base(1) {
            var mobileInfoNode = new AdbTreeUnit(LanguageService.FindResourceString("PhoneInfoAquiredItems"));
            var mobileFileNode = new AdbTreeUnit(LanguageService.FindResourceString("PhoneFileAquiredItems"));

            Action <AdbTreeUnit,MInfoType> addInfoAct = (node, tp) => {
                //对于所有文件项，将进行的特殊处理;
                if (tp == MInfoType.AllFiles) {
                    var child = new AdbAllFileTreeUnit(MInfoTypeHelper.GetInfoTypeWord(tp)) { Level = 1 };
                    child.IsChecked = false;
                    node.Children.Add(child);
                    child.CheckStateChanged += (sender, e) => {
                        //若选中所有文件,将其他文件进行选中处理;
                        if (e ?? false) {
                            foreach (var item in node.Children) {
                                if (item != child) {
                                    //item.IsChecked = true;
                                    //item.IsEnabled = false;
                                }
                            }
                        }
                        else {
                            foreach (var item in node.Children) {
                                item.IsEnabled = true;
                            }
                        }
                    };
                }
                else {
                    var child = new AdbInfoTreeUnit<IInfo>(tp) { Level = 1 };
                    node.Children.Add(child);
                    child.CheckStateChanged += (sender, e) => {
                        if (e != null) {
                            if (e == node.IsChecked) {
                                return;
                            }
                            if (node.Children.FirstOrDefault(p => p.IsChecked != e.Value) != null) {
                                node.IsChecked = null;
                            }
                            else {
                                node.IsChecked = e;
                            }
                        }
                    };
                }
            };

            foreach (var tp in Enum.GetValues(typeof(MInfoType))) {
                if((MInfoType)tp == MInfoType.BackUp) {
                    var unit = new AdbBackUpFilesTreeUnit();
                    mobileFileNode.Children.Add(unit);
                    
                }
                else {
                    switch (MInfoTypeHelper.GetMInfoTypeBox((MInfoType)tp)) {
                        case MInfoTypeBox.AdbFile:
                            addInfoAct(mobileFileNode, (MInfoType)tp);
                            break;
                        case MInfoTypeBox.AdbInfo:
                            addInfoAct(mobileInfoNode, (MInfoType)tp);
                            break;
                    }
                }
                
            }

            AdbUnits.Add(mobileInfoNode);
            AdbUnits.Add(mobileFileNode);
        }

        public ObservableCollection<AdbTreeUnit> AdbUnits { get; set; } = new ObservableCollection<AdbTreeUnit>();
        public Device Device { get; set; }

        private bool disposed;
        public void Dispose() {
            disposed = true;
        }
    }

    public partial class AdbInfoesCheckedViewModel {
        public event EventHandler<List<IInfoModelContainer>> AquireDone;

        //确认命令;
        private DelegateCommand _confirmCommand;
        public DelegateCommand ConfirmCommand =>
            _confirmCommand ?? (_confirmCommand = new DelegateCommand(
                () => {
                    if (IsAquiring) {
                        MsgBoxService.Show(LanguageService.FindResourceString("WaitUntilAuqiringDone"));
                        return;
                    }
                    SetAuiqring(true);
                    ThreadPool.QueueUserWorkItem(cb => {
                        List<AdbTreeUnit> slUnits = new List<AdbTreeUnit>();
                        //遍历上级;
                        foreach (var item in AdbUnits) {
                            //遍历子级;
                            foreach (var node in item.Children) {
                                if (node.IsChecked == true && node is AdbFileTreeUnit) {
                                    slUnits.Add(node as AdbFileTreeUnit);
                                }
                            }
                        }

                        //确定是否选择了项;
                        if (slUnits.Count == 0) {
                            ThreadInvoker.UIInvoke(() => {
                                MsgBoxService.ShowError(LanguageService.FindResourceString("PleaseSelectBeforeAquiring"));
                                SetAuiqring(false);
                            });
                            
                            return;
                        }

                        var handleIndex = 0;
                        var handles = new AutoResetEvent[slUnits.Count];
                        for (int i = 0; i < handles.Length; i++) {
                            handles[i] = new AutoResetEvent(false);
                        }
                       
                        //发布任务;
                        slUnits.ForEach(p => {
                            if (p is AdbInfoTreeUnit<IInfo>) {
                                #region
                                var node = (p as AdbInfoTreeUnit<IInfo>);
                                switch (node.MInfoType) {
                                    case MInfoType.Audio:
                                        GetInfoToNode<Audio>(node, handles[handleIndex]);
                                        break;
                                    case MInfoType.Basic:
                                        GetInfoToNode<Basic>(node, handles[handleIndex]);
                                        break;
                                    case MInfoType.Calllog:
                                        GetInfoToNode<CallLog>(node, handles[handleIndex]);
                                        break;
                                    case MInfoType.Contact:
                                        GetInfoToNode<Contact>(node, handles[handleIndex]);
                                        break;
                                    case MInfoType.GPS:
                                        GetInfoToNode<Gps>(node, handles[handleIndex]);
                                        break;
                                    case MInfoType.Image:
                                        GetInfoToNode<Image>(node, handles[handleIndex]);
                                        break;
                                    case MInfoType.Package:
                                        GetInfoToNode<Package>(node, handles[handleIndex]);
                                        break;
                                    case MInfoType.Sms:
                                        GetInfoToNode<Sms>(node, handles[handleIndex]);
                                        break;
                                    case MInfoType.Video:
                                        GetInfoToNode<Video>(node, handles[handleIndex]);
                                        break;
                                }
                                #endregion
                            }
                            else if(p is AdbBackUpFilesTreeUnit) {
                                var node = p as AdbBackUpFilesTreeUnit;
                                try {
                                    var dtString = $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}-{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}";
                                    var abPath = $"{ServiceProvider.Current?.GetInstance<ICaseService>()?.CurrentCase.Path}/{Device.Serial}/{dtString}/backup.ab";
                                    var bRes = Device.Backup(abPath,
                                        () => {
                                            AppendLine(LanguageService.FindResourceString("ConfirmToBackUp"));
                                            return true;
                                        },
                                        err => {
                                            LoggerService.Current?.WriteCallerLine(
                                                $"{nameof(AdbInfoesCheckedViewModel)}->{nameof(ConfirmCommand)}-{nameof(Device.Backup)}:" +
                                                $"{err.Code},{err.Message}");
                                        });

                                    AppendLine(string.Empty);

                                    
                                    if (bRes) {
                                        AppendLine(LanguageService.FindResourceString("ParsingTheBackUp"));
                                        var bParser = BackupParser.Create(abPath, 
                                            $"{ServiceProvider.Current?.GetInstance<ICaseService>().CurrentCase.Path}/{Device.Serial}/{dtString}/backup",
                                            err => {
                                                EventLogger.Logger.WriteLine($"{nameof(AdbInfoesCheckedViewModel)}->{nameof(ConfirmCommand)}:{err.Code}-{err.Message}");
                                                ThreadInvoker.UIInvoke(() => {
                                                    AppendLine($"{LanguageService.FindResourceString("FailedToBackup")}:{err.Code} Message - {err.Message}");
                                                    MsgBoxService.Show($"{LanguageService.FindResourceString("FailedToBackup")}:{err.Code} Message - {err.Message}");
                                                });
                                            });

                                        bRes = bParser.ParseAdbBackup(
                                             notCorrect => {
                                                string pwd = null;
                                                 ThreadInvoker.UIInvoke(() => {
                                                     pwd = DialogService.Current.GetInputValue(LanguageService.FindResourceString("PleaseInputBackUpPass"),
                                                            notCorrect ? ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("AdbBPPwdNotCorrect") : string.Empty);
                                                });

                                                return new PasswdResult {
                                                    Password = pwd,
                                                    Conntinue = pwd != null
                                                };
                                            }, 
                                            (cur,all) => {
                                                //AppendLine(已解析);
                                            }
                                        );

                                    }
                                    if (bRes) {
                                        node.Succeed = true;
                                        node.Direct = $"{Device.Serial}/{dtString}/backup/";
                                        AppendLine(LanguageService.FindResourceString("SucceedToBackUp"));
                                        node.Process = 100;
                                    }
                                }
                                catch (Exception ex) {
                                    EventLogger.Logger.WriteLine($"{nameof(AdbInfoesCheckedViewModel)}->{nameof(ConfirmCommand)}->{nameof(BackupParser)}:{ex.Message}");
                                    AppendLine(LanguageService.FindResourceString("FailedToBackup"));
                                    ThreadInvoker.UIInvoke(() => {
                                        MsgBoxService.ShowError($"{LanguageService.FindResourceString("FailedToBackup")}:{ex.Message}");
                                    });
                                }
                                finally {
                                    handles[handleIndex].Set();
                                }
                            }
                            else if (p is AdbAllFileTreeUnit) {
                                var node = (p as AdbAllFileTreeUnit);
                                GetAllFileToInfoNode(p as AdbAllFileTreeUnit, handles[handleIndex]);
                            }
                            handleIndex++;
                        });

                        WaitHandle.WaitAll(handles);
                        var containers = new List<IInfoModelContainer>();

                        //分拣容器;
                        slUnits.ForEach(p => {
                            if (p is AdbInfoTreeUnit<IInfo>) {
                                var node = (p as AdbInfoTreeUnit<IInfo>);
                                if (node.Infoes?.Count != 0) {
                                    IInfoModelContainer iContainer = GetInfoContainer<IInfo>(node.Infoes, node.MInfoType);
                                    if (iContainer != null) {
                                        containers.Add(iContainer);
                                    }
                                    else {
                                        EventLogger.Logger.WriteLine($"{nameof(AdbInfoesCheckedViewModel)}->{ConfirmCommand}:iContainer can't be null.");
                                    }
                                }
                            }
                            else if(p is AdbBackUpFilesTreeUnit) {
                                var node = p as AdbBackUpFilesTreeUnit;
                                if (node.Succeed) {
                                    var container = new BackUpFilesContainer($"{ServiceProvider.Current?.GetInstance<ICaseService>().CurrentCase.Path}/", node.Direct);
                                    containers.Add(container);
                                }
                            }
                            else if (p is AdbAllFileTreeUnit) {
                                var node = p as AdbAllFileTreeUnit;
                                var container = new AllFilesContainer(node.Files);
                                containers.Add(container);
                            }
                           
                            //else if(p is adb)
                        });

                        ThreadInvoker.UIInvoke(() => {
                            var msg = DialogService.Current.CreateLoadingDialog();
                            msg.WindowTitle = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("AdbFileDownloading");
                            //下载文件;
                            msg.DoWork += (sender, e) => {
                                //统计总大小;
                                var totalSize = containers.Sum(p => {
                                    if (p is IAdbMultiInfoContainer<IInfo,InfoModel> infoContainer && 
                                    MInfoTypeHelper.GetMInfoTypeBox(infoContainer.InfoType) == MInfoTypeBox.AdbFile) {
                                        return infoContainer.Infoes.Sum(q => (q as UrlInfo)?.Size??0);
                                    }
                                    return 0;
                                });
                                //统计总数量;
                                var totalCount = containers.Sum(p => {
                                    if (p is IAdbMultiInfoContainer<IInfo,InfoModel> infoContainer
                                    && infoContainer.InfoType.GetMInfoTypeBox() == MInfoTypeBox.AdbFile) { 
                                         return infoContainer.InfoModels?.Count()??0;
                                    }
                                    return 0;
                                });

                                //已下载大小;
                                long downLoadedSize = 0;
                                //已下载数目;
                                int downloadedCount = 1;

                                //是否已经取消;
                                bool broken = false;
                                containers.ForEach(con => {
                                    if (broken) {
                                        return;
                                    }
                                    
                                    if (con is IAdbMultiInfoContainer<IInfo,InfoModel> mulCon && 
                                    MInfoTypeHelper.GetMInfoTypeBox(con.InfoType) == MInfoTypeBox.AdbFile) {
                                        try {
                                            if (Directory.Exists(ServiceProvider.Current?.GetInstance<ICaseService>().CurrentCase?.Path)) {
                                                foreach(var f in mulCon.Infoes){
                                                    if (broken) {
                                                        return;
                                                    }
                                                    if (msg.CancellationPending) {
                                                        broken = true;
                                                        return;
                                                    }

                                                    var urlInfo = f as UrlInfo;
                                                    DownloadFileFromUrl(urlInfo.Url, $"{ServiceProvider.Current?.GetInstance<ICaseService>().CurrentCase.Path}/AdbDevices/{Device.Serial}",
                                                        per => {
                                                            if (totalSize == 0) {
                                                                EventLogger.Logger.WriteLine($"{nameof(AdbInfoesCheckedViewModel)}->{nameof(ConfirmCommand)}:totalSize {nameof(DownloadFileFromUrl)} is 0");
                                                                return;
                                                            }
                                                            var totalPer = (int)((downLoadedSize + urlInfo.Size * per / 100) * 100 / totalSize);
                                                            try {
                                                                msg.ReportProgress(totalPer >= 100 ? 100 : totalPer,
                                                                string.Format(LanguageService.FindResourceString("AdbNumDownloading"), downloadedCount, totalCount),
                                                                $"{LanguageService.FindResourceString("AdbFileBeingDownloaded")}:{GetFileNameFromUrl(urlInfo.Url)}");
                                                            }
                                                            catch (Exception ex) {
                                                                LoggerService.WriteCallerLine(ex.Message);
                                                            }
                                                        }
                                                    );

                                                    downloadedCount++;
                                                    downLoadedSize += urlInfo.Size;
                                                }
                                            }
                                            else {
                                                LoggerService.WriteCallerLine($"{ ServiceProvider.Current?.GetInstance<ICaseService>().CurrentCase?.Path}doesn't exist.");
                                            }
                                            
                                        }
                                        catch (Exception ex) {
                                            EventLogger.Logger.WriteLine($"{nameof(AdbInfoesCheckedViewModel)}->{nameof(ConfirmCommand)} DownLoading Error:{ex.Message}");
                                            ThreadInvoker.UIInvoke(() => {
                                                MsgBoxService.ShowError($"{LanguageService.FindResourceString("ErrorExportingAdbFile")}:{ex.Message}");
                                            });
                                        }

                                    }

                                    
                                });
                            };
                            msg.RunWorkerCompleted += (sender, e) => {
                                SetAuiqring(false);
                                ThreadInvoker.UIInvoke(() => {
                                    if (!disposed && 
                                    MsgBoxService.Current.Show(LanguageService.FindResourceString("WhetherToShowInfoAquired"),
                                        MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                                        AquireDone?.Invoke(this, containers);
                                    }
                                });
                            };
                            msg.ShowDialog();
                        });

                    });
                },
                () => !IsAquiring
            ));

        /// <summary>
        /// 从url下载文件到本地;
        /// </summary>
        /// <param name="url">源url</param>
        /// <param name="path">本地路径</param>
        /// <param name="tellProAct">通知进度的委托</param>
        public void DownloadFileFromUrl(string url,string path,Action<int> tellPerAct = null) {
            var dPath = GetPathFromUrl(path);
            if(string.IsNullOrEmpty(dPath)) {
                EventLogger.Logger.WriteLine($"{nameof(AdbInfoesCheckedViewModel)}->{nameof(DownloadFileFromUrl)}:Can't create path:{url}");
                return;
            }

            if (!Directory.Exists(dPath)) {
                try {
                    Directory.CreateDirectory(dPath);
                }
                catch(Exception ex) {
                    LoggerService.Current?.WriteCallerLine(ex.Message);
                    return;
                }
            }
            
            var res = Device.GetFile(url, path, 
                (cur,all) => {
                    tellPerAct?.Invoke((int)( all != 0?cur * 100 / all : 0));
                },
                err => {
                    LoggerService.Current?.WriteCallerLine($"{err.Code}-{err.Message}");
                }
            );

            if(!res) {
                ThreadInvoker.UIInvoke(() => {

                });
                //Logger.WriteLine($"{nameof(Adb)}")
            }
        }
        
        /// <summary>
        /// 获取所有文件到节点;
        /// </summary>
        /// <typeparam name="TInfo"></typeparam>
        /// <param name="node"></param>
        /// <param name="evt"></param>
        private void GetInfoToNode<TInfo>(AdbInfoTreeUnit<IInfo> node,AutoResetEvent evt) where TInfo : IInfo {
            node.Infoes.Clear();
            Device.GetInfo<TInfo>((infos, cur, total) => {
                if (total != 0) {
                    try {
                        AppendLine(string.Format(LanguageService.FindResourceString("AdbNumAquired"), cur, total));
                    }
                    catch (Exception ex) {
                        EventLogger.Logger.WriteLine($"{nameof(AdbInfoesCheckedViewModel)}->{nameof(GetInfoToNode)}:{ex.Message}" +
                            $"format :{LanguageService.FindResourceString("node.Process = cur * 100 / total;")}");
                    }
                    node.Process = cur * 100 / total;
                }
                else {
                    AppendLine(LanguageService.FindResourceString("AdbNoData"));
                }

                if(infos != null) {
                    node.Infoes.AddRange(infos.Select(p => p as IInfo));
                }
                if (total == cur || total == 0) {
                    node.Process = 100;
                    evt.Set();
                }
            },
            res => {
                var errCode = res.Code;
                var err = res.Message;
                EventLogger.Logger.WriteLine($"{nameof(AdbInfoesCheckedViewModel)}->{nameof(GetInfoToNode)} Socket OutPut:" +
                    $"{Environment.NewLine}OutType:{errCode},{err}");

                ThreadInvoker.UIInvoke(() => {
                    MsgBoxService.ShowError(LanguageService.FindResourceString("ConfirmToConnectYourPhone"));
                });
            });
            
        }

        /// <summary>
        /// 获取所有文件到文件节点中;
        /// </summary>
        /// <param name="node"></param>
        /// <param name="evt"></param>
        private void GetAllFileToInfoNode(AdbAllFileTreeUnit node,AutoResetEvent evt) {
            ThreadPool.QueueUserWorkItem(cb => {
                int count = 0;
                try {
                    node.Files = Device.GetFileList(null, 
                        (cur,all) => {
                            count++;
                            node.Process = all != 0?cur * 100 / all : 0;
                        },
                        err => {
                            EventLogger.Logger.WriteLine(
                                $"{nameof(AdbInfoesCheckedViewModel)}->{nameof(GetAllFileToInfoNode)}:Code {err.Code} Message {err.Message}");
                            ThreadInvoker.UIInvoke(() => {
                                MsgBoxService.ShowError($"{LanguageService.FindResourceString("FailedToGetAllFiles")}:Code {err.Code} Message {err.Message}");
                            });
                        }
                    );
                }
                catch(Exception ex) {
                    EventLogger.Logger.WriteLine($"{nameof(AdbInfoesCheckedViewModel)}->{nameof(GetAllFileToInfoNode)} Exception:{ex.Message}");
                    ThreadInvoker.UIInvoke(() => {
                        MsgBoxService.ShowError($"{LanguageService.FindResourceString("FailedToGetFileList")}:{ex.Message}");
                    });
                }
                finally {
                    evt.Set();
                }
            });
        }

        /// <summary>
        /// 根据信息类型创建容器;
        /// </summary>
        /// <param name="infoes"></param>
        /// <param name="ifType"></param>
        /// <returns></returns>
        private IInfoModelContainer GetInfoContainer<TInfo>(List<TInfo> infoes, MInfoType ifType) where TInfo:IInfo {
            IInfoModelContainer iContainer = null;
            if (ifType == MInfoType.Basic) {
                iContainer = new AdbSingleInfoContainer<Basic,AdbInfoBasicModel>(infoes.First() as Basic,MInfoType.Basic);
            }
            else if (MInfoTypeHelper.GetMInfoTypeBox(ifType) == MInfoTypeBox.AdbInfo ||
                MInfoTypeHelper.GetMInfoTypeBox(ifType) == MInfoTypeBox.AdbFile) {
                switch (ifType) {
                    case MInfoType.Audio:
                        iContainer = new AdbMultiInfoContainer<TInfo, AdbFileAudioModel>(ifType, infoes);
                        break;
                    case MInfoType.Video:
                        iContainer = new AdbMultiInfoContainer<TInfo, AdbFileVideoModel>(ifType, infoes);
                        break;
                    case MInfoType.Image:
                        iContainer = new AdbMultiInfoContainer<TInfo, AdbFileImageModel>(ifType, infoes);
                        break;

                    case MInfoType.Sms:
                        iContainer = new AdbMultiInfoContainer<TInfo, AdbSmsModel>(ifType, infoes);
                        break;
                    case MInfoType.GPS:
                        iContainer = new AdbMultiInfoContainer<TInfo, AdbGpsModel>(ifType, infoes);
                        break;
                    case MInfoType.Contact:
                        iContainer = new AdbMultiInfoContainer<TInfo, AdbContactModel>(ifType, infoes);
                        break;
                    case MInfoType.Package:
                        iContainer = new AdbMultiInfoContainer<TInfo, AdbPackageModel>(ifType, infoes);
                        break;
                    case MInfoType.Calllog:
                        iContainer = new AdbMultiInfoContainer<TInfo, AdbCalllogModel>(ifType, infoes);
                        break;
                }
                
            }
            return iContainer;
        }

        //是否正在获取;
        private bool _isAquiring;
        public bool IsAquiring {
            get {
                return _isAquiring;
            }
            set {
                SetProperty(ref _isAquiring, value);
            }
        }

        private void SetAuiqring(bool aquiring) {
            IsAquiring = aquiring;
            ConfirmCommand.RaiseCanExecuteChanged();
        }

        private string _dashWord;
        public string DashWord {
            set => SetProperty(ref _dashWord, value);
            get => _dashWord;
        }

        private void AppendLine(string line) => DashWord += $"{line}{Environment.NewLine}";
    }
    
    
}
