using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileSystem {
    /// <summary>
    /// 分区类型;
    /// </summary>
    public class Partition : BlockedStreamFileBase<PartitionStoken>,IPartition {
        public Partition(string key) : base(key) {

        }

        public IPartitionType PartType => _stoken.PartType;
    }

}
