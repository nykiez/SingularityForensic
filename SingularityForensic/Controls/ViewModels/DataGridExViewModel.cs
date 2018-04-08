using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using static CDFCCultures.Managers.ManagerLocator;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.App;

namespace SingularityForensic.Controls.ViewModels {
    /// <summary>
    /// 可通知的后台处理视图模型;
    /// </summary>
    public interface IInteractionDataGridViewModel {
        /// <summary>
        /// 通知后台双击事件;
        /// </summary>
        /// <param name="row"></param>
        void NotifyDoubleClick(object row);

    }

    public class DataGridExViewModel:BindableBase {
        private string _selectedText;
        public string SelectedText {
            get => _selectedText;
            set  {
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
                            _copySelectedTextCommandItem.CommandName = string.Format(FindResourceString("CopyCellTextFormat"), e);
                        }
                        catch(Exception ex) {
                            LoggerService.Current?.WriteCallerLine(ex.Message);
                        }
                    };

                    try {
                        _copySelectedTextCommandItem.CommandName = string.Format(FindResourceString("CopyCellTextFormat"), SelectedText);
                    }
                    catch(Exception ex) {
                        LoggerService.Current?.WriteCallerLine(ex.Message);
                    }
                }
                return _copySelectedTextCommandItem;
            }
        }
        
        private ObservableCollection<CommandItem> _contextCommands;
        /// <summary>
        /// 上下文菜单命令项;
        /// </summary>
        public virtual ObservableCollection<CommandItem> ContextCommands {
            get {
                if(_contextCommands == null) {
                    _contextCommands = new ObservableCollection<CommandItem>() {
                        CopySelectedTextCommandItem
                    };
                    
                }
                return _contextCommands;
            }
            set => SetProperty(ref _contextCommands, value);
        }
    }
}
