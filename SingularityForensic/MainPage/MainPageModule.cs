using Prism.Mef.Modularity;
using Prism.Modularity;
using Singularity.Contracts.Helpers;
using SingularityForensic.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.MainPage {
    [ModuleExport(typeof(MainPageModule))]
    public class MainPageModule : IModule {
        public MainPageModule() {
        }

        public void Initialize() {

            RegionHelper.RegisterViewWithRegion(RegionNames.MainRegion, typeof(Views.MainPage));
            //manager.RegisterViewWithRegion("FSCustomRegion", typeof(CustomIndexSearch));
        }
    }
}
