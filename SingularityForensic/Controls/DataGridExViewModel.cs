using Prism.Commands;
using Prism.Mvvm;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Controls;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Controls.GridView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
    }

    public class DataGridExViewModel : BindableBase, IInteractionGridViewModel {
        private string _selectedText;
        public string SelectedText {
            get => _selectedText;
            set {
                SetProperty(ref _selectedText, value);
                SelectedTextChanged?.Invoke(this, _selectedText);
            }
        }

        public event EventHandler<string> SelectedTextChanged;

        /// <summary>
        /// 拷贝单元格内内容;
        /// </summary>
        private CommandItem _copySelectedTextCommandItem;
        public CommandItem CopySelectedTextCommandItem {
            get {
                if (_copySelectedTextCommandItem == null) {
                    _copySelectedTextCommandItem = new CommandItem {
                        Command = new DelegateCommand(
                            () => {
                                if (SelectedText != null) {
                                    Clipboard.SetText(SelectedText);
                                }
                            },
                            () => SelectedText != null
                        ).ObservesProperty(() => SelectedText)
                    };
                    SelectedTextChanged += (sender, e) => {
                        try {
                            _copySelectedTextCommandItem.CommandName = string.Format(LanguageService.FindResourceString("CopyCellTextFormat"), e);
                        }
                        catch (Exception ex) {
                            LoggerService.Current?.WriteCallerLine(ex.Message);
                        }
                    };

                    try {
                        _copySelectedTextCommandItem.CommandName = string.Format(LanguageService.FindResourceString("CopyCellTextFormat"), SelectedText);
                    }
                    catch (Exception ex) {
                        LoggerService.WriteCallerLine(ex.Message);
                    }
                }
                return _copySelectedTextCommandItem;
            }
        }

        private IList<CommandItem> _contextCommands;
        /// <summary>
        /// 上下文菜单命令项;
        /// </summary>
        public virtual IList<CommandItem> ContextCommands {
            get {
                if (_contextCommands == null) {
                    _contextCommands = new ObservableCollection<CommandItem>() {
                        CopySelectedTextCommandItem
                    };
                }
                return _contextCommands;
            }
            set => SetProperty(ref _contextCommands, value);
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
    }
}
