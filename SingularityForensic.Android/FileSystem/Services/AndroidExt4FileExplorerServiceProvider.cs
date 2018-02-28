using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using System.ComponentModel.Composition;
using SingularityForensic.Android.FileSystem.Models;
using SingularityForensic.Contracts.Case;

namespace SingularityForensic.Android.FileSystem.Services {
    [Export(typeof(IFileExplorerServiceProvider))]
    public class AndroidExt4FileExplorerServiceProvider :
        EmptyServiceProvider<AndroidExt4FileExplorerServiceProvider>,
        IFileExplorerServiceProvider {
        public ICaseEvidenceServiceProvider CaseEvidenceServiceProvider => AndroidDeviceCaseEvidenceServiceProvider.StaticInstance;

        public IRowBuilder RowBuilder => AndroidExt4RowBuilder.StaticInstance;
    }
}
