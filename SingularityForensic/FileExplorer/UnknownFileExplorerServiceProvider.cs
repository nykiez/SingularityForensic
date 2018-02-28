using SingularityForensic.Contracts.Case;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Controls.FileExplorer.Services {
    [Export(typeof(IFileExplorerServiceProvider))]
    public class UnknownFileExplorerServiceProvider :
        EmptyServiceProvider<UnknownFileExplorerServiceProvider>,
        IFileExplorerServiceProvider {
        public ICaseEvidenceServiceProvider CaseEvidenceServiceProvider => DefaultFileSystemProvider.StaticInstance;

        public IRowBuilder RowBuilder => DefaultRowBuilder.StaticInstance;
    }
}
