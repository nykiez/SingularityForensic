using CDFCMessageBoxes.MessageBoxes;
using EventLogger;
using Microsoft.Practices.ServiceLocation;
using SingularityForensic.Android.FileSystem.Models;
using SingularityForensic.Contracts.Case;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.TreeView;
using SingularityForensic.Android.Info.Models;
using SingularityForensic.Android.Info.Helpers;
using SingularityForensic.Info.Models;
using SingularityForensic.Info.Models.Chating;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Xml.Linq;
using static CDFCCultures.Managers.ManagerLocator;
using static CDFCUIContracts.Helpers.ApplicationHelper;

namespace SingularityForensic.Android.Info.Models {
    /// <summary>
    /// 选项组导出;
    /// </summary>
    public static class CheckGroupDefinitions {
        [Export]
        public static readonly CheckGroupTreeItem<AndroidDeviceCaseEvidence> BasicGroup = 
            new CheckGroupTreeItem<AndroidDeviceCaseEvidence>(FindResourceString("BasicInfoTreeLabel"));

        [Export]
        public static readonly CheckGroupTreeItem<AndroidDeviceCaseEvidence> InstantChat = new CheckGroupTreeItem<AndroidDeviceCaseEvidence>(FindResourceString("InstantChat"));

        [Export]
        public static readonly CheckGroupTreeItem<AndroidDeviceCaseEvidence> MultiMediaGroup = new CheckGroupTreeItem<AndroidDeviceCaseEvidence>(FindResourceString("MultiMediaInfoTreeLabel"));
        
        [Export]
        public static readonly CheckGroupTreeItem<AndroidDeviceCaseEvidence> OtherGroup = new CheckGroupTreeItem<AndroidDeviceCaseEvidence>(FindResourceString("OtherFileTreeLabel"));
    }
    
    //重组相关信息项;
    public class RecomCheckTreeItem : CheckItemTreeItem<AndroidDeviceCaseEvidence> {
        public RecomCheckTreeItem(CheckGroupTreeItem group,string[] extName, string afc):base(group) {
            this.ExtNames = extName;
            this.ForensicCalssType = afc;
        }

        //重组名;
        public const string RecomItemName = "RecomItem";

        public string ForensicCalssType { get; }
        //装载;(保存,显示等)
        public override void Setup() {
            //显示;
            if(stNdList != null && ExtNames != null) {
                var ownNdList = stNdList.Where(p => ExtNames.FirstOrDefault(q => q == p.Type) != null).ToList();
                var part = SearcherPartition.LoadFromNodeList(CaseFile.Data, 
                    ownNdList,$"{CaseFile.Name}-{Name}");
                
                var fsService = ServiceProvider.Current.GetInstance<IFSNodeService>();
                
                if(fsService != null) {
                    try {
                        var fiUnit = ServiceProvider.Current.GetInstance<ICommonForensicService>().GetForensicInfoUnit<AndroidDeviceCaseEvidence>(CaseFile);
                        foreach (var unit in fiUnit.Children) {
                            if(unit is PinTreeUnit afcUnit && afcUnit.ContentId == ForensicCalssType) {
                                var preUnit = afcUnit.Children.FirstOrDefault(p => p.Label?.StartsWith(Name) ?? false);
                                var sunit = ServiceProvider.Current.GetInstance<IFSNodeService>()?.
                                CreateStorageUnit(part, afcUnit, DefaultFileExplorerServiceProvider.StaticInstance);
                                if (preUnit != null) {
                                    afcUnit.Children.Remove(preUnit);
                                }
                                afcUnit.Children.Add(sunit);
                            }
                        }
                    }
                    catch (Exception ex) {
                        CDFCMessageBox.Show($"{ex.Message}");
                    }
                    
                }
            }

            //保存;
            //CaseFile[$"{RecomItemName}_]
        }
        
        public override void StartForensic(Func<bool> isCancel) {
            if (!isWorking) {
                isWorking = true;
                if(Searcher != null) {
                    ThreadPool.QueueUserWorkItem(cb => {
                        try {
                            Searcher.SearchStart(0, CaseFile.Data.Size - 1);
                            stNdList = Searcher.GetFileList(string.Empty);
                        }
                        catch(Exception ex) {
                            EventLogger.Logger.WriteLine($"{nameof(RecomCheckTreeItem)}->{nameof(StartForensic)}:{ex.Message}");
                        }
                        finally {
                            isWorking = false;
                        }
                    });
                }
            }

            while (isWorking) {
                Pro = (int)((Searcher.CurOffset - 0) * 100 / (CaseFile.Data.Size - 0));
                Thread.Sleep(1000);
            }
        }

        private static List<IFileNode> stNdList;
        
