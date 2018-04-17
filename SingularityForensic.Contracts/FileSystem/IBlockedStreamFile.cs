using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    public interface IBlockedStreamFile<TStoken> :IFile<TStoken>,IBlockedStream, IDisposable,
         IHaveFileCollection where TStoken : BlockedStreamFileStoken, new() {
        event EventHandler Disposing;
    }
}
