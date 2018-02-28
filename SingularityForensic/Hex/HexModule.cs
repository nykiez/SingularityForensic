using Prism.Modularity;
using System.ComponentModel.Composition;

namespace SingularityForensic.Controls.Hex {
    [Export(typeof(HexModule))]
    public class HexModule : IModule {
        public void Initialize() {
            
        }
    }
}
