using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer {
    /// <summary>
    /// 设备-分区视图模型被创建事件;
    /// </summary>
    public interface IPartitionsBrowserViewModel {
        /// <summary>
        /// 所属设备;
        /// </summary>
        IDevice Device { get; }

        /// <summary>
        /// 选定的分区;
        /// </summary>
        IPartitionRow SelectedPart { get; }

        IEnumerable<ICommandItem> ContextCommands { get; }
    }

    
}
