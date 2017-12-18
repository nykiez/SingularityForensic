using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singularity.Contracts.Case {
    public static class Constants {
        //安卓镜像案件文件类型;
        public const string AndroidDeviceImg = nameof(AndroidDeviceImg);
        //ADB设备案件文件类型;
        public const string AdbDevice = nameof(AdbDevice);
        //未知镜像案件文件类型;
        public const string UnKnownDeviceImg = nameof(UnKnownDeviceImg);
        //ITunes案件文件(夹)类型;
        public const string ITunesBackUpFolder = nameof(ITunesBackUpFolder);
    }
}
