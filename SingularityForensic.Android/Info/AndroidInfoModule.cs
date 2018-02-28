using CDFCMessageBoxes.MessageBoxes;
using CDFCUIContracts.Models;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Android.FileSystem.Models;
using SingularityForensic.Contracts.Case;
using SingularityForensic.Contracts.Case.Events;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Info;
using SingularityForensic.Contracts.MainPage;
using SingularityForensic.Contracts.MainPage.Events;
using SingularityForensic.Contracts.TabControl;
using SingularityForensic.Contracts.TreeView;
using SingularityForensic.Android.Info.Models;
using SingularityForensic.Android.Info.Global.Services;
using SingularityForensic.Android.Info.TabModels;
using SingularityForensic.Info.Models;
using SingularityForensic.Info.Models.Chating;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using static CDFCCultures.Managers.ManagerLocator;


namespace SingularityForensic.Android.Info {
    [ModuleExport(typeof(AndroidInfoModule))]
    public class AndroidInfoModule : IModule {
        public void Initialize() {
            ////订阅镜像文件附加事件;
            //PubEventHelper.GetEvent<CaseEvidenceAddedEvent<AndroidDeviceCaseEvidence>>()?.Subscribe(adCFile => {
            //    var frService = ServiceProvider.Current.GetInstance<ForensicService>();
            //    //询问是否开始取证;
            //    if (CDFCMessageBox.Show(FindResourceString("ConfirmLaunchForensic"),
            //            MessageBoxButton.YesNo)
            //            == MessageBoxResult.Yes) {
            //        frService?.StartForensic(adCFile);
            //    }
            //});

            ////订阅镜像文件加载事件;
            //PubEventHelper.GetEvent<CaseEvidenceLoadedEvent<AndroidDeviceCaseEvidence>>()?.Subscribe(adCFile => {
            //    var frService = ServiceProvider.Current.GetInstance<ForensicService>();
            //    //加载取证分析节点;
            //    frService?.LoadForensicUnit(adCFile);
            //});

            ////订阅节点附加事件;
            //PubEventHelper.GetEvent<TreeNodeAdded<CaseEvidenceUnit<AndroidDeviceCaseEvidence>>>()?.Subscribe(cfUnit => {
            //    var adcFile = cfUnit.Evidence;
            //    cfUnit.ContextCommands.Add(new CDFCUIContracts.Commands.CommandItem {
            //        CommandName = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("StartForensic"),
            //        Command = new DelegateCommand(() => {
            //            var fService = ServiceProvider.Current.GetInstance<ForensicService>();
            //            fService?.StartForensic(adcFile);
            //        })
            //    });
            //});
            
            ////订阅节点点击事件;
            //PubEventHelper.GetEvent<TreeNodeClickEvent>()?.Subscribe(unit => {
            //    var fsTabService = ServiceProvider.Current.GetInstance<IDocumentTabService>();
            //    if(fsTabService == null) {
            //        RemainingMessageBox.Tell("FSTabService can't be null!");
            //        return;
            //    }
            //    if(unit is TreeUnit biUnit) {
            //        var advUnit = biUnit.GetParent<CaseEvidenceUnit<AndroidDeviceCaseEvidence>>();
            //        if (advUnit == null) {
            //            //RemainingMessageBox.Tell("advUnit can't be null!");
            //            return;
            //        }

            //        var advCaseFile = advUnit.Evidence;

            //        if (unit is IHavePinKind havePinKind) {
            //            //分配预计Tab标识;
            //            var tabPinKind = $"{advCaseFile.InterLabel}{PinTreeUnit.PinSpliter}{havePinKind.ContentId}";
            //            //确定是否已经点击过此节点;
            //            var preTabModel = fsTabService.CurrentTabs.FirstOrDefault(p =>
            //            p is IHavePinKind extTabModel && extTabModel.ContentId == tabPinKind);

            //            //若点击过,则跳转至此tab;
            //            if (preTabModel != null) {
            //                fsTabService.ChangeSelectedTab(preTabModel);
            //            }
            //            //否则生成一个新的Tab;
            //            else {
            //                if (unit is IHaveData<IEnumerable<ForensicInfoDbModel>> haveDbModels) {
            //                    TabModel tabModel = null;
            //                    //根据PinKind进行匹配;
            //                    bool MatchWithPinKind<TDbModel>() where TDbModel : ForensicInfoDbModel {
            //                        var pinKind = PinKindsDefinitions.GetPinKind<TDbModel>();
            //                        //根据PinKind进行匹配;
            //                        //若为非对话节点即可;
            //                        if (unit is MultiDbModelsUnit<TDbModel> dbUnit &&
            //                            (!(typeof(TDbModel).GetInterfaces()?.Contains(typeof(ITalkLog))??false) ||
            //                            !havePinKind.ContentId.Contains("id="))) {

            //                            tabModel = new AndroidGridTabModel<TDbModel>(tabPinKind, 
            //                                (unit as MultiDbModelsUnit<TDbModel>).Data) {
            //                                Title = $"{advCaseFile.Name}-{FindResourceString(pinKind)}"
            //                            };
            //                            return true;
            //                        }
            //                        return false;
            //                    };

            //                    //根据PinKind进行前匹配(针对对话分拣节点)
            //                    bool MatchTalkWithPrePin<TDbModel>() where TDbModel : ForensicInfoDbModel, ITalkLog {
            //                        var pinKind = PinKindsDefinitions.GetPinKind<TDbModel>();
                                    
            //                        if (havePinKind.ContentId?.Contains(pinKind) ?? false) {
            //                            if (typeof(TDbModel).GetInterfaces()?.Contains(typeof(ITalkLog)) ?? false) {
            //                                tabModel = new AndroidTalkTabModel<TDbModel>(tabPinKind, (unit as MultiDbModelsUnit<TDbModel>).Data) {
            //                                    Title = $"{advCaseFile.Name}-{FindResourceString(pinKind)}"
            //                                };
            //                                return true;
            //                            }
            //                        }
            //                        return false;
            //                    }
                                
            //                    if( MatchWithPinKind<SmsDbModel>() || MatchWithPinKind<CalllogDbModel>()
            //                        || MatchWithPinKind<ContactDbModel>()
            //                        || MatchWithPinKind<GroupMemberDbModel>() || MatchWithPinKind<GroupMsgDbModel>()
            //                        || MatchWithPinKind<FriendInfoDbModel>() || MatchWithPinKind<FriendMsgDbModel>()
            //                        //进行对话类信息节点匹配;
            //                        || MatchTalkWithPrePin<SmsDbModel>() || MatchTalkWithPrePin<GroupMsgDbModel>()
            //                        || MatchTalkWithPrePin<FriendMsgDbModel>()) {
            //                        if (tabModel != null) {
            //                            fsTabService.AddTab(tabModel);
            //                        }
            //                    }

                                
            //                }
            //            }

            //        }
            //    }
            //});
            
        }
    }
}
