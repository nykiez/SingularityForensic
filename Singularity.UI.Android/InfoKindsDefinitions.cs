using CDFC.Info.Android;
using Singularity.UI.Info.Android.Resouces;
using System;
using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.Info.Android {
    //消息类型;
    internal static class PinKindsDefinitions {
        //安卓通话记录;
        public const string AndBasicCalllog    = nameof(AndBasicCalllog);
        //安卓联系人;
        public const string AndBasicContact    = nameof(AndBasicContact);
        //安卓短信;
        public const string AndBasicSmses      = nameof(AndBasicSmses);

        //安卓QQ总钻风;
        public const string AndQQ           = nameof(AndQQ);

        //安卓微信总钻风;
        public const string AndWeChat = nameof(AndWeChat);

        //安卓群讯息;
        public const string AndGroupMsgs    = nameof(AndGroupMsgs);
        //安卓群成员;                                                 
        public const string AndGroupMembers = nameof(AndGroupMembers);
        //安卓好友;                                                            
        public const string AndFriends      = nameof(AndFriends);
        //安卓好友消息;
        public const string AndFriendMsgs   = nameof(AndFriendMsgs);

        /// <summary>
        /// 通过DbModel类型获取PinKind
        /// </summary>
        /// <typeparam name="TDbModel"></typeparam>
        /// <returns></returns>
        public static string GetPinKind<TDbModel>() {
            if (typeof(GroupMemberDbModel) == typeof(TDbModel)) {
                return AndGroupMembers;
            }
            else if (typeof(GroupMsgDbModel) == typeof(TDbModel)) {
                return PinKindsDefinitions.AndGroupMsgs;
            }
            else if (typeof(FriendInfoDbModel) == typeof(TDbModel)) {
                return PinKindsDefinitions.AndFriends;
            }
            else if (typeof(FriendMsgDbModel) == typeof(TDbModel)) {
                return PinKindsDefinitions.AndFriendMsgs;
            }
            else if (typeof(SmsDbModel) == typeof(TDbModel)) {
                return PinKindsDefinitions.AndBasicSmses;
            }
            else if (typeof(CalllogDbModel) == typeof(TDbModel)) {
                return PinKindsDefinitions.AndBasicCalllog;
            }
            else if (typeof(ContactDbModel) == typeof(TDbModel)) {
                return PinKindsDefinitions.AndBasicContact;
            }
            return string.Empty;
        }
        
        public const string ForensicClassBasic = nameof(ForensicClassBasic); //基础信息
        public const string ForensicClassInstantTalk = nameof(ForensicClassInstantTalk); //即时通讯;
        public const string ForensicClassBrowser = nameof(ForensicClassBrowser); //浏览器
        public const string ForensicClassMultiMedia = nameof(ForensicClassMultiMedia); //多媒体
        public const string ForensicClassOther = nameof(ForensicClassOther); //其它;

        public static string GetClassLabel(string afc) {
            switch (afc) {
                case ForensicClassBasic:
                    return "基本信息";
                case ForensicClassInstantTalk:
                    return "即时通讯";
                case ForensicClassMultiMedia:
                    return "多媒体文件";
                case ForensicClassOther:
                    return "其他";
                case ForensicClassBrowser:
                    return "浏览器";
                case AndQQ:
                    return FindResourceString("AndQQ");
                case AndWeChat:
                    return FindResourceString("AndWeChat");
            }
            return string.Empty;
        }

        /// <summary>
        /// 获得类别Icon;
        /// </summary>
        /// <param name="afc"></param>
        /// <returns></returns>
        public static Uri GetClassIcon(string afc) {
            switch (afc) {
                case AndWeChat:
                    return IconSources.WeChatIcon;
                case AndQQ:
                    return IconSources.QQIcon;
            }

            return null;
        }

        public static string[] ForensicClassTypes => new string[] {
            ForensicClassBasic,
            ForensicClassInstantTalk,
            ForensicClassBrowser,
            ForensicClassMultiMedia,
            ForensicClassOther
        };
    }


}
