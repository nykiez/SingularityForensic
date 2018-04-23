using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Controls;
using SingularityForensic.Contracts.FileExplorer.Models;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer.ViewModels {
    /// <summary>
    /// 目录/资源浏览器模型契约;
    /// </summary>
    public interface IFolderBrowserViewModel : IDataGridViewModel {
        /// <summary>
        /// 所属分区;
        /// </summary>
        IPartition Part { get; }

        /// <summary>
        /// 当前选定的文件;
        /// </summary>
        IFileRow SelectedFile { get; }

        ICollection<IFileRow> Files { get; }

        void FillRows(IEnumerable<IFile> files);

        ICollection<INavNodeModel> NavNodes{get;}
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
        public static IFolderBrowserViewModel CreateFolderBrowserViewModel(IPartition part) => Current?.CreateFolderBrowserViewModel(part);
        public static IPartitionsBrowserViewModel CreatePartitionsBrowserViewModel(IDevice device) => Current?.CreatePartitionsBrowserViewModel(device);
    }

    /// <summary>
    /// 拓展;
    /// </summary>
    public static class FolderBrowserViewModelExtensions {
        /// <summary>
        /// 填充行;
        /// </summary>
        ///<param name="fileCollection">母文件</param>
        public static void FillWithCollection(this IFolderBrowserViewModel vm, IHaveFileCollection fileCollection) {
            if (fileCollection == null) {
                return;
            }

            vm.FillRows(fileCollection.Children);

            vm.NavNodes.Clear();
            IFile file = fileCollection;
            var fileList = new List<IFile>();
            while (file != null) {
                fileList.Add(file);
                file = file.Parent;
            }

            var count = fileList.Count;
            for (int i = 0; i < count; i++) {
                var nodeFile = fileList[count - i - 1];
                var navNode = NavNodeFactory.CreateNew(nodeFile);
                navNode.EscapeRequired += (sender, e) => NavNode_EscapeRequiredWithVm(vm, e);
                vm.NavNodes.Add(navNode);
            }

        }

        private static void NavNode_EscapeRequiredWithVm(IFolderBrowserViewModel vm, IFile e) {
            if (!(e is IHaveFileCollection haveFileCollection)) {
                return;
            }

            if (haveFileCollection is IDevice) {
                return;
            }

            FillWithCollection(vm, haveFileCollection);
        }
    }
}
