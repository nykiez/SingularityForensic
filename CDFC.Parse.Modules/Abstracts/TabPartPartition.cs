using CDFC.Parse.Modules.DeviceObjects;
using EventLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFC.Parse.Modules.Abstracts {
    //public abstract class TabPartPartition:Partition {
    //    public TabPartPartition(TabPartInfo tabPartInfo, Device parent) : base(parent) {
    //        if (tabPartInfo == null)
    //            throw new ArgumentNullException(nameof(tabPartInfo));

    //        this.TabPartInfo = tabPartInfo;
    //    }

    //    public TabPartInfo TabPartInfo { get; }     //分区结构(自定义)指针;

    //    private long? _startLBA;
    //    public override long StartLBA {
    //        get {
    //            if (_startLBA == null) {
    //                var device = this.GetParent<Device>();
    //                if (device != null) {
    //                    if (TabPartInfo != null && TabPartInfo.StPartInfo != null) {
    //                        _startLBA = (long)TabPartInfo.StPartInfo.Value.PartTabStartLBA * device.SecSize;
    //                    }
    //                }
    //                else {
    //                    LoggerService.Current?.WriteCallerLine($"{nameof(device)} can't be null.");
    //                    throw new InvalidOperationException($"{nameof(device)} can't be null!");
    //                }
    //            }
                
    //            return _startLBA.Value;
    //        }
    //        protected set => _startLBA = value;
    //    }

    //    private long? _size;
    //    public override long Size {
    //        get {
    //            if(_size == null) {
    //                var device = this.GetParent<Device>();
    //                if (device != null) {
    //                    _size = (long)(TabPartInfo.StPartInfo.Value.PartTabEndLBA  - 
    //                        TabPartInfo.StPartInfo.Value.PartTabStartLBA + 1) * device.SecSize;
    //                }
    //                else {
    //                    _size = 0;
    //                }
    //            }
    //            return _size.Value;   
    //        }
    //    }


    //    //块数量;s
    //    public uint BlockCount => TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_blocks_count_lo ?? 0;

    //    /* 为文件系统预保留块数 */
    //    public uint BlocksCount_Lo => TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_blocks_count_lo ?? 0;

    //    //空闲块数量;
    //    public uint FreeBlockCount => TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_free_blocks_count_lo ?? 0;

    //    /* 空闲i-node数 */
    //    public uint FreeInodeCount => TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_free_inodes_count ?? 0;

    //    public uint FirstDataBlock => TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_first_data_block ?? 0;          /* 0号块组起始块号 */

    //    //public override uint? BlockSize => (TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_log_block_size ?? 0) * 2048;            /* Block size is 2 ^ (10 + s_log_block_size). n * 2 * 1024 (0=1K, 1=2K, 2=4K)*/

    //    public override uint ClusterSize => (TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_log_block_size ?? 0) * 2048;          /* Cluster size is (2 ^ s_log_cluster_size) blocks if bigalloc is enabled, zero otherwise.*/

    //    public uint BlockPergroup => TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_blocks_per_group ?? 0;          /* 每组块数 */

    //    public uint ClusterPerGroup => TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_clusters_per_group ?? 0;        /* Clusters per group, if bigalloc is enabled */

    //    public uint InodePerGroup => TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_inodes_per_group ?? 0;          /* 每组inode数 */

    //    public static DateTime iniTime = DateTime.Parse("1970/01/01");
    //    /* 最后挂载时间 */
    //    public DateTime LastMountTime {
    //        get {
    //            return iniTime.AddSeconds(TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_mtime ?? 0);
    //        }
    //    }
    //    /* 最后写入时间 */
    //    public DateTime LastWriteTime {
    //        get {
    //            return iniTime.AddSeconds(TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_wtime ?? 0);
    //        }
    //    }


    //    public ushort INodeSize => TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_inode_size ?? 0;                /*Inode大小*/

    //    public int SectorSize {
    //        get {
    //            var device = this.Parent as Device;
    //            return device.SecSize;
    //        }
    //    }

    //    public long TotalRegFileNum {
    //        get {
    //            throw new NotImplementedException();
    //        }
    //    }

        

    //    //public ushort s_mnt_count;             /* 当前挂载数 */
    //    //public ushort s_max_mnt_count;         /* 最大挂载数 */
    //    //public ushort s_magic;                 /* 签名标志53EF */
    //    //public ushort s_state;                 /* 文件系统状态 */
    //    //public ushort s_errors;                /* 错误处理方式 */
    //    //public ushort s_minor_rev_level;       /* 辅版本级别 */
    //    //public uint s_lastcheck;             /* 最后一次性检查时间 */
    //    //public uint s_checkinterval;         /* 一次性检查间隔时间 */
    //    //public uint s_creator_os;                /* 创建本文件系统的操作系统 */
    //    //public uint s_rev_level;             /* 主版本级别 */
    //    //public ushort s_def_resuid;            /* 默认UID保留模块 */
    //    //public ushort s_def_resgid;            /* 默认GDI保留模块 */
    //    //public override long StartLBA { get; protected set; }

    //    //public override long EndLBA { get; protected set; }

    //    //public override string Name { get; set; }                         //分区标识;
    //}
}
