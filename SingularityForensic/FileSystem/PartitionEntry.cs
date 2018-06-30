using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;

namespace SingularityForensic.FileSystem {
    /// <summary>
    /// 分区表项;
    /// </summary>
    public class PartitionEntry : HaveStokenBase<PartitionEntryStoken>,IPartitionEntry {
        public PartitionEntry(string key, PartitionEntryStoken stoken = null) : base(key, stoken) {

        }

        /// <summary>
        /// 分区表项起始偏移;
        /// </summary>
        public long StartLBA => _stoken?.StartLBA ?? -1;
        /// <summary>
        /// 分区表项长度;
        /// </summary>
        public long Size => _stoken?.Size ?? -1;

        /// <summary>
        /// 分区起始位移(针对部分分区表结构,如Dos);
        /// </summary>
        public long? PartStartLBA => _stoken.PartStartLBA;

        /// <summary>
        /// 分区大小(针对部分分区表结构,如Dos);
        /// </summary>
        public long? PartSize => _stoken.PartSize;

        //分区表项类型;
        public string TypeGUID => _stoken?.TypeGUID;

        //分区名;
        public string Name => _stoken?.Name;
    }
}
