﻿using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using CDFCMessageBoxes.MessageBoxes;
using CDFCUIContracts.Abstracts;
using EventLogger;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Singularity.UI.MessageBoxes.MessageBoxes;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using static CDFCCultures.Managers.ManagerLocator;
using static Singularity.UI.Controls.ViewModels.HexStreamEditorViewModel;
using static CDFCUIContracts.Helpers.ApplicationHelper;
using Singularity.Contracts.Helpers;
using Singularity.Contracts.MainPage.Events;
using Singularity.Contracts.FileExplorer;
using Singularity.Contracts.Common;
using Singularity.Contracts.TabControl;
using Singularity.Contracts.MainPage;
using Singularity.Contracts.Contracts.MainMenu;
using Singularity.Contracts.MainMenu;
using Singularity.Contracts.FileSystem;
using Singularity.Contracts.Shell;
using Singularity.Contracts.FileExplorer.Events;
using Singularity.Contracts.Case;
using Singularity.UI.Hex.Models;
using Singularity.Contracts.Hex;

namespace SingularityForensic.Hex {
    public static partial class MenuItemDefinitions {
        static MenuItemDefinitions() {
            PubEventHelper.Subscribe<InnerTabSelectedChangedEvent, ITabModel>(tab => {
                CurHexViewModel = tab as IHexDataContext;
                RaiseCanExcute();
            });

            PubEventHelper.Subscribe<SelectedTabChangedEvent, TabModel>(tab => {
                if(tab is IHaveTabModels haveTbs) {
                    
                }
            });
            PubEventHelper.Subscribe<SelectedTabChangedEvent, TabModel>(tab => {
                SearchKeyConfirmCommand.RaiseCanExecuteChanged();
            });
        }

        private static void RaiseCanExcute() {
            GoToOffsetCommand.RaiseCanExecuteChanged();
            FindTextCommand.RaiseCanExecuteChanged();
            FindHexValueCommand.RaiseCanExecuteChanged();
        }

        public static IHexDataContext CurHexViewModel { get; private set; }
    }

    public static partial class MenuItemDefinitions {
        private static DelegateCommand gotoOffsetCommand;                             //跳转到偏移量命令;
        public static DelegateCommand GoToOffsetCommand {
            get {
                return gotoOffsetCommand ??
                    (gotoOffsetCommand = new DelegateCommand(() => {
                        var msg = new GoToOffsetMessageBox();
                        var setting = msg.Show();
                        long newPosition = 0;
                        long maxLength = CurHexViewModel.Stream.Length;
                        long curPosition = CurHexViewModel.FocusPosition;
                        if (setting != null) {
                            switch (setting.EscapteMethod) {
                                case EscapteMethod.FromStart:
                                    newPosition = setting.Offset;
                                    break;
                                case EscapteMethod.BackFrom:
                                    newPosition = maxLength - setting.Offset - 1;
                                    break;
                                case EscapteMethod.Current:
                                    newPosition = curPosition + setting.Offset;
                                    break;
                                case EscapteMethod.CurrentBackFrom:
                                    newPosition = curPosition - setting.Offset;
                                    break;
                            }
                            if (newPosition >= 0 && newPosition < maxLength) {
                                CurHexViewModel.Position = newPosition;
                                CurHexViewModel.FocusPosition = newPosition;
                            }
                            else {
                                CDFCMessageBox.Show($"The current stream doesn't contains the position:{newPosition}");
                            }
                        }
                    },
                    () => CurHexViewModel != null));
            }
        }

        [Export]
        public static MenuButtonItemModel GoToOffsetMenuItem
            => _goToOffsetMenuItem ??(_goToOffsetMenuItem = new MenuButtonItemModel(
            MenuConstants.MenuMainGroup,
            FindResourceString("GoToOffset")) {
                Command = GoToOffsetCommand,
                IconSource = IconSources.GotoOffsetIcon,
                Modifier = ModifierKeys.Alt,
                Key = Key.G
            });

