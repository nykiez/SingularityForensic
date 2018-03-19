using Prism.Commands;
using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing.Events;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.MainPage;
using SingularityForensic.Contracts.Shell;
using SingularityForensic.Contracts.Splash.Events;
using SingularityForensic.Contracts.TreeView;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;

namespace SingularityForensic.Casing {
    [ModuleExport(typeof(CaseModule))]
    public class CaseModule : IModule {
        public void Initialize() {
            PubEventHelper.GetEvent<SplashMessageEvent>().
                Publish(LanguageService.FindResourceString(Constants.CaseModuleBeingLoaded));

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

                var unit = new TreeUnit(Contracts.Casing.Constants.CaseEvidenceUnit, csFile ) { Label = csFile.Name };
                try {
                    //设定上下文菜单;
                    unit.ContextCommands = new ObservableCollection<CommandItem> { 
                        //显示案件文件属性;
                        new CommandItem {
                            //Command = new DelegateCommand(() => ShowCaseFileProperty(csFile)),
                            CommandName = LanguageService.Current?.FindResourceString("Properties")
                        }
                    };
                }
                catch(Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                    MsgBoxService.ShowError(ex.Message);
                }
                nodeService.AddUnit(null,unit);
            },Prism.Events.ThreadOption.UIThread);

            //订阅案件加载事件;
            PubEventHelper.GetEvent<CaseLoadedEvent>().Subscribe(cs => {
                ServiceProvider.Current.GetInstance<IShellService>()?.SetTitle(cs.CaseName);
            });

            //订阅案件关闭事件;
            PubEventHelper.Subscribe<CaseUnloadedEvent>(() => {
                //清空Tab;
                documentService?.Value?.CloseAllTabs();
                //清空树形;
                nodeService?.Value?.ClearNodes();
                //重置标题;
                ShellService.Current?.SetTitle(string.Empty);
            });


            
        }
        
    }
}
