using Prism.Mef.Modularity;
using Prism.Modularity;

namespace SingularityForensic.Net {
    [ModuleExport(typeof(NetModule))]
    public class NetModule : IModule {

        public void Initialize() {
            
        }
    }
}
