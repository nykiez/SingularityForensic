using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.NTFS.LogFile {
    class LogFileRCRDLeftOverData {
        //public LogFileRCRDLeftOverData(lsn_hdr, lsn_data,byte[] data) {
        //    //self.lsn_hdr = lsn_hdr
        //    //self.lsn_data = lsn_data
        //    //self.data = data[self.lsn_hdr.length:]
        //    //self.data_length = len(self.data)
        //    //self.missing_data_length = self.lsn_hdr.data_length - self.data_length

        //}

        public int MissingDataLength { get; }
    }
}
