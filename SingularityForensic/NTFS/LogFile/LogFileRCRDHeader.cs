using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Common.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.NTFS.LogFile {
    /// <summary>
    /// RCRD的记录头;
    /// </summary>
    [StructLayout( LayoutKind.Sequential,Pack = 1)]
    public struct LogFileRCRDHeaderStruct {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] MAGIC_NUMBER;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] 
        public byte[] UPDATE_SEQ_ARRAY_OFFSET;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] UPDATE_SEQ_ARRAY_COUNT;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] LAST_LSN_or_OFFSET_TO_NEXT_PAGE;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] FLAGS;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] PAGE_COUNT;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] PAGE_POSITION;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] NEXT_RECORD_OFFSET;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] RESERVED1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] LAST_END_LSN;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] FIXUP_VALUE;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] FIXUP_ARRAY;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] RESERVED2;
    }

    class LogFileRCRDHeader : StructFieldDecriptorBase<LogFileRCRDHeaderStruct>,ILogFileRecordHeader {
        public const int LogFileRCRDHeaderSize = 64;
        public LogFileRCRDHeader(LogFileRCRDHeaderStruct logFileRCRDHeaderStruct) :base(logFileRCRDHeaderStruct){

        }

        public byte[] FixupValue => StructInstance.FIXUP_VALUE;

        public byte[] FixupArray => StructInstance.FIXUP_ARRAY;

        public override string DisplayName => LanguageService.FindResourceString(Constants.DisplayName_LogFileRSTRArea);

        protected override void OnEditFieldDescriptorDisplayName(FieldInfo fieldInfo, EditingValueEventArgs<string> args) {
            args.Value = LanguageService.FindResourceString($"{Constants.LogFileRCRDHeaderFieldPrefix}{fieldInfo.Name}"); 
        }

        /// <summary>
        /// RCRDHeader的头部标识规范('RCRD');
        /// </summary>
        private static readonly byte[] RCRDHeaderMagicNumber = new byte[] { 0x52, 0x43, 0x52, 0x44 };
        /// <summary>
        /// FixUp标识规范;
        /// </summary>
        private static readonly byte[] RCRDHeaderFixUpValue = new byte[] { 0x00, 0x00 };

        /// <summary>
        /// 是否损坏，即是否标识部分是否满足规范;
        /// </summary>
        public bool MalformedPage => ByteExtensions.CompareTo(StructInstance.MAGIC_NUMBER, RCRDHeaderMagicNumber)
            && !ByteExtensions.CompareTo(StructInstance.FIXUP_VALUE,RCRDHeaderFixUpValue);
        
    }
}
