using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Docking {
    /// <summary>
    /// 文档停靠服务;本单位不唯一;
    /// </summary>
    public interface IDockingService {
        /// <summary>
        /// 初始化;
        /// </summary>
        void Initialize();
        /// <summary>
        /// 停靠区域集合;
        /// </summary>
        IEnumerable<IDockingPane> DockingPanes { get; }
        /// <summary>
        /// 停靠组集合;
        /// </summary>
        IEnumerable<IDockingPaneGroup> DockingPaneGroups { get; }
        /// <summary>
        /// 停靠容器集合;
        /// </summary>
        IEnumerable<IDockingPaneContainer> DockingContainers { get; }
        /// <summary>
        /// 所有停靠单元;
        /// </summary>
        IEnumerable<IDockingItem> DockingItems { get; }
    }

    
}
