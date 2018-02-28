using Prism.Mef.Modularity;
using Prism.Modularity;

namespace SingularityForensic.Previewers {
    [ModuleExport(typeof(DefaultPreviewerModule))]
    public class DefaultPreviewerModule : IModule {
        public void Initialize() {
            
        }
    }
}
