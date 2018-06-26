using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Controls;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer {
    public interface IFolderBrowserDataContext:IUIObjectProvider,IExtensible {
        IFolderBrowserViewModel FolderBrowserViewModel { get; }
        IStackGrid<IUIObjectProvider> StackGrid { get; }
    }
    /// <summary>
    /// 文件系统资源管理器视图模型契约工厂类;
    /// </summary>
    public interface IFileExplorerDataContextFactory {
        /// <summary>
        /// 创建目录-文件资源管理器视图模型;
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        IFolderBrowserDataContext CreateFolderBrowserDataContext(IHaveFileCollection haveFileCollection);

        /// <summary>
        /// 创建设备-分区资源管理器视图模型;
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        IPartitionsBrowserViewModel CreatePartitionsBrowserViewModel(IDevice device);
    }

    public class FileExplorerDataContextFactory : GenericServiceStaticInstance<IFileExplorerDataContextFactory> {
        public static IFolderBrowserDataContext CreateFolderBrowserDataContext(IHaveFileCollection haveFileCollection) => Current?.CreateFolderBrowserDataContext(haveFileCollection);
        public static IPartitionsBrowserViewModel CreatePartitionsBrowserViewModel(IDevice device) => Current?.CreatePartitionsBrowserViewModel(device);
    }
}
