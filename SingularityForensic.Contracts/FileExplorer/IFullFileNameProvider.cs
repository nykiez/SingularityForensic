using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer {
    public interface IFullFileNameProvider {
        string GetFullFileName(IFile file, bool selfIncluded);
        int Sort { get; }
    }
}
