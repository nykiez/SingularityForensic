using CDFC.Util.IO;
using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
   

    ////通过文件创建流对象;
    //public static Stream CreateStreamByFile(IFile file) {
    //    if (file == null) {
    //        throw new ArgumentNullException(nameof(file));
    //    }

    //    try {
    //        if (file.Type == FileType.BlockDeviceFile) {                        //若为块文件，则文件为设备或分区;
    //            if (file is Partition part) {                                            //若为分区，则根据分区的起始，终止地址构造流;
    //                var device = file.GetParent<Device>();
    //                if (device != null) {
    //                    return InterceptStream.CreateFromStream(device.Stream, part.StartLBA, part.Size);
    //                }
    //            }
    //            else if (file is Device device) {                                          //若为设备,直接返回设备流;
    //                return device.Stream;
    //            }
    //        }
    //        //否则直接根据StartLBA,EndLBA构建截取流;
    //        else if (file is RegularFile regFile) {
    //            return regFile.GetStream();
    //        }
    //        else if (file is IBlockGroupedFile) {
    //            var blockGroupedFile = file as IBlockGroupedFile;
    //            var part = file.GetParent<Partition>();
    //            var device = file.GetParent<Device>();
    //            if (part != null && device != null) {
    //                var blockSize = part.ClusterSize;
    //                var partStream = InterceptStream.CreateFromStream(device.Stream, part.StartLBA, part.Size);
    //                //若块组不为空,则遍历块组组成虚拟流;
    //                if (blockGroupedFile.BlockGroups != null) {
    //                    var ranges = blockGroupedFile.BlockGroups.Select(p =>
    //                        ValueTuple.Create(p.BlockAddress * blockSize + p.Offset, p.Count * blockSize)).ToArray();

    //                    var blockSub = ranges.Sum(p => p.Item2) - file.Size;
    //                    if (ranges?.Count() > 0 && 0 < blockSub && blockSub < blockSize) {
    //                        ranges[ranges.Count() - 1].Item2 -= blockSub;
    //                    }
    //                    var multiStream = MulPeriodsStream.CreateFromStream(partStream, ranges);
    //                    return multiStream;
    //                }

    //            }
    //        }
    //    }
    //    catch (Exception ex) {
    //        LoggerService.Current?.WriteLine($"{nameof(StreamExtensions)}->{nameof(CreateStreamByFile)}:{ex.Message}");
    //    }

    //    return null;
    //}
}
