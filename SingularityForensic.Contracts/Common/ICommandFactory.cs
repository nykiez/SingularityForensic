using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SingularityForensic.Contracts.Common {
    public interface ICommandFactory {
        /// <summary>
        /// 创建一个命令;
        /// </summary>
        /// <param name="executeMethod"></param>
        /// <param name="canExecuteMethod"></param>
        /// <returns></returns>
        ICommand CreateDelegateCommand(Action executeMethod, Func<bool> canExecuteMethod = null);
        /// <summary>
        /// 通知可执行状态;
        /// </summary>
        /// <param name="command">需要通知的命令,当且仅当此实例由CommandFactory创建时才会生效</param>
        void RaiseCanExecutedChanged(ICommand command);
    }

    public class CommandFactory : GenericServiceStaticInstance<ICommandFactory> {
        public static ICommand CreateDelegateCommand(Action excuteMethod, Func<bool> canExecuteMethod = null)
            => Current.CreateDelegateCommand(excuteMethod, canExecuteMethod);

        public static void RaiseCanExecutedChanged(ICommand command) => Current.RaiseCanExecutedChanged(command);
    }
}
