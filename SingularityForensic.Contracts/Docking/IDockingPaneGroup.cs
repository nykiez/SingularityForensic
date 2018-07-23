using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Docking {
    /// <summary>
    /// 停靠组;
    /// </summary>
    public interface IDockingPaneGroup : IDockingItem {
        /// <summary>
        /// Container 的唯一标识;
        /// </summary>
        string ContainerGUID { get; }


        /// <summary>
        /// 是否无样式;
        /// </summary>
        bool NoStyle { get; }
    }
}
