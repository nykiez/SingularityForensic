using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Local {
    /// <summary>
    /// 本地文件/硬盘模块;
    /// </summary>
    [ModuleExport(typeof(IModule))]
    class LocalModule : IModule {
        public void Initialize() {
            ServiceProvider.GetInstance<LocalFileService>()?.Initialize();
        }
    }
}
