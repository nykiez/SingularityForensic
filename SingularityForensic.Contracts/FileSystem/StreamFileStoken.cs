using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    
    public class StreamFileStoken : FileStokenBase,ISerializable {
        public Stream BaseStream { get; set; }

        public int BlockSize { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            
        }
    }

}
