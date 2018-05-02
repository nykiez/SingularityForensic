using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Contracts.Hex.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SingularityForensic.FileExplorer.Events {
    /// <summary>
    /// 十六进制分区表高亮;
    /// </summary>
    [Export(typeof(IHexDataContextLoadedEventHandler))]
    class OnHexDataContextLoadedOnDeviceHandler : IHexDataContextLoadedEventHandler {
        public int Sort => 0;

        public bool IsEnabled => true;

        public void Handle(IHexDataContext hexDataContext) {
            if (hexDataContext == null) {
                return;
            }

            var device = hexDataContext.GetIntance<IFile>(Contracts.FileExplorer.Constants.HexDataContextTag_File) as IDevice;
            if (device == null) {
                return;
            }

            if(device.PartitionEntries == null) {
                return;
            }

            int i = 0;
            
            foreach (var ti in device.PartitionEntries.OrderBy(p => p.StartLBA)) {
                hexDataContext.CustomBackgroundBlocks?.Add(
                    (
                        ti.StartLBA,
                        ti.Size, i++ % 2 == 0 ? Brushes.LightBlue : Brushes.Chocolate
                    )
                );
            }
        }
    }
}
