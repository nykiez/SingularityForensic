using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.PropertyGrid {
    public interface IPropertyGridDataContext:IUIObjectProvider {
        /// <summary>
        /// 数据提供者;
        /// </summary>
        ICustomMemberDecriptor Descriptor { get; set; }
        /// <summary>
        /// 选定的成员信息;
        /// </summary>
        IMemberInfo SelectedMemberInfo { get; }
        event EventHandler SelectedMemberInfoChanged;
    }

    public interface IPropertyGridDataContextFactory {
        IPropertyGridDataContext CreateNew();
    }
}
