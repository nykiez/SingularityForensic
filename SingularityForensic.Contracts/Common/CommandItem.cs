using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SingularityForensic.Contracts.Common {
    ///// <summary>
    ///// 命令绑定项契约;
    ///// </summary>
    //public interface ICommandItem {
    //    ICommand Command { get; set; }
    //    string CommandName { get; set; }
    //    IEnumerable<ICommandItem> Children { get; set; }

    //    //排列项;
    //    int SortOrder { get; set; }
    //}

    

    /// <summary>
    /// 命令绑定项,可用于MenuItem等的绑定等;
    /// </summary>
    public class CommandItem : BindableBase {
        public ICommand Command { get; set; }
        
        private string _commandName;
        public virtual string CommandName {
            get => _commandName;
            set => SetProperty(ref _commandName, value);
        }

        private Uri _icon;
        public Uri Icon {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        public List<CommandItem> Children { get; } = new List<CommandItem>();
        
        //排列顺序;
        public int SortOrder { get; set; }
    }

    //public interface ICommandItem<TData> : ICommandItem {
    //    Func<TData> GetData { get; set; }
    //}

    ///// <summary>
    ///// 需要上下文数据的命令绑定项;
    ///// </summary>
    ///// <typeparam name="TData"></typeparam>
    //public class CommandItem<TData> : CommandItem, ICommandItem<TData> {
    //    public virtual Func<TData> GetData { get; set; }
    //}

    //具有参数的命令绑定项;
    //public class DelegateCommandItem<TPara> : CommandItem {
    //    public DelegateCommandItem(DelegateCommand<TPara> command, TPara para) {
    //        this.Command = command;
    //        this.CommandParameter = para;
    //    }
    //    public TPara CommandParameter { get; }
    //    public override bool HasParameter => true;
    //}
}
