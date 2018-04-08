using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    /// <summary>
    /// 分区类型描述单位;
    /// </summary>
    public interface IPartitionTypeDescripter {
        /// <summary>
        /// GUID;
        /// </summary>
        string GUID { get; }

        /// <summary>
        /// 类型名;
        /// </summary>
        string TypeName { get; }
    }
}
