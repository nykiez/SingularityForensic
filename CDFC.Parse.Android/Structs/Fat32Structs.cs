using EventLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CDFC.Parse.Android.Structs {

    //enum File_System_Type {
    //    U_nknown,   //未知类型
    //    U_FAT12,
    //    U_FAT16,
    //    U_FAT32,
    //    U_NTFS,
    //    U_EXFAT,
    //    U_EXT2,
    //    U_EXT3,
    //    U_EXT4,
    //    U_HFS,
    //    U_HFSP,
    //    U_HFSX
    //};

    enum eFatSearchType {
        eFatSearchType_FS,          //文件系统恢复
        eFatSearchType_FORMAT,      //格式化恢复
        eFatSearchType_FULL,        //文件签名扫描
    };

    //簇链表
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StFatCluster {
        public ulong nClusterNum;
        public ulong nLBAByte;
        public IntPtr next;
    }
    //12

    //文件属性;
    public enum Fat32FileAttr : byte {
        ReadOnly = 0x01,
        Hidden = 0x02,
        System = 0x04,
        Volume = 0x08,
        Directory = 0x10,
        SDocument = 0x20
    }

    //文件链表
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StFAT32FileNode {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct StTimeStamp {
            public UInt16 Second;
            public UInt16 Minute;
            public UInt16 Hour;
            public UInt16 Day;
            public UInt16 Month;
            public UInt16 Year;
        }
        
        public DateTime? FromStampToDateTime(ref StTimeStamp stamp) {
            try {
                return new DateTime(stamp.Year, stamp.Month, stamp.Day, stamp.Hour, stamp.Minute, stamp.Second);
            }
            catch(Exception ex) {
                Logger.WriteCallerLine(ex.Message);
                return null;
            }
            
        }

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        public byte[] _name;             //文件名
        public string Name {
            get {
                if (_name != null) {
                    //CDFCCultures.Helpers.ByteConverterHelper.ConvertToHexFormat
                    var sb = new StringBuilder();
                    for (int i = 0; i < _name.Length / 2; i++) {
                        if (_name[2 * i] != 0 || _name[2 * i + 1] != 0) {
                            sb.Append((char)((_name[2 * i + 1] << 8) | _name[2 * i]));
                        }
                    }
                    return sb.ToString();
                }
                return null;
            }
        }

        public Fat32FileAttr _fileAttrib;           //文件属性：0x01表示只读，0x02隐藏，0x04系统文件，0x08卷标 长文件名目录项，0x10表示目录，0x20表示存档
        public byte MTen;                 //精确1/10秒

        private StTimeStamp _createTime;
        public DateTime CreateTime => FromStampToDateTime(ref _createTime)??default(DateTime);

        public UInt16 LastAccessTime;		//最后访问时间；
        private StTimeStamp _changeTime;
        public DateTime ChangeTime => FromStampToDateTime(ref _changeTime) ?? default(DateTime);

        public uint FileSize;                 //文件大小，子目录设为0

        public byte bDeleted;               //是否被删除;
        public bool Deleted => bDeleted != 0;

        public IntPtr stClusterList;   //簇链表
        public IntPtr _next;

        
    }

    //文件系统引导扇区DBR
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StFatDBR {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] ignored;               //跳转指令
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] system_id;             //文件系统标志（ASCII码）	
        public UInt16 sector_size;             //每扇区字节数	
        public byte sectors_per_cluster;      //每簇扇区数
        public UInt16 reserved;                //保留扇区；//其FAT表紧跟在此之后
        public byte fats;                     //fat表的个数 一般为2；
        public UInt16 dir_entries;             //根目录最多可容纳的目录项FAT32不用为0，FAT12，16一般为512；
        public UInt16 sectors;                 //整个分区的扇区总数  小于32MB放在此；
        public byte media;                    //介质描述符一般为0xF8;
        public UInt16 fat_length;              //FAT32不用；每FAT表的大小扇区数/* 0x16 sectors/FAT */
        public UInt16 secs_track;              //每磁道扇区数
        public UInt16 heads;                   //磁头数；
        public uint hidden;                    //分区前的扇区数；
        public uint total_sect;                //文件系统的总扇区；
                                               /* 以下字段只能使用FAT32的 */
        public uint fat32_length;              //FAT表的扇区大小值
        public UInt16 flags;                   //确定FAT表的工作方式bit7设置为1表示只有一份FAT表是活动的 	/* 0x28 bit 8: fat mirroring, low 4: active fat */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] version;               //文件系统的版本号；
        public uint root_cluster;              //根目录起始簇号 一般为2
        public UInt16 info_sector;             //FSINFO所在扇区号； 1号；
        public UInt16 backup_boot;             //备份扇区号；（6号扇区）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public byte[] BPB_Reserved;         /* 0x34 Unused */
        public byte BS_DrvNum;                /* 0x40 */
        public byte BS_Reserved1;             /* 0x41 */
        public byte BS_BootSig;               /* 0x42 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] BS_VolID;              /* 0x43 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        public byte[] BS_VolLab;            /* 0x47 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] BS_FilSysType;         /* 0x52=82*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 420)]
        public byte[] nothing;             /* 0x5A */
        public UInt16 marker;
    }
    //512

    //FSINFO信息
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StFatFSINFO {
        public uint Flag;                //扩展引导标志'52 52 61 41'
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 480)]
        public byte[] Uable;            //未使用
        public uint FSINFO;              //FSINFO签名'72 71 41 61'
        public uint FreeCluster;         //空闲簇数量
        public uint NextFreeCluter;      //下一可用簇号
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
        public byte[] Uable2;                //未使用
        UInt16 End;				//"55aa"
    }
    //512

    //文件系统结构
    [StructLayout(LayoutKind.Sequential)]
    public struct StFatFileSys {
        public IntPtr stFatDBR;         //引导区
        public IntPtr stFSINFO;         //FSINFO信息区
        public IntPtr stFatDBR_BK;          //引导区备份
        public IntPtr stFSINFO_BK;			//FSINFO信息区备份
        //public StFatDBR? stFatDBR;         //引导区
        //public StFSINFO? stFSINFO;         //FSINFO信息区
        //public StFatDBR? stFatDBR_BK;          //引导区备份
        //public StFSINFO? stFSINFO_BK;			//FSINFO信息区备份
    }
}
