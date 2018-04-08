using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.Helpers;

namespace SingularityForensic.MainPage {
    [ModuleExport(typeof(MainPageModule))]
    public class MainPageModule : IModule {
        public MainPageModule() {
            
        }

        public void Initialize() {
            RegionHelper.RegisterViewWithRegion(Shell.RegionNames.MainRegion, typeof(Views.MainPage));
            //manager.RegisterViewWithRegion("FSCustomRegion", typeof(CustomIndexSearch));
        }
    }
}
