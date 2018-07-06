using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using SingularityForensic.App.Models;
using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.App.ViewModels
{
    /// <summary>
    /// 单选对话框模型;
    /// </summary>
    /// <typeparam name="TOption"></typeparam>
    public class SingleSelectOptionDialogViewModel<TOption>:BindableBase
    {
        public InteractionRequest<INotification> CloseRequest { get; } = new InteractionRequest<INotification>();
        public SingleSelectOptionDialogViewModel(IEnumerable<TOption> options,Func<TOption,string> getText) {
            if(options == null) {
                throw new ArgumentNullException(nameof(options));
            }
            if(getText == null) {
                throw new ArgumentNullException(nameof(getText));
            }

            foreach (var option in options) {
                OptionModels.Add(new OptionModel<TOption>(option, getText));
            }
            
            if(OptionModels.Count != 0) {
                SelectedOptionModel = OptionModels[0];
            }
        }

        public ObservableCollection<OptionModel<TOption>> OptionModels { get; } = new ObservableCollection<OptionModel<TOption>>();
        
        private OptionModel<TOption> _selectedOptionModel;
        public OptionModel<TOption> SelectedOptionModel {
            get => _selectedOptionModel;
            set => SetProperty(ref _selectedOptionModel, value);
        }


        private string _windowTitle;
        public string WindowTitle {
            get => _windowTitle;
            set => SetProperty(ref _windowTitle, value);
        }


        private string _description;
        public string Description {
            get => _description;
            set => SetProperty(ref _description, value);
        }


        /// <summary>
        /// 确定命令;
        /// </summary>
        private DelegateCommand _confirmCommand;
        public DelegateCommand ConfirmCommand => _confirmCommand ??
            (_confirmCommand = new DelegateCommand(
                () => {
                    if(SelectedOptionModel == null && OptionModels.Count != 0) {
                        MsgBoxService.Show(LanguageService.FindResourceString(Constants.MsgText_NoOptionSelected));
                        return;
                    }
                    _confirmed = true;
                    CloseRequest.Raise(new Notification());
                    
                }
            ));
        private bool _confirmed = false;

        /// <summary>
        /// 取消命令;
        /// </summary>
        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand => _cancelCommand ??
            (_cancelCommand = new DelegateCommand(
                () => {
                    SelectedOptionModel = null;
                    CloseRequest.Raise(new Notification());
                }
            ));

        /// <summary>
        /// 对话框关闭时;
        /// </summary>
        private DelegateCommand _closingCommand;
        public DelegateCommand ClosingCommand => _closingCommand ??
            (_closingCommand = new DelegateCommand(
                () => {
                    if (_confirmed) {
                        return;
                    }
                    SelectedOptionModel = null;
                }
            ));

    }
}
