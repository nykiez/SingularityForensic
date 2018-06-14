using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Splash.Events;
using System;
using System.Diagnostics;
using System.IO;
using static CDFCUIContracts.Helpers.ApplicationHelper;

namespace SingularityForensic.FileExplorer {
    [ModuleExport(typeof(FileExplorerModule))]
    public class FileExplorerModule:IModule {
        public void Initialize() {
            PubEventHelper.GetEvent<SplashMessageEvent>().
                Publish(LanguageService.FindResourceString(Constants.FileExploerLoading));
            PubEventHelper.PublishEventToHandlers(GenericServiceStaticInstances<IFileExplorerModuleLoadingEventHandler>.Currents);
        }

        
        
        
    }
}
