using SingularityForensic.App.ViewModels;
using SingularityForensic.App.Views;
using SingularityForensic.Contracts.App;
using System.ComponentModel.Composition;

namespace SingularityForensic.App {
   
    [Export(typeof(IMessageBoxService))]
    class MessageBoxServiceImpl : IMessageBoxService {
        public MessageBoxResult Show(string msg) {
            return Show(msg, LanguageService.FindResourceString(Constants.WindowTitle_Tip), MessageBoxButton.OK);
        }

        public void ShowError(string error) {
            Show(error);
        }

        public MessageBoxResult Show(string msgText, MessageBoxButton button) {
            var res = Show(msgText,LanguageService.FindResourceString(Constants.WindowTitle_Tip), button);
            return res;
        }

        public MessageBoxResult Show(string msgText, string caption, MessageBoxButton button) {
            var vm = new MessageBoxWindowViewModel(button, msgText, caption);
            var msg = new MessageBoxWindow();
            msg.ShowInTaskbar = false;
            msg.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            msg.DataContext = vm;
            msg.Owner =  System.Windows.Application.Current.MainWindow;
            var res = msg.ShowDialog();
            switch (vm.DialogResult) {
                case null:
                    if (button == MessageBoxButton.YesNoCancel)
                        return MessageBoxResult.Cancel;
                    return MessageBoxResult.None;
                case false:
                    return MessageBoxResult.No;
                case true:
                    if (button == MessageBoxButton.OK)
                        return MessageBoxResult.OK;
                    return MessageBoxResult.Yes;
                default:
                    return MessageBoxResult.None;
            }
            
            
        }
    }
}
