using System;
using System.Runtime.InteropServices;

/// <summary>
/// 硬盘相关结构;
/// </summary>
namespace SingularityForensic.Drive {

    /// <summary>
    /// 物理设备的结构;
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct PhysicsDeviceStruct {
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

    /// <summary>
    /// 柱面/磁道情况;
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct CHSStruct {
        public ulong m_Cylinder;                    //柱面数
        public uint m_Head_Track;              //每柱面磁道数
        public uint m_Track_Sector;                //每磁道扇区数	
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct _RW_BufferStruct {
        public IntPtr m_Buffer;                    //数据地址
        public uint m_DataSize;                    //数据大小
        public uint m_Read;                        //读取大小
        public Boolean m_Flags;                    //读入信号
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct HDDInfo2Struct {
        public int ID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string szModelNumber;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string szSerialNumber;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string szControllerNumber;
        public IntPtr Next;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct HDDInfoStruct {
        public int ID;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string VendorID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string ProductID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string ProductRevision;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string SerialNumber;

        public IntPtr info;
        public IntPtr Next;
    }
}
