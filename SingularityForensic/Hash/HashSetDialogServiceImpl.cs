using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Hash;
using SingularityForensic.Contracts.Shell;
using SingularityForensic.Hash.ViewModels;
using SingularityForensic.Hash.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.Hash
{
   
    [Export(typeof(IHashSetDialogService))]
    class HashSetDialogServiceImpl : IHashSetDialogService {
        public IHashSet CreateNewHashSet() {
            var vm = new CreateHashSetDialogViewModel();
            vm.Initialize();
            var dialog = new CreateHashSetDialog() {
                DataContext = vm
            };

            if (ShellService.Current.Shell is Window shell && shell.IsLoaded) {
                dialog.ShowInTaskbar = false;
                dialog.Owner = shell;
            }

            dialog.ShowDialog();
            dialog.DataContext = null;

            return vm.HashSet;
        }

        public IHashSet SelectHashSet() {
            var dialog = DialogService.Current.CreateSingleSelectOptionDialog(HashSetManagementService.HashSets, p => p.Name);
            dialog.WindowTitle = LanguageService.FindResourceString(Constants.WindowTitle_SelectHashSet);
            dialog.Description = LanguageService.FindResourceString(Constants.Description_SelectHashSet);
            return dialog.GetOption();
        }

        public void ShowManagementDialog() {
            var vm = new HashSetManagementDialogViewModel();
            vm.Initialize();
            var dialog = new HashSetManagementDialog {
                DataContext = vm
            };
            
            if (ShellService.Current.Shell is Window shell && shell.IsLoaded) {
                dialog.ShowInTaskbar = false;
                dialog.Owner = shell;
            }

            dialog.ShowDialog();
            dialog.DataContext = null;

#if DEBUG
            Contracts.App.ThreadInvoker.BackInvoke(() => {
                System.Threading.Thread.Sleep(1000);
                for (int i = 0; i < 2; i++) {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                }
            });
#endif
        }


    }
}
