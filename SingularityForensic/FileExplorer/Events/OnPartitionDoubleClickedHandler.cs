using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.FileExplorer.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer.Events {
    /// <summary>
    /// 双击分区进入分区查看器;
    /// </summary>
    [Export(typeof(IPartitionDoubleClickedEventHandler))]
    class OnPartitionDoubleClickedHandler : IPartitionDoubleClickedEventHandler {
        public int Sort => 2;

        public bool IsEnabled => true;

        public void Handle((IPartitionsBrowserViewModel vm, IPartition part) tuple) {
            if (tuple.part == null) {
                return;
            }

            FileExplorerUIHelper.GetOrAddFileDocument(tuple.part);
        }
    }
}
