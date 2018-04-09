using SingularityForensic.Contracts.Common;
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
    public interface IFolderBrowserViewModel {
        /// <summary>
        /// 所属分区;
        /// </summary>
        Partition Part { get; }

        /// <summary>
        /// 当前选定的文件;
        /// </summary>
        FileBase SelectedFile { get; }

        /// <summary>
        /// 当前展开的文件;
        /// </summary>
        IHaveFileCollection CurrentFileCollection { get; }

        IList<CommandItem> ContextCommands { get; }
    }
}
