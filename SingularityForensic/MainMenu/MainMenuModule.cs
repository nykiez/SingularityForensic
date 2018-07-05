using Prism.Modularity;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Shell;
using Prism.Mef.Modularity;

namespace SingularityForensic.MainMenu {
    [ModuleExport(typeof(MainMenuModule))]
    public class MainMenuModule : IModule {
        public void Initialize() {
            RegionHelper.RegisterViewWithRegion(Contracts.Shell.Constants.MenuRegion, typeof(Views.MainMenu));
        }
    }
}
