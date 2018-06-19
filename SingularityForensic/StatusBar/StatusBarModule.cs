using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.StatusBar;

namespace SingularityForensic.StatusBar {
    [ModuleExport(typeof(StatusBarModule))]
    public class StatusBarModule : IModule {
        public void Initialize() {
            RegionHelper.RegisterViewWithRegion(Contracts.Shell.Constants.StatusBarRegion, typeof(IStatusBar));
            ServiceProvider.Current.GetInstance<IStatusBarService>().Initialize();

#if DEBUG
            StatusBarService.Current.Report("dada");
#endif
        }
    }
}
