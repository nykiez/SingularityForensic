using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer {
    public interface IPartitionsBrowserViewModel {
        /// <summary>
        /// 所属设备;
        /// </summary>
        Device Device { get; }

        /// <summary>
        /// 选定的分区;
        /// </summary>
        Partition SelectedPart { get; }

        IList<CommandItem> ContextCommands { get; }
    }
}
