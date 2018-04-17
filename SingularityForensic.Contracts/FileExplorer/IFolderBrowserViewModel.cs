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

        IEnumerable<ICommandItem> ContextCommands { get; }
    }

    /// <summary>
    /// 目录/资源浏览器模型契约工厂类;
    /// </summary>
    public interface IFolderBrowserViewModelFactory {
        IFolderBrowserViewModel CreateNew(IPartition part);
    }
}
