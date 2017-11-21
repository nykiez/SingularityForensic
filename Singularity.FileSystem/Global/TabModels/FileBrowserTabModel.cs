using Singularity.UI.FileSystem.ViewModels;
using Singularity.UI.FileSystem.Views;
using SingularityForensic.Modules.MainPage.Models;

namespace Singularity.UI.FileSystem.Global.TabModels {
    public class FileBrowserTabModel : ExtTabModel<FileBrowserViewModel> {
        public FileBrowserTabModel(FileBrowserViewModel vm):base(vm,string.Empty) {
            Content = new FileBrowser() { DataContext = vm };
            Title = vm.Header;
            this.FileBrowserViewModel = vm;
        }
        
        public FileBrowserViewModel FileBrowserViewModel { get; }
        public override void Dispose() {
            FileBrowserViewModel.Dispose();
        }
    }
}
