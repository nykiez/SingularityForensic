using CDFCMessageBoxes.MessageBoxes;
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
        //[Import]
        //IFSNodeService fsNodeService;

        public void Initialize() {
            PubEventHelper.GetEvent<SplashMessageEvent>().
                Publish(LanguageService.FindResourceString(Constants.FileExploerLoading));

            _fileExplorerUIService = ServiceProvider.Current?.GetInstance<FileExplorerUIService>();
            _fileExplorerUIService?.RegisterEvents();
        }

        private FileExplorerUIService _fileExplorerUIService;
        

        private void ViewFile(ViewerProgramMessage e) {
            FileStream targetStream = null;
            try {
                var path = ServiceProvider.Current.GetInstance<ICaseService>()?.CurrentCase.Path;
                if (!System.IO.Directory.Exists($"{path}/Temp")) {
                    System.IO.Directory.CreateDirectory($"{path}/Temp");
                }
                var oriStream = e.FStream;
                targetStream = File.Create($"{path}/Temp/{e.FileName}");
                oriStream.CopyTo(targetStream);
                Process.Start(e.ViewerPath, $"{path}/Temp/{ e.FileName}");
            }
            catch (Exception ex) {
                LoggerService.Current?.WriteCallerLine($"{ex.Message}");
                AppInvoke(() => {
                    RemainingMessageBox.Tell($"{LanguageService.FindResourceString("FailedToExtractFile")}:{ex.Message}");
                });
            }
            finally {
                targetStream?.Close();
            }
        }
    }
}
