using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.FileExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer {
    [Export(typeof(IFileExplorerViewModelFactory))]
    public class FileExplorerViewModelFactoryImpl : IFileExplorerViewModelFactory {
        public IFolderBrowserViewModel CreateFolderBrowserViewModel(IPartition part) {
            var vm = new FolderBrowserViewModel(part);
            PubEventHelper.GetEvent<FolderBrowserViewModelCreatedEvent>().Publish(vm);
            return vm;
        }
        public IPartitionsBrowserViewModel CreatePartitionsBrowserViewModel(IDevice device) {
            var vm = new PartitionsBrowserViewModel(device);
            PubEventHelper.GetEvent<PartitionsBrowserViewModelCreatedEvent>().Publish(vm);
            return vm;
        }
    }
}
