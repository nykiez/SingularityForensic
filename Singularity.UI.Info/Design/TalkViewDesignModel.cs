using System.Collections.ObjectModel;
using System;
using Singularity.UI.Info.Contracts;

namespace Singularity.UI.Info.Design {
    public class TalkViewDesignModel {
        public ObservableCollection<ITalkLog> TalkLogs { get; set; } = new ObservableCollection<ITalkLog> {
            new TalkDesignModel()
        };
    }

    public class TalkDesignModel : ITalkLog {
        public FromWhom FromWhom => FromWhom.Talker;

        public string SendContent => "你好,NET的所有类型都是由基类System.Object继承过来的，包括最常用的基础类型：int, byte, short，bool等等，就是说所有的事物都是对象。";

        public DateTime? SendDate => DateTime.Parse("1970/01/01");

        public string SenderRemark => "王二小";

        public string SenderAccount => "王二小";
    }

}
