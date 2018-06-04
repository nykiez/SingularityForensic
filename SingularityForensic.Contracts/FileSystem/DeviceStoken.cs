using CDFC.Util.IO;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SingularityForensic.Contracts.FileSystem {
    public class DeviceStoken : StreamFileStoken {
        public string PartsType { get; set; }                    //分区表类型;
        public IList<IPartitionEntry> PartitionEntries { get; } = new List<IPartitionEntry>(); //分区表项集合;
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
        public static void FillParts(this IDevice device, XElement xElem, IProgressReporter reporter) {
            if (device == null) {
                throw new ArgumentNullException(nameof(device));
            }
            
            var partsGroup = xElem?.GetGroup(Constants.Device_InnerParts);

            foreach (var entry in device.PartitionEntries) {
                IFile file = null;

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

                file = ParseStream(partStream, string.Empty, partElem, reporter);
                device.Children.Add(file);

                //设定StartLBA;
                if (file is IPartition part) {
                    device.SetStartLBA(part, partStartLBA);
                }
            }
        }

        /// <summary>
        /// 解析流为分区;
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="name"></param>
        /// <param name="xElem"></param>
        /// <param name="reporster"></param>
        /// <returns></returns>
        private static IFile ParseStream(Stream stream,string name,XElement xElem,IProgressReporter reporter) {
            IStreamParsingProvider provider = null;
            IFile file = null;
            var providers = ServiceProvider.Current.GetAllInstances<IStreamParsingProvider>().OrderBy(p => p.Order);

            try {
                provider = providers.FirstOrDefault(p =>
                    p.CheckIsValidStream(stream)
                );
            }
            catch(Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
            
            if (provider != null) {
                file = provider.ParseStream(stream, name, xElem, reporter);
            }
            else {
                LoggerService.WriteCallerLine("Failed to have the stream parsed.");
                file = ServiceProvider.Current?.GetInstance<IUnknownPartitionParsingProvider>()?.ParseStream(stream, name, xElem);
            }

            return file;
        }
    }

    
}
