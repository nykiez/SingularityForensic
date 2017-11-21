using Prism.Mef.Modularity;
using Prism.Modularity;

namespace Singularity.Net {
    [ModuleExport(typeof(NetModule))]
    public class NetModule : IModule {

        public void Initialize() {
            
        }
    }
}
