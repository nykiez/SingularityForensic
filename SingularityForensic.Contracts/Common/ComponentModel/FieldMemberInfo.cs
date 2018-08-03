using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common.ComponentModel {

    /// <summary>
    /// 字段自行描述的单位;
    /// </summary>
    public class FieldMemberInfo : IMemberInfo {
        public FieldMemberInfo(FieldInfo fieldInfo) {
            this.FieldInfo = fieldInfo ?? throw new ArgumentNullException(nameof(fieldInfo));
        }

        public FieldInfo FieldInfo { get; }
        /// <summary>
        /// 成员名
        /// </summary>
        public string MemberName { get; internal set; }
        /// <summary>
        /// 字段大小;
        /// </summary>
        public int MemberSize { get; internal set; }
        /// <summary>
        /// 值;
        /// </summary>
        public object Value { get; internal set; }

        public Type MemberType => FieldInfo.FieldType;

        /// <summary>
        /// 显示名;
        /// </summary>
        public string DisplayName { get; internal set; }

    }
}
