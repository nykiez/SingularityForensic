using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.NTFS {
    /// <summary>
    /// Contains the USN Record Length(32bits), USN(64bits), File Reference Number(64bits), 
    /// Parent File Reference Number(64bits), Reason Code(32bits), File Attributes(32bits),
    /// File Name Length(32bits), the File Name Offset(32bits) and the File Name.
    /// </summary>
    public class UsnRecordV2 {
        private const int FR_OFFSET = 8;
        private const int PFR_OFFSET = 16;
        private const int USN_OFFSET = 24;
        private const int DateTime_OFFSET = 32;
        private const int REASON_OFFSET = 40;
        private const int FA_OFFSET = 52;
        private const int FNL_OFFSET = 56;
        private const int FN_OFFSET = 58;
        private const int StructWithoutFileName_Size = 60;

        public long RecordPosition { get; private set; }
        public UInt32 RecordLength { get; private set; }
        public UInt64 FileReferenceNumber { get; private set; }
        public UInt64 ParentFileReferenceNumber { get; private set; }
        public Int64 Usn { get; private set; }
        public DateTime? DateTime { get;private set; }
        public UInt32 Reason { get; private set; }
        public UInt32 FileAttributes { get; private set; }
        public Int32 FileNameLength { get; private set; }
        public ushort FileNameOffset { get; private set; }
        public string FileName { get; private set; }

        /// <summary>
        /// USN Record Constructor
        /// </summary>
        /// <param name="usnRecordPtr">Buffer of bytes representing the USN Record</param>
        public UsnRecordV2(IntPtr usnRecordPtr) {
            this.RecordLength = (UInt32)Marshal.ReadInt32(usnRecordPtr);
            this.FileReferenceNumber = (UInt64)Marshal.ReadInt64(usnRecordPtr, FR_OFFSET);
            this.ParentFileReferenceNumber = (UInt64)Marshal.ReadInt64(usnRecordPtr, PFR_OFFSET);
            this.Usn = Marshal.ReadInt64(usnRecordPtr, USN_OFFSET);
            this.DateTime = System.DateTime.FromFileTime(Marshal.ReadInt64(usnRecordPtr, DateTime_OFFSET));
            this.Reason = (UInt32)Marshal.ReadInt32(usnRecordPtr, REASON_OFFSET);
            this.FileAttributes = (UInt32)Marshal.ReadInt32(usnRecordPtr, FA_OFFSET);
            this.FileNameLength = Marshal.ReadInt16(usnRecordPtr, FNL_OFFSET);
            this.FileNameOffset = (ushort)Marshal.ReadInt16(usnRecordPtr, FN_OFFSET);
            this.FileName = Marshal.PtrToStringUni(new IntPtr(usnRecordPtr.ToInt32() + this.FileNameOffset), this.FileNameLength / sizeof(char));
        }

        private UsnRecordV2() {

        }

#if DEBUG
        ~UsnRecordV2() {

        }
#endif

        //轮询缓冲区长度;
        const int TestBufferLength = 512;
        //记录所在Position必为8的倍数;
        const int UnitLength = 8;
        /// <summary>
        /// 从流的当前位置读取一个Usn记录;
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static UsnRecordV2 ReadFromStream(Stream stream) {
            return ReadFromStreamCore(stream);
        }

        /// <summary>
        /// 从流的当前位置读取一个Usn记录;
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="testBuffer">轮询至零缓冲区</param>
        /// <returns></returns>
        private static UsnRecordV2 ReadFromStreamCore(Stream stream,byte[] testBuffer = null) {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            if (stream.Position >= stream.Length) {
                return null;
            }

            if(testBuffer == null) {
                testBuffer = new byte[TestBufferLength];
            }
            else if(testBuffer.Length != TestBufferLength){
                throw new ArgumentException($"The length of {nameof(testBuffer)} is not valid,Expected:{TestBufferLength},Actual:{testBuffer.Length}");
            }

            stream.Position = stream.Position / UnitLength * UnitLength;
            
            var readLength = 0;
            var noZeroIndex = -1;
            while ((readLength = stream.Read(testBuffer, 0, TestBufferLength)) != 0) {
                for (int i = 0; i < readLength; i++) {
                    if (testBuffer[i] != 0) {
                        noZeroIndex = i;
                        break;
                    }
                }
                if (noZeroIndex != -1) {
                    break;
                }
            }

            if (noZeroIndex == -1) {
                return null;
            }

            stream.Position -= (readLength - noZeroIndex);
            while (stream.ReadByte() == 0) ;
            stream.Position--;
            
            var startPosition = stream.Position;

            var leftCount = stream.Length - stream.Position;
            if (leftCount < StructWithoutFileName_Size) {
                return null;
            }

            var usnRecord = new UsnRecordV2();
            var bts = stream.ReadExact(StructWithoutFileName_Size);
            usnRecord.RecordLength = bts.ToUInt32LittleEndian(0);
            usnRecord.FileReferenceNumber = bts.ToUInt64LittleEndian(FR_OFFSET);
            usnRecord.ParentFileReferenceNumber = bts.ToUInt64LittleEndian(PFR_OFFSET);
            usnRecord.Usn = (long)bts.ToUInt64LittleEndian(USN_OFFSET);
            usnRecord.DateTime = System.DateTime.FromFileTime(bts.ToInt64LittleEndian(DateTime_OFFSET));
            usnRecord.Reason = bts.ToUInt32LittleEndian(REASON_OFFSET);
            usnRecord.FileAttributes = bts.ToUInt32LittleEndian(FA_OFFSET);
            usnRecord.FileNameLength = bts.ToUInt16LittleEndian(FNL_OFFSET);
            usnRecord.FileNameOffset = bts.ToUInt16LittleEndian(FN_OFFSET);
            usnRecord.RecordPosition = startPosition;

            leftCount = stream.Length - stream.Position;
            if (leftCount < usnRecord.FileNameLength) {
                return usnRecord;
            }
            var nameBts = stream.ReadExact(usnRecord.FileNameLength);
            usnRecord.FileName = Encoding.Unicode.GetString(nameBts);

            stream.Position = startPosition + usnRecord.RecordLength;
            return usnRecord;
        }

        public static IEnumerable<UsnRecordV2> ReadRecordsFromStream(Stream stream) {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }
            
            stream.Position = 0;

            UsnRecordV2 record = null;
            var testBuffer = new byte[TestBufferLength];
            while((record = ReadFromStreamCore(stream,testBuffer)) != null) {
                yield return record;
            }
        }
    }
}
