using CDFCUIContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Info {
    /// <summary>
    /// 取证结果节点;
    /// </summary>
    public class ForensicTreeUnit:TreeUnit {
        public ForensicTreeUnit():base(null) {

        }
        
        /// <summary>
        /// 节点类型;
        /// </summary>
        string Type { get; }

        //以下为针对对话,列表节点的成员;

        //数据列集合;
        //colName为列名,type为类型,prop为属性名;
        IEnumerable<(string colName,string type,string prop)> Cols { get; set; }

        //数据内容集合;
        IEnumerable<object> Data { get; set; }
    }

    /// <summary>
    /// 即时通讯结果分类节点示例;
    /// </summary>
    public class InstantChatingForensicTreeUnitExample : ForensicTreeUnit {
        public string Type => Constants.ForensicResTreeUnit_Cate;
    }
}
