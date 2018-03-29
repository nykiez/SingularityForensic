using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileSystem {
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StEFIInfo {
        public ulong EFIPART;						//EFI签名(EFI PART)		8 byte
	    public uint Version;                                 //版本					4 byte
        public uint EFISize;                                 //EFI信息大小字节数		4 byte
        public uint CRC;                                     //EFI信息 CRC校验和		4 byte
        public uint Unknown;                                 //保留					4 byte
        public ulong EFICurrSecNum;					//当前EFI LBA扇区号		8 byte
	    public ulong EFIBackupSecNum;				//备份EFI LBA扇区号		8 byte
	    public ulong GPTStartLBA;					//GPT分区区域起始LBA	8 byte
	    public ulong GPTEndLBA;						//GPT分区区域结束LBA	8 byte
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] DiskGUID;                             //磁盘GUID				16 byte
        public ulong GPTPartTabStartLBA;			//备份EFI LBA扇区号		8 byte
	    public uint PartTabCount;                            //分区表项数			4 byte
        public uint PartTabCRC;                              //分区表CRC校验和		4 byte
    };
    
    /*
    GPT分区表项	128字节
    */
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StEFIPTable {
        [MarshalAs(UnmanagedType.ByValArray,SizeConst = 16)]
        public byte[] PartTabType;              //分区类型GUID		16 byte
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] PartTabOnly;              //分区唯一GUID		16 byte
        public ulong PartTabStartLBA;		//分区起始LAB		8 byte
	    public ulong PartTabEndLBA;         //分区结束LAB		8 byte
        public ulong PartTabProp;			//分区属性			8 byte
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 72)]
        public byte[] PartTabNameUnicode;           //分区名unicode码	72 byte
    }


    [StructLayout(LayoutKind.Sequential,Pack = 1)]
    public struct StInFoDisk {
        public byte BootID;                   //80h表示可启动分区，否则为0；对主分区有用；
        public byte SartHead;             //分区的起始磁头号； 
        public UInt16 SartSectorTrack;         //分区的起始扇区和磁道号
        public byte FileSystemID;             //05H或0FH为扩展分区，06H或0EH为FAT16，0BH或0CH为FAT32 ,07为NTFS；
        public byte EndHead;                  //分区结束磁头号；
        public UInt16 EndSectorTrack;          //分区结束扇区和磁道号
        public UInt32 HeadSector;                //分区前的扇区； 
        public UInt32 AllSector;                //分区的总扇区； 
    }

    /// <summary>
    /// DOS分区表项类型;
    /// </summary>
    public enum DosPartType {
        Error,
        Main,
        Extend,
        Logic
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StDosPTable {
        public ulong nOffset;
        public DosPartType DosPartType;
        //InFoDisk* InFo;
        public IntPtr Info;
        //TagDosPTable* next;
        public IntPtr next;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StGptPTable {
        public ulong nOffset;
        //InFoDisk* InFo;
        public IntPtr Info;
        //StEFIInfo* EFIInfo;
        public IntPtr EFIInfo;
        //StEFIPTable* EFIPTable;
        public IntPtr EFIPTable;
        //TagGptPTable* next;
        public IntPtr next;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StPartition {
        public IntPtr stStream;
        public IntPtr stPTable;
        public IntPtr stDosPTable;
        public IntPtr stGptPTable;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StPTable {
        int eType;
        ulong nOffset;
	    IntPtr InFo;
        IntPtr EFIInfo;
        IntPtr EFIPTable;
        IntPtr next;
    }

}
