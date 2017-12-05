using Singularity.Contracts.Common;
using Singularity.Contracts.FileExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Singularity.Contracts.FileSystem;
using System.ComponentModel.Composition;

namespace Singularity.Android.Services {
    [Export(typeof(IFileExplorerServiceProvider))]
    public class AndroidDeviceFileExplorerServiceProvider :
        EmptyServiceProvider<AndroidDeviceFileExplorerServiceProvider>,
        IFileExplorerServiceProvider {
        public IFileSystemServiceProvider FileSystemServiceProvider => AndroidDeviceFileSystemServiceProvider.StaticInstance;

        public IRowBuilder RowBuilder => AndroidDeviceRowBuilder.StaticInstance;
    }
}
