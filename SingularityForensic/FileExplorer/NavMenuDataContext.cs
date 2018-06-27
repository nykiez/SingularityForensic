using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.FileExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer {
    class NavMenuDataContext : ExtensibleObject,INavMenuDataContext {
        public NavMenuDataContext() {
            UIObject = ViewProvider.CreateView(Constants.NavMenuView, NavMenuViewModel);
        }

        public INavMenuViewModel NavMenuViewModel { get; } = new NavMenuViewModel();

        public object UIObject { get; }

        
    }
}
