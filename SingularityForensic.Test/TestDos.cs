using CDFC.Util.PInvoke;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test {


    public enum File_System_Type {
        U_nknown,   //未知类型
        U_FAT12,
        U_FAT16,
        U_FAT32,
        U_NTFS,
        U_EXFAT,
        U_EXT2,
        U_EXT3,
        U_EXT4,
        U_HFS,
        U_HFSP,
        U_HFSX
    };

    public enum ePTableType {
        e_nknown,   //未知类型
        e_gpt,
        e_dos,
        e_apple,
        e_fat,
        e_ntfs
    };

    [TestClass]
    public class TestDos {
        public const int P_NO_OS = 0x00;

        public const int P_EXTENDED = 0x05; //扩展分区
        public const int P_EXTENDX = 0x0F;

        public const int P_32FAT = 0x0B;    //FAT32
        public const int P_32FAT_LBA = 0x0C;
        public const int P_32FATH = 0x1B;
        public const int P_32FAT_LBAH = 0x1C;

        public const int P_16FAT = 0x04;    //FAT16
        public const int P_16FATBD = 0x06;
        public const int P_16FATBD_LBA = 0x0E;
        public const int P_16FATH = 0x14;
        public const int P_16FATBDH = 0x16;
        public const int P_16FATBD_LBAH = 0x1E;

        public const int P_12FATH = 0x11;    //FAT12
        public const int P_12FAT = 0x01;

        public const int P_NTFS = 0x07;    //NTFS
        public const int P_NTFSH = 0x17;

        public const int P_NEWS_NTFS = 0x42;    //NTFS动态分区

        public const int P_LINUX = 0x83;    //EXT2,3,4
        public const int P_LINUXEXTENDX = 0x85;

        public const int P_WIN_GPT = 0xEE;  //windows下的GPT分区

        public const int P_HFS = 0xAF;  //APPLE的分区类型



        public const int P_RAID = 0xFF; //0xff自己定义的值。。
        public const int SIGE_DEF = 0x32;   //自定义的RAID类型虚拟出来的盘符。


        public const int SIGE_ON = 0x33;	//自定义当只有分区没有符号时，当盘符号大于等于0x33都为自定义的。

        private const string DosAssembly = "DiskManager.dll";
        [DllImport(DosAssembly, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr DiskManager_Get_PTable(SafeFileHandle hDisk);


        /*
        EFI 信息:
        */
        [StructLayout(LayoutKind.Sequential,Pack = 1)]
        public struct StEFIInfoTag {
            ulong EFIPART;						//EFI签名(EFI PART)		8 byte
	        uint Version;                                 //版本					4 byte
            uint EFISize;                                 //EFI信息大小字节数		4 byte
            uint CRC;                                     //EFI信息 CRC校验和		4 byte
            uint Unknown;                                 //保留					4 byte
            ulong EFICurrSecNum;					//当前EFI LBA扇区号		8 byte
	        ulong EFIBackupSecNum;				//备份EFI LBA扇区号		8 byte
	        ulong GPTStartLBA;					//GPT分区区域起始LBA	8 byte
	        ulong GPTEndLBA;						//GPT分区区域结束LBA	8 byte
            [MarshalAs(UnmanagedType.ByValArray,SizeConst = 16)]
	        byte[] DiskGUID;                             //磁盘GUID				16 byte
            ulong GPTPartTabStartLBA;			//备份EFI LBA扇区号		8 byte
	        uint PartTabCount;                            //分区表项数			4 byte
            uint PartTabCRC;                              //分区表CRC校验和		4 byte
        }

        /*
        GPT分区表项	128字节
        */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct StEFIPTable {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            byte[] PartTabType;              //分区类型GUID		16 byte
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            byte[] PartTabOnly;              //分区唯一GUID		16 byte
            ulong PartTabStartLBA;		//分区起始LAB		8 byte
	        ulong PartTabEndLBA;			//分区结束LAB		8 byte
	        ulong PartTabProp;			//分区属性			8 byte
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 72)]
            byte[] PartTabNameUnicode;           //分区名unicode码	72 byte
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct StInFoDisk {
            byte BootID;                   //80h表示可启动分区，否则为0；对主分区有用；
            byte SartHead;             //分区的起始磁头号； 
            ushort SartSectorTrack;         //分区的起始扇区和磁道号
            byte FileSystemID;             //05H或0FH为扩展分区，06H或0EH为FAT16，0BH或0CH为FAT32 ,07为NTFS；
            byte EndHead;                  //分区结束磁头号；
            ushort EndSectorTrack;          //分区结束扇区和磁道号
            uint HeadSecor;                //分区前的扇区； 
            uint AllSector;                //分区的总扇区； 
        }


        //位于磁盘的0磁头0拄面1扇区； 
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _System_Boot_Sector {
            [MarshalAs(UnmanagedType.ByValArray,SizeConst = 0x1be)]
            byte[] BootCode;          //引导代码； 
            /// <summary>
            /// <see cref="StInFoDisk"/>
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            IntPtr[] InFo;    //主分区的基本信息 (
            ushort LoGo;                        //"0xAA55";     
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TagDosPTable {
            ulong nOffset;
            /// <summary>
            /// <see cref="StInFoDisk"/>
            /// </summary>
	        IntPtr InFo;
            IntPtr next;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct StPTable{
            public ePTableType eType;
            public ulong nOffset;
            /// <summary>
            /// <see cref="StInFoDisk"/>
            /// </summary>
	        public IntPtr InFo;
            /// <summary>
            /// <see cref="StEFIInfoTag"/>
            /// </summary>
            public IntPtr EFIInfo;
            /// <summary>
            /// <see cref="StEFIPTable"/>
            /// </summary>
            public IntPtr EFIPTable;
            public IntPtr next;
        }
        
        //"J://anli/dos.img"
        [TestMethod]
        public void TestGetPTable() {
            DataTable dt = null;
            var row = dt.NewRow();
            row["da"] = 312;
            
            //"J:/use/手机镜像/Honor/2017-01-10 10-03-05 867064029171533/mmcblk0"
            var fs = System.IO.File.OpenRead("G://MCR3XXX.rar");
            Assert.IsTrue(fs != null);
            var ptr = DiskManager_Get_PTable(fs.SafeFileHandle);
            Assert.IsTrue(ptr != IntPtr.Zero);
            while (ptr != IntPtr.Zero) {
                var st = ptr.GetStructure<StPTable>();
                ptr = st.next;
                Trace.WriteLine(st.eType);
                if(st.InFo != IntPtr.Zero) {
                    var info = st.InFo.GetStructure<StInFoDisk>();
                }
            }
            


        }
    }
}
