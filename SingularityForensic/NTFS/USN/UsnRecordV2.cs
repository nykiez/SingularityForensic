using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.NTFS.USN {
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

        /// <summary>
        /// 记录在流中的位置,非原结构体中成员;
        /// </summary>
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
        
        private UsnRecordV2() {

        }

#if DEBUG
        ~UsnRecordV2() {

        }
#endif
        
        //记录所在位置必为8的倍数;
        const int UnitLength = 8;
        const int ClusterSize = 4096;
        /// <summary>
        /// 从流的当前位置读取一个Usn记录;
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static UsnRecordV2 ReadFromStream(Stream stream) {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            if (stream.Position >= stream.Length) {
                return null;
            }

            var buffer = new byte[ClusterSize];

            stream.Position = stream.Position / UnitLength * UnitLength;

            var readLength = 0;
            var nonZeroIndex = -1;
            while ((readLength = stream.Read(buffer, 0, ClusterSize)) != 0) {
                for (int i = 0; i < readLength; i++) {
                    if (buffer[i] != 0) {
                        nonZeroIndex = i;
                        break;
                    }
                }
                if (nonZeroIndex != -1) {
                    break;
                }
            }

            if (nonZeroIndex == -1) {
                return null;
            }

            stream.Position -= (readLength - nonZeroIndex);
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

        

        //轮询缓冲区长度;
        const int TraverseBufferLength = ClusterSize * 1024;
        /// <summary>
        /// 从流中读取所有可能的Usn序列,注意,此方法延迟返回;
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static IEnumerable<UsnRecordV2> ReadRecordsFromStream(Stream stream) {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }
            
            stream.Position = 0;

            //To-Do,根据观察,usn存储在文件中的位置以簇大小(大多为4096,由于尚未遇见非4096的情况,所以未做其它考虑)取整
            //即在上一个usn记录存储完后,所在簇的剩余空间
            //不足以存入下下一个记录时，那么，下一条记录将以下一个簇的起始位置为起始被存储,本簇的余下部分将被零填充;
            //利用如上特性,将缓冲区的大小设置为簇大小的倍数,可以将流(文件)切分为多个块进行内存内解析,以提高处理速度;
            UsnRecordV2 record = null;
            var traverseBuffer = new byte[TraverseBufferLength];
            byte[] buffer = null;
            while(stream.Position < stream.Length) {
                var bufferPosition = stream.Position;
                var startIndex = 0;

                if(stream.Length - stream.Position > TraverseBufferLength) {
                    stream.Read(traverseBuffer, 0, TraverseBufferLength);
                    buffer = traverseBuffer;
                }
                else {
                    buffer = new byte[stream.Length - stream.Position];
                    stream.Read(buffer, 0, buffer.Length);
                }
                
                while((record = ReadFromBuffer(buffer,ref startIndex)) != null) {
                    record.RecordPosition = bufferPosition + startIndex;
                    yield return record;
                }
            }
        }

        /// <summary>
        /// 从缓冲区中某个起始位置读取一个记录;
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="startIndex">可能的起始位置</param>
        /// <returns></returns>
        private static UsnRecordV2 ReadFromBuffer(byte[] buffer,ref int startIndex) {
            if(buffer == null) {
                throw new ArgumentNullException(nameof(buffer));
            }

            while(startIndex < buffer.Length && buffer[startIndex] == 0) {
                startIndex++;
            }

            //记录余下的长度;
            var leftLength = buffer.Length - startIndex;
            if (leftLength < StructWithoutFileName_Size) {
                return null;
            }

            var usnRecord = new UsnRecordV2 {
                RecordLength = buffer.ToUInt32LittleEndian(startIndex),
                FileReferenceNumber = buffer.ToUInt64LittleEndian(FR_OFFSET + startIndex),
                ParentFileReferenceNumber = buffer.ToUInt64LittleEndian(PFR_OFFSET + startIndex),
                Usn = (long)buffer.ToUInt64LittleEndian(USN_OFFSET),
                DateTime = System.DateTime.FromFileTime(buffer.ToInt64LittleEndian(DateTime_OFFSET + startIndex)),
                Reason = buffer.ToUInt32LittleEndian(REASON_OFFSET + startIndex),
                FileAttributes = buffer.ToUInt32LittleEndian(FA_OFFSET + startIndex),
                FileNameLength = buffer.ToUInt16LittleEndian(FNL_OFFSET + startIndex),
                FileNameOffset = buffer.ToUInt16LittleEndian(FN_OFFSET + startIndex)
            };

            leftLength = buffer.Length - startIndex - StructWithoutFileName_Size;

            if(leftLength >= usnRecord.FileNameLength) {
                //读取名称;
                try {
                    usnRecord.FileName = Encoding.Unicode.GetString(buffer, startIndex + StructWithoutFileName_Size, usnRecord.FileNameLength);
                }
                catch(Exception ex) {
                    LoggerService.WriteException(ex);
                }
            }
            

            startIndex += (int)usnRecord.RecordLength;
            return usnRecord;
        }
        
    }
}
