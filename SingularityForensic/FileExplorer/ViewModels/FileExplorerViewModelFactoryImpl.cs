using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.FileExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace SingularityForensic.FileExplorer.ViewModel {
    [Export(typeof(IFileExplorerViewModelFactory))]
    public class FileExplorerViewModelFactoryImpl : IFileExplorerViewModelFactory {
        [ImportingConstructor]
        public FileExplorerViewModelFactoryImpl([ImportMany]IEnumerable<IFolderBrowserViewModelCreatedEventHandler> folderBrowserViewModelCreatedEventHandlers) {
            _folderBrowserViewModelCreatedEventHandlers = folderBrowserViewModelCreatedEventHandlers;
        }

        private IEnumerable<IFolderBrowserViewModelCreatedEventHandler> _folderBrowserViewModelCreatedEventHandlers;

        public IFolderBrowserViewModel CreateFolderBrowserViewModel(IHaveFileCollection haveFileCollection) {
            var vm = new FolderBrowserViewModel(haveFileCollection);
            try {
                PubEventHelper.PublishEventToHandlers(vm as IFolderBrowserViewModel, _folderBrowserViewModelCreatedEventHandlers);
                PubEventHelper.GetEvent<FolderBrowserViewModelCreatedEvent>().Publish(vm);
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
            }

            try {
                vm.Initialize();
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
            }
            
            return vm;
        }
        public IPartitionsBrowserViewModel CreatePartitionsBrowserViewModel(IDevice device) {
            var vm = new PartitionsBrowserViewModel(device);
            PubEventHelper.GetEvent<PartitionsBrowserViewModelCreatedEvent>().Publish(vm);
            return vm;
        }
    }
}
