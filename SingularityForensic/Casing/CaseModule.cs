using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Casing.Events;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Splash.Events;

namespace SingularityForensic.Casing {
    [ModuleExport(typeof(CaseModule))]
    public class CaseModule : IModule {
        public void Initialize() {
            CommonEventHelper.GetEvent<SplashMessageEvent>().
                Publish(LanguageService.FindResourceString(Constants.CaseModuleBeingLoaded));

            CommonEventHelper.PublishEventToHandlers(GenericServiceStaticInstances<ICaseModuleLoadingEventHandler>.Currents);

            _caseUiService = ServiceProvider.Current?.GetInstance<ICaseUIService>();
            _caseUiService?.Initialize();
        }
        
        private ICaseUIService _caseUiService;
        
    }


}
