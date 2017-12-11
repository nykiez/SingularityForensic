using System.Runtime.InteropServices;

namespace CDFC.Parse.Local.Structs {
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct CHSStruct {
        public ulong m_Cylinder;                    //柱面数
        public uint m_Head_Track;              //每柱面磁道数
        public uint m_Track_Sector;                //每磁道扇区数	
    };

}
