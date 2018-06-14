using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Contracts.Hex.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.BaseDevice.Events {
    /// <summary>
    /// 高亮GPT设备信息;
    /// </summary>
    [Export(typeof(IHexDataContextLoadedEventHandler))]
    class OnHexDataContextLoadedOnGPTDeviceHandler : IHexDataContextLoadedEventHandler {
        public int Sort => 64;

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

            gptDeviceInfo.GptPartInfos.ForEach(p => {
                
            });
        }
    }
}
