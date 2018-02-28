using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.ToolBar
{
    [ModuleExport(typeof(ToolBarModule))]
    public class ToolBarModule : IModule {
        public void Initialize() {
            RegionHelper.RegisterViewWithRegion(MainPage.RegionNames.ToolBarRegion, typeof(Views.ToolBar));
        }


    }
}
