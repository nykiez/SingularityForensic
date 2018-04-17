using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Shell;

namespace SingularityForensic.MainPage {
    [ModuleExport(typeof(MainPageModule))]
    public class MainPageModule : IModule {
        public MainPageModule() {
            
        }

        public void Initialize() {
            RegionHelper.RegisterViewWithRegion(Contracts.Shell.Constants.MainRegion, typeof(Views.MainPage));
            //manager.RegisterViewWithRegion("FSCustomRegion", typeof(CustomIndexSearch));
        }
    }
}
