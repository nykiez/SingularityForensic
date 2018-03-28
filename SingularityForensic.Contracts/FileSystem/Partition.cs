using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    public class PartitonStoken : BlockedStreamFileStoken {
        public long StartLBA { get; set; }
    }

    //分区类型;
    public class Partition : BlockedStreamFileBase<PartitonStoken> {
        public Partition(string key, PartitonStoken stoken = null) : base(key, stoken) {

        }

        public long StartLBA => _stoken?.StartLBA ?? 0;
    }

    /// <summary>
    /// 分区助手;
    /// </summary>
    public static class PartitionHelper {
        
    }
}
