using System;
using System.Runtime.InteropServices;

namespace CDFC.Parse.Modules.Structs {
    //每个文件对应的块列表;
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct StBlockList {
        public ulong address;
        public uint count;
        public IntPtr Next;
    }

}
