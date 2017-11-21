using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.ServiceLocation;
using System.Linq;
using CDFC.Info.Android;
using Singularity.UI.Android.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using static CDFCCultures.Managers.ManagerLocator;
using Singularity.UI.Info.Global.Services;
using EventLogger;
using Singularity.Interfaces;
using CDFCUIContracts.Models;
using Singularity.UI.FileSystem.Models;
using SingularityForensic.Modules.MainPage.Models;
using Singularity.UI.Case.Contracts;
using Singularity.UI.Case;

namespace Singularity.UI.Info.Android.Models {
    [Export(typeof(ICaseManager))]
    public class AndroidInfoCaseManager : ICaseManager {
        public void LoadCase(CaseLoaderHelper.CaseLoadingHanlder loadingHanlder, Func<bool> isCancel) {
            foreach (var csFile in SingularityCase.Current.CaseFiles) {
                if(csFile is AndroidDeviceCaseFile advCFile) {
                    Action<string> loadBasicAct = ik => {
                        if(!string.IsNullOrEmpty(advCFile[ik])) {
                            try {
                                LoadBasicInfo(advCFile, ik);
                            }
                            catch(Exception ex) {
                                Logger.WriteCallerLine(ex.Message);
                            }
                        }
                    };

                    loadBasicAct( PinKindsDefinitions.AndBasicCalllog );
                    loadBasicAct( PinKindsDefinitions.AndBasicContact );
                    loadBasicAct( PinKindsDefinitions.AndBasicSmses   );
                    //LoadBasicInfo(advCFile, );
                }
            } 
        }

        public void Uninstall() {
            
        }

        /// <summary>
        /// 加载现有基本信息(Contact+sms);
        /// </summary>
        /// <param name="advCFile"></param>
        /// <param name="binPath"></param>
        /// <param name="pinKind"></param>
        public static void LoadBasicInfo(AndroidDeviceCaseFile advCFile,string pinKind) {
            var fiUnit = ServiceLocator.Current.GetInstance<ICommonForensicService>().GetForensicInfoUnit(advCFile);
            if (fiUnit == null) {
                return;
            }

            if (fiUnit.Children.FirstOrDefault(p => p is IHavePinKind havePin && havePin.ContentId == pinKind) is ITreeUnit preUnit) {
                fiUnit.Children.Remove(preUnit);
                //var fsTabService = ServiceLocator.Current.GetInstance<FSTabService>();
            }

            TreeUnit newUnit = null;
            try {
                using (var fs = File.OpenRead($"{SingularityCase.Current.Path}/{advCFile.BasePath}/{advCFile[pinKind]}")) {
                    var bf = new BinaryFormatter();
                    var dbModels = (List<ForensicInfoDbModel>) bf.Deserialize(fs);
                    //显示;
                    if (pinKind == PinKindsDefinitions.AndBasicCalllog) {
                        newUnit = new MultiDbModelsUnit<CalllogDbModel>(dbModels?.Select(p => p as CalllogDbModel).ToList(), pinKind, fiUnit);
                    }
                    else if (pinKind == PinKindsDefinitions.AndBasicSmses) {
                        newUnit = new MultiDbModelsUnit<SmsDbModel>(
                            dbModels?.Select(p => p as SmsDbModel).ToList(),
                            PinKindsDefinitions.AndBasicSmses,
                            fiUnit,
                            sms => (sms.relationship_account, sms.relationship_name));
                    }
                    else if (pinKind == PinKindsDefinitions.AndBasicContact) {
                        newUnit = new MultiDbModelsUnit<ContactDbModel>(dbModels?.Select(p => p as ContactDbModel).ToList(), pinKind, fiUnit);
                    }
                    if (newUnit != null) {
                        newUnit.Label = $"{FindResourceString(pinKind)}({dbModels?.Count ?? 0})";
                        fiUnit.Children.Add(newUnit);
                    }
                }
            }
            catch(Exception ex) {
                EventLogger.Logger.WriteCallerLine(ex.Message);
            }
        }
        
        /// <summary>
        /// 加载即时通讯信息;
        /// </summary>
        /// <param name="advCFile"></param>
        /// <param name="infoKind"></param>
        public static void LoadInstanceChat(AndroidDeviceCaseFile advCFile,string pinKind) {
            //保存内容到案件;
            var fiUnit = ServiceLocator.Current.GetInstance<ICommonForensicService>().GetForensicInfoUnit(advCFile);
            if (fiUnit == null) {
                return;
            }

            var afcUnit = fiUnit.Children.
                FirstOrDefault(p => p is PinTreeUnit fcUnit && fcUnit.ContentId == PinKindsDefinitions.ForensicClassInstantTalk) as PinTreeUnit;
            if (afcUnit == null) {
                return;
            }

            PinTreeUnit chatUnit = null;
            if (afcUnit.Children.FirstOrDefault(p => p is PinTreeUnit oTreeUnit && oTreeUnit.ContentId == pinKind)
                is PinTreeUnit preChatUnit) {
                chatUnit = preChatUnit;
            }
            else {
                chatUnit = new PinTreeUnit(pinKind, afcUnit) {
                    Label = PinKindsDefinitions.GetClassLabel(pinKind),
                    Icon = PinKindsDefinitions.GetClassIcon(pinKind)
                };
                afcUnit.Children.Add(chatUnit);
            }

            //取证结果保存字段;
            List<GroupMsgDbModel> groupMsgs = null;
            List<GroupMemberDbModel> groupMembers = null;
            List<FriendInfoDbModel> friends = null;
            List<FriendMsgDbModel> friendMsgs = null;

            void dealUnit<TDbModel>(IEnumerable<TDbModel> models, string thisPinKind,
                Func<TDbModel, (string id, string name)> idFunc = null)
                where TDbModel : ForensicInfoDbModel {
                //移除之前的节点;
                var preUnit = chatUnit.Children.FirstOrDefault(p => p is MultiDbModelsUnit<TDbModel> extUnit
                && extUnit.ContentId == thisPinKind);
                if (preUnit != null) {
                    chatUnit.Children.Remove(preUnit);
                }
                var theUnit = new MultiDbModelsUnit<TDbModel>(
                            models, $"{chatUnit.ContentId}{PinTreeUnit.PinSpliter}{thisPinKind}", chatUnit,
                            idFunc) {
                    Label = $"{FindResourceString(thisPinKind)}({models?.Count() ?? 0})"
                };
                chatUnit.Children.Add(theUnit);
            }

            dealUnit(groupMembers, PinKindsDefinitions.AndGroupMembers, member => (member.group_num, member.group_name));
            dealUnit(groupMsgs, PinKindsDefinitions.AndGroupMsgs, msg => (msg.group_num, msg.SenderRemark));
            dealUnit(friendMsgs, PinKindsDefinitions.AndFriendMsgs, msg => (msg.friend_account, msg.friend_nickname));
            dealUnit(friends, PinKindsDefinitions.AndFriends);

        }

      
    }
}
