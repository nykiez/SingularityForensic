using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
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

            _fileExplorerUIService = ServiceProvider.Current?.GetInstance<IFileExplorerUIReactService>();
            _fileExplorerUIService.Initialize();
        }

        private IFileExplorerUIReactService _fileExplorerUIService;
        
        
    }
}
