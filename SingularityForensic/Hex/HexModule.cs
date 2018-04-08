using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Helpers;
using System.ComponentModel.Composition;

namespace SingularityForensic.Hex {
    [ModuleExport(typeof(HexModule))]
    public class HexModule : IModule {
        public void Initialize() {
            PubEventHelper.
                GetEvent<Contracts.Splash.Events.SplashMessageEvent>().
                Publish("HexModule being loaded");

            _uiService = new HexUIService();
            _uiService.Initialize();
        }

        private HexUIService _uiService;
    }
}
