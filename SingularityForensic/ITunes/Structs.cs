using System;
using System.Runtime.InteropServices;

namespace SingularityForensic.ITunes {
    /// <summary>
    /// IOS备份文件结构体;
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct IOSFileStruct {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
        public string strPhonePath;     //ios的实际文件路径
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
        public string strLocalPath;     //备份加密的路径
        public ulong nLastModifyTime;
        public ulong nLastAccessTime;
        public ulong nCreateTime;
        public IntPtr next;

        public string PhonePath => strPhonePath;
        public string LocalPath => strLocalPath;

        public DateTime? ModifiedTime => GetTime(nLastModifyTime);

        public DateTime? AccessTime => GetTime(nLastAccessTime);

        public DateTime? CreateTime => GetTime(nCreateTime);

        private static readonly DateTime _startTime = new DateTime(1970, 1, 1, 0, 0, 0);
        private static DateTime? GetTime(ulong timeStamp) {
            return _startTime.AddSeconds(timeStamp);
        }
    }

    /// <summary>
    /// IOS基本信息结构体;
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct StIOSBasicInfo {
        [MarshalAs(UnmanagedType.LPStr)]
        public string DeviceName;            //设备名称
        [MarshalAs(UnmanagedType.LPStr)]
        public string LastBackupDate;        //最后备份时间
        [MarshalAs(UnmanagedType.LPStr)]
        public string ProductType;           //类型
        [MarshalAs(UnmanagedType.LPStr)]
        public string ProductName;       //名称
        [MarshalAs(UnmanagedType.LPStr)]
        public string ProductVersion;        //版本号
        [MarshalAs(UnmanagedType.LPStr)]
        public string BuidVersion;           //编译版本
        [MarshalAs(UnmanagedType.LPStr)]
        public string SerialNumber;      //序列号
        [MarshalAs(UnmanagedType.LPStr)]
        public string ITunesVer;         //itunes版本
        [MarshalAs(UnmanagedType.LPStr)]
        public string GUID;              //GUID
        [MarshalAs(UnmanagedType.LPStr)]
        public string ICCID;             //ICCID
        [MarshalAs(UnmanagedType.LPStr)]
        public string IMEI;              //IMEI
        [MarshalAs(UnmanagedType.LPStr)]
        public string MEID;              //MEID
        [MarshalAs(UnmanagedType.LPStr)]
        public string PhoneNumber;       //电话号码
        [MarshalAs(UnmanagedType.LPStr)]
        public string TargetIdentifier;      //TargetIdentifier
        [MarshalAs(UnmanagedType.LPStr)]
        public string UniqueIdentifier;      //UniqueIdentifier
    };
}
