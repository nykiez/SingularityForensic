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

        public void Handle(IHexDataContext args) {
            
        }
    }
}
