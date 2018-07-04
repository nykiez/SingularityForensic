using SingularityForensic.App.ViewModels;
using SingularityForensic.App.Views;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Shell;
using System.ComponentModel.Composition;
using System.Threading;

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
            var msg = new MessageBoxWindow {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
                DataContext = vm
            };

            if (ShellService.Current.Shell is System.Windows.Window shell && shell.IsLoaded) {
                msg.ShowInTaskbar = false;
                msg.Owner = shell;
            }
            var res = msg.ShowDialog();

#if DEBUG
            ThreadInvoker.BackInvoke(() => {
                Thread.Sleep(1000);
                for (int i = 0; i < 10; i++) {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                }
            });
#endif
            msg.DataContext = null;
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
