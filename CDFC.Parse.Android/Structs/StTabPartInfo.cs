using System;
using System.Runtime.InteropServices;

namespace CDFC.Parse.Android.Structs {
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct StTabPartInfo {
        public IntPtr PartInfoPtr;           //分区表项StAndroidPartInfo
        public IntPtr Ext4SuperBlock;   //本分区超级块StExt4SuperBlock*
        public IntPtr Ext4GroupDesc;     //本分区组描述符表StExt4GroupDesc*
        public ulong  FreeSpace;
        public int    FsType;//0:未知分区,1:FAT32,2:fat16,3:ext4
        public IntPtr Next; //PartTabInfoTag*
        public IntPtr Pre;//PartTabInfoTag
    }
}

