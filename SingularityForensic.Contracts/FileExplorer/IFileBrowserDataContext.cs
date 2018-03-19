using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer {
    public interface IFileBrowserDataContext {
        FileBase File { get; }
        FileBase CurFile { get; }
        FileBase OwnerFile { get; }
        void FillFiles(IEnumerable<FileBase> files);
    }
}
