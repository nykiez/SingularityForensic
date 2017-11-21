using Prism.Modularity;
using System.ComponentModel.Composition;

namespace Singularity.UI.Hex {
    [Export(typeof(HexModule))]
    public class HexModule : IModule {
        public void Initialize() {
            
        }
    }
}
