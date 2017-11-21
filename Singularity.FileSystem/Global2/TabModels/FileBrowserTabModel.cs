using SingularityForensic.Modules.FileSystem.ViewModels;
using SingularityForensic.Modules.FileSystem.Views;
using System.Windows;

namespace SingularityForensic.Modules.FileSystem.Global.TabModels {
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
