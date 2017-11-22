using System;
using System.Runtime.InteropServices;

namespace CDFC.Parse.Android.Structs {
    struct StPhotoFileNode1 {
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
    }
}
