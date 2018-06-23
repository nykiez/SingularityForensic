using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    public interface IPropertyDescriptor {
        /// <summary>
        /// 成员名;
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 显示名;
        /// </summary>
        string DisplayName { get; }
        /// <summary>
        /// 值;
        /// </summary>
        object Value { get; }
        /// <summary>
        /// 成员大小;
        /// </summary>
        int MemberSize { get; }
        string GroupName { get; }
    }

    //public abstract class PropertyDescriptorBase : PropertyDescriptor,IPropertyDescriptor {
    //    public PropertyDescriptorBase() {

    //    }
    //    public IPropertyDescriptor PropertyDescriptorInfo { get; }
    //}
}
