using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Shell;
using SingularityForensic.Shell.ViewModels;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SingularityForensic.Shell {
    /// <summary>
    /// 主窗体服务;
    /// </summary>
    [Export(typeof(IShellService))]
    public class ShellServiceImpl:IShellService
    {
        [ImportingConstructor]
        public ShellServiceImpl(ShellViewModel shellVM) {
            this._shellVM = shellVM;
            _shellView = ViewProvider.GetView(Contracts.Shell.Constants.ShellView);
                
        }

        
        private ShellViewModel _shellVM;
        private object _shellView;

        //更改标题栏文字;
        public void SetTitle(string word,bool saveBrandName = true) {
            _shellVM.SetTitle(word,saveBrandName);
            
        }

        public void Focus() {
            _shellVM.Focus();
        }

        /// <summary>
        /// 改变等待状态;
        /// </summary>
        /// <param name="isLoading"></param>
        /// <param name="word"></param>
        public void ChangeLoadState(bool isLoading, string word = null) {
            _shellVM.IsLoading = isLoading;
            _shellVM.LoadingWord = word;
        }

        
        /// <summary>
        /// /// 添加快捷键绑定;
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="key">案件</param>
        /// <param name="modifier">修饰键</param>
        /// <param name="commandPara">命令参数</param>
        public void AddKeyBinding(ICommand command,Key key,ModifierKeys modifier = ModifierKeys.None) {
            var kb = new KeyBinding {
                Modifiers = modifier,
                Key = key,
                Command = command
            };
            
            if(_shellView is UIElement uiElem) {
                uiElem.InputBindings.Add(kb);
            }
        }
    }
}
