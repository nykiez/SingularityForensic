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
    struct LogFileLSNRecordHeaderStruct {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] THIS_LSN;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] PREVIOUS_LSN;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] UNDO_NEXT_LSN;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] DATA_LENGTH;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] SEQ_NUMBER;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] CLIENT_INDEX;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] RECORD_TYPE;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] TRANSACTION_ID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] FLAG;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] RESERVED;
    }

    class LogFileLSNRecordHeader: StructFieldDecriptorBase<LogFileLSNRecordHeaderStruct> {
        public LogFileLSNRecordHeader(LogFileLSNRecordHeaderStruct logFileLSNRecordHeaderStruct) :base(logFileLSNRecordHeaderStruct) {

        }

   
        public override string DisplayName => LanguageService.FindResourceString(Constants.DisplayName_LogFileLSNRecordHeader);

        protected override void OnEditFieldDescriptorDisplayName(FieldInfo fieldInfo, EditingValueEventArgs<string> args) {
            args.Value = LanguageService.FindResourceString($"{Constants.LogFileLSNRecordFieldPrefix}{fieldInfo.Name}");
        }

        public byte[] ThisLSNRaw => throw new NotImplementedException();
            //ByteExtensions.SubBytes(this. self.data[self.THIS_LSN[1]:self.THIS_LSN[2] + 1]

        public bool MalformedEntry {
            get {
                throw new NotImplementedException();
                //return True if self.this_lsn == 0 or \
                //       self.record_type == 0 or \
                //       self.record_type > 37 \
                //       else False
            }
        }
        
    }
}
