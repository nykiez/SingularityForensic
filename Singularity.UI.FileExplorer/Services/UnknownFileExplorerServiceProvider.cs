using Singularity.Contracts.Case;
using Singularity.Contracts.Common;
using Singularity.Contracts.FileExplorer;
using Singularity.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singularity.UI.FileExplorer.Services {
    [Export(typeof(IFileExplorerServiceProvider))]
    public class UnknownFileExplorerServiceProvider :
        EmptyServiceProvider<UnknownFileExplorerServiceProvider>,
        IFileExplorerServiceProvider {
        public ICaseEvidenceServiceProvider CaseEvidenceServiceProvider => DefaultFileSystemProvider.StaticInstance;

        public IRowBuilder RowBuilder => DefaultRowBuilder.StaticInstance;
    }
}
