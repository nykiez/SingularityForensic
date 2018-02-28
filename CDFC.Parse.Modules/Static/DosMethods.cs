using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CDFC.Parse.Modules.Static {
    internal static class DosMethods {
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


        public const int SIGE_ON = 0x33;    //自定义当只有分区没有符号时，当盘符号大于等于0x33都为自定义的。

        
    }
}
