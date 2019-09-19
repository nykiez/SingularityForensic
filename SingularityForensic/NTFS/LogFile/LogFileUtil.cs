using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.NTFS.LogFile {
    public static class LogFileUtil {
        public static Dictionary<int,byte[]> SearchFixup(ILogFileRecord record, byte[] data) {
            var offset_dict = new Dictionary<int, byte[]>();
            var offset = record.SectorSize - 2;
            var fixup_value = record.Header.FixupValue;
            var count = 0;
            //start at 510, read 2 bytes, repeat every 512.
            for (int x = offset; x < data.Length; x += record.SectorSize) {
                if (ByteExtensions.CompareTo(data, record.Header.FixupValue, x)) {
                    count += 1;
                    var start = (x / offset - 1) << 1;
                    var stop = (x / offset) << 1;
                    offset_dict[x] = record.Header.FixupArray.SubBytes(x, 2);
                }
                else {
                    LoggerService.WriteCallerLine($"'Data: {data.SubBytes(x,2).BytesToHexString()} at offset {x} is not fixup_value: {fixup_value}");
                }
            }

            return count == record.SectorAmount?offset_dict:null;
        }
        
        /// Replace the fixup values with the actual values from a offset dictionary
        /// Uses bytarray as it is mutable and memoryview to adjust the data.
        ///
        /// input:
        ///   - data (4096 bytes) ~~non writable~~
        /// output:
        ///   - data (4096 bytes) ~~non writable~~
        public static void ReplaceFixup(ILogFileRecord record, byte[] data) {
            /// make it writeable through memoryview
            foreach (var offset in record.OffsetDict.Keys) {
                ///print('offset: %i -> old data: %s' % (offset, hexlify(wdata[offset:offset+2])))
                Array.Copy(record.OffsetDict[offset], 0, data, offset, 2);
                /// print('offset: %i -> new data: %s' % (offset, hexlify(wdata[offset:offset+2])))
                /// make it immutable again
            }
        }
    }
}
