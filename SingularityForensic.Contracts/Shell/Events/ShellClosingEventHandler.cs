using Prism.Events;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Shell.Events {
    public class ShellClosingEventArgs  {
        public ShellClosingEventArgs(CancelEventArgs cancelEventArgs) {
            this.CancelEventArgs = cancelEventArgs ?? throw new ArgumentNullException(nameof(cancelEventArgs));
        }
        public CancelEventArgs CancelEventArgs { get; }
        public bool Handled { get; set; }
    }

    public interface IShellClosingEventHandler : IEventHandler<ShellClosingEventArgs> {

    }
}
