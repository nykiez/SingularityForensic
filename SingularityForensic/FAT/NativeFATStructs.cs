using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SingularityForensic.FAT {
    /// <summary>
    /// 簇节点;
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StFatClusterNode{
        public ulong nClusterNum;		//簇号
	    public ulong nLBAByte;         //簇对应的字节地址(相对于本分区)
        //tagClusterList* Next;
        public IntPtr Next;
    }
    
    /// <summary>
    /// //文件链表(StFileList);
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StFatFileNode {
        [MarshalAs(UnmanagedType.ByValArray,SizeConst = 512)]
        public byte[] NameBuffer;

        public string Name => ConvertFatBytesToString(NameBuffer);

        public FATFileAttr FileAttrib;           //文件属性：0x01表示只读，0x02隐藏，0x04系统文件，0x08卷标 长文件名目录项，0x10表示目录，0x20表示存档
        public byte MTen;                 //精确1/10秒

        public struct FileTime {
            public ushort Second;
            public ushort Minute;
            public ushort Hour;
            public ushort Day;
            public ushort Month;
            public ushort Year;
        }
        public struct FileDate {
            public ushort Day;
            public ushort Month;
            public ushort Year;
        }

        public FileTime CreateFileTime;
        public FileDate LastAccessDate;		//最后访问时间；
        public FileTime ChangeFileTime;
        
        public DateTime CreateTime => ConvertFileTimeToDateTime(CreateFileTime);
        public DateTime ModifiedTime => ConvertFileTimeToDateTime(ChangeFileTime);
        public DateTime AccessTime => new DateTime(LastAccessDate.Year, LastAccessDate.Month, LastAccessDate.Day);

        public uint FileSize;                 //文件大小，子目录设为0
        public byte bDeleted;
        public bool Deleted => bDeleted == 1;
        //StClusterList* stClusterList;   //簇链表
        public IntPtr stClusterList;   //簇链表
        //StFileList* Next;
        public IntPtr Next;

        //文件属性;
        [Flags]
        public enum FATFileAttr : byte {
            ReadOnly = 0x01,
            Hidden = 0x02,
            System = 0x04,
            Volume = 0x08,
            Directory = 0x10,
            SDocument = 0x20
        }

        /// <summary>
        /// 从文件事件转换至标准时间;
        /// </summary>
        /// <param name="fileTime"></param>
        /// <returns></returns>
        public static DateTime ConvertFileTimeToDateTime(FileTime fileTime) {
            return new DateTime(
                fileTime.Year, 
                fileTime.Month, 
                fileTime.Day, 
                fileTime.Hour,
                fileTime.Minute,
                fileTime.Second);
        }

        public static string ConvertFatBytesToString(byte[] bts) {
            if(bts == null) {
                throw new ArgumentNullException(nameof(bts));
            }

            //以二为步长,截取0至连续出现两个0字符的位置;
            var maxCount = 0;
            for (int i = 0; i < bts.Length / 2; i++) {
                if(bts[2 * i] == 0 && bts[2 * i + 1] == 0) {
                    break;
                }
                maxCount += 2;
            }

            return ConvertFatBytesToString(bts, 0, maxCount);
        }

        public static string ConvertFatBytesToString(byte[] bts,int index,int count) {
            if(bts == null) {
                throw new ArgumentNullException(nameof(bts));
            }

            return Encoding.Unicode.GetString(bts,index,count);
        }
    };
    
    /// <summary>
    /// //文件系统引导扇区DBR
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StFatDBR {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] ignored;               //跳转指令
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] system_id;             //文件系统标志（ASCII码）	
        public ushort sector_size;             //每扇区字节数	
        public byte sectors_per_cluster;      //每簇扇区数
        public ushort reserved;                //保留扇区；//其FAT表紧跟在此之后
        public byte fats;                     //fat表的个数 一般为2；
        public ushort dir_entries;             //根目录最多可容纳的目录项FAT32不用为0，FAT12，16一般为512；
        public ushort sectors;                 //整个分区的扇区总数  小于32MB放在此；
        public byte media;                    //介质描述符一般为0xF8;
        public ushort fat_length;              //FAT32不用；每FAT表的大小扇区数/* 0x16 sectors/FAT */
        public ushort secs_track;              //每磁道扇区数
        public ushort heads;                   //磁头数；
        public uint hidden;                    //分区前的扇区数；
        public uint total_sect;                //文件系统的总扇区；
                                        /* 以下字段只能使用FAT32的 */
        public uint fat32_length;              //FAT表的扇区大小值
        public ushort flags;                   //确定FAT表的工作方式bit7设置为1表示只有一份FAT表是活动的 	/* 0x28 bit 8: fat mirroring, low 4: active fat */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] version;               //文件系统的版本号；
        public uint root_cluster;              //根目录起始簇号 一般为2
        public ushort info_sector;             //FSINFO所在扇区号； 1号；
        public ushort backup_boot;             //备份扇区号；（6号扇区）
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
        public ushort marker;
        public int ClusterSize => sectors_per_cluster * sector_size;
    }
    
    /// <summary>
    /// //FSINFO信息(StFSInfo);
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StFatINFO {
        public uint Flag;                //扩展引导标志'52 52 61 41'
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 480)]
        public byte[] Uable;            //未使用
        public uint FSINFO;              //FSINFO签名'72 71 41 61'
        public uint FreeCluster;         //空闲簇数量
        public uint NextFreeCluter;      //下一可用簇号
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
        public byte[] Uable2;                //未使用
        public ushort End;				//"55aa"
    }
    
    /// <summary>
    /// //FSINFO信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StFatFSDBR {
        public ulong nOffset;
        //StFatDBR* stFatDBR;
        public IntPtr stFatDBR;
    }
    
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StFatFSInfo {
        public ulong nOffset;
        //StFSINFO* stFSINFO;
        public IntPtr stFatINFO;
    }
}
