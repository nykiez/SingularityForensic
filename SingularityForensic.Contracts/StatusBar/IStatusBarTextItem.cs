using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SingularityForensic.Contracts.StatusBar {
    public interface IStatusBarTextItem:IStatusBarObjectItem {
        /// <summary>
        /// 状态栏
        /// </summary>
        string Text { get; set; }

        Brush Foreground { get; set; }

        Thickness Margin { get; set; }

    }
}
