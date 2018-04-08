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
        public IList<PartitionEntry> PartitionEntries { get; } = new List<PartitionEntry>(); //分区表项集合;
    }

    public class Device : BlockedStreamFileBase<DeviceStoken> {
        public Device(string key, DeviceStoken stoken = null) : base(key, stoken) {

        }

        //分区类型;
        public string PartsType => _stoken.PartsType;

        //分区表项;
        public IEnumerable<PartitionEntry> PartitionEntries => _stoken.PartitionEntries;
        public Dictionary<Partition, long> _startLBADicts = new Dictionary<Partition, long>();

        /// <summary>
        /// 设定分区起始LBA;
        /// </summary>
        /// <param name="part"></param>
        public void SetStartLBA(Partition part, long startLBA) {
            if (part.Parent != this) {
                throw new InvalidOperationException($"The StartLBA can only be indicated with {nameof(Parent)} of this instance.");
            }
            if (!(Children?.Contains(part) ?? false)) {
                throw new InvalidOperationException($"The {nameof(Device)} doesn't contain the child");
            }

            _startLBADicts[part] = startLBA;
        }

        /// <summary>
        /// 获取分区起始LBA;
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public long GetStartLBA(Partition part) {
            return _startLBADicts[part];
        }
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
                //若分区表项描述分区大小为空;
                if(entry.PartSize == null
                    || entry.PartStartLBA == null) {
                    continue;
                }

                var partSize = entry.PartSize.Value;
                var partStartLBA = entry.PartStartLBA.Value;

                //若起始位移大于设备流大小,则舍弃;
                if (entry.PartStartLBA >= device.Size) {
                    continue;
                }

                var partElem = partsGroup?.CreateElem(Constants.Device_InnerPart);
                //若分区描述大小偏移超出设备流,则截取从StartLBA到设备流大小长度的区间作为分区区间;
                var size = Math.Min(partSize + partStartLBA, device.Size) - partStartLBA;
                var partStream = MulPeriodsStream.CreateFromStream(
                    device.BaseStream,
                    new(long StartIndex, long Size)[] {
                        (partStartLBA,size)
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
                    device.SetStartLBA(part, partStartLBA);
                }
            }
            
        }

        
    }

}
