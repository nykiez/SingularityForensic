using System;

namespace CDFC.Info.Infrastructure {
    //交谈记录来源;
    public enum FromWhom {
        Self,               //本身;
        Group,              //群;
        Talker,             //对方联系人;
        Unknown             //未知;
    }

    /// <summary>
    /// 交谈模型契约;
    /// </summary>
    public interface ITalkLog {
        FromWhom FromWhom { get; }
        //对话内容;
        string SendContent { get; }
        //发生时间;
        DateTime? SendDate { get; }
        //发送者名称;
        string SenderRemark { get; }
        //发送者账号;
        string SenderAccount { get; }
    }

    
}
