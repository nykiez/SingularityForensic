using SingularityForensic.App.ViewModels;
using SingularityForensic.App.Views;
using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SingularityForensic.App.Dialogs
{
    /// <summary>
    /// 单选对话框实现;
    /// </summary>
    /// <typeparam name="TOption">选项类型</typeparam>
    public class SingleSelectDialog<TOption> : ISingleSelectDialog<TOption> where TOption:class {
        public SingleSelectDialog(IEnumerable<TOption> options,Func<TOption,string> getText) {
            if (options == null) {
                throw new ArgumentNullException(nameof(options));
            }
            if (getText == null) {
                throw new ArgumentNullException(nameof(getText));
            }
            _vm = new SingleSelectOptionDialogViewModel<TOption>(options, getText);
        }
        private SingleSelectOptionDialogViewModel<TOption> _vm;

        public string WindowTitle {
            get => _vm.WindowTitle;
            set => _vm.WindowTitle = value;
        }

        public string Description {
            get => _vm.Description;
            set => _vm.Description = value;
        }

        public TOption GetOption() {
            var window = new SingleSelectOptionDialog {
                DataContext = _vm
            };

            if ((Contracts.Shell.ShellService.Current.Shell as Window)?.IsLoaded ?? false) {
                window.Owner = Contracts.Shell.ShellService.Current.Shell as Window;
                window.ShowInTaskbar = false;
            }

            window.InputBindings.Add(new KeyBinding(_vm.ConfirmCommand, new KeyGesture(Key.Enter)));

            window.ShowDialog();
            window.DataContext = null;
            window.InputBindings.Clear();

            return _vm.SelectedOptionModel?.Option;
        }
    }
}
