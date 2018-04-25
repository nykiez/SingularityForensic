using SingularityForensic.Contracts.FileSystem;

namespace SingularityForensic.FileSystem {
    /// <summary>
    /// 分区类型;
    /// </summary>
    public class Partition : StreamFileBase<PartitionStoken>,IPartition {
        public Partition(string key) : base(key) {

        }

        public IPartitionType PartType => _stoken.PartType;
    }

}
