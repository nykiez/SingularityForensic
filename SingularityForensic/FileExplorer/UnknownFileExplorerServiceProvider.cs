using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using System.ComponentModel.Composition;

namespace SingularityForensic.Controls.FileExplorer.Services {
    [Export(typeof(IFileExplorerServiceProvider))]
    public class UnknownFileExplorerServiceProvider :
        EmptyServiceProvider<UnknownFileExplorerServiceProvider>,
        IFileExplorerServiceProvider {
        //public ICaseEvidenceServiceProvider CaseEvidenceServiceProvider => DefaultFileSystemProvider.StaticInstance;

        //public IRowBuilder RowBuilder => DefaultRowBuilder.StaticInstance;
    }
}
