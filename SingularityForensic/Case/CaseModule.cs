using CDFCUIContracts.Commands;
using Prism.Commands;
using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Case;
using SingularityForensic.Contracts.Case.Events;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.MainPage;
using SingularityForensic.Contracts.TreeView;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;

namespace SingularityForensic.Case {
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
            //当案件文件被加载时,向树形节点中加入案件文件;
            PubEventHelper.GetEvent<CaseEvidenceLoadedEvent>().Subscribe(csFile => {
                var nodeService = ServiceProvider.Current?.GetInstance<INodeService>();
                if(nodeService == null) {
                    LoggerService.Current?.WriteCallerLine($"{nameof(nodeService)} can't be null.");
                    return;
                }

                var unit = new TreeUnit(Contracts.Case.Constants.CaseEvidenceUnit, csFile ) { Label = csFile.Name };
                try {
                    //设定上下文菜单;
                    unit.ContextCommands = new ObservableCollection<ICommandItem> { 
                        //显示案件文件属性;
                        new CommandItem {
                            //Command = new DelegateCommand(() => ShowCaseFileProperty(csFile)),
                            CommandName = LanguageService.Current?.FindResourceString("Properties")
                        }
                    };
                }
                catch {

                }
                nodeService.AddUnit(null,unit);
            },Prism.Events.ThreadOption.UIThread);

            //订阅案件关闭事件;
            PubEventHelper.Subscribe<CloseCaseEvent>(() => {
                nodeService?.Value?.ClearNodes();
                documentService?.Value?.CloseAllTab();
            });

            
        }
        
    }
}
