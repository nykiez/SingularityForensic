using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer
{

    static partial class Constants {
        public const string FileSystemTreeUnit = nameof(FileSystemTreeUnit);
    }

    /// <summary>
    /// 语言部分;
    /// </summary>
    static partial class Constants {
        //文件系统资源管理器模块正在加载;
        public const string FileExploerLoading = nameof(FileExploerLoading);
    }
        //public class DefaultFileExplorerServiceProvider :
        //    EmptyServiceProvider<DefaultFileExplorerServiceProvider>, IFileExplorerServiceProvider {
        //    public ICaseEvidenceServiceProvider CaseEvidenceServiceProvider => DefaultFileSystemProvider.StaticInstance;

        //    public IRowBuilder RowBuilder => DefaultRowBuilder.StaticInstance;
        //}
}
