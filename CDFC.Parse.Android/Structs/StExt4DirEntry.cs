using System.Runtime.InteropServices;

namespace CDFC.Parse.Android.Structs {

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct StExt4DirEntry {
        public uint inode;                                      /* inode号 */
        public ushort rec_len;                                  /* 本目录项长度字节数 */
        public byte name_len;                                   /* 目录名字字节数 */
        public Ext4FileType file_type;                                  /* 文件类型 */
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
        public string name;                                     /* File name */
    }
    /*文件类型
    Unknown,                0未知文件;
    RegularFile,            1常规文件;
    Directory,              2目录;
    CharacterDeviceFile,    3挂载设备文件;
    BlockDeviceFile,        4块设备文件;
    FIFO,                   5管道通信;
    Socket,                 6套接字;
    SymbolicLink            7快捷方式;
    */
    public enum Ext4FileType : byte {
        Unknown,                //0未知文件;
        RegularFile,            //1常规文件;
        Directory,              //2目录;
        CharacterDeviceFile,    //3挂载设备文件;
        BlockDeviceFile,        //4块设备文件;
        FIFO,                   //5管道通信;
        Socket,                 //6套接字;
        SymbolicLink            //7快捷方式;
    }
}
