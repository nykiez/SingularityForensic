using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer {
    /// <summary>
    /// 查看程序服务;
    /// </summary>
    public interface IViewerService {
        /// <summary>
        /// 使用指定程序打开文件;
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="proPath"></param>
        void OpenFileWith(string fileName, string proPath);

        /// <summary>
        /// 获取所有的查看器;
        /// </summary>
        /// <returns></returns>
        IEnumerable<(string viewerName, string path)?> GetAllViewers();

        /// <summary>
        /// 添加查看器;
        /// </summary>
        /// <param name="viewerName"></param>
        /// <param name="path"></param>
        void AddViewer(string viewerName, string path);

        /// <summary>
        /// 复位,移除所有的非默认查看器;
        /// </summary>
        void Reset();
    }

    public class ViewerService : GenericServiceStaticInstance<IViewerService> {
        public static IEnumerable<(string viewerName, string path)?> GetAllViewers() => Current?.GetAllViewers();
    }
}
