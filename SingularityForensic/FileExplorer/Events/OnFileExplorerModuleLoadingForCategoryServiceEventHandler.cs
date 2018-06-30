using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer.Events {
    [Export(typeof(IFileExplorerModuleLoadingEventHandler))]
    class OnFileExplorerModuleLoadingForCategoryServiceEventHandler : IFileExplorerModuleLoadingEventHandler {
        public int Sort => 2;

        public bool IsEnabled => true;

        public void Handle() {
            try {

                CategoryNameService.Current.Initialize();
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
            }
        }
    }
}
