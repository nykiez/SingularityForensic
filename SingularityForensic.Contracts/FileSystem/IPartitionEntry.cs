using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    /// <summary>
    /// //分区表项信息;
    /// </summary>
    public class PartitionEntryStoken : ExtensibleObject {
        /// <summary>
        /// //分区表起始偏移;
        /// </summary>
        public long StartLBA { get; set; }

        /// <summary>
        /// //分区表项长度;
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 分区起始位移;
        /// </summary>
        public long? PartStartLBA { get; set; }
        /// <summary>
        /// 分区大小;
        /// </summary>
        public long? PartSize { get; set; }
        
        /// <summary>
        /// 分区表项类型;
        /// </summary>
        public string TypeGUID { get; set; }

        /// <summary>
        /// 分区名;
        /// </summary>
        public string Name { get; set; }
    }

    public interface IPartitionEntry:IHaveStoken<PartitionEntryStoken> {
        /// <summary>
        /// //分区表起始偏移;
        /// </summary>
        long StartLBA { get; }
        /// <summary>
        /// //分区表项长度;
        /// </summary>
        long Size { get; }

        /// <summary>
        /// 分区起始位移;
        /// </summary>
        long? PartStartLBA { get; }
        /// <summary>
        /// 分区大小;
        /// </summary>
        long? PartSize { get; }

        /// <summary>
        /// 分区表项类型;
        /// </summary>
        string TypeGUID { get; }

        /// <summary>
        /// 分区名;
        /// </summary>
        string Name { get; }
    }

    /// <summary>
    /// 分区表项工厂类;
    /// </summary>
    public interface IPartitionEntryFactory {
        IPartitionEntry CreatePartitionEntry(string key);
    }

    public class PartitionEntryFactory : GenericServiceStaticInstance<IPartitionEntryFactory> {
        public static IPartitionEntry CreatePartitionEntry(string key) => Current?.CreatePartitionEntry(key);
    }
}
