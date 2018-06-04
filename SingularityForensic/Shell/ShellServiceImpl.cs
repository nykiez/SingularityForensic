using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Shell;
using SingularityForensic.Contracts.Shell.Events;
using SingularityForensic.Shell.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
            Initialize();
        }

        private void Initialize() {
            RegisterEvents();
        }
        private void RegisterEvents() {
            _shellVM.ClosingRequest += ShellVM_ClosingRequest;
        }

        private void ShellVM_ClosingRequest(object sender, System.ComponentModel.CancelEventArgs e) {
            if(ShellClosingEventHandlers == null) {
                return;
            }

            var args = new ShellClosingEventArgs(e);
            foreach (var handler in ShellClosingEventHandlers) {
                handler.Handle(args);
                if (args.Handled) {
                    break;
                }
            }
        }

        private ShellViewModel _shellVM;
        private object _shellView;

        private IEnumerable<IShellClosingEventHandler> _shellClosingEventHandlers;
        private IEnumerable<IShellClosingEventHandler> ShellClosingEventHandlers {
            get {
                if(_shellClosingEventHandlers == null) {
                    _shellClosingEventHandlers = ServiceProvider.GetAllInstances<IShellClosingEventHandler>();
                }
                return _shellClosingEventHandlers;
            }
        }

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

        public void Show() {
            (_shellView as Window).Show();
        }

        public object Shell => ViewProvider.GetView(Contracts.Shell.Constants.ShellView);
    }
}
