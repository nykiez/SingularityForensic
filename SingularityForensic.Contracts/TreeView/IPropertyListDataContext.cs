using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Common.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.TreeView {
    /// <summary>
    /// 属性网格上下文;
    /// </summary>
    public interface IPropertyListDataContext:IUIObjectProvider {
        /// <summary>
        /// 数据提供者;
        /// </summary>
        IEnumerable<ICustomMemberDescriptor> CustomMemberDescriptors { get; }
        void AddCustomMemberDescriptor(ICustomMemberDescriptor descriptor);
        /// <summary>
        /// 选定的成员信息;
        /// </summary>
        IMemberInfo SelectedMemberInfo { get; }
        event EventHandler SelectedMemberInfoChanged;
    }

    public interface IPropertyGridDataContextFactory {
        IPropertyListDataContext CreateNew();
    }

    public class PropertyGridDataContextFactory: GenericServiceStaticInstance<IPropertyGridDataContextFactory>{
        public static IPropertyListDataContext CreateNew() => Current.CreateNew();
    }
}
