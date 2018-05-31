using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.StatusBar {
    public interface IStatusBarService {
        /// <summary>
        /// 常规显示文字;
        /// </summary>
        /// <param name="text">显示的文字</param>
        /// <param name="statusBarItemGUID">状态栏项GUID,若为空则操作默认的项</param>
        void Report(string text,string statusBarItemGUID = null);
        /// <summary>
        /// 添加状态栏项;
        /// </summary>
        /// <param name="item"></param>
        /// <param name="statusBarItemGUID"></param>
        void AddStatusBarItem(IStatusBarObjectItem item, GridChildLength gridChildLength, int index = -1);
        /// <summary>
        /// 移除状态栏项;
        /// </summary>
        /// <param name="item"></param>
        void RemoveStatusBarItem(IStatusBarObjectItem item);
        /// <summary>
        /// 所有状态栏项;
        /// </summary>
        IEnumerable<IStatusBarObjectItem> Children { get; }
        /// <summary>
        /// 初始化;
        /// </summary>
        void Initialize();

        IStatusBarObjectItem CreateStatusBarObjectItem(object content, string guid);
        IStatusBarTextItem CreateStatusBarTextItem(string guid);
        IStatusBarTextItem GetOrCreateStatusBarTextItem(string guid, GridChildLength gridChildLength, int sort);
    }

    public class StatusBarService : GenericServiceStaticInstance<IStatusBarService> {
        public static void Report(string text, string statusBarItemGUID = null) => Current.Report(text, statusBarItemGUID);
        public static IStatusBarObjectItem CreateStatusBarObjectItem(object content, string guid) =>
            Current.CreateStatusBarObjectItem(content, guid);
        public static IStatusBarTextItem CreateStatusBarTextItem(string guid) =>
            Current.CreateStatusBarTextItem(guid);
        public static IStatusBarTextItem GetOrCreateStatusBarTextItem(string guid, GridChildLength gridChildLength, int sort) =>
            Current.GetOrCreateStatusBarTextItem(guid, gridChildLength, sort);
    }
}
