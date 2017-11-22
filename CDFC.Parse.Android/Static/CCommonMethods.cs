using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;

namespace CDFC.Parse.Android.Static {
    public static class CCommonMethods {
        [DllImport("cdfcqd.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Cflabqd_Partition_Init(IntPtr ST_PartTabInfo,SafeFileHandle handle);
        [DllImport("cdfcqd.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Cflabqd_Get_InodeInfo(uint N_Inode);
        [DllImport("cdfcqd.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Cflabqd_Get_BlockList(IntPtr ST_Ext4Inode);
        [DllImport("cdfcqd.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Cflabqd_Parse_Dir(IntPtr stBlockList);
        [DllImport("cdfcqd.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Cflabqd_InodeInfo_Free(IntPtr ST_Ext4Inode);
        [DllImport("cdfcqd.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Cflabqd_BlockList_Free(IntPtr ST_BlockList);
        [DllImport("cdfcqd.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Cflabqd_Dir_Free(IntPtr ST_DirDntry);
    }
}
