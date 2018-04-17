using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SingularityForensic.Common {
    /// <summary>
    /// 命令绑定项,可用于MenuItem等的绑定等;
    /// </summary>
    class CommandItem : ExtensibleBindableBase, ICommandItem {
        public CommandItem(ICommand command) {
            this.Command = command;
        }

        public ICommand Command { get; }

        private string _commandName;
        public virtual string Name {
            get => _commandName;
            set => SetProperty(ref _commandName, value);
        }

        private Uri _icon;
        public Uri Icon {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        public ICollection<ICommandItem> Children => _children;
        private List<ICommandItem> _children { get; } = new List<ICommandItem>();


        private bool _isEnabled;
        public bool IsEnabled {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        //排列顺序;
        public int Sort { get; set; }
    }

    [Export(typeof(ICommandItemFactory))]
    class CommandItemFactory : ICommandItemFactory {
        public ICommandItem CreateNew(ICommand command) => new CommandItem(command);
    }
}
