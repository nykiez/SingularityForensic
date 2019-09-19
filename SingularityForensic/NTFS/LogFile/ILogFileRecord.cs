using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.NTFS.LogFile {
    public interface ILogFileRecord {
        ILogFileRecordHeader Header { get; }
        int SectorSize { get; }
        int SectorAmount { get; }
        byte[] Data { get; }
        Dictionary<int, byte[]> OffsetDict { get; }
    }
}