        public override void Init(AndroidDeviceCaseEvidence deviceFile) {
            base.Init(deviceFile);
            if(Searcher == null || Searcher.Device != deviceFile.Data) {
                Free();
                Searcher = new RecompositeSearcher(deviceFile.Data);
            }
        }
        
        public override void Free() {
            base.Free();
            if(Searcher != null) {
                Searcher.Dispose();
                Searcher = null;
            }
        }

        public string[] ExtNames { get; }
        
        private static bool isWorking = false;
        private static RecompositeSearcher Searcher;
    }

    //现有基本相关信息(Python);
    public class BasicCheckTreeItem : CheckItemTreeItem<AndroidDeviceCaseEvidence> {
        public BasicCheckTreeItem(string pinKind,CheckGroupTreeItem group):base(group) {
            this.PinKind = pinKind;
        }

        public string PinKind { get; }
        //取证结果保存字段;
        private List<ForensicInfoDbModel> dbModels;

        public override void Init(AndroidDeviceCaseEvidence adcFile) {
            base.Init(adcFile);
            dbModels = new List<ForensicInfoDbModel>();
        }

        public override void Free() {
            base.Free();
            evt.Reset();
            dbModels = null;
            outParams.outPath = null;
            outParams.outXDocName = null;
            isWorking = false;
            dbModels = null;
        }

        //保存以及显示;
        public override void Setup() {
            //进行保存,使用二进制序列化;
            try {
                var bf = new BinaryFormatter();
                //记录二进制文件的位置;
                var binName = $"{PinKind}.bin";
                var csService = ServiceProvider.Current.GetInstance<ICaseService>();
                if(csService == null) {
                    return;
                }
                using (var fs = File.Create($"{csService.CurrentCase.Path}/{CaseFile.BasePath}/{binName}")) {
                    bf.Serialize(fs, dbModels);
                }
                CaseFile[PinKind] = binName;

                AndroidInfoCaseManager.LoadBasicInfo(CaseFile, PinKind);
            }
            catch(Exception ex) {
                Logger.WriteCallerLine(ex.Message);
            }
        }
        
        public override void StartForensic(Func<bool> isCancel) {
            Pro = 50;
            
            if(!isWorking) {
                isWorking = true;
                try {
                    outParams = AdvPythonHelper.GetProcessOutPut(CaseFile, "Phone_msg_calllog_extract.py");
                }
                catch {
                    throw;
                }
                finally {
                    evt.Set();
                }
            }
            evt.WaitOne();
            
            AndroidDeviceBasicContext context = null;
            var OutPutXDocPath = $"{outParams.outPath}/{outParams.outXDocName}";

            if (!System.IO.File.Exists(OutPutXDocPath)) {
                throw new Exception($"{OutPutXDocPath} doesn't exists!");
            }
            else {
                try {
                    var outPutDoc = XDocument.Load(OutPutXDocPath);
                    var root = outPutDoc.Root;
                    var dbElem = root.Element("DbPath");
                    if (dbElem != null) {
                        try {
                            context = new AndroidDeviceBasicContext($"data source = '{dbElem.Value.Replace("\\","/")}'");
                        }
                        catch (Exception ex) {
                            Logger.WriteLine($"{ex.Message}");
                        }
                    }
                    else {
                        throw new Exception($"{nameof(dbElem)} can't be null!");
                    }
                }
                catch (Exception ex) {
                    Logger.WriteLine($"{nameof(BasicCheckTreeItem)}->{nameof(StartForensic)}:{ex.Message}");
                    throw;
                }
            }
            
            if(context != null && !(isCancel?.Invoke()??false)) {
                try {
                    if (PinKind == PinKindsDefinitions.AndBasicContact) {
                        dbModels.AddRange(context.Contacts);
                    }
                    else if (PinKind == PinKindsDefinitions.AndBasicCalllog) {
                        dbModels.AddRange(context.Calllogs);
                    }
                    else if (PinKind == PinKindsDefinitions.AndBasicSmses) {
                        dbModels.AddRange(context.Smses);
                    }
                }
                catch(Exception ex) {
                    Logger.WriteCallerLine(ex.Message);
                }

                try {
                    context.Dispose();
                }
                catch(Exception ex) {
                    Logger.WriteCallerLine(ex.Message);
                }
            }
            
            Pro = 100;
        }
        
        private static (string outPath,string outXDocName) outParams;
        private static ManualResetEvent evt = new ManualResetEvent(false);
        private static bool isWorking;
    }
    
    /// <summary>
    /// 即时通讯相关信息(Python);
    /// </summary>
    public class InstanceChatTreeItem:CheckItemTreeItem<AndroidDeviceCaseEvidence> {
        /// <summary>
        /// py文件名;
        /// </summary>
        /// <param name="pyName"></param>
        public InstanceChatTreeItem(string pyName):base(CheckGroupDefinitions.InstantChat) {
            this.pyName = pyName;
        }

