using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Hash {
    public interface IHasher {
        byte[] ComputeHash(Stream inputStream,IProgressReporter reporter);
    }
}
