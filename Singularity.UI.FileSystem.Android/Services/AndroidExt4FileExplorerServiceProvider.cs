using Singularity.Contracts.Common;
using Singularity.Contracts.FileExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Singularity.Contracts.FileSystem;
using System.ComponentModel.Composition;
using Singularity.Android.Models;

namespace Singularity.Android.Services {
    [Export(typeof(IFileExplorerServiceProvider))]
    public class AndroidExt4FileExplorerServiceProvider :
        EmptyServiceProvider<AndroidExt4FileExplorerServiceProvider>,
        IFileExplorerServiceProvider {
        public IFileSystemServiceProvider FileSystemServiceProvider => AndroidDeviceFileSystemServiceProvider.StaticInstance;

        public IRowBuilder RowBuilder => AndroidExt4RowBuilder.StaticInstance;
    }
}
