using System.Collections.Generic;

namespace SingularityForensic.Contracts.Parse.Contracts {
    //具有快组连续的特性的实体契约;
    public interface IBlockGroupedFile {
        IEnumerable<BlockGroup> BlockGroups { get; }
        long ClusterSize { get; }
    }

    //块组(连续)实体;
    public class BlockGroup {
        /// <summary>
        /// 块组(连续)实体;的构造方法；
        /// </summary>
        /// <param name="blockAddress">块地址</param>
        /// <param name="count">块数目</param>
        /// <param name="offset">块起始偏移</param>
        public BlockGroup(long blockAddress,long count,long offset = 0,int reserved = 0,int blockSize = 4096) {
            this.BlockAddress = blockAddress;
            this.Count = count;
            this.Offset = offset;
            this.Reserved = reserved;
            this.BlockSize = blockSize;
        }

        /// <summary>
        /// //块地址;
        /// </summary>
        public long BlockAddress { get; private set; }

        /// <summary>
        /// 单个块大小
        /// </summary>
        public int BlockSize { get; }

        /// <param name="offset">块起始偏移</param>
        public long Offset { get; }

        /// <summary>
        /// 保留块数目;
        /// </summary>
        public int Reserved { get; }
        /// <summary>
        /// //连续的大小;
        /// </summary>
        public long Count { get; private set; }                             
    }
}
