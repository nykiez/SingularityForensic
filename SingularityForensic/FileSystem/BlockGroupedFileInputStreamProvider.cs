using CDFC.Util.IO;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileSystem {
    /// <summary>
    /// 基于文件块获取文件内容;
    /// </summary>
    [Export(typeof(IFileInputStreamProvider))]
    public class BlockGroupedFileInputStreamProvider : IFileInputStreamProvider {
        public Stream GetInputStream(IFile file) {
            if (!(file is IBlockGroupedFile blockGrouped)) {
                return null;
            }

            var streamFile = blockGrouped.GetParent<IStreamFile>();
            if (streamFile == null) {
                LoggerService.Current?.WriteCallerLine($"{nameof(streamFile)} can't be null!");
                return null;
            }

            //检查块组是否为空;
            if (blockGrouped.BlockGroups == null) {
                LoggerService.WriteCallerLine($"{nameof(blockGrouped.BlockGroups)} can't be null.");
                return null;
            }

            //检查块组集合是否为空;
            if (blockGrouped.BlockGroups.FirstOrDefault() == null) {
                LoggerService.WriteCallerLine($"{nameof(blockGrouped.BlockGroups)} can't be empty.");
                return null;
            }

            var blockSize = streamFile.BlockSize;
            var partStream = streamFile.BaseStream;
            if (partStream == null) {
                LoggerService.WriteCallerLine($"{nameof(streamFile.BaseStream)} can't be null.");
                return null;
            }

            //若块组不为空,则遍历块组组成虚拟流;
            var blockSum = blockGrouped.BlockGroups.Sum(p => p.BlockSize * p.Count);
            //核对和Size之间的差,最终将要切去最后一个簇的多余内容;
            var blockSub = blockSum - file.Size;
            //取所有块组的位置;
            var ranges = blockGrouped.BlockGroups.Select(p =>
                        ValueTuple.Create(p.Offset, p.Count * p.BlockSize)).
                        ToArray();

            //假如file.Size大于块组合,则忽视大小,直接取块组合;
            if (blockSub < 0) {
                return MulPeriodsStream.CreateFromStream(partStream, ranges);
            }
            //假如file.Size小于块组合;
            else {
                long blockSumSize = 0;
                int rangeIndex = 0;
                long lastRangeSize = 0;
                foreach (var range in ranges) {
                    if (blockSumSize + range.Item2 >= file.Size) {
                        lastRangeSize = file.Size - blockSumSize;
                        break;
                    }
                    blockSumSize += range.Item2;
                    rangeIndex++;
                }

                ranges[rangeIndex].Item2 = lastRangeSize;

                return MulPeriodsStream.CreateFromStream(partStream, ranges.Take(rangeIndex + 1).ToArray());
            }

        }
    }
}
