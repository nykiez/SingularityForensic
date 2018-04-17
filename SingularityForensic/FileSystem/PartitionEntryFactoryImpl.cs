using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileSystem {
    [Export(typeof(IPartitionEntryFactory))]
    class PartitionEntryFactoryImpl : IPartitionEntryFactory {
        public IPartitionEntry CreatePartitionEntry(string key) => new PartitionEntry(key);
    }
}
