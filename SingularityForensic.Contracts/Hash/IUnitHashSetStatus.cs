using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Hash {
    /// <summary>
    /// 单元的哈希集状态;
    /// </summary>
    public interface IUnitHashSetStatus {
        /// <summary>
        /// 哈希集集合的GUID;
        /// </summary>
        IEnumerable<string> HashSetGuids { get; }
        /// <summary>
        /// 单元名称;比如,对于文件,此处则为存储文件相对文件系统的路径;
        /// </summary>
        string UnitName { get; }
        /// <summary>
        /// 单元类型;用于区分文件的哈希集状态等;
        /// </summary>
        string StatusType { get; }
    }
}
