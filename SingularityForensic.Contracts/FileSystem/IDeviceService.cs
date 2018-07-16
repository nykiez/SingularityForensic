using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SingularityForensic.Contracts.FileSystem {
    /// <summary>
    /// 设备服务对象;
    /// </summary>
    public interface IDeviceService {
        /// <summary>
        /// 根据分区表项内容生成子分区;
        /// </summary>
        /// <param name="device"></param>
        /// <param name="key"></param>
        /// <param name="reporter"></param>
        void FillParts(IDevice device, IProgressReporter reporter);
    }

    public class DeviceService : GenericServiceStaticInstance<IDeviceService> {
        public static void FillParts(IDevice device, IProgressReporter reporter) => Current.FillParts(device, reporter);
    }

    public static class DeviceExtensions {
        public static void FillParts(this IDevice device, IProgressReporter reporter) => DeviceService.FillParts(device, reporter);
    }
}
