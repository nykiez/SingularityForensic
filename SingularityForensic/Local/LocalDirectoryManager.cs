using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Local {
    /// <summary>
    /// 本地目录管理器;
    /// </summary>
    public class LocalDirectoryManager {
        /// <summary>
        /// 实际存储的内容;
        /// </summary>
        public IDirectory Directory { get; internal set; }
    }
}