        private string pyName;
        //类别标识;
        public string PinKind { get; set; }

        //取证结果保存字段;
        private List<GroupMsgDbModel> groupMsgs;
        private List<GroupMemberDbModel> groupMembers;
        private List<FriendInfoDbModel> friends;
        private List<FriendMsgDbModel> friendMsgs;

        public override void Init(AndroidDeviceCaseEvidence adcFile) {
            base.Init(adcFile);
            groupMsgs = new List<GroupMsgDbModel>();
            groupMembers = new List<GroupMemberDbModel>();
            friends = new List<FriendInfoDbModel>();
            friendMsgs = new List<FriendMsgDbModel>();
        }
        
        /// <summary>
        /// 保存,显示;
        /// </summary>
        public override void Setup() {
            //进行保存,使用二进制序列化;
            try {
                var bf = new BinaryFormatter();
                //记录二进制文件的位置;
                var binName = $"{PinKind}.bin";
            }
            catch {

            }
            //保存内容到案件;
            var fiUnit = ServiceProvider.Current.GetInstance<ICommonForensicService>().GetForensicInfoUnit(CaseFile);
            if (fiUnit == null) {
                return;
            }

            var afcUnit = fiUnit.Children.
                FirstOrDefault(p => p is PinTreeUnit fcUnit && fcUnit.ContentId == PinKindsDefinitions.ForensicClassInstantTalk) as PinTreeUnit;
            if (afcUnit == null) {
                return;
            }

            PinTreeUnit chatUnit = null;
            if (afcUnit.Children.FirstOrDefault(p => p is PinTreeUnit oTreeUnit && oTreeUnit.ContentId == PinKind)
                is PinTreeUnit preChatUnit) {
                chatUnit = preChatUnit;
            }
            else {
                chatUnit = new PinTreeUnit(PinKind, afcUnit) {
                    Label = PinKindsDefinitions.GetClassLabel(PinKind),
                    Icon = PinKindsDefinitions.GetClassIcon(PinKind)
                };
                afcUnit.Children.Add(chatUnit);
            }

            void dealUnit<TDbModel>(IEnumerable<TDbModel> models, string pinKind,
                Func<TDbModel, (string id, string name)> idFunc = null)
                where TDbModel : ForensicInfoDbModel {
                //移除之前的节点;
                var preUnit = chatUnit.Children.FirstOrDefault(p => p is MultiDbModelsUnit<TDbModel> extUnit
                && extUnit.ContentId == pinKind);
                if (preUnit != null) {
                    chatUnit.Children.Remove(preUnit);
                }
                var theUnit = new MultiDbModelsUnit<TDbModel>(
                            models, $"{chatUnit.ContentId}{PinTreeUnit.PinSpliter}{pinKind}", chatUnit,
                            idFunc) {
                    Label = $"{FindResourceString(pinKind)}({models?.Count() ?? 0})"
                };
                chatUnit.Children.Add(theUnit);
            }

            dealUnit(groupMembers, PinKindsDefinitions.AndGroupMembers, member => (member.group_num, member.group_name));
            dealUnit(groupMsgs, PinKindsDefinitions.AndGroupMsgs, msg => (msg.group_num, msg.SenderRemark));
            dealUnit(friendMsgs, PinKindsDefinitions.AndFriendMsgs, msg => (msg.friend_account, msg.friend_nickname));
            dealUnit(friends, PinKindsDefinitions.AndFriends);
        }

        public override void Free() {
            base.Free();
            groupMembers = null;
            groupMsgs = null;
            friendMsgs = null;
            friends = null;
        }

        public override void StartForensic(Func<bool> isCancel) {
            Pro = 50;
            var outPutParams = AdvPythonHelper.GetProcessOutPut(CaseFile, pyName);
            if (!File.Exists($"{outPutParams.outPutPath}/{outPutParams.outDocName}")) {
                //throw new Exception($"{outPutParams.outPutPath}/{outPutParams.outDocName} doesn't exist.");
                Pro = 100;
                return;
            }

            AndroidDeviceQQContext context = null;

            try {
                var outPutDoc = XDocument.Load($"{outPutParams.outPutPath}/{outPutParams.outDocName}");
                var root = outPutDoc.Root;
                var dbElem = root.Element("DbPath");
                if (dbElem != null) {
                    try {
                        context = new AndroidDeviceQQContext($"data source = '{outPutParams.outPutPath}/{dbElem.Value}'");
                    }
                    catch (Exception ex) {
                        Logger.WriteCallerLine(ex.Message);
                    }
                }
                else {
                    throw new Exception($"{nameof(dbElem)} can't be null!");
                }
            }
            catch(Exception ex) {
                Logger.WriteLine($"{nameof(InstanceChatTreeItem)}->{nameof(StartForensic)}:{ex.Message}");
                AppInvoke(() => {
                    RemainingMessageBox.Tell(ex.Message);
                });
            }

            if(!(isCancel?.Invoke()??false)) {
                try {
                    InitWithDbSet(ref groupMsgs, context?.GroupMsgs);
                    InitWithDbSet(ref groupMembers,context?.GroupMembers);
                    InitWithDbSet(ref friends,context?.Friends);
                    InitWithDbSet(ref friendMsgs,context?.FriendMsgs);
                }
                catch(Exception ex) {
                    Logger.WriteCallerLine(ex.Message);
                    throw;
                }
                finally {
                    Pro = 100;
                    context?.Dispose();
                }
            }
            
        }

