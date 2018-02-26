using Prism.Modularity;
using Singularity.Contracts.Helpers;
using SingularityForensic.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mef.Modularity;

namespace SingularityForensic.MainMenu
{
    [ModuleExport(typeof(MainMenuModule))]
    public class MainMenuModule : IModule {
        public void Initialize() {
            RegionHelper.RegisterViewWithRegion(RegionNames.MenuRegion, typeof( Views.MainMenu));
        }
    }
}
