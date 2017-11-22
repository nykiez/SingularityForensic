using CDFC.Parse.Android.Structs;
using CDFC.Parse.Contracts;
using System;
using System.Collections.Generic;
using EventLogger;
using CDFC.Util.PInvoke;
using static CDFC.Parse.Android.Static.CCommonMethods;
using CDFC.Parse.Abstracts;

namespace CDFC.Parse.Android.DeviceObjects {
    /// <summary>
    /// 安卓分区结构(Ext4)
    /// </summary>
    public class AndroidPartition : Partition,IDelAndRegNumedFile {
        /// <summary>
        /// 安卓分区的构造方法;
        /// </summary>
        /// <param name="tabPartInfo">分区结构(自定义)</param>
        /// <param name="parent">父文件</param>
        public AndroidPartition(TabPartInfo tabPartInfo, Device parent) : base(parent) {
            this.TabPartInfo = tabPartInfo;
            Index++;
        }

        public static int Index = 0;
        public TabPartInfo TabPartInfo { get; private set; }     //分区结构(自定义)指针;

        private IntPtr stExt4INodePtr;
        public StExt4Inode? StExt4INode { get; private set; }               //属于该分区的Inode节点;

        private IntPtr stBlockListPtr;
        public List<StBlockList?> StBlockList { get; private set; }
            = new List<StBlockList?>();                                     //属于该分区的BlockList节点;

        private IntPtr stDirEntryPtr;

        /// <summary>
        /// 加载子文件，并获取相关信息(如Ext4Node);
        /// </summary>
        internal void LoadChildren(Action<long> ntfSizeAct = null,Func<bool> isCancel = null) {
            if (TabPartInfo != null && TabPartInfo.StTabPartInfoPtr != IntPtr.Zero) {
                try {
                    var device = this.GetParent<AndroidDevice>();                                       //确定StartLBA/EndLBA;
                    Cflabqd_Partition_Init(TabPartInfo.StTabPartInfoPtr,device.Handle);                   //初始化选定当前分区;

                    stExt4INodePtr = Cflabqd_Get_InodeInfo(2);                  //加载Inode,BlockList,Dir;
                    stBlockListPtr = Cflabqd_Get_BlockList(stExt4INodePtr);     //加载Inode,BlockList,Dir;
                    stDirEntryPtr = Cflabqd_Parse_Dir(stBlockListPtr);            //加载Inode,BlockList,Dir;

                    StExt4INode = stExt4INodePtr.GetStructure<StExt4Inode>();   //获取INode信息;

                    var stBlockNode = stBlockListPtr;                           //获取BlockList信息;
                    while (stBlockNode != IntPtr.Zero) {                         //循环获取,直到节点为空;
                        var stBlock = stBlockNode.GetStructure<StBlockList>();
                        StBlockList.Add(stBlock);
                        stBlockNode = stBlock.Next;
                    }

                                   
                    if (device != null) {
                        if (TabPartInfo != null && TabPartInfo.StPartInfo != null) {
                            StartLBA = (long)TabPartInfo.StPartInfo.Value.PartTabStartLBA * device.SecSize;
                            EndLBA = (long)TabPartInfo.StPartInfo.Value.PartTabEndLBA * device.SecSize + device.SecSize - 1;
                        }
                    }
                    else {
                        Logger.WriteLine($"{nameof(AndroidPartition)}->{nameof(Size)}:{nameof(device)} can't be null.");
                    }

                    #region 加载子文件信息;
                    var stDirNode = stDirEntryPtr;
                    //var savedDirects = new List<AndroidDirectory>();            //为名称为".."或"."的目录所保存的目录列表;

                    while (stDirNode != IntPtr.Zero) {                           //获取DirEntity信息;
                        var dirTab = stDirNode.GetStructure<StDirEntry>();
                        var dirEntity = dirTab.DirInfo.GetStructure<StExt4DirEntry>();

                        IFile file = null;
                        if (dirEntity.file_type == 2) {
                            var direct = new AndroidDirectory(stDirNode, this);
                            direct.LoadChildren(ntfSizeAct,isCancel);

                            file = direct;
                        }
                        else if (dirEntity.file_type == 1) {
                            var regFile = new AndroidRegFile(stDirNode, this);
                            file = regFile;
                        }
                        else {
                            var otherFile = new AndroidOtherFile(stDirNode, this);
                            file = otherFile;
                        }

                        ntfSizeAct?.Invoke(file.Size);
                        Children.Add(file);
                        stDirNode = dirTab.Next;

                        if (dirTab.bDel) {
                            DelFileNum++;
                        }
                        else {
                            RegFileNum++;
                        }

                        if(isCancel?.Invoke() == true) {
                            break;
                        }
                    }
                    #endregion
                }
                catch (Exception ex) {
                    Logger.WriteLine($"{nameof(AndroidPartition)}->{nameof(LoadChildren)}:{ex.Message}");
                }
            }

        }

