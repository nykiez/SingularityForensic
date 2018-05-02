using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Hex;

namespace SingularityForensic.Hex {
    [ModuleExport(typeof(HexModule))]
    public class HexModule : IModule {
        public void Initialize() {
            PubEventHelper.
                GetEvent<Contracts.Splash.Events.SplashMessageEvent>().
                Publish(LanguageService.FindResourceString(Constants.SplashText_HexModuleBeingLoaded));
            
            HexUIService.Current.Initialize();
        }

        private IHexUIService _hexUIService;
    }
}
