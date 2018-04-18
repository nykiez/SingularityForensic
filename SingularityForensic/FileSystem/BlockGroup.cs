using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileSystem {

    /// <summary>
    /// 块组;
    /// </summary>
    public class BlockGroup:IBlockGroup {
        /// <summary>
        /// 块组(连续)实体;的构造方法；
        /// </summary>
        /// <param name="blockAddress">起始块地址</param>
        /// <param name="count">块数目</param>
        /// <param name="offset">块起始偏移</param>
        /// <param name="blockSize">块大小</param>
        public BlockGroup(
            long blockAddress,
            long count,
            long blockSize,
            long offset = 0) {

            this.BlockAddress = blockAddress;
            this.Count = count;
            this.Offset = offset;
            this.BlockSize = blockSize;
        }

        /// <summary>
        /// //块地址;
        /// </summary>
        public long BlockAddress { get; }

        /// <summary>
        /// 单个块大小
        /// </summary>
        public long BlockSize { get; }

        /// <summary>
        /// 块起始偏移
        /// </summary>
        public long Offset { get; }


        /// <summary>
        /// //连续的大小;
        /// </summary>
        public long Count { get; set; }
    }

    [Export(typeof(IBlockGroupFactory))]
    class BlockGroupFactory : IBlockGroupFactory {
        public IBlockGroup CreateNewBlockGroup(long blockAddress, long count, long blockSize, long offset = 0) {
            return new BlockGroup(blockAddress, count, blockSize, offset);
        }
    }
}
