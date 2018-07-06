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
        /// 所属集合文件;
        /// </summary>
        IHaveFileCollection OwnedFileCollection { get; }

        /// <summary>
        ///当前显示的路径;分割符为<see cref="SingularityForensic.Contracts.FileSystem.Constants.Path_SplitChar"/>
        /// </summary>
        string CurrentPath { get; set; }
        event EventHandler CurrentPathChanged;

        /// <summary>
        /// 当前选定的文件;
        /// </summary>
        IFileRow SelectedFileRow { get; }
        /// <summary>
        /// 当前聚焦的文件行;
        /// </summary>
        IEnumerable<IFileRow> SelectedFileRows { get; }
        //void AddSelectedFile(IEnumerable<IFileRow> fileRows);

        event EventHandler SelectedFileChanged;
        //INavNodeModel SelectedNavNode { get; }


        bool IsBusy { get; set; }
        string BusyWord { get; set; }

        IEnumerable<IFileRow> FileRows { get; }
        event EventHandler FileCollectionChanged;

        /// <summary>
        /// 添加文件;
        /// </summary>
        /// <param name="files"></param>
        /// <param name="isFromUIThread">是否从UI线程调用的</param>
        void FillRows(IEnumerable<IFile> files);
        
        
    }
    
  

    /// <summary>
    /// 拓展;
    /// </summary>
    public static class FolderBrowserViewModelExtensions {
        /// <summary>
        /// 填充行;
        /// </summary>
        ///<param name="haveFileCollection">母文件</param>
        public static void FillWithCollection(this IFolderBrowserViewModel vm, IHaveFileCollection haveFileCollection) {
            if (haveFileCollection == null) {
                return;
            }
            vm.FillRows(haveFileCollection.Children);
        }
        
    }
}
