using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.ToolBar {
    public interface IToolBarObjectItem : IUIObjectProvider {
        string GUID { get; }
        /// <summary>
        /// 排序;
        /// </summary>
        int Sort { get; set; }
    }
    
}
