using Prism.Events;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer.Events {
    /// <summary>
    /// 聚焦分区(行)变化时发生;
    /// </summary>
    public class FocusedPartitionChangedEvent : PubSubEvent<(IPartitionsBrowserViewModel sender, IPartitionRow part)> {
    }
}
