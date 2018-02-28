using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Info {
    //取证信息选择项节点(组);
    public interface IInfoProvider {
        //类型标识;(组或节点);
        string Type { get; }

        //本节点标识;
        string GUID { get; }
        
        //父节点标识(针对选项节点);
        string GroupGUID { get; }
        
        //节点名称;(比如"微信")
        string Label { get; }
        
    }

    /// <summary>
    /// 即时聊天选项组示例;
    /// </summary>
    [Export(typeof(IInfoProvider))]
    public class InstantChatGroupExample : IInfoProvider {
        public string Type => Constants.ForensicInfoGroup;

        public string GUID => Constants.ForensicInfoGroup_InstantChating;

        public string GroupGUID => string.Empty;

        public string Label => CDFCCultures.Managers.ManagerLocator.FindResourceString("InstanceChating");
    }

    /// <summary>
    /// 微信聊天选项示例;
    /// </summary>
    [Export(typeof(IInfoProvider))]
    public class WeChatGroupExample : IInfoProvider {
        public string Type => Constants.ForensicInfoItem;

        public string GUID => Constants.ForensicInfoItem_Wechat;

        public string GroupGUID => Constants.ForensicInfoGroup_InstantChating;

        public string Label => CDFCCultures.Managers.ManagerLocator.FindResourceString("WeChat");
    }

}
