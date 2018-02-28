using CDFC.Parse.Modules.DeviceObjects;
using Prism.Mef.Modularity;
using Prism.Modularity;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Hex.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SingularityForensic.Dos {
    [ModuleExport(typeof(IModule))]
    public class DosModule : IModule {
        public void Initialize() {
            
            PubEventHelper.GetEvent<HexEditorLoadedEvent>().Subscribe(hex => {
                int i = 0;
                if(hex.Data is DosDevice device) {
                    foreach (var ti in device.TableItems.OrderBy(p => p.Offset)) {
                        hex.CustomBackgroundBlocks?.Add((ti.Offset, ti.Length, i++ % 2 == 0 ? Brushes.Blue : Brushes.Red));
                    }
                    
                }
            });
        }
    }
}
