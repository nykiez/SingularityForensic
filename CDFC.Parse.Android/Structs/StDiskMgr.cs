using System;

namespace CDFC.Parse.Android.Structs {
    public struct StDiskMgr {
        public IntPtr MbrInfoPtr;           //保护MBR信息(StAndroidMbrInfo)
        public IntPtr EFIInfoPtr;           //EFI信息(StAndroidEFIInfo)
        public IntPtr PartTabInfoPtr;         //分区表链表(StAndroidPartInfo)
    }
}
