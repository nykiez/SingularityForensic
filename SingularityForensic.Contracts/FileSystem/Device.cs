using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    
}
