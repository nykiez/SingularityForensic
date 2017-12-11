using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CDFC.Parse.Local.Structs {
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct JPGStruct {
        public ulong nStartAddress;
	    public ulong nSizeTrue;
	    public IntPtr next;
    }


}
