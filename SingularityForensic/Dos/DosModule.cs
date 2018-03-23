using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Hex.Events;

namespace SingularityForensic.Dos {
    [ModuleExport(typeof(DosModule))]
    public class DosModule : IModule {
        public void Initialize() {
            
            PubEventHelper.GetEvent<HexEditorLoadedEvent>().Subscribe(hex => {
                //int i = 0;
                //if(hex.Data is DosDevice device) {
                //    foreach (var ti in device.TableItems.OrderBy(p => p.Offset)) {
                //        hex.CustomBackgroundBlocks?.Add((ti.Offset, ti.Length, i++ % 2 == 0 ? Brushes.Blue : Brushes.Red));
                //    }
                    
                //}
            });
        }
    }
}
