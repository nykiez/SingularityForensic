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
    [Export(typeof(IFolderBrowserViewModelFactory))]
    public class FolderBrowserViewModelFactory : IFolderBrowserViewModelFactory {
        public IFolderBrowserViewModel CreateNew(IPartition part) {
            var vm = new FolderBrowserViewModel(part);
            PubEventHelper.GetEvent<FolderBrowserViewModelCreatedEvent>().Publish(vm);
            return vm;
        }
    }
}
