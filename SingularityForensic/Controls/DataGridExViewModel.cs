using Prism.Commands;
using Prism.Mvvm;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Controls;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Controls.GridView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SingularityForensic.Controls {
    /// <summary>
    /// 可通知的后台处理视图模型;
    /// </summary>
    public interface IInteractionGridViewModel {
        /// <summary>
        /// 通知后台双击事件;
        /// </summary>
        /// <param name="row"></param>
        void NotifyDoubleClickOnRow(object row);
        /// <summary>
        /// 通知后台正在自动生成列事件;
        /// </summary>
        /// <param name="row"></param>
        void NotifyAutoGeneratingColumns(GridViewAutoGeneratingColumnEventArgs e);
        /// <summary>
        /// 当前选定的项集合;
        /// </summary>
        Func<IEnumerable> GetSelectedRows { get; set; }
        /// <summary>
        /// 选定所有项;
        /// </summary>
        Action SelectedAllRows { get; set; }
        IEnumerable<CustomColumn> CustomColumns { get; }
    }

    public class DataGridExViewModel : BindableBase, IInteractionGridViewModel,IDataGridViewModel {
        private string _selectedText;
        public string SelectedText {
            get => _selectedText;
            set {
                SetProperty(ref _selectedText, value);
                SelectedTextChanged?.Invoke(this, _selectedText);
            }
        }

        public event EventHandler<string> SelectedTextChanged;
        public event EventHandler UnLoaded;

        /// <summary>
        /// 选定所有项;
        /// </summary>
        public Action SelectedAllRows { get; set; }

        /// <summary>
        /// 拷贝单元格内内容;
        /// </summary>
        private ICommandItem _copySelectedTextCommandItem;
        public ICommandItem CopySelectedTextCommandItem {
            get {
                if (_copySelectedTextCommandItem == null) {
                    _copySelectedTextCommandItem = CommandItemFactory.CreateNew(
                        new DelegateCommand(
                            () => {
                                if (SelectedText != null) {
                                    ClipBoardService.SetText(SelectedText);
                                }
                            },
                            () => SelectedText != null
                        ).ObservesProperty(() => SelectedText));

                    SelectedTextChanged += (sender, e) => {
                        try {
                            _copySelectedTextCommandItem.Name = string.Format(
                                LanguageService.FindResourceString("CopyCellTextFormat"),
                                e.Length >= 8?(e.Substring(0,8) + "..."):e
                            );
                        }
                        catch (Exception ex) {
                            LoggerService.Current?.WriteCallerLine(ex.Message);
                        }
                    };

                    try {
                        _copySelectedTextCommandItem.Name = LanguageService.TryGetStringWithFormat("CopyCellTextFormat", SelectedText);
                    }
                    catch (Exception ex) {
                        LoggerService.WriteCallerLine(ex.Message);
                    }
                }
                return _copySelectedTextCommandItem;
            }
        }

        protected ObservableCollection<ICommandItem> _contextCommands;
        /// <summary>
        /// 上下文菜单命令项;
        /// </summary>
        public virtual ICollection<ICommandItem> ContextCommands {
            get {
                if (_contextCommands == null) {
                    _contextCommands = new ObservableCollection<ICommandItem>() {
                        CopySelectedTextCommandItem,
                        SelectedAllCommandItem
                    };
                }
                return _contextCommands;
            }
        }

        private ICommandItem _selecteAllCommandItem;
        public ICommandItem SelectedAllCommandItem {
            get {
                if(_selecteAllCommandItem == null) {
                    _selecteAllCommandItem = CommandItemFactory.CreateNew(
                        new DelegateCommand(
                            () => {
                                SelectedAllRows?.Invoke();
                            }
                        ));

                    _selecteAllCommandItem.Name = LanguageService.FindResourceString(Constants.GridViewCommandName_SelectedAll);
                }
                return _selecteAllCommandItem;
            }
        }
            

        public virtual void NotifyDoubleClickOnRow(object row) {
        }

        public virtual void NotifyAutoGeneratingColumns(GridViewAutoGeneratingColumnEventArgs arg) {
        }

        /// <summary>
        /// 当前过滤设定;
        /// </summary>
        private IEnumerable<FilterSetting> _filterSettings;
        public IEnumerable<FilterSetting> FilterSettings {
            get => _filterSettings;
            set {
                SetFilterSettings(value);
                RaisePropertyChanged();
                PubEventHelper.GetEvent<FilterSettingsChangedEvent>().Publish(_filterSettings);
            }
        }

        /// <summary>
        /// 设定过滤;
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="notify">是否通知前台</param>
        public void SetFilterSettings(IEnumerable<FilterSetting> settings) {
            _filterSettings = settings;

            if (_filterSettings == null || _filterSettings.Count() == 0) {
                AnyFiltering = false;
            }
            else {
                AnyFiltering = true;
            }
        }

        public void AddContextCommand(ICommandItem commandItem) {
            if (commandItem == null) {
                throw new ArgumentNullException(nameof(commandItem));
            }

            var index = 0;

            foreach (var ci in ContextCommands) {
                if (ci.Sort > commandItem.Sort) {
                    break;
                }
                index++;
            }

            _contextCommands.Insert(index, commandItem);
        }

        //是否开启了任何过滤;
        private bool anyFiltering;
        public bool AnyFiltering {
            get {
                return anyFiltering;
            }
            set {
                SetProperty(ref anyFiltering, value);
            }
        }

        /// <summary>
        /// 卸载命令;
        /// </summary>
        private DelegateCommand _unLoadedCommand;
        public DelegateCommand UnLoadedCommand => _unLoadedCommand ??
            (_unLoadedCommand = new DelegateCommand(
                () => {
                    UnLoaded?.Invoke(this, EventArgs.Empty);
                }
            ));

        public Func<IEnumerable> GetSelectedRows { get ; set ; }


        public virtual IEnumerable<CustomColumn> CustomColumns => null;
    }
}
