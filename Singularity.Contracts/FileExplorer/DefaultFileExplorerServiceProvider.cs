using Singularity.Contracts.Case;
using Singularity.Contracts.Common;
using Singularity.Contracts.FileSystem;

namespace Singularity.Contracts.FileExplorer {
    public class DefaultFileExplorerServiceProvider :
        EmptyServiceProvider<DefaultFileExplorerServiceProvider>, IFileExplorerServiceProvider {
        public ICaseEvidenceServiceProvider FileSystemServiceProvider => DefaultFileSystemProvider.StaticInstance;

        public IRowBuilder RowBuilder => DefaultRowBuilder.StaticInstance;
    }
}
