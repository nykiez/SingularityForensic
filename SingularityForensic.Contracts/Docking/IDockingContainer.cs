using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Docking {
    /// <summary>
    /// 停靠容器;
    /// </summary>
    public interface IDockingPaneContainer:IDockingItem {
        /// <summary>
        /// 初始停靠位置;
        /// </summary>
        DockingPosition InitDockingPosition { get; set; }

        /// <summary>
        /// 停靠服务GUID;
        /// </summary>
        string DockingServiceName { get; }

    }
}
