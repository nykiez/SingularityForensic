using SingularityForensic.Contracts.FileSystem;
using System.ComponentModel.Composition;

namespace SingularityForensic.FileSystem {
    [Export(typeof(IPartitionEntryFactory))]
    class PartitionEntryFactoryImpl : IPartitionEntryFactory {
        public IPartitionEntry CreatePartitionEntry(string key) => new PartitionEntry(key);
    }
}
