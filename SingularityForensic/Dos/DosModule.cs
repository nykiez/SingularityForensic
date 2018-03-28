using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Hex.Events;

namespace SingularityForensic.Dos {
    [ModuleExport(typeof(DosModule))]
    public class DosModule : IModule {

        public void Initialize() {
            _dosUIService = ServiceProvider.Current?.GetInstance<DosUIService>();
            _dosUIService?.RegisterEvents();
        }

        private DosUIService _dosUIService;
    }
}
