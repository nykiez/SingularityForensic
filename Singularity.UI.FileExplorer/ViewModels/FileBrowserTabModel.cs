using Singularity.Contracts.FileExplorer;
using Singularity.Contracts.TabControl;
using Singularity.UI.FileExplorer.Views;

namespace Singularity.UI.FileExplorer.ViewModels {
    public class FileBrowserTabModel : ExtTabModel<IFileBrowserDataContext> {
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
