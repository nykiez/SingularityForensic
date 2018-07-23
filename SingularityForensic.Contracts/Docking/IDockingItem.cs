using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Docking {
    /// <summary>
    /// 停靠基契约;
    /// </summary>
    public interface IDockingItem {
        /// <summary>
        /// 唯一标识;
        /// </summary>
        string GUID { get; }
    }
}
