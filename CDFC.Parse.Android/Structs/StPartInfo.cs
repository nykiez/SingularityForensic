using System.Runtime.InteropServices;

namespace CDFC.Parse.Android.Structs {
    public struct StPartInfo {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string PartTabType;                                  //分区类型GUID		16 byte
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string PartTabGUID;                                  //分区唯一GUID		16 byte
        public ulong PartTabStartLBA;                               //分区起始LAB		8 byte
        public ulong PartTabEndLBA;                                 //分区结束LAB		8 byte
        public ulong PartTabProp;			                        //分区属性			8 byte
        [MarshalAs(UnmanagedType.ByValArray,SizeConst = 72)]
        public byte[] PartTabNameUnicode;                           //分区名unicode码	72 byte

        public string PartTabName {
            get {
                var sb = new System.Text.StringBuilder();
                for (int i = 0; i < PartTabNameUnicode.Length / 2; i++) {
                    var bt = PartTabNameUnicode[2 * i];
                    if(bt == 0 && PartTabNameUnicode[2 * i + 1] == 0) {
                        break;
                    }
                    sb.Append((char)bt);
                }
                return sb.ToString();
            }
        }
    }
}
