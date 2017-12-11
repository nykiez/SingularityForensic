using System;
using System.Runtime.InteropServices;

namespace CDFC.Parse.Local.Structs {
    //分区结构
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct PartitonStruct {
        public int m_LoGo;   //为那个物理设备的分区
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        public string VolumeName;      //卷标名称
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        public string FileSystem;      //文件系统
        public IntPtr m_Name;              //分区名称
        public ulong m_Size;                 //分区大小

        public uint m_Type;                    //分区类型
        public ulong m_Offset;                //MBR的偏移
        public bool m_Boot;                    //是否引导
        public IntPtr m_pDev;               //指向设备
        public byte m_Sign;                    //分区盘符
        public IntPtr pDBR;                 //Boot扇区（文件系统第一扇区）
    };
    //分区结构
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct PartitonStruct2 {
        public int m_LoGo;   //为那个物理设备的分区
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        public string VolumeName;      //卷标名称
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        public string FileSystem;      //文件系统
        public IntPtr m_Name;              //分区名称
        public ulong m_Size;                 //分区大小

        public long m_Type;                    //分区类型
        public ulong m_Offset;                //MBR的偏移

        public bool m_Boot;                    //是否引导
        public IntPtr m_pDev2;               //指向设备
        public bool m_Sign1;                    //分区盘符
        public short m_Sign2;                    //分区盘符

        public byte m_Sign;                    //分区盘符
        //public IntPtr pDBR;                 //Boot扇区（文件系统第一扇区）
        //public byte m_Sign1;                    //分区盘符
    };
    //分区列表结构;
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct PartitonListStruct {
        public IntPtr m_ThisPartition;
        public IntPtr m_prev;
        public IntPtr m_next;
    }
}
