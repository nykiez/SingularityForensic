﻿using CDFC.Parse.ITunes.Models;
using CDFCMessageBoxes.MessageBoxes;
using CDFCUIContracts.Models;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Singularity.Contracts.Case;
using Singularity.Contracts.Case.Events;
using Singularity.Contracts.Common;
using Singularity.Contracts.Helpers;
using Singularity.Contracts.MainPage;
using Singularity.Contracts.MainPage.Events;
using Singularity.Contracts.TabControl;
using Singularity.Contracts.TreeView;
using Singularity.UI.Info.TabModels;
using Singularity.UI.ITunes.Global.Services;
using Singularity.UI.ITunes.Models;
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
            PubEventHelper.GetEvent<CaseEvidenceAddedEvent<ITunesBackUpCaseFile>>()?.Subscribe(csFile => {
                var frService = ServiceProvider.Current.GetInstance<ForensicService>();
                if (CDFCMessageBox.Show("是否立即取证?", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                    frService?.StartForensic(csFile);
                }
            });

            //订阅节点附加事件;
            PubEventHelper.GetEvent<TreeNodeAdded<CaseEvidenceUnit<ITunesBackUpCaseFile>>>()?.Subscribe(cfUnit => {
                var itbFile = cfUnit.Evidence;
                cfUnit.ContextCommands.Add(new CDFCUIContracts.Commands.CommandItem {
                    CommandName = FindResourceString("StartForensic"),
                    Command = new DelegateCommand(() => {
                        var fService = ServiceProvider.Current.GetInstance<ForensicService>();
                        fService?.StartForensic(itbFile);
                    })
                });
            });

            PubEventHelper.Subscribe<CaseFileLoadedEvent<ITunesBackUpCaseFile>, ITunesBackUpCaseFile>(csFile => {
                var frService = ServiceProvider.Current.GetInstance<ForensicService>();
                frService?.LoadForensicUnit(csFile);
            });

            PubEventHelper.GetEvent<TreeNodeClickEvent>()?.Subscribe(unit => {
                if(unit is PinTreeUnit pinUnit) {
                    var tabService = ServiceProvider.Current.GetInstance<IDocumentTabService>();
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
            //ServiceProvider.Current.GetInstance<Fsnode>
        }
    }
}