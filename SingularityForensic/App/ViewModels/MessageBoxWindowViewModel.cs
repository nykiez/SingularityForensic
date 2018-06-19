using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using SingularityForensic.App.Models;
using SingularityForensic.Contracts.App;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SingularityForensic.App.ViewModels {
    public partial class MessageBoxWindowViewModel : INotifyPropertyChanged {
        public InteractionRequest<Notification> CloseRequest { get; } = new InteractionRequest<Notification>();
        public MessageBoxWindowViewModel(MessageBoxButton button, string warnWords, string titleWords) {
            if (button == MessageBoxButton.OKCancel || button == MessageBoxButton.OK) {
                AddButton(MessageButtonModel.CreateOKBtn(), ConfirmCommand);
                if (button == MessageBoxButton.OKCancel) {
                    AddButton(MessageButtonModel.CreateCancelBtn(), DenyCommand);
                }
            }
            else if (button == MessageBoxButton.YesNo || button == MessageBoxButton.YesNoCancel) {
                AddButton(MessageButtonModel.CreateYESBtn(), ConfirmCommand);
                AddButton(MessageButtonModel.CreateNOBtn(), DenyCommand);
                if (button == MessageBoxButton.YesNoCancel) {
                    AddButton(MessageButtonModel.CreateCancelBtn(), EmptyCommand);
                }
            }
            this.WarnWords = warnWords;
            this.TitleWords = titleWords;
        }
        private void AddButton(MessageButtonModel btnModel, DelegateCommand command) {
            btnModel.Command = command;
            Buttons.Add(btnModel);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<MessageButtonModel> Buttons { get; private set; } = new ObservableCollection<MessageButtonModel>();
        public string WarnWords { get; private set; }
        public string TitleWords { get; private set; }
        
        private bool? dialogResult;
        public bool? DialogResult {
            get {
                return dialogResult;
            }
            set {
                dialogResult = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DialogResult)));
            }
        }
    }
    public partial class MessageBoxWindowViewModel {

        private DelegateCommand _confirmCommand;
        public DelegateCommand ConfirmCommand => _confirmCommand ??
            (_confirmCommand = new DelegateCommand(
                () => {
                    DialogResult = true;
                    CloseRequest.Raise(new Notification());
                }
            ));



        private DelegateCommand _denyCommand;
        public DelegateCommand DenyCommand => _denyCommand ??
            (_denyCommand = new DelegateCommand(
                () => {
                    DialogResult = false;
                    CloseRequest.Raise(new Notification());
                }
            ));


        private DelegateCommand _emptyCommand;
        public DelegateCommand EmptyCommand => _emptyCommand ??(_emptyCommand = 
            new DelegateCommand(() => {
                DialogResult = null;
                CloseRequest.Raise(new Notification());
             })
        );
    }

}
