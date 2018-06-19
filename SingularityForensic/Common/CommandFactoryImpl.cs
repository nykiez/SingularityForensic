using Prism.Commands;
using SingularityForensic.Contracts.Common;
using System;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace SingularityForensic.Common {
    [Export(typeof(ICommandFactory))]
    class CommandFactoryImpl : ICommandFactory {
        public ICommand CreateDelegateCommand(Action executeMethod, Func<bool> canExecuteMethod = null) {
            if(executeMethod == null) {
                throw new ArgumentNullException(nameof(executeMethod));
            }

            if(canExecuteMethod == null) {
                return new DelegateCommand(executeMethod);
            }
            else {
                return new DelegateCommand(executeMethod, canExecuteMethod);
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
}
