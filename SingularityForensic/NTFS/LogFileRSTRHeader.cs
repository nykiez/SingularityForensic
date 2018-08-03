using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Common.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.NTFS {
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct LogFileRSTRHeaderStruct {
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
        public byte[] IXUP_VALUE;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] FIXUP_ARRAY;
    }

    class LogFileRSTRHeader : StructFieldDecriptorBase<LogFileRSTRHeaderStruct>, ICustomMemberDescriptor {
        
        //public static readonly (string propName, int offset, int length) MAGIC_NUMBER = ('magic number (name)', 0, 4);
        //UPDATE_SEQUENCE_OFFSET = ('update seq. offset', 4, 5)
        //UPDATE_SEQUENCE_COUNT  = ('update seq. count', 6, 7)
        //CHECK_DISK_LSN         = ('check disk lsn', 8, 15)
        //SYSTEM_PAGE_SIZE       = ('system page size', 16, 19)
        //LOG_PAGE_SIZE          = ('log page size', 20, 23)
        //RESTART_AREA_OFFSET    = ('restart area offset', 24, 25)
        //MINOR_VERSION          = ('minor version', 26, 27)
        //MAJOR_VERSION          = ('major version', 28, 29)
        //FIXUP_VALUE            = ('fixup value', 30, 31)
        //FIXUP_ARRAY            = ('fixup array', 32, 47)


        private const int RSTRHeaderSize = 48;
        public LogFileRSTRHeader(LogFileRSTRHeaderStruct headerStruct) :base(headerStruct){
            
        }

        public byte[] Data { get; }

        public override string DisplayName => throw new NotImplementedException();

        protected override void OnEditFieldDescriptorDisplayName(FieldInfo fieldInfo, EditingValueEventArgs<string> args) {
            throw new NotImplementedException();
        }
    }
}
