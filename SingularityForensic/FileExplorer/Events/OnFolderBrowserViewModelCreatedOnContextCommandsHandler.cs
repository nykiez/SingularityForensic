using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using System.ComponentModel.Composition;

namespace SingularityForensic.FileExplorer.Events {
    /// <summary>
    /// 分区加入时加入基础右键菜单;
    /// </summary>
    [Export(typeof(IFolderBrowserViewModelCreatedEventHandler))]
    public class OnFolderBrowserViewModelCreatedOnContextCommandsHandler : IFolderBrowserViewModelCreatedEventHandler {
        public int Sort => 0;

        public bool IsEnabled => true;

        public void Handle(IFolderBrowserViewModel vm) {
            vm.AddContextCommand(FolderBrowserCommandItemFactory.CreateSaveAsFileCommandItem(vm));
            vm.AddContextCommand(FolderBrowserCommandItemFactory.CreateSaveCheckedFilesCommandItem(vm));
            vm.AddContextCommand(FolderBrowserCommandItemFactory.CreateCheckCommandItem(vm));
            vm.AddContextCommand(FolderBrowserCommandItemFactory.CreateUnCheckCommandItem(vm));
            vm.AddContextCommand(FolderBrowserCommandItemFactory.CreateCheckAllCommandItem(vm));
            vm.AddContextCommand(FolderBrowserCommandItemFactory.CreateViewFileCommandItem(vm));
            vm.AddContextCommand(FolderBrowserCommandItemFactory.CreateNavigateCommandItem(vm));
            vm.AddContextCommand(FolderBrowserCommandItemFactory.CreateComputeHashCommandItem(vm));
        }
    }
}
