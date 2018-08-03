using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common.ComponentModel {
    /// <summary>
    /// 成员描述契约;
    /// </summary>
    public interface IMemberInfo {
        /// <summary>
        /// 成员名;
        /// </summary>
        string MemberName { get; }
        /// <summary>
        /// 显示名;
        /// </summary>
        string DisplayName { get; }
        /// <summary>
        /// 成员大小;
        /// </summary>
        int MemberSize { get; }
        /// <summary>
        /// 值;
        /// </summary>
        object Value { get; }
        /// <summary>
        /// 成员类型;
        /// </summary>
        Type MemberType { get; }
    }
}
