using Prism.Commands;
using SingularityForensic.App.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace SingularityForensic.App.ViewModels {
    public partial class SingularityMessageBoxDialogViewModel : INotifyPropertyChanged {
        public SingularityMessageBoxDialogViewModel(MessageBoxButton button,string warnWords,string titleWords) {
            if(button == MessageBoxButton.OKCancel || button == MessageBoxButton.OK) {
                AddButton(MessageButtonModel.CreateOKBtn(), ConfirmCommand);
                if(button == MessageBoxButton.OKCancel) {
                    AddButton(MessageButtonModel.CreateCancelBtn(), DenyCommand);
                }
            }
            else if(button == MessageBoxButton.YesNo || button == MessageBoxButton.YesNoCancel) {
                AddButton(MessageButtonModel.CreateYESBtn(), ConfirmCommand);
                AddButton(MessageButtonModel.CreateNOBtn(), DenyCommand);
                if(button == MessageBoxButton.YesNoCancel) {
                    AddButton(MessageButtonModel.CreateCancelBtn(), EmptyCommand);
                }
            }
            this.WarnWords = warnWords;
            this.TitleWords = titleWords;
        }
        private void AddButton(MessageButtonModel btnModel,DelegateCommand command) {
            btnModel.Command = command;
            Buttons.Add(btnModel);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<MessageButtonModel> Buttons { get; private set; } = new ObservableCollection<MessageButtonModel>();
        public string WarnWords { get; private set; }
        public string TitleWords { get; private set; }
        private bool isEnabled = true;
        public bool IsEnabled {
            get {
                return isEnabled;
            }
            set {
                isEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
            }
        }

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
    public partial class SingularityMessageBoxDialogViewModel  {
       public DelegateCommand ConfirmCommand {
            get {
                return new DelegateCommand(() => {
                    DialogResult = true; IsEnabled = false;
                });
            }
        }
        public DelegateCommand DenyCommand {
            get {
                return new DelegateCommand(() => { DialogResult = false; IsEnabled = false; });
            }
        }
        public DelegateCommand EmptyCommand {
            get {
                return new DelegateCommand(() => { DialogResult = null; IsEnabled = false; });
            }
        }
    }
    
}
