using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
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

        public void ListHashSetPairs(IHashSet hashSet) {
            if(hashSet == null) {
                throw new ArgumentNullException(nameof(hashSet));
            }

            IHashPair[] hashPairArray = null;
            //由于加载的哈希集可能较大,使用等待对话框;
            var loadingDialog = DialogService.Current.CreateLoadingDialog();
            loadingDialog.IsProgressVisible = false;
            loadingDialog.WindowTitle = LanguageService.FindResourceString(Constants.WindowTitle_LoadingHashPairs);
            loadingDialog.DoWork += delegate {
                try {
                    hashSet.BeginOpen();
                    hashPairArray = hashSet.GetAllHashPairs().ToArray();
                }
                catch(Exception ex) {
                    LoggerService.WriteException(ex);
                }
                finally {
                    hashSet.EndOpen();
                }
                
            };
            loadingDialog.RunWorkerCompleted += delegate {
                if(hashPairArray == null) {
                    return;
                }

                var window = new ListHashValuesDialog();
                var vm = new ListHashValuesDialogViewModel(hashPairArray);
                window.DataContext = vm;
                if (ShellService.Current.Shell is Window shell && shell.IsLoaded) {
                    window.ShowInTaskbar = false;
                    window.Owner = shell;
                }

                window.ShowDialog();
                window.DataContext = null;

               

#if DEBUG
                for (int i = 0; i < 2; i++) {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
#endif
            };
            loadingDialog.ShowDialog();
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
