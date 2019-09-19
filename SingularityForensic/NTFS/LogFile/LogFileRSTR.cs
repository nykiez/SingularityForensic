using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.NTFS.LogFile {
    /// <summary>
    /// LogFile-RSTR;
    /// </summary>
    public class LogFileRSTR : ILogFileRecord {
        public const int RSTRSize = 4096;
        public LogFileRSTR(byte[] data) {
            this.Data = data ?? throw new ArgumentNullException(nameof(data));
            if (data.Length != RSTRSize) {
                throw new ArgumentException($"The length of {nameof(data)} is not valid({RSTRSize}).");
            }

            var headerStruct = data.ToStructWithMarshal<LogFileRSTRHeaderStruct>();
            if(headerStruct == null) {
                throw new InvalidOperationException($"Failed to get {nameof(LogFileRSTRHeaderStruct)}.");
            }
            RSTRHeader = new LogFileRSTRHeader(headerStruct.Value);

            var areaStruct = data.ToStructWithMarshal<LogFileRSTRRestartAreaStruct>(LogFileRSTRHeader.RSTRHeaderSize);
            if(areaStruct == null) {
                throw new InvalidOperationException($"Failed to get {nameof(LogFileRSTRRestartAreaStruct)}.");
            }
            RSTRArea = new LogFileRSTRRestartArea(areaStruct.Value);

            var clientStruct = data.ToStructWithMarshal<LogFileRSTRClientStruct>(LogFileRSTRHeader.RSTRHeaderSize + LogFileRSTRRestartArea.RSTRRestartAreaSize); 
            if(clientStruct == null) {
                throw new InvalidOperationException($"Failed to get {nameof(LogFileRSTRClientStruct)}.");
            }
            RSTRClient = new LogFileRSTRClient(clientStruct.Value);
        }

        public LogFileRSTRHeader RSTRHeader { get; }
        public LogFileRSTRRestartArea RSTRArea { get; }
        public LogFileRSTRClient RSTRClient { get; }
        public byte[] Data { get; }

        public int SectorSize => 512;

        public int SectorAmount => 8;

        public ILogFileRecordHeader Header => RSTRHeader;

        public Dictionary<int, byte[]> OffsetDict { get; private set; }
    }
}
