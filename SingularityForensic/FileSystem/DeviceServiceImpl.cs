using CDFC.Util.IO;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SingularityForensic.FileSystem {
    /// <summary>
    /// 设备帮助拓展服务;
    /// </summary>
    [Export(typeof(IDeviceService))]
    public class DeviceServiceImpl : IDeviceService {
        /// <summary>
        /// 根据分区表项内容生成子分区;
        /// </summary>
        /// <param name="device"></param>
        /// <param name="key"></param>
        /// <param name="reporter"></param>
        public void FillParts(IDevice device, IProgressReporter reporter) {
            if (device == null) {
                throw new ArgumentNullException(nameof(device));
            }
            
            var partIndex = 0;
            foreach (var entry in device.PartitionEntries) {
                IFile file = null;

                //若分区表项描述分区大小为空;
                if (entry.PartSize == null
                    || entry.PartStartLBA == null) {
                    continue;
                }

                var partSize = entry.PartSize.Value;
                var partStartLBA = entry.PartStartLBA.Value;

                //若起始位移大于设备流大小,则舍弃;
                if (entry.PartStartLBA >= device.Size) {
                    continue;
                }

                
                //若分区描述大小偏移超出设备流,则截取从StartLBA到设备流大小长度的区间作为分区区间;
                var size = Math.Min(partSize + partStartLBA, device.Size) - partStartLBA;
                var partStream = MulPeriodsStream.CreateFromStream(
                    device.BaseStream,
                    new(long StartIndex, long Size)[] {
                        (partStartLBA,size)
                    }
                );
                
                file = ParseStream(partStream, entry.Name??$"{LanguageService.FindResourceString(Constants.Prefix_Partition)}{++partIndex}", reporter);
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
        private static IFile ParseStream(Stream stream, string name, IProgressReporter reporter) {
            IStreamParsingProvider provider = null;
            IFile file = null;
            var providers = ServiceProvider.Current.GetAllInstances<IStreamParsingProvider>().OrderBy(p => p.Order);

            try {
                provider = providers.FirstOrDefault(p =>
                    p.CheckIsValidStream(stream)
                );
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }

            if (provider != null) {
                file = provider.ParseStream(stream, name, reporter);
            }
            else {
                LoggerService.WriteCallerLine("Failed to have the stream parsed.");
                file = ServiceProvider.Current?.GetInstance<IUnknownPartitionParsingProvider>()?.ParseStream(stream, name);
            }

            return file;
        }
    }
}
