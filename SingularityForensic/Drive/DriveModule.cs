using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Drive {
    [ModuleExport(typeof(IModule))]
    public class DriveModule : IModule {
        public void Initialize() {
            ServiceProvider.Current?.GetInstance<DriveService>();
        }
        
        
    }
}
