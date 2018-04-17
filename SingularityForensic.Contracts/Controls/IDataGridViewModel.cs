using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Controls {
    public interface IDataGridViewModel {
        string SelectedText { get; }
        event EventHandler<string> SelectedTextChanged;
        IEnumerable<ICommandItem> ContextCommands { get; }
        void AddContextCommand(ICommandItem commandItem);
        /// <summary>
        /// 被卸载事件;
        /// </summary>
        event EventHandler UnLoaded;
    }
}
