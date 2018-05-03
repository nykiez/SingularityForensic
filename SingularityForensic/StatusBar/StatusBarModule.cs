using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.StatusBar
{
    [ModuleExport(typeof(StatusBarModule))]
    public class StatusBarModule : IModule {
        public void Initialize() {
            RegionHelper.RegisterViewWithRegion(Contracts.Shell.Constants.StatusBarRegion, typeof(Views.StatusBar));
        }
    }
}
