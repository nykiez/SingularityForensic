using SingularityForensic.Contracts.FileSystem;
using System.ComponentModel.Composition;

namespace SingularityForensic.FileSystem {
    [Export(typeof(IFileFactory))]
    public class FileFactoryImpl : IFileFactory {
        public IDevice CreateDevice(string key) => new Device(key);

        public IDirectory CreateDirectory(string key) => new Directory(key);

        public IPartition CreatePartition(string key) => new Partition(key);

        public IRegularFile CreateRegularFile(string key) => new RegularFile(key);
    }
}
