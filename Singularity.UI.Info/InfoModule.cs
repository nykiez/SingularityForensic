using Prism.Mef.Modularity;
using Prism.Modularity;

namespace Singularity.UI.Info {
    [ModuleExport(typeof(InfoModule))]
    public class InfoModule : IModule {
        public void Initialize() {
            //throw new NotImplementedException();
        }
    }
}
