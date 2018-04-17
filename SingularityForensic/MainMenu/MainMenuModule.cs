using Prism.Modularity;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Shell;
using Prism.Mef.Modularity;

namespace SingularityForensic.MainMenu {
    [ModuleExport(typeof(MainMenuModule))]
    public class MainMenuModule : IModule {
        public void Initialize() {
            RegionHelper.RegisterViewWithRegion(Constants.MenuRegion, typeof( Views.MainMenu));
        }
    }
}
