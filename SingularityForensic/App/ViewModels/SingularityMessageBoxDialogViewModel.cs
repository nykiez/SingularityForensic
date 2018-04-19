using CDFCMessageBoxes.Commands;
using CDFCMessageBoxes.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace SingularityForensic.App.ViewModels {
    public partial class SingularityMessageBoxDialogViewModel : INotifyPropertyChanged {
        public SingularityMessageBoxDialogViewModel(MessageBoxButton button,string warnWords,string titleWords) {
            if(button == MessageBoxButton.OKCancel || button == MessageBoxButton.OK) {
                AddButton(MessageButtonModel.OK, ConfirmCommand);
                if(button == MessageBoxButton.OKCancel) {
                    AddButton(MessageButtonModel.Cancel, DenyCommand);
                }
            }
            else if(button == MessageBoxButton.YesNo || button == MessageBoxButton.YesNoCancel) {
                AddButton(MessageButtonModel.YES, ConfirmCommand);
                AddButton(MessageButtonModel.NO, DenyCommand);
                if(button == MessageBoxButton.YesNoCancel) {
                    AddButton(MessageButtonModel.Cancel, EmptyCommand);
                }
            }
            this.WarnWords = warnWords;
            this.TitleWords = titleWords;
        }
        private void AddButton(MessageButtonModel btnModel,RelayCommand command) {
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
       public RelayCommand ConfirmCommand {
            get {
                return new RelayCommand(() => {
                    DialogResult = true; IsEnabled = false;
                });
            }
        }
        public RelayCommand DenyCommand {
            get {
                return new RelayCommand(() => { DialogResult = false; IsEnabled = false; });
            }
        }
        public RelayCommand EmptyCommand {
            get {
                return new RelayCommand(() => { DialogResult = null; IsEnabled = false; });
            }
        }
    }
    
}
