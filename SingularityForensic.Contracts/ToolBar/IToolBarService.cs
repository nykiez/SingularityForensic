using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SingularityForensic.Contracts.ToolBar {
    /// <summary>
    /// 工具栏服务契约;
    /// </summary>
    public interface IToolBarService {
        /// <summary>
        /// 创建一个工具栏按钮;
        /// </summary>
        /// <param name="command"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        IToolBarButtonItem CreateToolBarButtonItem(ICommand command, string guid);
        /// <summary>
        /// 创建工具栏自定义项;
        /// </summary>
        /// <param name="uiObject"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        IToolBarObjectItem CreateToolBarObjectItem(object uiObject, string guid);
    }

    public class ToolBarService : GenericServiceStaticInstance<IToolBarService> {
        public static IToolBarButtonItem CreateToolBarButtonItem(ICommand command, string guid) =>
            Current.CreateToolBarButtonItem(command, guid);

        public static IToolBarObjectItem CreateToolBarObjectItem(object uiObject, string guid) => 
            Current.CreateToolBarObjectItem(uiObject, guid);
    }
}
