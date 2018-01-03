using CDFCUIContracts.Commands;
using Prism.Commands;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Singularity.Contracts.Case;
using Singularity.Contracts.Case.Events;
using Singularity.Contracts.Helpers;
using Singularity.Contracts.MainPage;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;

namespace Singularity.UI.Case {
    [ModuleExport(typeof(CaseModule))]
    public class CaseModule : IModule {
        public void Initialize() {
            RegisterEvents();   
        }


        [Import]
        private Lazy<INodeService> nodeService;

        [Import]
        private Lazy<IDocumentTabService> documentService;

        private void RegisterEvents() {
            //订阅案件关闭事件;
            PubEventHelper.Subscribe<CloseCaseEvent>(() => {
                nodeService?.Value?.ClearNodes();
                documentService?.Value?.CloseAllTab();
            });

            
        }

        
    }
}
