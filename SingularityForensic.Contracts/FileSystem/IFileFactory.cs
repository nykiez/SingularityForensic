using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    public interface IFileFactory {
        IPartition CreatePartition(string key);
        IDirectory CreateDirectory(string key);
        IRegularFile CreateRegularFile(string key);
        IDevice CreateDevice(string key);
    }

    public class FileFactory : GenericServiceStaticInstance<IFileFactory> {
        public static IPartition CreatePartition(string key) => Current?.CreatePartition(key);

        public static IDirectory CreateDirectory(string key) => Current?.CreateDirectory(key);

        public static IRegularFile CreateRegularFile(string key) => Current?.CreateRegularFile(key);

        public static IDevice CreateDevice(string key) => Current?.CreateDevice(key);
    }
}
