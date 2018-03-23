using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.Helpers;

namespace SingularityForensic.ToolBar {
    [ModuleExport(typeof(ToolBarModule))]
    public class ToolBarModule : IModule {
        public void Initialize() {
            RegionHelper.RegisterViewWithRegion(MainPage.Constants.ToolBarRegion, typeof(Views.ToolBar));
        }


    }
}
