using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Contracts.Hex.Events;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.BaseDevice.Events.Hex {
    /// <summary>
    /// 高亮MBR设备信息;
    /// </summary>
    [Export(typeof(IHexDataContextLoadedEventHandler))]
    class OnHexDataContextLoadedOnMBRDeviceHandler : IHexDataContextLoadedEventHandler {
        public int Sort => 619;

        public bool IsEnabled => true;

        public void Handle(IHexDataContext hexDataContext) {
            if (hexDataContext == null) {
                return;
            }

            var device = hexDataContext.GetInstance<IFile>(Contracts.FileExplorer.Constants.HexDataContextTag_File) as IDevice;
            if (device == null) {
                return;
            }

            if (!device.TypeGuids?.Contains(Constants.DeviceType_DOS) ?? false) {
                return;
            }

            var dosDeviceInfo = device.GetInstance<DOSDeviceInfo>(Constants.DeviceStokenTag_DOSDeviceInfo);
            if (dosDeviceInfo == null) {
                LoggerService.WriteCallerLine($"{nameof(dosDeviceInfo)} can't be null.");
                return;
            }

            var pTableIndex = 0;
            foreach (var p in dosDeviceInfo.DosPartInfos.OrderBy(p => p.DosPTable.StructInstance.nOffset)) {
                if (p.InfoDisk != null) {
                    hexDataContext.UpdateDescriptorBackgroundAndToolTips(
                        p.InfoDisk,
                        (long)p.DosPTable.StructInstance.nOffset,
                        pTableIndex % 2 == 0 ? BrushBlockFactory.FirstBrush : BrushBlockFactory.SecondBrush,
                        BrushBlockFactory.HighLightBrush,
                        Constants.BaseDeviceFieldPrefix_InfoDisk
                    );
                }
                
                pTableIndex++;
            }
        }
    }
}
