using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Controls;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer {
    /// <summary>
    /// 目录/资源浏览器模型契约;
    /// </summary>
    public interface IFolderBrowserViewModel:IDataGridViewModel {
        /// <summary>
        /// 所属分区;
        /// </summary>
        IPartition Part { get; }

        /// <summary>
        /// 当前选定的文件;
        /// </summary>
        IFileRow SelectedFile { get; }

        ICollection<IFileRow> Files { get;}
    }

    /// <summary>
    /// 文件系统资源管理器视图模型契约工厂类;
    /// </summary>
    public interface IFileExplorerViewModelFactory {
        /// <summary>
        /// 创建分区-文件资源管理器视图模型;
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        IFolderBrowserViewModel CreateFolderBrowserViewModel(IPartition part);

        /// <summary>
        /// 创建设备-分区资源管理器视图模型;
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        IPartitionsBrowserViewModel CreatePartitionsBrowserViewModel(IDevice device);
    }

    public class FileExplorerViewModelFactory : GenericServiceStaticInstance<IFileExplorerViewModelFactory> {
        public static IFolderBrowserViewModel CreateNew(IPartition part) => Current?.CreateFolderBrowserViewModel(part);
        public static IPartitionsBrowserViewModel CreatePartitionsBrowserViewModel(IDevice device) => Current?.CreatePartitionsBrowserViewModel(device);
    }
}