        private static MenuButtonItemModel _goToOffsetMenuItem;
        

        private static FindMethod lastFindMethod;
        
        [Export]
        public static MenuButtonItemModel FindHexMenuItem 
            => _findHexMenuItem ?? (_findHexMenuItem = new MenuButtonItemModel(MenuConstants.MenuMainGroup, 
                FindResourceString("SearchForHex")) {
                        Command = FindHexValueCommand,
                        IconSource = IconSources.FindHexIcon,
                        Modifier = ModifierKeys.Alt | ModifierKeys.Control,
                        Key = Key.X
                    });

        private static MenuButtonItemModel _findHexMenuItem;

        private static FindHexValueSetting findHexSetting;
        private static DelegateCommand findHexValueCommand;                           //检索十六进制的命令;
        public static DelegateCommand FindHexValueCommand {
            get {
                return findHexValueCommand ??
                    (findHexValueCommand = new DelegateCommand(() => {
                        var msg = new FindHexValueMessageBox();
                        var setting = msg.Show(ref findHexSetting);
                        if (setting != null) {
                            FindHex(setting);
                        }

                    }, () => CurHexViewModel != null));
            }
        }
        private static void FindHex(FindHexValueSetting setting) {
            if (setting != null) {
                if (!setting.IsBlockSearch) {
                    ServiceProvider.Current.GetInstance<IHexServiceProvider>()?.FindNextBytes(CurHexViewModel,setting.HexBytes);
                }
                else if (setting.BlockSize != null && setting.BlockOffset != null) {
                    ServiceProvider.Current.GetInstance<IHexServiceProvider>()?.FindNextBytes(CurHexViewModel,setting.HexBytes,
                        FindMethod.Hex, true,
                        setting.BlockSize.Value, setting.BlockOffset.Value);
                }
                lastFindMethod = FindMethod.Hex;
            }
        }


        [Export]
        public static MenuButtonItemModel FindTextMenuItem
              => _findTextMenuItem ?? (_findTextMenuItem = new MenuButtonItemModel(MenuConstants.MenuMainGroup,
                        FindResourceString("SearchForText")) {
                        Command = FindTextCommand,
                        IconSource = IconSources.FindTextIcon,
                        Modifier = ModifierKeys.Control,
                        Key = Key.F
                    });

        private static MenuButtonItemModel _findTextMenuItem;
        private static FindTextStringSetting findStringSetting;
        private static DelegateCommand findTextCommand;                               //检索文本的命令;
        public static DelegateCommand FindTextCommand {                               //查找字符串命令;
            get {
                return findTextCommand ??
                    (findTextCommand = new DelegateCommand(() => {
                        var msg = new FindTextMessageBox();
                        var setting = msg.Show(ref findStringSetting);
                        if (setting != null) {
                            FindText(setting);
                        }
                        lastFindMethod = FindMethod.Text;
                    }, () => CurHexViewModel != null));
            }
        }

        private static void FindText(FindTextStringSetting setting) {
            if (setting != null) {
                if (!setting.IsBlockSearch) {
                    ServiceProvider.Current.GetInstance<IHexServiceProvider>()?.
                        FindNextString(CurHexViewModel,setting.FindText);
                }
                else if (setting.BlockSize != null && setting.BlockOffset != null) {
                    ServiceProvider.Current.GetInstance<IHexServiceProvider>()?.
                        FindNextString(CurHexViewModel,setting.FindText, true,
                        setting.BlockSize.Value, setting.BlockOffset.Value);
                }
            }
        }

        


        /// <summary>
        /// 验证输入;
        /// </summary>
        /// <param name="searchingKey"></param>
        /// <returns></returns>
        private static SearchValidateRes ValidateInput(string searchingKey) {
            if (string.IsNullOrWhiteSpace(searchingKey)) {
                Application.Current.Dispatcher.Invoke(() => {
                    CDFCMessageBox.Show(FindResourceString("SearchKeyCannotBeNullOrEmpty"));
                });
                return SearchValidateRes.NullRes;
            }

            if (!Regex.IsMatch(searchingKey, "[^? !@#$%\\^&*()]+")) {
                Application.Current.Dispatcher.Invoke(() => {
                    CDFCMessageBox.Show(FindResourceString("SearchKeyIllegal"));
                });
                return SearchValidateRes.IlegalChar;
            }

            return SearchValidateRes.OK;
        }

