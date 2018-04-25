using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Splash.Events;

namespace SingularityForensic.Document {
    [ModuleExport(typeof(DocumentModule))]
    public class DocumentModule : IModule {
        public void Initialize() {
            PubEventHelper.GetEvent<SplashMessageEvent>().Publish(LanguageService.FindResourceString(Constants.DocumentModule_BeingLoaded));
            DocumentService.MainDocumentService.Initialize();
        }

        
    }
}
