using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.StatusBar {
    public interface IStatusBarTextItem:IStatusBarObjectItem {
        /// <summary>
        /// 状态栏
        /// </summary>
        string Text { get; set; }
    }
}