        ////搜寻关键字;
        //private string _searchingKey;
        //public string SearchingKey {
        //    get {
        //        return _searchingKey;
        //    }
        //    set {
        //        SetProperty(ref _searchingKey, value);
        //    }
        //}

        //搜寻选项;
        public static ObservableCollection<SearchKeyOption> SearchOptions { get; set; } =
            new ObservableCollection<SearchKeyOption>(
                Enum.GetValues(typeof(SearchMethod)).
                Cast<SearchMethod>().Select(p => new SearchKeyOption { Method = p }));

        public enum SearchValidateRes {
            NullRes,
            IlegalChar,
            OK
        }
    
        private static SearchKeyOption _slSearchKeyOption;
        public static SearchKeyOption SlSearchKeyOption {
            get {
                if (_slSearchKeyOption == null) {
                    _slSearchKeyOption = SearchOptions[1];
                }
                return _slSearchKeyOption;
            }
            set {
                //SetProperty(ref _slSearchKeyOption, value);
                //OnPropertyChanged(nameof(SearchMethod));
            }
        }

        [Export]
        public static MenuButtonItemModel SearchKeyMenuItem {
            get {
                if (_searchKeyMenuItem == null) {
                    _searchKeyMenuItem = new MenuButtonItemModel(MenuConstants.MenuMainGroup, FindResourceString("IndexSearch")) {
                        Command = SearchKeyConfirmCommand,
                        IconSource = IconSources.FindTextIcon
                    };

                }
                return _searchKeyMenuItem;
            }
        }
        private static MenuButtonItemModel _searchKeyMenuItem;

