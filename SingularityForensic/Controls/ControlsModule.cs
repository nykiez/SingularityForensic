using Prism.Mef.Modularity;
using Prism.Modularity;
using Telerik.Windows.Controls;

namespace SingularityForensic.Controls {
    [ModuleExport(typeof(ControlsModule))]
    public class ControlsModule : IModule {
        public void Initialize() {
            LocalizationManager.Manager = new LanguageServiceToTelerikAdapter();
        }
    }


}
