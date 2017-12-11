using System;
using System.Runtime.InteropServices;

namespace CDFC.Parse.Local.Structs {
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct MovVideoStruct {
        public int Name;                          //无用
        public ulong  FileSize;			//文件大小

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string FileData;                 //修改日期  这里没有结束日期,所以结束日期写---
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string FileType;                 //无用

        public ulong File_Data_Start;	//无用
	    public ulong File_Data_End;	//无用
	    public ulong File_Head_Start;	//无用
	    //public ulong File_Head_End;	//无用
        
        public IntPtr Next;
    }
}
