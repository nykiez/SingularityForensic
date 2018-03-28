using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileSystem {
    
    [StructLayout(LayoutKind.Sequential,Pack = 1)]
    public struct StInFoDisk {
        public byte BootID;                   //80h表示可启动分区，否则为0；对主分区有用；
        public byte SartHead;             //分区的起始磁头号； 
        public UInt16 SartSectorTrack;         //分区的起始扇区和磁道号
        public byte FileSystemID;             //05H或0FH为扩展分区，06H或0EH为FAT16，0BH或0CH为FAT32 ,07为NTFS；
        public byte EndHead;                  //分区结束磁头号；
        public UInt16 EndSectorTrack;          //分区结束扇区和磁道号
        public UInt32 HeadSecor;                //分区前的扇区； 
        public UInt32 AllSector;                //分区的总扇区； 
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StDosPTable {
        public ulong nOffset;
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
    
}
