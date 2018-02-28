using CDFCUIContracts.Commands;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using static CDFCCultures.Managers.ManagerLocator;
using EventLogger;

namespace SingularityForensic.Controls.ViewModels {
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
        private ICommandItem _copySelectedTextCommandItem;
        public ICommandItem CopySelectedTextCommandItem {
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
                            Logger.WriteCallerLine(ex.Message);
                        }
                    };

                    try {
                        _copySelectedTextCommandItem.CommandName = string.Format(FindResourceString("CopyCellTextFormat"), SelectedText);
                    }
                    catch(Exception ex) {
                        Logger.WriteCallerLine(ex.Message);
                    }
                }
                return _copySelectedTextCommandItem;
            }
        }
        
        private ObservableCollection<ICommandItem> _contextCommands;
        /// <summary>
        /// 上下文菜单命令项;
        /// </summary>
        public virtual ObservableCollection<ICommandItem> ContextCommands {
            get {
                if(_contextCommands == null) {
                    _contextCommands = new ObservableCollection<ICommandItem>() {
                        CopySelectedTextCommandItem
                    };
                    
                }
                return _contextCommands;
            }
            set => SetProperty(ref _contextCommands, value);
        }
    }
}
