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
    /// <summary>
    /// 哈希集对话框服务;
    /// </summary>
    public interface IHashSetDialogService
    {
        /// <summary>
        /// 显示管理对话框;
        /// </summary>
        void ShowManagementDialog();

        /// <summary>
        /// 选择哈希集;
        /// </summary>
        /// <returns></returns>
        IHashSet SelectIHashSet();
    }

    [Export(typeof(IHashSetDialogService))]
    class HashSetDialogServiceImpl : IHashSetDialogService {
        public IHashSet SelectIHashSet() {
            throw new NotImplementedException();
        }

        public void ShowManagementDialog() {
            var dialog = new HashSetManagementDialog {
                DataContext = new HashSetManagementDialogViewModel()
            };

            
            
            if (ShellService.Current.Shell is Window shell && shell.IsLoaded) {
                dialog.ShowInTaskbar = false;
                dialog.Owner = shell;
            }
        }
    }
}
