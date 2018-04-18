using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    public class StreamFileStoken : FileStokenBase {
        public Stream BaseStream { get; set; }

        public int BlockSize { get; set; }
    }

}
