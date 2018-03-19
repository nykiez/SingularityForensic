using CDFC.Parse.Modules.Structs;
using CDFC.Parse.Contracts;
using CDFC.Util.PInvoke;
using EventLogger;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CDFC.Parse.Modules.Static {
    internal static class FAT32Methods {
        internal const string FAT32Assembly = "FatRecover.dll";
        /// <summary>
        /// 返回Fat文件系统信息;
        /// </summary>
        /// <param name="hDisk"></param>
        /// <param name="__int64"></param>
        /// <param name=""></param>
        /// <param name="eFsType"></param>
        /// <returns></returns>
        [DllImport(FAT32Assembly, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr fat_init(SafeFileHandle hDisk, ulong nPosition, FsType eFsType);

        /*
        获取根目录
        stFatDBR：引导区
        */
        [DllImport(FAT32Assembly, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr fat_get_rootdir(IntPtr stFatDBR);

        [DllImport(FAT32Assembly, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr fat_parse_dir(IntPtr stClusterList);

        /// <summary>
        /// 从节点加载簇链表;
        /// </summary>
        /// <param name="stFAT32FileNode"></param>
        /// <returns></returns>
        internal static (List<BlockGroup> groups,long startLBA) LoadClusterInfoFromNode(StFAT32FileNode stFAT32FileNode,long offset,int clusterSize) {
            var groups = new List<BlockGroup>();
            long startLBA = 0;
            try {
                var node = stFAT32FileNode.stClusterList;
                while(node != IntPtr.Zero) {
                    var cluster = node.GetStructure<StFatCluster>();
                    groups.Add(new BlockGroup((long)cluster.nClusterNum, 1, offset,2,clusterSize));
                    node = cluster.next;
                }
                var firstGroup = groups.FirstOrDefault();
                if(firstGroup != null) {
                    startLBA = (firstGroup.BlockAddress - firstGroup.Reserved) * firstGroup.BlockSize + firstGroup.Offset;
                }
                
            }
            catch(Exception ex) {
                LoggerService.Current?.WriteCallerLine(ex.Message);
            }

            return (groups,startLBA);
        }
        
    }
}
