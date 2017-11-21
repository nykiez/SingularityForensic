using CDFC.Parse.ITunes.Models;
using CDFCMessageBoxes.MessageBoxes;
using CDFCUIContracts.Models;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Singularity.UI.Case.Events;
using Singularity.UI.Case.Models;
using Singularity.UI.Info.TabModels;
using Singularity.UI.ITunes.Global.Services;
using Singularity.UI.ITunes.Models;
using SingularityForensic.Helpers;
using SingularityForensic.Modules.MainPage.Global.Events;
using SingularityForensic.Modules.MainPage.Global.Services;
using SingularityForensic.Modules.MainPage.Models;
using System.Linq;
using System.Text;
using System.Windows;
using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.ITunes {
    [ModuleExport(typeof(ITunesModule))]
    public class ITunesModule : IModule {
        public void Initialize() {
            RegisterEvents();
        }

        private void RegisterEvents() {
            PubEventHelper.GetEvent<CaseFileAddedEvent<ITunesBackUpCaseFile>>()?.Subscribe(csFile => {
                var frService = ServiceLocator.Current.GetInstance<ForensicService>();
                if (CDFCMessageBox.Show("是否立即取证?", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                    frService?.StartForensic(csFile);
                }
            });

            //订阅节点附加事件;
            PubEventHelper.GetEvent<TreeNodeAdded<CaseFileUnit<ITunesBackUpCaseFile>>>()?.Subscribe(cfUnit => {
                var itbFile = cfUnit.Data;
                cfUnit.ContextCommands.Add(new CDFCUIContracts.Commands.CommandItem {
                    CommandName = FindResourceString("StartForensic"),
                    Command = new DelegateCommand(() => {
                        var fService = ServiceLocator.Current.GetInstance<ForensicService>();
                        fService?.StartForensic(itbFile);
                    })
                });
            });

            PubEventHelper.GetEvent<TreeNodeClickEvent>()?.Subscribe(unit => {
                if(unit is PinTreeUnit pinUnit) {
                    var tabService = ServiceLocator.Current.GetInstance<IDocumentTabService>();
                    var cFileUnit = pinUnit.GetParent<ExtTreeUnit<ITunesBackUpCaseFile>>();
                    //预计分配TabKind;
                    var tabPinKind = $"{cFileUnit?.Data.InterLabel}{PinTreeUnit.PinSpliter}{pinUnit.ContentId}";

                    var preTab = tabService?.CurrentTabs.FirstOrDefault(p => p is PinTabModel pinTab && pinTab.ContentId == tabPinKind);

                    if (preTab != null){
                        tabService.ChangeSelectedTab(preTab);
                        return;
                    }

                    if (pinUnit.ContentId == PinKindsDefinitions.ForensicClassBasic) {
                        var iBasicUnit = pinUnit as ExtTreeUnit<IOSBasicStruct?>;
                        
                        var basic = iBasicUnit?.Data;
                        if(basic != null) {
                            var sb = new StringBuilder();
                            foreach (var field in basic.GetType().GetFields()) {
                                sb.AppendLine($"{FindResourceString(field.Name)}:{field.GetValue(basic)}");
                            }
                            var basicTabModel = new BasicTextTabModel(tabPinKind, sb.ToString());

                            basicTabModel.Title = cFileUnit?.Data.Name + "-" + PinKindsDefinitions.GetClassLabel(PinKindsDefinitions.ForensicClassBasic);

                            tabService?.AddTab(basicTabModel);
                        }
                    }
                }
            });
            //ServiceLocator.Current.GetInstance<Fsnode>
        }
    }
}
