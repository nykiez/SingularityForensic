using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.NTFS {
    /// <summary>
    /// LogFile-RSTR;
    /// </summary>
    class LogFileRSTR {
        private const int RSTRSize = 4096;
        public LogFileRSTR(byte[] data) {
            this.Data = data??throw new ArgumentNullException(nameof(data));
            if(data.Length != RSTRSize) {
                throw new ArgumentException($"The length of {nameof(data)} is not valid({RSTRSize}).");
            }


        }

        public byte[] Data { get; }
    }
}
