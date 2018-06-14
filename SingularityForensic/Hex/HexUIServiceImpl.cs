using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Document.Events;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Hex.Controls;
using SingularityForensic.Hex.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text.RegularExpressions;

namespace SingularityForensic.Hex {
    [Export(typeof(IHexUIService)),Export]
    public partial class HexUIServiceImpl : ExtensibleBindableBase,IHexUIService {
        public void Initialize() {
            RegisterEvents();
        }

        private void RegisterEvents() {
            PubEventHelper.GetEvent<SelectedDocumentChangedEvent>().Subscribe(tuple => {
                CanHexOperatable = GetCurrentHexDataContext() != null;
            });
        }

        /// <summary>
        /// 查找匹配的字符串;
        /// </summary>
        /// <param name="findString"></param>
        public void FindNextString(IHexDataContext hex, string findString) {
            FindNextBytes(hex, ByteExtensions.StringToByte(findString), FindMethod.Text);
        }

        public void FindNextString(IHexDataContext hex, string findString, bool isBlockSearch, int blockSize, int blockOffset) =>
            FindNextBytes(hex, ByteExtensions.StringToByte(findString), FindMethod.Text, isBlockSearch, blockSize, blockOffset);

        public void FindNextBytes(IHexDataContext hex, byte[] findBytes) =>
            FindNextBytes(hex, findBytes, FindMethod.Hex, false, -1, -1);

        public void FindNextBytes(IHexDataContext hex, byte[] findBytes, FindMethod method) =>
            FindNextBytes(hex, findBytes, method, false, -1, -1);

        public void FindNextBytes(IHexDataContext hex, byte[] findBytes,
            FindMethod findMethod, bool isBlockSearch, int blockSize, int blockOffset) {
            if (findBytes == null || findBytes.Length == 0) {
                LoggerService.Current?.WriteCallerLine($"{nameof(findBytes)} can't be null or empty.");
                return;
            }

            if (isBlockSearch && (blockSize <= 0 || blockOffset < 0)) {
                throw new ArgumentException($"Invalid Argument(s):{nameof(blockSize)} or {nameof(blockOffset)}");
            }

            long pos = (hex.FocusPosition == -1 ? 0 : hex.FocusPosition) + 1;

            var dialog = DialogService.Current.CreateLoadingDialog();
            dialog.WindowTitle = findMethod == FindMethod.Text ?
                ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("SearchingForText") :
                ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("SearchingForHex");

            dialog.DoWork += (sender, e) => {
                if (isBlockSearch) {
                    pos = hex.Stream.SearchBlock(pos, blockSize, blockOffset, findBytes, index => {
                        dialog.ReportProgress((int)(index * 100 / hex.Stream.Length));
                    }, () => dialog.CancellationPending);
                }
                else {
                    pos = hex.Stream.Search(pos, findBytes, index => {
                        dialog.ReportProgress((int)(index * 100 / hex.Stream.Length));
                    }, () => dialog.CancellationPending);
                }
            };

            dialog.RunWorkerCompleted += (sender, e) => {
                if (!e.Cancelled) {
                    if (pos != -1) {
                        //SelectionStart = pos;
                        //SelectionStop = pos + findBytes.Length - 1;
                        hex.Position = pos;
                        hex.FocusPosition = pos;
                    }
                    else {
                        MsgBoxService.Show(LanguageService.FindResourceString("CannotFindTheContent"));
                    }
                }
            };

            dialog.ShowDialog();
        }

        
    }

    public partial class HexUIServiceImpl {
        /// <summary>
        /// 获得当前所关注的十六进制实例;
        /// </summary>
        /// <returns></returns>
        private IHexDataContext GetCurrentHexDataContext() {
            var mainDocService = DocumentService.MainDocumentService;
            if (mainDocService == null) {
                LoggerService.WriteCallerLine($"{nameof(mainDocService)} can't be null.");
                return null;
            }

            var slDoc = mainDocService.SelectedDocument;
            if (slDoc == null) {
                return null;
            }

            if (slDoc is IEnumerableDocument enumDoc && enumDoc.SelectedDocument != null) {
                return enumDoc.SelectedDocument.GetInstance<IHexDataContext>(Contracts.Hex.Constants.Tag_HexDataContext);
            }

            return null;
        }

