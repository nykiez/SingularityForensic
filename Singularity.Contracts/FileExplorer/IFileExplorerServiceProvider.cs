using Singularity.Contracts.Case;
using Singularity.Contracts.Common;
using Singularity.Contracts.FileSystem;

namespace Singularity.Contracts.FileExplorer {
    /// <summary>
    /// 资源管理器契约;
    /// </summary>
    public interface IFileExplorerServiceProvider : IServiceProvider {
        ICaseEvidenceServiceProvider FileSystemServiceProvider { get; }

        /// <summary>
        /// 行生成器;
        /// </summary>
        IRowBuilder RowBuilder { get; }
        
    }
}
