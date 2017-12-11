using System;
using System.Runtime.InteropServices;

namespace CDFC.Parse.Local.Structs {
    /// <summary>
    /// 物理设备的结构;
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct StPhysicsDevice {
        public int ObjectID;                     //设备标数(如果为16不以物理名称打开)
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string Lable;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string DevName;                //驱动名称
        public ulong DevSize;                 //设备大小
        public CHSStruct DevCHS;                    //设备几何
        public uint DevMomd;                    //访问模式
        public uint DevType;                 //设备类型
        public IntPtr Handle;
        public int SectorSize;              //扇区字节
        public IntPtr Buffer;                   //读写缓存
        public IntPtr Partiton;                  //分区结构（废弃）
        public IntPtr Arch;                 //调用指针
        public IntPtr DevRW;                    //设备读写
        public bool DevState;                    //是否使用
        //public IntPtr Handle;
    };
    /// <summary>
    /// 物理设备列表结构;
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct DeviceListStruct {
        public IntPtr m_ThisDevice;                //当前设备
        public IntPtr m_prev;                   //上一链表
        public IntPtr m_next;                   //下一链表
    };

}
