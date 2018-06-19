using SingularityForensic.Contracts.FileSystem;
using System.Collections.Generic;

namespace SingularityForensic.BaseDevice {
    /// <summary>
    /// DOS/GPT设备存储信息基类,将会保存在FileBase->Tag字段中;
    /// </summary>
    internal abstract class BaseDeviceInfo {
        public IUnmanagedBasicDeviceManager UnmanagedManager { get; set; }
    }

    /// <summary>
    /// //Dos设备信息;
    /// </summary>
    internal class DOSDeviceInfo : BaseDeviceInfo {
        public List<DOSPartInfo> DosPartInfos { get; } = new List<DOSPartInfo>();

        private const int SECSIZE = 512;
    }

    /// <summary>
    /// GPT设备信息;
    /// </summary>
    internal class GPTDeviceInfo : BaseDeviceInfo {
        public List<GPTPartInfo> GptPartInfos { get; } = new List<GPTPartInfo>();

        private const int SECSIZE = 512;
    }
}
