using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    /// <summary>
    /// 文件创建工厂;
    /// </summary>
    public interface IFileFactory {
        /// <summary>
        /// 创建一个分区;
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IPartition CreatePartition(string key);
        /// <summary>
        /// 创建一个目录;
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IDirectory CreateDirectory(string key);
        /// <summary>
        /// 创建一个文件;
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IRegularFile CreateRegularFile(string key);
        /// <summary>
        /// 创建一个设备;
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IDevice CreateDevice(string key);
    }

    public class FileFactory : GenericServiceStaticInstance<IFileFactory> {
        public static IPartition CreatePartition(string key) => Current?.CreatePartition(key);

        public static IDirectory CreateDirectory(string key) => Current?.CreateDirectory(key);

        public static IRegularFile CreateRegularFile(string key) => Current?.CreateRegularFile(key);

        public static IDevice CreateDevice(string key) => Current?.CreateDevice(key);
    }
}
