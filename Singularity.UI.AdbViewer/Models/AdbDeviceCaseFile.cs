using CDFC.Info.Adb;
using Singularity.Contracts.Case;
using System;
using System.Xml.Linq;

namespace Singularity.UI.AdbViewer.Models {
    /// <summary>
    /// adb设备案件文件;
    /// </summary>
    public class AdbDeviceCaseFile : StandardCaseFile {
        public const string AdbDeviceClassFolder = "AdbDevices";
        /// <summary>
        /// adb设备案件文件构造方法(针对新加入的文件);
        /// </summary>
        /// <param name="container">实体设备</param>
        /// <param name="xelem"></param>
        public AdbDeviceCaseFile(PhoneFullInfoContainer container,XElement xelem):base(xelem) {
            this.Container = container;
        }

        public AdbDeviceCaseFile(PhoneFullInfoContainer container, DateTime dateAdded):
            base(AdbCaseFileType,container.Device.Serial,container.Device.Serial,dateAdded) {
            this.Container = container;
        }

        protected override string GetBasePath() => $"{AdbDeviceClassFolder}/{Guid.NewGuid().ToString("N")}-{Name}";

        public PhoneFullInfoContainer Container { get; }

        //adb设备案件文件类型(值);
        public const string AdbCaseFileType = "AdbDevice";
        //adb设备序列号(名称);
        public const string AdbSerialNumber = "AdbSN";
        //adb设备文件文档位置(名称);
        public const string AdbStorageFile = "AdbStorageFile.bin";
    }
}
