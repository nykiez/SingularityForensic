using System.Runtime.InteropServices;

namespace CDFC.Parse.Android.Structs {
    public struct StMbrInfo {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 450)]
        public string Unknown;
        public byte EELogo;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string Unknown0;
        public uint DiskSize;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
        public string Unknown1;
        public ushort AA55Logo;
    }
    
}
