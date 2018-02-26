using Prism.Mef.Modularity;
using Prism.Modularity;

namespace Singularity.Previewers {
    [ModuleExport(typeof(DefaultPreviewerModule))]
    public class DefaultPreviewerModule : IModule {
        public void Initialize() {
            
        }
    }
}
