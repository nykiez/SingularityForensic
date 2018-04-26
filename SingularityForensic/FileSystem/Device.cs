using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;

namespace SingularityForensic.FileSystem {
    public class Device : StreamFileBase<DeviceStoken>, IDevice {
        public Device(string key) : base(key) {

        }

        //分区类型;
        public string PartsType => _stoken.PartsType;

        //分区表项;
        public IEnumerable<IPartitionEntry> PartitionEntries => _stoken.PartitionEntries;
        public Dictionary<IPartition, long> _startLBADicts = new Dictionary<IPartition, long>();

        /// <summary>
        /// 设定分区起始LBA;
        /// </summary>
        /// <param name="part"></param>
        public void SetStartLBA(IPartition part, long startLBA) {
            if (part.Parent != this) {
                throw new InvalidOperationException($"The StartLBA can only be indicated with {nameof(Parent)} of this instance.");
            }
            if (!(Children?.Contains(part) ?? false)) {
                throw new InvalidOperationException($"The {nameof(IDevice)} doesn't contain the child");
            }

            _startLBADicts[part] = startLBA;
        }

        /// <summary>
        /// 获取分区起始LBA;
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public long GetStartLBA(IPartition part) {
            return _startLBADicts[part];
        }
    }
}
