using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Splash.Events;
using System;

namespace SingularityForensic.FileExplorer {
    [ModuleExport(typeof(FileExplorerModule))]
    public class FileExplorerModule:IModule {
        public void Initialize() {
            CommonEventHelper.GetEvent<SplashMessageEvent>().
                Publish(LanguageService.FindResourceString(Constants.FileExploerLoading));

            try {
                CommonEventHelper.GetEvent<FileExplorerModuleLoadingEvent>().Publish();
                CommonEventHelper.PublishEventToHandlers(GenericServiceStaticInstances<IFileExplorerModuleLoadingEventHandler>.Currents);
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
            }
            
        }

        
        
        
    }
}
