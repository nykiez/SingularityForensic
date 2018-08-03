using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common.ComponentModel {
    public interface ICustomMemberDescriptor {
        IEnumerable<IMemberInfo> GetMemberInfos();
        /// <summary>
        /// 数据类型;
        /// </summary>
        Type ObjectType { get; }
        /// <summary>
        /// 显示名;
        /// </summary>
        string DisplayName { get; }
    }
}