        private DelegateCommand gotoOffsetCommand;                             //跳转到偏移量命令;
        public DelegateCommand GoToOffsetCommand {
            get {
                return gotoOffsetCommand ??
                    (gotoOffsetCommand = new DelegateCommand(() => {
                        var curHexContext = GetCurrentHexDataContext();
                        if (curHexContext == null) {
                            LoggerService.WriteCallerLine($"{nameof(curHexContext)} is null.");
                            //MsgBoxService.Show()
                            return;
                        }

                        var msg = new GoToOffsetMessageBox();
                        var setting = msg.Show();
                        long newPosition = 0;
                        long maxLength = curHexContext.Stream.Length;
                        long curPosition = curHexContext.FocusPosition;
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
                                curHexContext.Position = newPosition;
                                curHexContext.FocusPosition = newPosition;
                            }
                            else {
                                MsgBoxService.Show($"The current stream doesn't contains the position:{newPosition}");
                            }
                        }
                    },
                    () => CanHexOperatable).ObservesProperty(() => CanHexOperatable));
            }
        }

        private static FindMethod lastFindMethod;

        private FindHexValueSetting findHexSetting;
        private DelegateCommand findHexValueCommand;                           //检索十六进制的命令;
        public DelegateCommand FindHexValueCommand {
            get {
                return findHexValueCommand ??
                    (findHexValueCommand = new DelegateCommand(() => {
                        var msg = new FindHexValueMessageBox();
                        var setting = msg.Show(ref findHexSetting);
                        if (setting != null) {
                            FindHex(setting);
                        }

                    }, () => CanHexOperatable).ObservesProperty(() => CanHexOperatable));
            }
        }

        private void FindHex(FindHexValueSetting setting) {
            if (setting == null) {
                return;
            }
            var curHexContext = GetCurrentHexDataContext();
            if (curHexContext == null) {
                LoggerService.WriteCallerLine($"{nameof(curHexContext)} is null.");
                return;
            }

            if (!setting.IsBlockSearch) {
                HexUIService.Current.FindNextBytes(curHexContext, setting.HexBytes);
            }
            else if (setting.BlockSize != null && setting.BlockOffset != null) {
                HexUIService.Current.FindNextBytes(curHexContext, setting.HexBytes,
                    FindMethod.Hex, true,
                    setting.BlockSize.Value, setting.BlockOffset.Value);
            }
            lastFindMethod = FindMethod.Hex;

        }

        public bool _canHexOperatable;
        public bool CanHexOperatable {
            get => _canHexOperatable;
            set => SetProperty(ref _canHexOperatable, value);
        }

        private static FindTextStringSetting findStringSetting;
        private DelegateCommand findTextCommand;                               //检索文本的命令;
        public DelegateCommand FindTextCommand {                               //查找字符串命令;
            get {
                return findTextCommand ??
                    (findTextCommand = new DelegateCommand(() => {
                        var msg = new FindTextMessageBox();
                        var setting = msg.Show(ref findStringSetting);
                        if (setting != null) {
                            FindText(setting);
                        }
                        lastFindMethod = FindMethod.Text;
                    }, () => CanHexOperatable)).ObservesProperty(() => CanHexOperatable);
            }
        }

        private void FindText(FindTextStringSetting setting) {
            if (setting == null) {
                return;
            }

            var curHexContext = GetCurrentHexDataContext();
            if (curHexContext == null) {
                LoggerService.WriteCallerLine($"{nameof(curHexContext)} is null.");
                return;
            }


            if (!setting.IsBlockSearch) {
                HexUIService.Current.FindNextString(curHexContext, setting.FindText);
            }
            else if (setting.BlockSize != null && setting.BlockOffset != null) {
                HexUIService.Current.FindNextString(curHexContext, setting.FindText, true,
                    setting.BlockSize.Value, setting.BlockOffset.Value);
            }

        }

        /// <summary>
        /// 验证输入;
        /// </summary>
        /// <param name="searchingKey"></param>
        /// <returns></returns>
        private static SearchValidateRes ValidateInput(string searchingKey) {
            if (string.IsNullOrWhiteSpace(searchingKey)) {
                ThreadInvoker.UIInvoke(() => {
                    MsgBoxService.Show(LanguageService.FindResourceString("SearchKeyCannotBeNullOrEmpty"));
                });
                return SearchValidateRes.NullRes;
            }

            if (!Regex.IsMatch(searchingKey, "[^? !@#$%\\^&*()]+")) {
                ThreadInvoker.UIInvoke(() => {
                    MsgBoxService.Show(LanguageService.FindResourceString("SearchKeyIllegal"));
                });

                return SearchValidateRes.IlegalChar;
            }

            return SearchValidateRes.OK;
        }


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

        //搜索命令;
        private DelegateCommand _searchKeyConfirmCommand;
        public DelegateCommand SearchKeyConfirmCommand =>
            _searchKeyConfirmCommand ?? (_searchKeyConfirmCommand = new DelegateCommand(() => {
                //var fsTabService = ServiceProvider.Current.GetInstance<IDocumentTabService>();
                //if (fsTabService == null) {
                //    LoggerService.Current?.WriteCallerLine("FsTabService is null!");
                //    return;
                //}

                //var fileBrowserViewModel = (fsTabService.SelectedTab as ExtTabModel<IFileBrowserDataContext>).Data;

                ////确认搜索设备;
                //var blDevice = fileBrowserViewModel.File as Device;

                //var device = blDevice ?? fileBrowserViewModel.File.GetParent<Device>() as Device;
                ////var indexableFile = ServiceProvider.Current.GetInstance<ICaseService>().CurrentCase.CaseEvidences.
                ////    FirstOrDefault(p =>
                ////    p is IHaveData<IFile> fcsFile
                ////    && fcsFile.Data == device) as IIndexable;

                //if (SlSearchKeyOption.Method == SearchMethod.Content) {
                //    if (indexableFile == null) {
                //        LoggerService.Current?.WriteCallerLine($"{nameof(indexableFile)} can't be null");
                //        RemainingMessageBox.Tell(FindResourceString("NullCaseFileUnknownError"));
                //        return;
                //    }
                //    //开始搜索索引的委托;
                //    Action searchAct = () => {
                //        ServiceProvider.Current.GetInstance<IShellService>()?.ChangeLoadState(true);
                //        ThreadPool.QueueUserWorkItem(cb => {
                //            try {
                //                var itrFile = fileBrowserViewModel.CurFile as IEnumerableFileFile;
                //                var searchingKey = string.Empty;
                //                Application.Current.Dispatcher.Invoke(() => {
                //                    searchingKey = InputValueMessageBox.Show(FindResourceString("PleaseInputContent"));
                //                });
                //                if (searchingKey == null) {
                //                    return;
                //                }
                //                if (ValidateInput(searchingKey) == SearchValidateRes.OK && itrFile != null) {
                //                    IFile fileNode = itrFile;
                //                    var sb = new StringBuilder();
                //                    while (fileNode != null) {
                //                        if (fileNode is Partition) {
                //                            sb.Insert(0, $"{device.IndexOf(fileNode)}/");
                //                        }
                //                        else if (!(fileNode is Device)) {
                //                            sb.Insert(0, $"{fileNode.Name}/");
                //                        }
                //                        fileNode = fileNode.Parent;
                //                    }


                //                    var files = indexableFile.IndexSearchKey(searchingKey.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries), sb.ToString());
                //                    if (files != null) {
                //                        fileBrowserViewModel.FillFiles(files);
                //                    }
                //                    else {
                //                        AppInvoke(() => {
                //                            RemainingMessageBox.Tell(FindResourceString("SearchResNull"));
                //                        });
                //                    }
                //                }
                //            }
                //            catch (Exception ex) {
                //                LoggerService.Current?.WriteCallerLine($"{nameof(SearchKeyConfirmCommand)}:{ex.Message}");
                //            }
                //            finally {
                //                ServiceProvider.Current.GetInstance<IShellService>()?.ChangeLoadState(false);
                //            }
                //        });
                //    };

                //    //若未建立索引,询问是否建立索引。
                //    if (!indexableFile.HasIndexes) {
                //        //若确定,则开始建立索引;
                //        if (CDFCMessageBox.Show(FindResourceString("IndexesNeeded"), ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("IndexesNotBeenBuilt"), MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                //            ServiceProvider.Current.GetInstance<IShellService>()?.ChangeLoadState(true, null);
                //            var msg = new ProgressMessageBox();
                //            bool broke = false;
                //            var proc = 0;

                //            msg.DoWork += (sender, e) => {
                //                msg.ReportProgress(0, ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("IndexesBeingBuilt"), $"{proc}%");
                //                try {
                //                    var succeed = indexableFile.BuildIndexFiles((period, pro, total) => {
                //                        if (period == BuildPeriod.BuildDoc) {
                //                            msg.ReportProgress(0, ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("IndexesInitializing"), string.Empty);
                //                        }
                //                        else if (total != null) {
                //                            var newProc = (int)(pro * 100 / total);
                //                            if (newProc > proc) {
                //                                proc = newProc;
                //                                msg.ReportProgress(proc > 100 ? 100 : proc, ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("IndexesBeingBuilt"), $"{proc}%");
                //                            }
                //                        }
                //                    },
                //                    () => {
                //                        if (msg.CancellationPending) {
                //                            broke = true;
                //                        }
                //                        return broke;
                //                    });
                //                    //若成功创建索引;开始搜索;
                //                    if (succeed && indexableFile.HasIndexes) {
                //                        ServiceProvider.Current.GetInstance<ICaseService>()?.CurrentCase.Save();
                //                        //分隔空格;
                //                        searchAct();
                //                    }
                //                }
                //                catch (Exception ex) {
                //                    Logger.WriteLine($"{nameof(SearchKeyConfirmCommand)}:{ex.Message}");
                //                    AppInvoke(() => {
                //                        CDFCMessageBox.Show($"{FindResourceString("IndexBuildingFailed")}:{ex.Message}");
                //                    });
                //                }
                //            };

                //            msg.RunWorkerCompleted += (sender, e) => {
                //                ServiceProvider.Current.GetInstance<IShellService>()?.ChangeLoadState(false, null);
                //            };
                //            msg.ShowDialog();
                //        }
                //    }
                //    else {
                //        searchAct();
                //    }

                //}
                //else {
                //    //ThreadPool.QueueUserWorkItem(cb => {
                //    //    try {
                //    //        _fileBrowserTabModel.SearchNameInCurFile(searchingKey);
                //    //    }
                //    //    finally {
                //    //        Aggregator?.GetEvent<IsLoadingChangedEvent>()?.Publish(false);
                //    //    }
                //    //});
                //}
            }));
    }
}
