using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer {
    /// <summary>
    /// 完整路径提供者;
    /// </summary>
    public interface IFullFileNameProvider {
        /// <summary>
        /// 提供完整路径;
        /// </summary>
        /// <param name="file"></param>
        /// <param name="selfIncluded"></param>
        /// <returns></returns>
        string GetFullFileName(IFile file, bool selfIncluded);
        int Sort { get; }
    }
}
