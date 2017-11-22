using Microsoft.Practices.ServiceLocation;
using SingularityForensic.Modules.Shell.Models;
using SingularityForensic.ViewModels.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace SingularityForensic.Modules.Shell.Global.Services {
    public interface IShellService {
        //更改标题栏文字;
        void ChangeTitleWord(string word, bool saveBrandName = true);
        void Focus();
        void ChangeLoadState(bool isLoading, string word = null);
        void AddKeyGestrue(ICommand command, Key key, ModifierKeys modifier = ModifierKeys.None, object commandPara = null);
    }

    /// <summary>
    /// 主窗体服务;
    /// </summary>
    [Export(typeof(IShellService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ShellService:IShellService
    {
        
        public ShellService() {
            //PubEventHelper.Subscribe<ShellLoadedEvent, IShell>(shell => {
            //    while(_inputBindings.Count != 0) {
            //        shell?.AddInputBinding(_inputBindings.Dequeue());
            //    }
            //});
        }

        [Import]
        Lazy<ShellViewModel> ShellVM;

        //更改标题栏文字;
        public void ChangeTitleWord(string word,bool saveBrandName = true) {
            if(ShellVM?.Value != null) {
                ShellVM.Value.SetTitle(word,saveBrandName);
            }
        }

        public void Focus() {
            try {
                var ss = new List<IShell>(ServiceLocator.Current.GetAllInstances<IShell>());
                foreach (var s in ss) {
                    s.Focus();
                }
            }
            catch {

            }
            
            //ServiceLocator.Current.GetInstance<IShell>()?.Focus();
        }

        /// <summary>
        /// 改变等待状态;
        /// </summary>
        /// <param name="isLoading"></param>
        /// <param name="word"></param>
        public void ChangeLoadState(bool isLoading, string word = null) {
            ShellVM.Value.IsLoading = isLoading;
            ShellVM.Value.LoadingWord = word;
        }

        private Queue<InputBinding> _inputBindings = new Queue<InputBinding>();
        /// <summary>
        /// /// 添加快捷键绑定;
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="key">案件</param>
        /// <param name="modifier">修饰键</param>
        /// <param name="commandPara">命令参数</param>
        public void AddKeyGestrue(ICommand command,Key key,ModifierKeys modifier = ModifierKeys.None,object commandPara = null) {
            var kb = new KeyBinding {
                Modifiers = modifier,
                Key = key,
                CommandParameter = commandPara,
                Command = command
            };
            _inputBindings.Enqueue(kb);
            //Shell?.InputBindings.Add(kb);
        }
    }
}
