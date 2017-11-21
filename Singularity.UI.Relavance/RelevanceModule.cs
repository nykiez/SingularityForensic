using Prism.Mef.Modularity;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singularity.UI.Relevance {
    [ModuleExport(typeof(RelevanceModule))]
    public class RelevanceModule : IModule {
        public void Initialize() {
            
        }
    }
}