        //删除文件数;(仅限下一级)
        public long RegFileNum  { get;internal set; }

        //正常文件数;(仅限下一级)
        public long DelFileNum  { get;internal set; }

        //正常目录数;(仅限下一级)
        public long RegDirNum   { get;internal set; }

        //删除目录数;(仅限下一级)
        public long DelDirNum  { get;internal set; }
        
        public override FileSystemType FSType {
            get {
                if (TabPartInfo != null ) {
                    return TabPartInfo.FSType;
                }
                return FileSystemType.Unknown;
            }
        }
        
        public void Exit() {
            Children.ForEach(p => {
                if(p is AndroidDirectory) {
                    (p as AndroidDirectory).Exit();
                }
            });
        }

        //Inode数量;
        public uint INodeCount {
            get {
                return TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_inodes_count??0;
            }
        }

        //块数量;s
        public uint BlockCount => TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_blocks_count_lo ?? 0;

        /* 为文件系统预保留块数 */
        public uint BlocksCount_Lo => TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_blocks_count_lo ?? 0;

        //空闲块数量;
        public uint FreeBlockCount => TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_free_blocks_count_lo ?? 0;

        /* 空闲i-node数 */
        public uint FreeInodeCount => TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_free_inodes_count??0;         
        
        public uint FirstDataBlock => TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_first_data_block??0;          /* 0号块组起始块号 */

        public override uint? BlockSize => (TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_log_block_size??0) * 2048;            /* Block size is 2 ^ (10 + s_log_block_size). n * 2 * 1024 (0=1K, 1=2K, 2=4K)*/

        public uint ClusterSize => (TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_log_block_size ?? 0) * 2048;          /* Cluster size is (2 ^ s_log_cluster_size) blocks if bigalloc is enabled, zero otherwise.*/

        public uint BlockPergroup => TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_blocks_per_group??0;          /* 每组块数 */

        public uint ClusterPerGroup => TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_clusters_per_group??0;        /* Clusters per group, if bigalloc is enabled */

        public uint InodePerGroup => TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_inodes_per_group??0;          /* 每组inode数 */

        public static DateTime iniTime = DateTime.Parse("1970/01/01");
        /* 最后挂载时间 */
        public DateTime LastMountTime {
            get {
                return iniTime.AddSeconds(TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_mtime ?? 0);
            }
        }
        /* 最后写入时间 */
        public DateTime LastWriteTime {
            get {
                return iniTime.AddSeconds(TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_wtime ?? 0);
            }
        }
        

        public ushort INodeSize => TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_inode_size ?? 0;                /*Inode大小*/

        public int SectorSize {
            get {
                var device = this.Parent as Device;
                return device.SecSize;
            }
        }

        public long TotalRegFileNum {
            get {
                throw new NotImplementedException();
            }
        }

        //public ushort s_mnt_count;             /* 当前挂载数 */
        //public ushort s_max_mnt_count;         /* 最大挂载数 */
        //public ushort s_magic;                 /* 签名标志53EF */
        //public ushort s_state;                 /* 文件系统状态 */
        //public ushort s_errors;                /* 错误处理方式 */
        //public ushort s_minor_rev_level;       /* 辅版本级别 */
        //public uint s_lastcheck;             /* 最后一次性检查时间 */
        //public uint s_checkinterval;         /* 一次性检查间隔时间 */
        //public uint s_creator_os;                /* 创建本文件系统的操作系统 */
        //public uint s_rev_level;             /* 主版本级别 */
        //public ushort s_def_resuid;            /* 默认UID保留模块 */
        //public ushort s_def_resgid;            /* 默认GDI保留模块 */
        //public override long StartLBA { get; protected set; }

        //public override long EndLBA { get; protected set; }

        //public override string Name { get; set; }                         //分区标识;
    }
}
