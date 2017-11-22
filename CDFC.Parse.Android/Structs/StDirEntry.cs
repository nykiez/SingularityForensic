using System;
using System.Runtime.InteropServices;

namespace CDFC.Parse.Android.Structs {
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct StDirEntry {
        public IntPtr DirInfo;      /* 目录表项信息 StExt4_Dir_Dntry**/
        [MarshalAs(UnmanagedType.I1)]
        public bool bDel;           /*是否已被删除*/
        public IntPtr Next;
        public IntPtr Pre;
    }
}
