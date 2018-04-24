using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SingularityForensic.Contracts.ToolBar {
    public interface IToolBarButtonItem : IToolBarObjectItem {
        /// <summary>
        /// 命令;
        /// </summary>
        ICommand Command { get; }

        /// <summary>
        /// 按钮图标;
        /// </summary>
        Uri Icon { get; set; }
        /// <summary>
        /// 提示;
        /// </summary>
        string ToolTip { get; set; }
    }
}
