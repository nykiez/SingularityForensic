using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Info {
    /// <summary>
    /// 导入标识契约,可自行增加成员;
    /// </summary>
    public static class Constants {
        //取证信息选择项组;
        public const string ForensicInfoGroup = nameof(ForensicInfoGroup);
        //取证信息选择项成员;
        public const string ForensicInfoItem = nameof(ForensicInfoItem);

        //即时通讯选项组;
        public const string ForensicInfoGroup_InstantChating = nameof(ForensicInfoGroup_InstantChating);

        //微信选项标识;
        public const string ForensicInfoItem_Wechat = nameof(ForensicInfoItem_Wechat);


        //以下为结果常量标识;

        //取证结果节点标识;
        public const string ForensicResTreeUnit = nameof(ForensicResTreeUnit);

        //取证结果分类节点,(比如基本信息,即时通讯,以及具体应用等)
        public const string ForensicResTreeUnit_Cate = nameof(ForensicResTreeUnit_Cate);
        
        //取证结果列表节点;
        public const string ForesicResTreeUnit_ListInfo = nameof(ForesicResTreeUnit_ListInfo);

        //日期列类型;
        public const string ForensicResTreeUnit_ListInfo_ColType_DateTime = nameof(ForensicResTreeUnit_ListInfo_ColType_DateTime);

        //字符串类型;
        public const string ForensicResTreeUnit_ListInfo_ColType_String = nameof(ForensicResTreeUnit_ListInfo_ColType_String);

        //数字类型;
        public const string ForensicResTreeUnit_ListInfo_ColType_Decimal = nameof(ForensicResTreeUnit_ListInfo_ColType_Decimal);

        //取证结果对话节点;
        public const string ForesicResTreeUnit_Cate_TalkInfo = nameof(ForesicResTreeUnit_Cate_TalkInfo);


    }
}
