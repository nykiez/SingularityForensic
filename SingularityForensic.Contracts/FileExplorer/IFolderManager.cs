using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer {
    public interface IFolderManager {
        Partition Part { get; }
        FileBase SelectedFile { get; }
    }
}
