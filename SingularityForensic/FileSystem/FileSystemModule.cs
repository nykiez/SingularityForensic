using Prism.Modularity;
using Prism.Mef.Modularity;
using SingularityForensic.Contracts.FileSystem;

namespace SingularityForensic.FileSystem {
    [ModuleExport(typeof(FileSystemModule))]
    public class FileSystemModule : IModule {
        public void Initialize() {
            FileSystemService.Current?.Initialize();
            FileService.Current?.Initialize();
        }
    }
}
