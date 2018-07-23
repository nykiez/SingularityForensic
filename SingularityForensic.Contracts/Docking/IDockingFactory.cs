using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Docking {
    /// <summary>
    /// 停靠组件工厂契约;
    /// </summary>
    public interface IDockingFactory {
        /// <summary>
        /// 创建停靠容器;
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        IDockingPaneContainer CreatePaneContainer(string guid);
        /// <summary>
        /// 创建停靠组;
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="containerGUID"></param>
        /// <returns></returns>
        IDockingPaneGroup CreatePaneGroup(string guid, string containerGUID);
        /// <summary>
        /// 创建停靠Pane;
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="initGroupGUID">初始化停靠组唯一标识</param>
        /// <param name="regionName"></param>
        /// <returns></returns>
        IDockingPane CreatePane(string guid, string initGroupGUID, string regionName);
    }
}
