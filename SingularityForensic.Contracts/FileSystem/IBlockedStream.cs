using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    public interface IBlockedStream {
        Stream BaseStream { get; }
        int BlockSize { get; }
    }
}
