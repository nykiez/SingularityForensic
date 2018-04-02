using CDFC.Util.IO;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SingularityForensic.Contracts.FileSystem {
    public class DeviceStoken : BlockedStreamFileStoken {
        public string PartsType { get; set; }                    //分区表类型;
        public List<PartitionEntry> PartitionEntries { get; } = new List<PartitionEntry>(); //分区表项集合;
    }

    public class Device : BlockedStreamFileBase<DeviceStoken> {
        public Device(string key, DeviceStoken stoken = null) : base(key, stoken) {

        }

        //分区类型;
        public string PartsType => _stoken.PartsType;

        //分区表项;
        public IEnumerable<PartitionEntry> PartitionEntries => _stoken.PartitionEntries;
    }

    /// <summary>
    /// 设备帮助拓展;
    /// </summary>
    public static class DeviceExtensions {
        /// <summary>
        /// 根据分区表项内容生成子分区;
        /// </summary>
        /// <param name="device"></param>
        /// <param name="key"></param>
        /// <param name="reporter"></param>
        public static void FillParts(this Device device, XElement xElem, ProgressReporter reporter) {
            if (device == null) {
                throw new ArgumentNullException(nameof(device));
            }

            var providers = ServiceProvider.Current.GetAllInstances<IStreamParsingProvider>();

            var partsGroup = xElem?.GetGroup(Constants.Device_InnerParts);

            foreach (var entry in device.PartitionEntries) {
                //若起始位移大于设备流大小,则舍弃;
                if (entry.PartStartLBA >= device.Size) {
                    continue;
                }

                var partElem = partsGroup?.CreateElem(Constants.Device_InnerPart);
                //若分区描述大小偏移超出设备流,则截取从StartLBA到设备流大小长度的区间作为分区区间;
                var size = Math.Min(entry.PartSize + entry.PartStartLBA, device.Size) - entry.PartStartLBA;
                var partStream = MulPeriodsStream.CreateFromStream(
                    device.BaseStream,
                    new(long StartIndex, long Size)[] {
                        (entry.PartStartLBA,size)
                    }
                );

                var provider = providers.FirstOrDefault(p =>
                    p.CheckIsValidStream(partStream)
                );

                if (provider == null) {
                    LoggerService.WriteCallerLine($"{nameof(provider)} is null.");
                    continue;
                }

                var file = provider.ParseStream(partStream, string.Empty, partElem, reporter);
                device.Children.Add(file);

                //设定StartLBA;
                if (file is Partition part) {
                    part.SetStartLBA(device, entry.PartStartLBA);
                }
            }

            //foreach (var provider in providers) {
            //    if(provider.CheckIsValidStream())
            //}
        }
    }

}
