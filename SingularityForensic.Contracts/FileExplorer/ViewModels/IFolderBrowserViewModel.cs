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
        IHaveFileCollection HaveFileCollection { get; }

        /// <summary>
        /// 当前选定的文件;
        /// </summary>
        IFileRow SelectedFile { get; }
        /// <summary>
        /// 当前聚焦的文件行;
        /// </summary>
        IEnumerable<IFileRow> SelectedFiles { get; }
        //void AddSelectedFile(IEnumerable<IFileRow> fileRows);

        event EventHandler SelectedFileChanged;
        
        IEnumerable<IFileRow> Files { get; }
        event EventHandler FileCollectionChanged;

        /// <summary>
        /// 添加文件;
        /// </summary>
        /// <param name="files"></param>
        /// <param name="isFromUIThread">是否从UI线程调用的</param>
        void FillRows(IEnumerable<IFile> files);
        
        INavNodeModel SelectedNavNode { get; }
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

            //var rootNavNode = 
            //vm.NavNodes.Clear();
            //IFile file = fileCollection;
            //var fileList = new List<IFile>();
            //while (file != null) {
            //    fileList.Add(file);
            //    file = file.Parent;
            //}

            //var count = fileList.Count;
            //for (int i = 0; i < count; i++) {
            //    var nodeFile = fileList[count - i - 1];
            //    var navNode = NavNodeFactory.CreateNew();
            //    navNode.SetInstance(nodeFile, Constants.NavNodeTag_File);
            //    vm.NavNodes.Add(navNode);
            //}

        }

        private static void NavNode_EscapeRequiredWithVm(IFolderBrowserDataContext vm, IFile e) {
            if (!(e is IHaveFileCollection haveFileCollection)) {
                return;
            }

            if (haveFileCollection is IDevice) {
                return;
            }

            FillWithCollection(vm.FolderBrowserViewModel, haveFileCollection);
        }
    }
}
