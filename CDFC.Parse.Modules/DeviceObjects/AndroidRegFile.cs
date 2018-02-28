using CDFC.Parse.Abstracts;
using CDFC.Parse.Modules.Structs;
using CDFC.Parse.Contracts;
using System;
using System.Collections.Generic;
using EventLogger;
using System.Linq;
using static CDFC.Parse.Modules.Static.Ext4Methods;
using System.Runtime.ExceptionServices;
using CDFC.Parse.Modules.Contracts;
using System.Text;
using CDFC.Parse.Modules.Static;

namespace CDFC.Parse.Modules.DeviceObjects {
#pragma warning disable 0169
    //安卓/EXT4分区常规文件;
    public class AndroidRegFile : RegularFile, IExt4Node,IBlockGroupedFile {
        /// <summary>
        /// 安卓常规文件构造方法;
        /// </summary>
        ///<param name="parent">父文件</param>
        ///<param name="stDirEntryPtr">文件结构(自定义)指针</param>
        public AndroidRegFile(IntPtr stDirEntryPtr, IIterableFile parent):base(parent) {
            this.stDirEntryPtr = stDirEntryPtr;
        }

        private IntPtr stDirEntryPtr;                               //文件结构(自定义)指针;
        private StDirEntry? stDirEntry;                             //文件结构(自定义);
        public StDirEntry? StDirEntry {
            get {
                if(stDirEntry == null) {
                    LoadContent();
                }
                return stDirEntry;
            }
        }

        //public override long EndLBA {
        //    get {
        //        return 0;
        //    }
        //}

        //private IntPtr stExt4InodePtr;                              //inode指针;
        private StExt4Inode? stExt4Inode;                           //inode结构;
        public StExt4Inode? StExt4Inode {
            get {
                if(stExt4Inode == null) {
                    LoadContent();
                }
                return stExt4Inode;
            }
        }

        private StExt4DirEntry? stExt4DirEntry;                     //文件结构(原生);
        public StExt4DirEntry? StExt4DirEntry {
            get {
                if(stExt4DirEntry == null) {
                    LoadContent();
                }
                return stExt4DirEntry;
            }
        }
        
        //private IntPtr stBlockListPtr;                                //扇区块链表所指向的指针;

        //private List<StBlockList?> stBlockList;                   //扇区块链表;
        //public List<StBlockList?> StBlockList {
        //    get {
        //        if(stBlockList == null) {
        //            LoadContent();
        //        }
        //        return stBlockList;
        //    }
        //}
        private List<BlockGroup> blockGroups;                           //文件块组;
        public override IEnumerable<BlockGroup> BlockGroups {
            get {
                if (blockGroups == null) {
                    LoadContent();
                }
                return blockGroups;
            }
        }


        private long? size;
        public override long Size {
            get {
                if (size == null && StExt4Inode != null) {
                    size = StExt4Inode.Value.i_size_lo + StExt4Inode.Value.i_size_high << 32;
                }
                return size??0;
            }
        }

        private long? startLBA;                                     //起始LBA;
        public override long StartLBA {
            get {
                if(startLBA == null) {
                    LoadContent();
                }
                return startLBA??0;
            }
        }

        private byte[] nameBytes;
        private string _name;                                         //文件名称;(暂未继承自接口);
        public override string Name {
            get {
                if(_name == null) {
                    LoadContent();
                    if(nameBytes != null) {
                        _name = Encoding.UTF8.GetString(nameBytes);
                    }
                }
                return _name;
            }
        }

        /// <summary>
        /// 加载相关信息,子文件,LBA等;
        /// </summary>
        [HandleProcessCorruptedStateExceptions]
        internal void LoadContent() {
            blockGroups = new List<BlockGroup>();
            startLBA = 0;

            if(stDirEntryPtr != null && Parent != null) {
                var partition = this.GetParent<AndroidPartition>();
                if (partition == null) {
                    Logger.WriteLine($"{nameof(AndroidRegFile)}->{nameof(LoadContent)}:{nameof(partition)} can't be null!");
                    return;
                }

                try {
                    var device = this.GetParent<AndroidDevice>();
                    Ext4Methods.Cflabqd_Partition_Init(partition.TabPartInfo.StTabPartInfoPtr,device.Handle);
                    ParseByDirEntryPtr(stDirEntryPtr, out stDirEntry,
                        out stExt4DirEntry,
                        out stExt4Inode,
                        out blockGroups);  //解析inode,blockList等;
                    deleted = StDirEntry?.bDel ?? false;
                    //确定名称;
                    nameBytes = Encoding.Default.GetBytes(stExt4DirEntry.Value.name);

                    // 确定startLBA;
                    var superBlock = partition.TabPartInfo.SuperBlockInfo;
                    if (superBlock != null && superBlock.StSuperBlock != null) {
                        startLBA = superBlock.StSuperBlock.Value.s_log_block_size * 2048 *  //块大小 = 2 ^ (10 + s_long_block_size)
                            (blockGroups.FirstOrDefault()?.BlockAddress??0);                       //块序号;
                    }
                    else {
                        startLBA = 0;
                    }

                    stExt4Inode?.GetMacTime(out modifiedTime, out accessedTime, out createTime);        //确定MAC时间;

                }
                catch (Exception ex) {
                    Logger.WriteLine($"{nameof(AndroidRegFile)}->{nameof(LoadContent)}:{ex.Message}");
                }
            }
            
        }

        private bool? deleted;                                                  //是否被删除;
        public override bool? Deleted {
            get {
                if (deleted == null) {
                    LoadContent();
                }
                return deleted;
            }
        }

        private DateTime? modifiedTime;
        public override DateTime? ModifiedTime {
            get {
                if (modifiedTime == null) {
                    LoadContent();
                }
                return modifiedTime;
            }
        }

        private DateTime? accessedTime;
        public override DateTime? AccessedTime {
            get {
                if (accessedTime == null) {
                    LoadContent();
                }
                return accessedTime;
            }
        }

        private DateTime? createTime;
        public override DateTime? CreateTime {
            get {
                if (createTime == null) {
                    LoadContent();
                }
                return createTime;
            }
        }
        
    }

#pragma warning restore 0169
}