        private static void InitWithDbSet<TDbModel>(ref List<TDbModel> dbModels,DbSet<TDbModel> dtsets) where TDbModel:ForensicInfoDbModel {
            if(dtsets == null) {
                return;
            }
            try {
                dbModels.AddRange(dtsets);
            }
            catch(Exception ex) {
                Logger.WriteCallerLine(ex.Message);
            }
        }
    }

    public static class CheckItemDefinitions {
        [Export(typeof(CheckItemTreeItem<AndroidDeviceCaseEvidence>))]
        public static readonly RecomCheckTreeItem PicRecomTreeItem = new RecomCheckTreeItem(CheckGroupDefinitions.MultiMediaGroup,
            new string[] { "jpg", "png" }, PinKindsDefinitions.ForensicClassMultiMedia) {
            Name = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("PicFileTreeLabel")
        };

        [Export(typeof(CheckItemTreeItem<AndroidDeviceCaseEvidence>))]
        public static readonly RecomCheckTreeItem AudioRecomTreeItem = new RecomCheckTreeItem(CheckGroupDefinitions.MultiMediaGroup,
            new string[] { "mp3", "amr" }, PinKindsDefinitions.ForensicClassMultiMedia) {
            Name = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("AudioFileTreeLabel")
        };

        [Export(typeof(CheckItemTreeItem<AndroidDeviceCaseEvidence>))]
        public static readonly RecomCheckTreeItem VideoRecomTreeItem = new RecomCheckTreeItem(CheckGroupDefinitions.MultiMediaGroup,
            new string[] { "mp4" }, PinKindsDefinitions.ForensicClassMultiMedia) {
            Name = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("VideoFileTreeLabel")
        };

        [Export(typeof(CheckItemTreeItem<AndroidDeviceCaseEvidence>))]
        public static readonly RecomCheckTreeItem ApkRecomTreeItem = new RecomCheckTreeItem(CheckGroupDefinitions.OtherGroup,
            new string[] { "apk" }, PinKindsDefinitions.ForensicClassOther) {
            Name = "APK文件"
        };

        [Export(typeof(CheckItemTreeItem<AndroidDeviceCaseEvidence>))]
        public static readonly RecomCheckTreeItem DbRecomTreeItem = new RecomCheckTreeItem(CheckGroupDefinitions.OtherGroup,
            new string[] { "db", "sqlite" }, PinKindsDefinitions.ForensicClassOther) {
            Name = "数据库文件"
        };

        [Export(typeof(CheckItemTreeItem<AndroidDeviceCaseEvidence>))]
        public static readonly BasicCheckTreeItem SmsInfoTreeItem = new BasicCheckTreeItem(PinKindsDefinitions.AndBasicSmses, CheckGroupDefinitions.BasicGroup) {
            Name = "短信"
        };

        [Export(typeof(CheckItemTreeItem<AndroidDeviceCaseEvidence>))]
        public static readonly BasicCheckTreeItem CalllogInfoTreeItem = new BasicCheckTreeItem(PinKindsDefinitions.AndBasicCalllog, CheckGroupDefinitions.BasicGroup) {
            Name = "通话记录"
        };

        [Export(typeof(CheckItemTreeItem<AndroidDeviceCaseEvidence>))]
        public static readonly BasicCheckTreeItem ContactInfoTreeItem = new BasicCheckTreeItem(PinKindsDefinitions.AndBasicContact, CheckGroupDefinitions.BasicGroup) {
            Name = "联系人"
        };

        [Export(typeof(CheckItemTreeItem<AndroidDeviceCaseEvidence>))]
        public static readonly InstanceChatTreeItem QQChatTreeItem = new InstanceChatTreeItem("qq_extract.py") {
            Name = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("QQTreeLabel"),
            PinKind = PinKindsDefinitions.AndQQ
        };

        [Export(typeof(CheckItemTreeItem<AndroidDeviceCaseEvidence>))]
        public static readonly InstanceChatTreeItem WeChatTreeItem = new InstanceChatTreeItem("wechat_extract.py") {
            Name = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("WeChatTreeLabel"),
            PinKind = PinKindsDefinitions.AndWeChat
        };


    }
}
