using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    //具有快组连续的特性的实体契约;
    public interface IBlockGroupedFile:IFile {
        IEnumerable<IBlockGroup> BlockGroups { get; }
    }

    public interface IBlockGroup {
        long BlockAddress { get; }
        long BlockSize { get; }
        long Offset { get; }
        long Count { get; }
    }

    /// <summary>
    /// 块组创建工厂类契约;
    /// </summary>
    public interface IBlockGroupFactory {
        /// <summary>
        /// 块组(连续)实体;的构造方法；
        /// </summary>
        /// <param name="blockAddress">起始块地址</param>
        /// <param name="count">块数目</param>
        /// <param name="offset">块起始偏移</param>
        /// <param name="blockSize">块大小</param>
        IBlockGroup CreateNewBlockGroup(long blockAddress,long count,long blockSize,long offset = 0);
    }

    public class BlockGroupFactory : GenericServiceStaticInstance<IBlockGroupFactory> {
        public static IBlockGroup CreateNewBlockGroup(long blockAddress, long count, long blockSize, long offset = 0) =>
            Current.CreateNewBlockGroup(blockAddress, count, blockSize, offset);
    }

}
