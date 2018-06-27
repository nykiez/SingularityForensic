using Prism.Commands;
using SingularityForensic.Contracts.Common;
using System;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace SingularityForensic.Common {
    [Export(typeof(ICommandFactory))]
    class CommandFactoryImpl : ICommandFactory {
        public IDelegateCommand CreateDelegateCommand(Action executeMethod, Func<bool> canExecuteMethod = null) {
            if(executeMethod == null) {
                throw new ArgumentNullException(nameof(executeMethod));
            }

            if(canExecuteMethod == null) {
                return new DelegateCommandWrapper(executeMethod);
            }
            else {
                return new DelegateCommandWrapper(executeMethod, canExecuteMethod);
            }
            
        }

        public void RaiseCanExecutedChanged(ICommand command) {
            if(command == null) {
                throw new ArgumentNullException(nameof(command));
            }

            if(command is DelegateCommand comm) {
                comm.RaiseCanExecuteChanged();
            }
        }
    }

    class DelegateCommandWrapper : DelegateCommand,IDelegateCommand {
        public DelegateCommandWrapper(Action executeMethod, Func<bool> canExecuteMethod):base(executeMethod,canExecuteMethod) {

        }

        public DelegateCommandWrapper(Action executeMethod):base(executeMethod) {

        }
    }
}
