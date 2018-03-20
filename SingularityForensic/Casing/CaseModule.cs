using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Splash.Events;

namespace SingularityForensic.Casing {
    [ModuleExport(typeof(CaseModule))]
    public class CaseModule : IModule {
        public void Initialize() {
            PubEventHelper.GetEvent<SplashMessageEvent>().
                Publish(LanguageService.FindResourceString(Constants.CaseModuleBeingLoaded));

            _caseUiService = ServiceProvider.Current?.GetInstance<CaseUIService>();
            _caseUiService?.RegisterEvents();
        }
        
        private CaseUIService _caseUiService;
        
    }


}
