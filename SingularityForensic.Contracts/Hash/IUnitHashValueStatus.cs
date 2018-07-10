using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Hash {
    /// <summary>
    /// 单元的哈希值状态类型;
    /// </summary>
    public interface IUnitHashValueStatus {
        /// <summary>
        /// 哈希值(十六进制表示);
        /// </summary>
        string Value { get; }

        /// <summary>
        /// 名称;
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 哈希类型;
        /// </summary>
        string HasherGUID { get; }

        /// <summary>
        /// 单元类型;用于区分文件的哈希值,哈希集的哈希值等;
        /// </summary>
        string StatusType { get; }
    }
}
