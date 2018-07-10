using SingularityForensic.Contracts.FileExplorer.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer.Events.TreeView {
    /// <summary>
    /// 为设备/分区/目录加入匹配哈希集菜单;
    /// </summary>
    [Export(typeof(IFileExplorerModuleLoadingEventHandler))]
    class OnFileExplorerModuleLoadingOnMatchHashSetsHandler : IFileExplorerModuleLoadingEventHandler {
        public int Sort => 3;

        public bool IsEnabled => true;

        public void Handle() {
            
        }
    }
}
