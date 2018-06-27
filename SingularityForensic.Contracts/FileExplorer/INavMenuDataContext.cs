using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer {
    /// <summary>
    /// 导航菜单上下文;
    /// </summary>
    public interface INavMenuDataContext : IUIObjectProvider,IExtensible{
        INavMenuViewModel NavMenuViewModel { get;}
    }
}
