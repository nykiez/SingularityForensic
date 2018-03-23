using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.Common;

namespace SingularityForensic.Drive {
    [ModuleExport(typeof(DriveModule))]
    public class DriveModule : IModule {
        public void Initialize() {
            ServiceProvider.Current?.GetInstance<DriveService>()?.Initialize();
        }
        
        
    }
}
