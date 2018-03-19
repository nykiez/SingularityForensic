using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    public sealed class Device : BlockedStreamFileBase<DeviceStoken> {
        public Device(string key, DeviceStoken stoken = null) : base(key, stoken) {

        }
        public string PartsType => _stoken?.PartsType;

        public IEnumerable<PartitionEntry> PartitionEntries => _stoken?.PartitionEntries;
    }

   
}
