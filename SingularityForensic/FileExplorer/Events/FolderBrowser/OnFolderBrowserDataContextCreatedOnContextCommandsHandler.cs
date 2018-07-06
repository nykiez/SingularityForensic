using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using System;
using System.ComponentModel.Composition;

namespace SingularityForensic.FileExplorer.Events {
    /// <summary>
    /// 分区资源管理器加入时加入基础右键菜单;
    /// </summary>
    [Export(typeof(IFolderBrowserDataContextCreatedEventHandler))]
    public class OnFolderBrowserDataContextCreatedOnContextCommandsHandler : IFolderBrowserDataContextCreatedEventHandler {
        public int Sort => 0;

        public bool IsEnabled => true;

        public void Handle(IFolderBrowserDataContext dataContext) {
            if(dataContext == null) {
                throw new ArgumentNullException(nameof(dataContext));
            }
            var vm = dataContext.FolderBrowserViewModel;
            vm.AddContextCommand(FolderBrowserCommandItemFactory.CreateSaveAsFileCommandItem(vm));
            vm.AddContextCommand(FolderBrowserCommandItemFactory.CreateSaveCheckedFilesCommandItem(vm));
            vm.AddContextCommand(FolderBrowserCommandItemFactory.CreateCheckCommandItem(vm));
            vm.AddContextCommand(FolderBrowserCommandItemFactory.CreateUnCheckCommandItem(vm));
            vm.AddContextCommand(FolderBrowserCommandItemFactory.CreateCheckAllCommandItem(vm));
            vm.AddContextCommand(FolderBrowserCommandItemFactory.CreateViewFileCommandItem(vm));
            vm.AddContextCommand(FolderBrowserCommandItemFactory.CreateNavigateCommandItem(vm));
        }
    }
}
