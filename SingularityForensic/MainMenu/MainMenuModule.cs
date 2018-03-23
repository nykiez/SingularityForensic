using Prism.Modularity;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Shell;
using Prism.Mef.Modularity;

namespace SingularityForensic.MainMenu {
    [ModuleExport(typeof(MainMenuModule))]
    public class MainMenuModule : IModule {
        public void Initialize() {
            RegionHelper.RegisterViewWithRegion(RegionNames.MenuRegion, typeof( Views.MainMenu));
        }
    }
}
