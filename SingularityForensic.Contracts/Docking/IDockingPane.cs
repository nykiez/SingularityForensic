using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.Contracts.Docking {
    /// <summary>
    /// 停靠Pane契约;
    /// </summary>
    public interface IDockingPane : IDockingItem {
        event EventHandler HeaderChanged;
        event EventHandler IsHiddenChanged;

        /// <summary>
        /// 初始停靠组唯一标识;
        /// </summary>
        string InitPaneGroupGUID { get; }
        /// <summary>
        /// 用于设定停靠Pane内的内容区域名称,对应Prism的RegionName;
        /// </summary>
        string RegionName { get; }
        /// <summary>
        /// 头部语言;
        /// </summary>
        string Header { get; set; }
        /// <summary>
        /// 默认初始视图名;(可以为空);
        /// </summary>
        string DefaultlViewName { get; }
        /// <summary>
        /// 能否关闭;
        /// </summary>
        bool CanUserClose { get; set; }
        /// <summary>
        /// 头部栏可见状态;
        /// </summary>
        Visibility PaneHeaderVisibility { get; set; }
        /// <summary>
        /// 是否隐藏;
        /// </summary>
        bool IsHidden { get; set; }

        /// <summary>
        /// 初始宽/高度;
        /// </summary>
        double InitialWidth { get; }
        double InitialHeight { get; }
    }
}
