using Singularity.Contracts.Common;
using Singularity.Contracts.FileSystem;

namespace Singularity.Contracts.FileExplorer {
    public interface IFileExplorerServiceProvider : IServiceProvider {
        IFileSystemServiceProvider FileSystemServiceProvider { get; }

        IRowBuilder RowBuilder { get; }
        
    }
}
