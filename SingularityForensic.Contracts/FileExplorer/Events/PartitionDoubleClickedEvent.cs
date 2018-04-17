using Prism.Events;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer.Events {
    /// <summary>
    /// 分区(行)被双击时发生;
    /// </summary>
    public class PartitionDoubleClickedEvent:PubSubEvent<(IPartitionsBrowserViewModel vm, IPartition part)> {
    }
}
