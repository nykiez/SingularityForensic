using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            vm.AddContextCommand(FolderBrowserCommandItemFactory.CreateViewFileCommandItem(vm));
            vm.AddContextCommand(FolderBrowserCommandItemFactory.CreateOpenFileWithCommandItem(vm));
            vm.AddContextCommand(FolderBrowserCommandItemFactory.CreateNavigateCommandItem(vm));
            vm.AddContextCommand(FolderBrowserCommandItemFactory.CreateComputeHashCommandItem(vm));
        }
    }
}
