using Prism.Mef.Modularity;
using Prism.Modularity;
using Singularity.Contracts.Helpers;
using SingularityForensic.Modules.MainPage.Views;
using SingularityForensic.Modules.Shell;
using SingularityForensic.Views.Shell;

namespace SingularityForensic {
    [ModuleExport(typeof(MainModule))]
    public class MainModule : IModule {
        public MainModule() {
        }
        
        public void Initialize() {
            RegionHelper.RegisterViewWithRegion(RegionNames.MenuRegion, typeof(MainMenu));
            RegionHelper.RegisterViewWithRegion(RegionNames.MainRegion, typeof(MainPage));

            
            //manager.RegisterViewWithRegion("FSCustomRegion", typeof(CustomIndexSearch));
        }
    }

    
   
}
