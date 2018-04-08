using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    public class PartitionStoken : BlockedStreamFileStoken {
        public string PartTypeName { get; set; }
    }

    /// <summary>
    /// 分区类型;
    /// </summary>
    public class Partition : BlockedStreamFileBase<PartitionStoken> {
        public Partition(string key, PartitionStoken stoken = null) : base(key, stoken) {

        }
        
        

        public string PartTypeName => _stoken.PartTypeName;
    }

    /// <summary>
    /// 分区助手;
    /// </summary>
    public static class PartitionHelper {
        
    }
}
