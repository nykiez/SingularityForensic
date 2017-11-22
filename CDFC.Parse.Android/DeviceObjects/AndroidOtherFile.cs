using CDFC.Parse.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using CDFC.Parse.Contracts;
using CDFC.Parse.Android.Structs;
using EventLogger;
using static CDFC.Parse.Android.Static.InodeBlockMethods;
using System.Runtime.ExceptionServices;
using CDFC.Parse.Android.Contracts;

namespace CDFC.Parse.Android.DeviceObjects {
    //安卓其他文件(尚未区分);
    public class AndroidOtherFile : OtherFile, IExt4Node {
        /// <summary>
        /// 安卓其他文件构造方法;
        /// </summary>
        /// <param name="parent">父文件</param>
        /// <param name="stDirEntryPtr">文件结构(自定义)指针</param>
        public AndroidOtherFile(IntPtr stDirEntryPtr,IFile parent) {
            this.parent = parent;
            this.stDirEntryPtr = stDirEntryPtr;
        }

        private IntPtr stDirEntryPtr;                               //文件结构(自定义)指针;
        private StDirEntry? stDirEntry;                             //文件结构(自定义);
        public StDirEntry? StDirEntry {
            get {
                if (stDirEntry == null) {
                    LoadContent();
                }
                return stDirEntry;
            }
        }
        
        private StExt4Inode? stExt4Inode;                           //inode结构;
        public StExt4Inode? StExt4Inode {
            get {
                if (stExt4Inode == null) {
                    LoadContent();
                }
                return stExt4Inode;
            }
        }

        private StExt4DirEntry? stExt4DirEntry;                     //文件结构(原生);
        public StExt4DirEntry? StExt4DirEntry {
            get {
                if (stExt4DirEntry == null) {
                    LoadContent();
                }
                return stExt4DirEntry;
            }
        }
        
        //private List<StBlockList?> stBlockList;
        //public List<StBlockList?> StBlockList {
        //    get {
        //        if (stBlockList == null) {
        //            LoadContent();
        //        }
        //        return stBlockList;
        //    }
        //}


        //文件类型尚未确定;
        private List<BlockGroup> blockGroups;                                           //文件块组;
        public override List<BlockGroup> BlockGroups {
            get {
                if (blockGroups == null) {
                    LoadContent();
                }
                return blockGroups;
            }
        }

        public override FileType FileType {
            get {
                return FileType.Unknown;
            }
        }

        //父文件;
        private IFile parent;
        public override IFile Parent {
            get {
                return parent;
            }
        }

        private long? startLBA;                                     //起始LBA;
        public override long StartLBA {
            get {
                if (startLBA == null) {
                    LoadContent();
                }
                return startLBA == null ? 0 : startLBA.Value;
            }
        }

        //终止LBA(相对分区);
        //public override long EndLBA {
        //    get {
        //        return 0;
        //    }
        //}

        private byte[] nameBytes;
        private string name;                                         //文件名称;(暂未继承自接口);
        public override string Name {
            get {
                if (name == null) {
                    LoadContent();
                }
                return name;
            }
        }

        private long? size;
        public override long Size {
            get {
                if (size == null && StExt4Inode != null) {
                    size = StExt4Inode.Value.i_size_lo + StExt4Inode.Value.i_size_high << 32;
                }
                return size ?? 0;
            }
        }

        //加载内容，如起始地址，终止地址等;
        [HandleProcessCorruptedStateExceptions]
        public void LoadContent() {
            startLBA = 0;
            blockGroups = null;

            if (stDirEntryPtr != null && Parent != null) {
                var partition = this.GetParent<AndroidPartition>();
                if (partition == null) {
                    Logger.WriteLine($"{nameof(AndroidRegFile)}->{nameof(LoadContent)}:{nameof(partition)} can't be null!");
                    return;
                }

                try {
                    ParseByDirEntryPtr(stDirEntryPtr,
                        out stDirEntry,
                        out stExt4DirEntry,
                        out stExt4Inode,
                        out blockGroups);  //解析inode,blockList等;
                    deleted = StDirEntry?.bDel ?? false;

                    // 确定startLBA;
                    var superBlock = partition.TabPartInfo.SuperBlockInfo;
                    var firstBlock = blockGroups.FirstOrDefault();
                    if (superBlock != null && superBlock.StSuperBlock != null && firstBlock != null) {
                        startLBA = superBlock.StSuperBlock.Value.s_log_block_size * //块大小 = 2 ^ (10 + s_long_block_size)
                            2048 * firstBlock.BlockAddress;                       //块序号;
                    }

                    stExt4Inode?.GetMacTime(out modifiedTime, out accessedTime, out createTime);        //确定MAC时间;

                    name = stExt4DirEntry.Value.name;                                    // 确定名称;
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
}
