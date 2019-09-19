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
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct LogFileRSTRHeaderStruct  {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] MAGIC_NUMBER;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] UPDATE_SEQUENCE_OFFSET;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] UPDATE_SEQUENCE_COUNT;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] CHECK_DISK_LSN;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] YSTEM_PAGE_SIZE;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] OG_PAGE_SIZE;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] RESTART_AREA_OFFSET;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] MINOR_VERSION;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] MAJOR_VERSION;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] FIXUP_VALUE;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] FIXUP_ARRAY;

    }
    
    public class LogFileRSTRHeader : StructFieldDecriptorBase<LogFileRSTRHeaderStruct>, ICustomMemberDescriptor, ILogFileRecordHeader {
        public LogFileRSTRHeader(LogFileRSTRHeaderStruct headerStruct) :base(headerStruct){
            
        }

        public const int RSTRHeaderSize = 48;

        public override string DisplayName => LanguageService.FindResourceString(Constants.DisplayName_LogFileRSTRHeader);

        public byte[] FixupValue => StructInstance.FIXUP_VALUE;

        public byte[] FixupArray => StructInstance.FIXUP_ARRAY;

        protected override void OnEditFieldDescriptorDisplayName(FieldInfo fieldInfo, EditingValueEventArgs<string> args) {
            args.Value = LanguageService.FindResourceString($"{Constants.LogFileRSTRHeaderFieldPrefix}{fieldInfo.Name}");
        }
    }
}
