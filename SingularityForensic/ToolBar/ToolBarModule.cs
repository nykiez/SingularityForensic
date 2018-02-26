using Prism.Mef.Modularity;
using Prism.Modularity;
using Singularity.Contracts.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Modules.ToolBar
{
    [ModuleExport(typeof(ToolBarModule))]
    public class ToolBarModule : IModule {
        public void Initialize() {
            RegionHelper.RegisterViewWithRegion(MainPage.RegionNames.ToolBarRegion, typeof(Views.ToolBar));
        }


    }
}
