using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.ToolBar;

namespace SingularityForensic.ToolBar {
    [ModuleExport(typeof(ToolBarModule))]
    public class ToolBarModule : IModule {
        public void Initialize() {
            RegionHelper.RegisterViewWithRegion(Contracts.Shell.Constants.ToolBarRegion, typeof(Views.ToolBar));
            ServiceProvider.GetInstance<IToolBarService>().Initialize();
        }
        
    }
}
