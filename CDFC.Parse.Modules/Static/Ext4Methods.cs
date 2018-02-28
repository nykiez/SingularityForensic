using CDFC.Parse.Modules.Structs;
using CDFC.Parse.Contracts;
using CDFC.Util.PInvoke;
using EventLogger;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace CDFC.Parse.Modules.Static {
    internal static class Ext4Methods {
        internal const string EXT4Assembly = "cdfcqd.dll";

        [DllImport(EXT4Assembly, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Cflabqd_Partition_Init(IntPtr ST_PartTabInfo,SafeFileHandle handle);
        [DllImport(EXT4Assembly, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Cflabqd_Get_InodeInfo(uint N_Inode);
        [DllImport(EXT4Assembly, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Cflabqd_Get_BlockList(IntPtr ST_Ext4Inode);
        [DllImport(EXT4Assembly, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Cflabqd_Parse_Dir(IntPtr stBlockList);
        [DllImport(EXT4Assembly, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Cflabqd_InodeInfo_Free(IntPtr ST_Ext4Inode);
        [DllImport(EXT4Assembly, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Cflabqd_BlockList_Free(IntPtr ST_BlockList);
        [DllImport(EXT4Assembly, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Cflabqd_Dir_Free(IntPtr ST_DirDntry);

        /// <summary>
        /// 通过Inode号解析出inode,块链表;
        /// </summary>
        /// <param name="stDirEntryPtr">输入的文件结构(自定义)指针</param>
        /// <param name="bGroups">需输出的块组链表</param>
        /// <param name="stExt4Inode">需输出的inode结构</param>
        /// <param name="stDirEntry">需输出的文件结构(自定义)</param>
        /// <param name="stExt4DirEntry">需输出的文件结构(原生)</param>
        [HandleProcessCorruptedStateExceptions]
        internal static void ParseByDirEntryPtr(
            IntPtr stDirEntryPtr,
            out StDirEntry? stDirEntry,

            out StExt4DirEntry? stExt4DirEntry,

            out IntPtr stExt4InodePtr,
            out StExt4Inode? stExt4Inode,

            out IntPtr stBlockListPtr,
            out List<BlockGroup> bGroups) {

            stExt4Inode = null;                                                    //初始化inode结构;
            bGroups = new List<BlockGroup>();                                                 //初始化块链表;
            stDirEntry = null;
            stExt4DirEntry = null;

            stBlockListPtr = IntPtr.Zero;
            stExt4InodePtr = IntPtr.Zero;

            try {
                stDirEntry = stDirEntryPtr.GetStructure<StDirEntry>();                          //加载文件结构(自定义);
                stExt4DirEntry = stDirEntry.Value.DirInfo.GetStructure<StExt4DirEntry>();       //加载文件结构(原生);
                ////Console.WriteLine(stExt4DirEntry.Value.inode);
                //if (stExt4DirEntry.Value.inode == 113880) {

                //}
                if (stExt4DirEntry.Value.inode == 0) {
                    return;
                }
                stExt4InodePtr = Cflabqd_Get_InodeInfo(stExt4DirEntry.Value.inode);         //加载Inode,BlockList;

                if (stExt4InodePtr == IntPtr.Zero) {
                    return;
                }

                stBlockListPtr = Cflabqd_Get_BlockList(stExt4InodePtr);                     //加载Inode,BlockList;
                stExt4Inode = stExt4InodePtr.GetStructure<StExt4Inode>();                              //获取INode信息;

                if (stBlockListPtr != IntPtr.Zero) {                                            //获取BlockList信息;
                    for (var stBlockNode = stBlockListPtr; stBlockNode != IntPtr.Zero;) {
                        var stBlock = stBlockNode.GetStructure<StBlockList>();
                        bGroups.Add(new BlockGroup((long)stBlock.address, stBlock.count));
                        if (stBlock.Next != stBlockNode) {
                            stBlockNode = stBlock.Next;
                        }
                        else {
                            break;
                        }
                    }

                }

                //Cflabqd_InodeInfo_Free(stExt4InodePtr);                                         //释放inode信息;

            }
            catch (Exception ex) {
                Logger.WriteLine($"{nameof(Ext4Methods)}->{nameof(ParseByDirEntryPtr)}:{ex.Message}");
                bGroups = bGroups ?? new List<BlockGroup>();
                stExt4Inode = stExt4Inode ?? new StExt4Inode();
                stDirEntry = stDirEntry ?? new StDirEntry();
                stExt4DirEntry = stExt4DirEntry ?? new StExt4DirEntry();
            }

        }

        /// <summary>
        /// 通过Inode号解析出inode,块链表;
        /// </summary>
        /// <param name="stDirEntryPtr">输入的文件结构(自定义)指针</param>
        /// <param name="bGroups">需输出的块组链表</param>
        /// <param name="inode">需输出的inode结构</param>
        /// <param name="stDirEntry">需输出的文件结构(自定义)</param>
        /// <param name="stExt4DirEntry">需输出的文件结构(原生)</param>
        public static void ParseByDirEntryPtr(
            IntPtr stDirEntryPtr,
            out StDirEntry? stDirEntry,
            out StExt4DirEntry? stExt4DirEntry,
            out StExt4Inode? stExt4Inode,
            out List<BlockGroup> bGroups) {
            IntPtr stBlockListPtr = IntPtr.Zero;
            IntPtr stExt4InodePtr = IntPtr.Zero;

            ParseByDirEntryPtr(stDirEntryPtr,
                                out stDirEntry,

                                out stExt4DirEntry,

                                out stExt4InodePtr,
                                out stExt4Inode,

                                out stBlockListPtr,
                                out bGroups);

            //if (stBlockListPtr != IntPtr.Zero) {
            //    Cflabqd_BlockList_Free(stBlockListPtr);
            //}
            //if (stExt4InodePtr != IntPtr.Zero) {
            //    Cflabqd_InodeInfo_Free(stExt4InodePtr);
            //}


        }
    }
}
