using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.TabControl;
using SingularityForensic.Controls.FileExplorer.Views;

namespace SingularityForensic.Controls.FileExplorer.ViewModels {
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
