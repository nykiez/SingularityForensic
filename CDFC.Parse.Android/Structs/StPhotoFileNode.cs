using System;
using System.Runtime.InteropServices;

namespace CDFC.Parse.Android.Structs {
    [StructLayout(LayoutKind.Sequential,Pack = 1)]
    struct StPhotoFileNode1 {
#pragma warning disable CS0169 

        public ulong Start;
	    public ulong End;
	    public ulong Filesize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Name;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Data;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Type;
        public IntPtr Next;
#pragma warning restore CS0169  
    }
}
