using CDFC.Parse.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer {
    public interface IFileBrowserDataContext {
        IFile File { get; }
        IFile CurFile { get; }
        IFile OwnerFile { get; }
        void FillFiles(IEnumerable<IFile> files);
    }
}
