using System.Runtime.InteropServices;

namespace CDFC.Parse.Android.Structs {
    public struct StEFIInfo {
        public ulong  EFIPART;                       //EFI签名(EFI PART)		8 byte
        public ushort Version;                                 //版本					4 byte
        public ushort EFISize;                                 //EFI信息大小字节数		4 byte
        public ushort CRC;                                     //EFI信息 CRC校验和		4 byte
        public ushort Unknown;                                 //保留					4 byte
        public ulong  EFICurrSecNum;                 //当前EFI LBA扇区号		8 byte
        public ulong  EFIBackupSecNum;               //备份EFI LBA扇区号		8 byte
        public ulong  GPTStartLBA;                   //GPT分区区域起始LBA	8 byte
        public ulong  GPTEndLBA;						//GPT分区区域结束LBA	8 byte
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string DiskGUID;                          //磁盘GUID				16 byte
        public ulong  GPTPartTabStartLBA;            //备份EFI LBA扇区号		8 byte
        public ushort PartTabCount;                            //分区表项数			4 byte
        public ushort PartTabCRC;                              //分区表CRC校验和		4 byte
    }
}
