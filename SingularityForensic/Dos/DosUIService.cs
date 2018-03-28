using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Contracts.Hex.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Dos {
    [Export]
    public class DosUIService {
        public void RegisterEvents() {
            PubEventHelper.GetEvent<HexEditorLoadedEvent>().Subscribe(OnHexEditorLoaded);
        }

        //加载Hex时若为DosDevice,进行高亮块;
        private void OnHexEditorLoaded(IHexDataContext hex) {
            //int i = 0;
            //if(hex.Data is DosDevice device) {
            //    foreach (var ti in device.TableItems.OrderBy(p => p.Offset)) {
            //        hex.CustomBackgroundBlocks?.Add((ti.Offset, ti.Length, i++ % 2 == 0 ? Brushes.Blue : Brushes.Red));
            //    }

            //}
        }

    }
}
