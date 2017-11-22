using System.Collections.Generic;

namespace CDFC.Parse.Contracts {
    //具有快组连续的特性的实体契约;
    public interface IBlockGroupedFile {
        List<BlockGroup> BlockGroups { get; }
    }

    //块组(连续)实体;
    public class BlockGroup {
        /// <summary>
        /// 块组(连续)实体;的构造方法；
        /// </summary>
        /// <param name="blockAddress">块地址</param>
        /// <param name="count">块数目</param>
        public BlockGroup(long blockAddress,long count) {
            this.BlockAddress = blockAddress;
            this.Count = count;
        }
        /// <summary>
        /// //块地址;
        /// </summary>
        public long BlockAddress { get; private set; }
        /// <summary>
        /// //连续的大小;
        /// </summary>
        public long Count { get; private set; }                             
    }
}
