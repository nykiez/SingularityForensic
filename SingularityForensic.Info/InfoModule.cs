using Prism.Mef.Modularity;
using Prism.Modularity;

namespace SingularityForensic.Info {
    [ModuleExport(typeof(InfoModule))]
    public class InfoModule : IModule {
        public void Initialize() {
            //throw new NotImplementedException();
        }
    }
}
