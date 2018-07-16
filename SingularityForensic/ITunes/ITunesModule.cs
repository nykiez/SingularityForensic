using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.Common;

namespace SingularityForensic.ITunes {
    [ModuleExport(typeof(ITunesModule))]
    public class ITunesModule : IModule {
        
        public void Initialize() {
            ServiceProvider.GetInstance<ITunesBackUpService>()?.Initialize();
        }
    }
}