        //搜索命令;
        private static DelegateCommand _searchKeyConfirmCommand;
        public static DelegateCommand SearchKeyConfirmCommand =>
            _searchKeyConfirmCommand ?? (_searchKeyConfirmCommand = new DelegateCommand(() => {
                var fsTabService = ServiceProvider.Current.GetInstance<IDocumentTabService>();
                if (fsTabService == null) {
                    Logger.WriteCallerLine("FsTabService is null!");
                    return;
                }

                var fileBrowserViewModel = (fsTabService.SelectedTab as ExtTabModel<IFileBrowserDataContext>).Data;

                //确认搜索设备;
                var blDevice = fileBrowserViewModel.File as Device;

                var device = blDevice ?? fileBrowserViewModel.File.GetParent<Device>() as Device;
                var indexableFile = ServiceProvider.Current.GetInstance<ICaseService>().CurrentCase.CaseEvidences.
                    FirstOrDefault(p =>
                    p is IHaveData<IFile> fcsFile
                    && fcsFile.Data == device) as IIndexable;

                if (SlSearchKeyOption.Method == SearchMethod.Content) {
                    if (indexableFile == null) {
                        Logger.WriteCallerLine($"{nameof(indexableFile)} can't be null");
                        RemainingMessageBox.Tell(FindResourceString("NullCaseFileUnknownError"));
                        return;
                    }
                    //开始搜索索引的委托;
                    Action searchAct = () => {
                        ServiceProvider.Current.GetInstance<IShellService>()?.ChangeLoadState(true);
                        ThreadPool.QueueUserWorkItem(cb => {
                            try {
                                var itrFile = fileBrowserViewModel.CurFile as IIterableFile;
                                var searchingKey = string.Empty;
                                Application.Current.Dispatcher.Invoke(() => {
                                    searchingKey = InputValueMessageBox.Show(FindResourceString("PleaseInputContent"));
                                });
                                if (searchingKey == null) {
                                    return;
                                }
                                if (ValidateInput(searchingKey) == SearchValidateRes.OK && itrFile != null) {
                                    IFile fileNode = itrFile;
                                    var sb = new StringBuilder();
                                    while (fileNode != null) {
                                        if (fileNode is Partition) {
                                            sb.Insert(0, $"{device.IndexOf(fileNode)}/");
                                        }
                                        else if (!(fileNode is Device)) {
                                            sb.Insert(0, $"{fileNode.Name}/");
                                        }
                                        fileNode = fileNode.Parent;
                                    }


                                    var files = indexableFile.IndexSearchKey(searchingKey.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries), sb.ToString());
                                    if (files != null) {
                                        fileBrowserViewModel.FillFiles(files);
                                    }
                                    else {
                                        AppInvoke(() => {
                                            RemainingMessageBox.Tell(FindResourceString("SearchResNull"));
                                        });
                                    }
                                }
                            }
                            catch (Exception ex) {
                                Logger.WriteCallerLine($"{nameof(SearchKeyConfirmCommand)}:{ex.Message}");
                            }
                            finally {
                                ServiceProvider.Current.GetInstance<IShellService>()?.ChangeLoadState(false);
                            }
                        });
                    };

                    //若未建立索引,询问是否建立索引。
                    if (!indexableFile.HasIndexes) {
                        //若确定,则开始建立索引;
                        if (CDFCMessageBox.Show(FindResourceString("IndexesNeeded"), FindResourceString("IndexesNotBeenBuilt"), MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                            ServiceProvider.Current.GetInstance<IShellService>()?.ChangeLoadState(true, null);
                            var msg = new ProgressMessageBox();
                            bool broke = false;
                            var proc = 0;

                            msg.DoWork += (sender, e) => {
                                msg.ReportProgress(0, FindResourceString("IndexesBeingBuilt"), $"{proc}%");
                                try {
                                    var succeed = indexableFile.BuildIndexFiles((period, pro, total) => {
                                        if (period == BuildPeriod.BuildDoc) {
                                            msg.ReportProgress(0, FindResourceString("IndexesInitializing"), string.Empty);
                                        }
                                        else if (total != null) {
                                            var newProc = (int)(pro * 100 / total);
                                            if (newProc > proc) {
                                                proc = newProc;
                                                msg.ReportProgress(proc > 100 ? 100 : proc, FindResourceString("IndexesBeingBuilt"), $"{proc}%");
                                            }
                                        }
                                    },
                                    () => {
                                        if (msg.CancellationPending) {
                                            broke = true;
                                        }
                                        return broke;
                                    });
                                    //若成功创建索引;开始搜索;
                                    if (succeed && indexableFile.HasIndexes) {
                                        ServiceProvider.Current.GetInstance<ICaseService>()?.CurrentCase.Save();
                                        //分隔空格;
                                        searchAct();
                                    }
                                }
                                catch (Exception ex) {
                                    Logger.WriteLine($"{nameof(SearchKeyConfirmCommand)}:{ex.Message}");
                                    AppInvoke(() => {
                                        CDFCMessageBox.Show($"{FindResourceString("IndexBuildingFailed")}:{ex.Message}");
                                    });
                                }
                            };

                            msg.RunWorkerCompleted += (sender, e) => {
                                ServiceProvider.Current.GetInstance<IShellService>()?.ChangeLoadState(false, null);
                            };
                            msg.ShowDialog();
                        }
                    }
                    else {
                        searchAct();
                    }

                }
                else {
                    //ThreadPool.QueueUserWorkItem(cb => {
                    //    try {
                    //        _fileBrowserTabModel.SearchNameInCurFile(searchingKey);
                    //    }
                    //    finally {
                    //        Aggregator?.GetEvent<IsLoadingChangedEvent>()?.Publish(false);
                    //    }
                    //});
                }
            },
                () => (ServiceProvider.Current.GetInstance<IDocumentTabService>()?.SelectedTab is IHaveData<IFileBrowserDataContext> fbTabModel)
            && (fbTabModel?.Data?.OwnerFile is Partition)));
    }
}