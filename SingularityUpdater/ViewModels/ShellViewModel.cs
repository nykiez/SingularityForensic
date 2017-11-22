using SingularityUpdater.Abstracts;
using SingularityUpdater.Commands;
using SingularityUpdater.Converters;
using SingularityUpdater.Helpers;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using static SingularityUpdater.Helpers.CultureHelper;

namespace SingularityUpdater.ViewModels {
    public partial class ShellViewModel:BindableBase {
        private const string BrandSource = "WelcomeImage.png";
        private MemoryStream brandImage;
        public MemoryStream BrandImage {
            get {
                if(brandImage == null) {
                    if (File.Exists(BrandSource)) {
                        brandImage = new MemoryStream(File.ReadAllBytes(BrandSource));
                    }
                }
                return brandImage;
            }
        }

        private double curItemPtg;
        public double CurItemPtg {
            get {
                return curItemPtg;
            }
            set {
                SetProperty(ref curItemPtg, value);
            }
        }

        private long totalSize;
        private double totalItemPtg;
        public double TotalItemPtg {
            get {
                return totalItemPtg;
            }
            set {
                SetProperty(ref totalItemPtg, value);
            }
        }
    }
    public partial class ShellViewModel {
        private RelayCommand loadedCommand;
        public RelayCommand LoadedCommand {
            get {
                return loadedCommand ??
                    (loadedCommand = new RelayCommand(() => {
                        try {
                            var items = VersionHelper.ItemsNeed;
                            if (items.Count == 0) {
                                UpdatingWord = CultureHelper.TryFindResourceString("NoNeedToUpdate");
                                IsWorking = false;
                            }
                            else {
                                totalSize = items.Sum(p => p.Size);
                                long copiedSize = 0;
                                int readSize = 0;
                                var buffer = new byte[10240];
                                var tempPath = "Temp/";

                                var errorBreaked = false;
                                var errorWord = string.Empty;
                                Action<string> fillErro = e => {
                                    errorBreaked = true;
                                    errorWord = e;
                                };

                                IsWorking = true;

                                ThreadPool.QueueUserWorkItem(callBack => {

                                    #region 下载文件至本地临时目录中;
                                    foreach (var item in items) {
                                        UpdatingWord = $"{TryFindResourceString("DownloadingFile")}:{item.Name}";
                                        var desPath = $"{tempPath}{item.LocalRlaPath}/";
                                        if (!Directory.Exists(desPath)) {
                                            Directory.CreateDirectory(desPath);
                                        }
                                        var fs = File.Create($"{desPath}{ item.Name}");
                                        try {
                                            var request = WebRequest.Create($"{item.RemotePreAbPath}{item.RemoteRlaPath}{item.Name}");
                                            var response = request.GetResponse();
                                            var oriFs = response.GetResponseStream();
                                            long itemCopiedSize = 0;

                                            while ((readSize = oriFs.Read(buffer, 0, buffer.Length)) != 0) {
                                                try {
                                                    fs.Write(buffer, 0, readSize);
                                                    copiedSize += readSize;
                                                    itemCopiedSize += readSize;
                                                    CurFileWord = $"{TryFindResourceString("CurFile")}:{item.Name}\t" +
                                                                    $"{ByteSizeToSizeConverter.StaticInstance.Convert(itemCopiedSize, null, null, null)}/" +
                                                                    $"{ByteSizeToSizeConverter.StaticInstance.Convert(item.Size, null, null, null)}";

                                                    TotalItemPtg = copiedSize * 100 / totalSize;
                                                    CurItemPtg = itemCopiedSize * 100 / item.Size;
                                                    UpdateLogger.WriteLine($"{item.Name} Downloaded");
                                                    if (canceled) {
                                                        break;
                                                    }
                                                }
                                                catch (Exception ex) {
                                                    UpdateLogger.WriteLine($"{nameof(ShellViewModel)}->{nameof(LoadedCommand)}({item.Name}){Environment.NewLine}uri:{item.Uri}{Environment.NewLine}:{ex.Message}");
                                                    if (MessageBox.Show(
                                                        string.Format(
                                                            TryFindResourceString("FailedToReadFileFormat"),
                                                            item.Name, ex.Message),
                                                        TryFindResourceString("ErrorWhenDownLoading"), MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                                                        fillErro(string.Format(TryFindResourceString("TerminatedWhenDownReadingTheFile"), item.Name));
                                                        break;
                                                    }
                                                }

                                            }

                                            fs.Close();
                                            oriFs.Close();
                                            response.Dispose();

                                        }
                                        catch (Exception ex) {
                                            UpdateLogger.WriteLine($"{nameof(ShellViewModel)}->{nameof(LoadedCommand)}({item.Name}):{ex.Message}");

                                            if (MessageBox.Show(
                                                string.Format(
                                                            TryFindResourceString("FailedToReadFileFormat"),
                                                            item.Name, ex.Message),
                                                            TryFindResourceString("Error"),
                                                            MessageBoxButton.YesNo) == MessageBoxResult.No) {
                                                fillErro(string.Format(TryFindResourceString("TerminatedWhenDownLoadingTheFile"), item.Name));
                                                break;
                                            }
                                        }

                                        if (canceled || errorBreaked) {
                                            break;
                                        }
                                    }
                                    #endregion

                                    UpdateLogger.WriteLine(canceled.ToString());

                                    if (!canceled) {
                                        UpdatingWord = TryFindResourceString("SetupingFile");
                                        long totalCopiedSize = 0;
                                        #region 校验文件并复制;
                                        foreach (var item in items) {
                                            try {
                                                CurFileWord = $"{UpdatingWord}:{item.Name}";
                                                var tempFile = File.OpenRead(tempPath + item.LocalRlaPath + item.Name);
                                                var hash = Helpers.MD5.ComputeHashByStream(tempFile);
                                                tempFile.Position = 0;

                                                if (hash == item.MD5) {
                                                    if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + item.LocalRlaPath)) {
                                                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + item.LocalRlaPath);
                                                    }
                                                    var desFile = File.Create(AppDomain.CurrentDomain.BaseDirectory + item.LocalRlaPath + item.Name);
                                                    long itemCopiedSize = 0;

                                                    while ((readSize = tempFile.Read(buffer, 0, buffer.Length)) != 0) {
                                                        desFile.Write(buffer, 0, readSize);
                                                        itemCopiedSize += readSize;
                                                        totalCopiedSize += readSize;

                                                        CurItemPtg = itemCopiedSize * 100 / tempFile.Length;
                                                        TotalItemPtg = totalCopiedSize * 100 / totalSize;

                                                        if (canceled) {
                                                            break;
                                                        }
                                                        Thread.Sleep(1);
                                                    }
                                                    desFile.Close();
                                                }
                                                else {
                                                    UpdateLogger.WriteLine($"{nameof(ShellViewModel)}->{nameof(LoadedCommand)}:Unmatched MD5\n" +
                                                        $"origin:{item.MD5}\ttemp{hash}");
                                                }
                                                tempFile.Close();

                                                if (canceled) {
                                                    break;
                                                }
                                            }
                                            catch {

                                            }
                                        }
                                        #endregion
                                    }

                                    if (canceled) {
                                        UpdatingWord = TryFindResourceString("UpdatingCanceled");
                                    }
                                    else if (errorBreaked) {
                                        UpdatingWord = errorWord;
                                    }
                                    else {
                                        TotalItemPtg = 100;
                                        UpdatingWord = TryFindResourceString("UpdatingFinished");
                                        Application.Current.Dispatcher.Invoke(() => {
                                            if (MessageBox.Show(TryFindResourceString("ConfirmToRestartProgram"),
                                                TryFindResourceString("UpdatingFinished"),
                                                MessageBoxButton.OKCancel) == MessageBoxResult.OK) {
                                                try {
                                                    if (VersionHelper.VersionBranch != "All") {
                                                        Process.Start($"SingularityForensic.exe");
                                                    }
                                                    else {
                                                        Process.Start($"SingularityForensic.exe");
                                                    }
                                                    Environment.Exit(0);
                                                }
                                                catch (Exception ex) {
                                                    MessageBox.Show(ex.Message);
                                                }
                                            }
                                        });
                                    }

                                    CurFileWord = string.Empty;
                                    IsWorking = false;
                                });

                            }
                        }
                        catch(WebException ex) {
                            MessageBox.Show($"{TryFindResourceString("FailedToConnectToServer")}{ex.Message}");
                        }
                        catch(Exception ex) {
                            MessageBox.Show($"{TryFindResourceString("UnknownError")}{ex.Message}");
                        }
                    }));
                }
        }   

        public event EventHandler CloseRequired;
        private RelayCommand cancleOrCloseCommand;
        public RelayCommand CancleOrCloseCommand {
            get {
                return cancleOrCloseCommand ??
                    (cancleOrCloseCommand = new RelayCommand(() => {
                        if (IsWorking) {
                            canceled = true;
                        }
                        else {
                            UpdateLogger.WriteLine("dada");
                            CloseRequired?.Invoke(this, new EventArgs());
                        }
                    }));
            }
        }

        private DelegateCommand<CancelEventArgs> closingCommand;
        public DelegateCommand<CancelEventArgs> ClosingCommand {
            get {
                return closingCommand ??
                    (closingCommand = new DelegateCommand<CancelEventArgs>(e => {
                        if (IsWorking) {
                            if (MessageBox.Show(TryFindResourceString("ConfirmToTerminateWhileUpdating"), 
                                TryFindResourceString("Tip")  ,MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                                canceled = true;
                            }
                            else {
                                e.Cancel = true;
                            }
                        }
                    }));
            }
        }

        //是否取消了任务;
        private bool canceled = false;
        //是否处于工作状态;
        private bool isWorking;
        public bool IsWorking {
            get {
                return isWorking;
            }
            set {
                SetProperty(ref isWorking, value);
            }
        }

        //升级提示语;
        private string updatingWord;
        public string UpdatingWord {
            get {
                return updatingWord;
            }
            set {
                SetProperty(ref updatingWord, value);
            }
        }

        private string curFileWord;
        public string CurFileWord {
            get {
                return curFileWord;
            }
            set {
                SetProperty(ref curFileWord, value);
            }
        }
    }

    
}
