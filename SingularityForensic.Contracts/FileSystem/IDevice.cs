using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    public interface IDevice : IBlockedStreamFile<DeviceStoken>{
        IEnumerable<IPartitionEntry> PartitionEntries { get; }
        void SetStartLBA(IPartition part, long startLBA);
        long GetStartLBA(IPartition part);
    }
    
}
