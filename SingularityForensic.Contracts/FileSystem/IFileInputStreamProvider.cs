using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    /// <summary>
    /// 文件流提供器;
    /// </summary>
    public interface IFileInputStreamProvider {
        Stream GetInputStream(IFile file);
    }
}
