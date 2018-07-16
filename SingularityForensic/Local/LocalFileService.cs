using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Local {
    /// <summary>
    /// 本地文件服务;
    /// </summary>
    [Export]
    class LocalFileService {
        public void Initialize() {

        }
        public IEnumerable<LocalDirectoryManager> DirectoryManagers { get; }
        private List<LocalDirectoryManager> localDirectoryManagers = new List<LocalDirectoryManager>();
    }
}
