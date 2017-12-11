using System;
using System.Runtime.InteropServices;

namespace CDFC.Parse.Local.Structs {
    
    
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
