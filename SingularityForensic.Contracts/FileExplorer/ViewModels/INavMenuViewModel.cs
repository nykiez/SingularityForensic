using SingularityForensic.Contracts.FileExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer.ViewModels {
    /// <summary>
    /// 导航菜单视图模型;
    /// </summary>
    public interface INavMenuViewModel {
        /// <summary>
        /// 选定路径;
        /// </summary>
        string SelectedPath { get; set; }
        /// <summary>
        /// 内部选定路径发生变化时;
        /// </summary>
        event EventHandler InternalSelectedPathChanged;
        /// <summary>
        /// 根节点;
        /// </summary>
        INavNodeModel RootNavNode { get; set; }
    }
}
