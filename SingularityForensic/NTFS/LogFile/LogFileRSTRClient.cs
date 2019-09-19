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
    [StructLayout(LayoutKind.Sequential,Pack = 1)]
    public struct LogFileRSTRClientStruct {
        [MarshalAs(UnmanagedType.ByValArray,SizeConst = 8)]
        public byte[] OLDEST_LSN;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] CLIENT_RESTART_LSN;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] PREVIOUS_CLIENT;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] NEXT_CLIENT;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] SEQUENCE_NUMBER;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] RESERVED_LC_AREA;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] CLIENT_NAME_LENGTH;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] CLIENT_NAME_1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] CLIENT_NAME_2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] CLIENT_NAME_3;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] CLIENT_NAME_4;
    }

    public class LogFileRSTRClient:StructFieldDecriptorBase<LogFileRSTRClientStruct> {
        public LogFileRSTRClient(LogFileRSTRClientStruct clientStruct):base(clientStruct) {
            
        }

        public const int LogFileRSTRClientSize = 96;

        public override string DisplayName => LanguageService.FindResourceString(Constants.DisplayName_LogFileRSTRClient);

        protected override void OnEditFieldDescriptorDisplayName(FieldInfo fieldInfo, EditingValueEventArgs<string> args) {
            args.Value = LanguageService.FindResourceString($"{Constants.LogFileRSTRClientFieldPrefix}{fieldInfo.Name}");
        }
    }
}
