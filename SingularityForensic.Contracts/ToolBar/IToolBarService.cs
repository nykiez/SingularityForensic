using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.ToolBar {
    public interface IToolBarService {
        void Initialize();     
        /// <summary>
        /// 所有工具栏项;
        /// </summary>
        IEnumerable<IToolBarObjectItem> ToolBarItems { get; }
        /// <summary>
        /// 添加工具栏项;
        /// </summary>
        /// <param name="item"></param>
        void AddToolBarItem(IToolBarObjectItem item);
        /// <summary>
        /// 移除工具栏项;
        /// </summary>
        /// <param name="item"></param>
        void RemoveToolBar(IToolBarObjectItem item);

    }

    public class ToolBarService:GenericServiceStaticInstance<IToolBarService> {
        public static void AddToolBarItem(IToolBarObjectItem item) => Current.AddToolBarItem(item);
        public static void RemoveToolBarItem(IToolBarObjectItem item) => Current.RemoveToolBar(item);
    }
}
