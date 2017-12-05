using CDFCMessageBoxes.MessageBoxes;
using EventLogger;
using SingularityForensic.Helpers;
using SingularityUpdater.Helpers;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using static CDFCCultures.Managers.ManagerLocator;
using System.ComponentModel.Composition;
using Prism.Commands;
using Prism.Regions;
using Prism.Modularity;
using Prism.Mvvm;
using Singularity.Contracts.Shell.Events;
using Singularity.Contracts.Helpers;

namespace SingularityForensic.ViewModels.Shell {
    //主模型;
    [Export]
    public partial class ShellViewModel:BindableBase {
        public ShellViewModel() {
            
        }
        
        [Import]
        protected IRegionManager regionManager;
        
        [Import]
        protected IModuleManager moduleManager;
        
        private string BrandName {
            get {
                return FindResourceString("SoftWareName") + ConfigState.VersionNum;
            }
        }
        
        private string _title;
        public string Title {
            get => _title ?? (_title = BrandName);
            set {
                SetProperty(ref _title, value);
            }
        }

        public void SetTitle(string word,bool saveBrandName = true) {
            if (saveBrandName && word != null) {
                Title = $"{word} - {BrandName}";
            }
            else if(word == null) {
                Title = BrandName;
            }
            else {
                Title = word;
            }
            
        }
        
    }
    
    //查找，搜索相关;
    public partial class ShellViewModel {
        //搜寻关键字;
        private string _searchingKey;
        public string SearchingKey {
            get {
                return _searchingKey;
            }
            set {
                SetProperty(ref _searchingKey, value);
            }
        }

        ////搜寻选项;
        //public ObservableCollection<SearchKeyOption> SearchOptions { get; set; } = 
        //    new ObservableCollection<SearchKeyOption> (
        //        Enum.GetValues(typeof(SearchMethod)).
        //        Cast<SearchMethod>().Select(p => new SearchKeyOption { Method = p }));
        
        //private SearchKeyOption _slSearchKeyOption;
        //public SearchKeyOption SlSearchKeyOption {
        //    get {
        //        if(_slSearchKeyOption == null) {
        //            SlSearchKeyOption = SearchOptions[1];
        //        }
        //        return _slSearchKeyOption;
        //    }
        //    set {
        //        SetProperty(ref _slSearchKeyOption, value);
        //        RaisePropertyChanged(nameof(SearchMethod));
        //    }
        //}

        //public SearchMethod SearchMethod => SlSearchKeyOption.Method;
        
        private string _loadingWord;
        public string LoadingWord {
            get {
                return _loadingWord;
            }
            set {
                SetProperty(ref _loadingWord, value);
            }
        }
    }
    //帮助菜单栏;
    public partial class ShellViewModel {
        private DelegateCommand _uptOnlineCommand;
        public DelegateCommand UptOnlineCommand => _uptOnlineCommand ??
            (_uptOnlineCommand = new DelegateCommand(
                () => {
                    ThreadPool.QueueUserWorkItem(cb => {
                        Action<Action> sta = Application.Current.Dispatcher.Invoke;

                        try {
                            if (VersionHelper.LatestVersion != ConfigState.VersionNum) {
                                sta(() => {
                                    if (CDFCMessageBox.Show(
                                    $"{FindResourceString("NewVersionFound")}{FindResourceString("Comma")}{VersionHelper.LatestVersion}{FindResourceString("Colon")}{FindResourceString("ConfirmToUpdate")}",
                                    MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                                        Update();
                                    }
                                });
                            }
                            else {
                                sta(() => {
                                    try {
                                        CDFCMessageBox.Show(string.Format($"{FindResourceString("CurVersionUpToDateFormat")}", VersionHelper.LatestVersion));
                                    }
                                    catch(Exception ex) {
                                        Logger.WriteLine($"{nameof(ShellViewModel)}->{nameof(UptOnlineCommand)}:");
                                        CDFCMessageBox.Show($"{FindResourceString("UnknownError")}:{ex.Message}");
                                    }
                                });
                            }
                        }
                        catch(Exception ex) {
                            Logger.WriteLine($"{nameof(ShellViewModel)}->{nameof(UptOnlineCommand)}:{ex.Message}");
                            sta(() => {
                                CDFCMessageBox.Show($"{FindResourceString("FailedToCheckUpdate")}:{ex.Message}");
                            });
                        }
                    });                    
                })
            );
        
        private void Update() {
            Process.Start("SingularityUpdater.exe");
            Environment.Exit(0);
        }

        private DelegateCommand _loadedCommand;
        public DelegateCommand LoadedCommand => _loadedCommand ??
            (_loadedCommand =
            new DelegateCommand(() => {
                
                return;
                ThreadPool.QueueUserWorkItem(cb => {
                    Action<Action> sta = Application.Current.Dispatcher.Invoke;

                    try {
                        if (VersionHelper.LatestVersion != ConfigState.VersionNum) {
                            sta(() => {
                                if (CDFCMessageBox.Show(
                                $"{FindResourceString("NewVersionFound")}{FindResourceString("Comma")}{VersionHelper.LatestVersion}{FindResourceString("Colon")}{FindResourceString("ConfirmToUpdate")}",
                                MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                                    Update();
                                }
                            });
                        }
                        else {
                            try {
                                ConfigState.NotifyUpdateLog();
                            }
                            catch(Exception ex) {
                                //CDFCMessageBox.Show()
                            }
                        }
                    }
                    catch (Exception ex) {
                        Logger.WriteLine($"{nameof(ShellViewModel)}->{nameof(UptOnlineCommand)}:{ex.Message}");
                        sta(() => {
                            CDFCMessageBox.Show($"{FindResourceString("FailedToCheckUpdate")}:{ex.Message}");
                        });
                    }
                });
            }));

        private DelegateCommand _contentRenderedCommand;
        public DelegateCommand ContentRenderedCommand => _contentRenderedCommand ??
            (_contentRenderedCommand = new DelegateCommand(
                () => {
                    PubEventHelper.Publish<ShellRenderedEvent>();
                }
            ));
    }

    public partial class ShellViewModel {
        private bool isLoading;
        public bool IsLoading {
            get {
                return isLoading;
            }
            set {
                SetProperty(ref isLoading, value);
            }
        }

        private bool _isToolBarShown = true;
        public bool IsToolBarCollapsed {
            get {
                return _isToolBarShown;
            }
            set {
                SetProperty(ref _isToolBarShown, value);
            }
        }
    }
}
