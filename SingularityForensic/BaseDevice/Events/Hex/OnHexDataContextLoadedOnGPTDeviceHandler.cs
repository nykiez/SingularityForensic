using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Contracts.Hex.Events;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.BaseDevice.Events.Hex {
    /// <summary>
    /// 高亮GPT设备信息;
    /// </summary>
    [Export(typeof(IHexDataContextLoadedEventHandler))]
    class OnHexDataContextLoadedOnGPTDeviceHandler : IHexDataContextLoadedEventHandler {
        public int Sort => 618;

        public bool IsEnabled => true;

        public void Handle(IHexDataContext hexDataContext) {
            if (hexDataContext == null) {
                return;
            }

            var device = hexDataContext.GetInstance<IFile>(Contracts.FileExplorer.Constants.HexDataContextTag_File) as IDevice;
            if (device == null) {
                return;
            }

            if (!device.TypeGuids?.Contains(Constants.DeviceType_GPT) ?? false) {
                return;
            }
            
            var gptDeviceInfo = device.GetInstance<GPTDeviceInfo>(Constants.DeviceStokenTag_GPTDeviceInfo);
            if(gptDeviceInfo == null) {
                LoggerService.WriteCallerLine($"{nameof(gptDeviceInfo)} can't be null.");
                return;
            }

            var pTableIndex = 0;
            foreach (var p in gptDeviceInfo.GptPartInfos.OrderBy(p => p.StGptPTable.nOffset)) {
                if (p.InfoDisk != null) {
                    hexDataContext.UpdateDescriptorBackgroundAndToolTips(
                        p.InfoDisk,
                        (long)p.StGptPTable.nOffset,
                        pTableIndex % 2 == 0 ? BrushBlockFactory.FirstBrush : BrushBlockFactory.SecondBrush,
                        BrushBlockFactory.HighLightBrush,
                        Constants.BaseDeviceFieldPrefix_InfoDisk
                    );
                }

                if (p.EFIInfo != null) {
                    hexDataContext.UpdateDescriptorBackgroundAndToolTips(
                        p.EFIInfo,
                        (long)p.StGptPTable.nOffset,
                        pTableIndex % 2 == 0 ? BrushBlockFactory.FirstBrush : BrushBlockFactory.SecondBrush,
                        BrushBlockFactory.HighLightBrush,
                        Constants.GptFieldPrefix_EFIInfo
                    );
                }

                if(p.EFIPTable != null) {
                    hexDataContext.UpdateDescriptorBackgroundAndToolTips(
                        p.EFIPTable,
                        (long)p.StGptPTable.nOffset,
                        pTableIndex % 2 == 0 ? BrushBlockFactory.FirstBrush : BrushBlockFactory.SecondBrush,
                        BrushBlockFactory.HighLightBrush,
                        Constants.GptFieldPrefix_EFIPTable);
                }
                pTableIndex++;
            }
            
        }
    }
}
