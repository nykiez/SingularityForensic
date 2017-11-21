using Prism.Mef.Modularity;
using Prism.Modularity;

namespace Singularity.UI.FileSystem.Android {
    [ModuleExport(typeof(AndroidFSModule))]
    public class AndroidFSModule : IModule {
        public void Initialize() {
            
        }
    }
}
