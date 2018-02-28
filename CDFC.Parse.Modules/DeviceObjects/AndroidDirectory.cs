using CDFC.Parse.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using CDFC.Parse.Modules.Structs;
using EventLogger;
using static CDFC.Parse.Modules.Static.Ext4Methods;
using CDFC.Util.PInvoke;
using CDFC.Parse.Abstracts;
using static CDFC.Parse.Modules.Static.Ext4Methods;
using System.Runtime.ExceptionServices;
using CDFC.Parse.Modules.Contracts;

namespace CDFC.Parse.Modules.DeviceObjects {
    //安卓目录;
    public partial class AndroidDirectory : Directory , IExt4Node {
        /// <summary>
        /// 安卓目录结构;
        /// </summary>
        /// <param name="stDirEntryPtr">目录结构指针</param>
        public AndroidDirectory(IntPtr stDirEntryPtr, IIterableFile parent):base(parent) {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));

            this.stDirEntryPtr = stDirEntryPtr;
        }
        
        public IntPtr stDirEntryPtr { get; }                                               //文件结构(自定义结构)指针;

        private IntPtr stBlockListPtr;                                              //为解析子文件，留下stBlockList的指针;
        private IntPtr stExt4InodePtr;
        
        private long? size;
        public override long Size {
            get {
                if (size == null && StExt4Inode != null) {
                    size = (StExt4Inode.Value.i_blocks_lo + StExt4Inode.Value.i_size_high << 32) * 512;
                }
                return size ?? 0;
            }
        }

        private List<BlockGroup> _blockGroups;                                           //目录块组;
        public override IEnumerable<BlockGroup> BlockGroups => _blockGroups;

        private long? startLBA;                                                         //目录起始位置;
        public override long StartLBA {
            get {
                if(startLBA == null) {
                    //LoadChildren();
                }
                return startLBA ?? 0;
            }
        }

        private static DateTime InitTime = DateTime.Parse("1970/01/01");
        
        /// <summary>
        /// 加载子内容(子文件，名称,Inode,BlockList等);
        /// </summary>
        /// <param name="ntfSizeAct">通知进度委托</param>
        [HandleProcessCorruptedStateExceptions]
        internal void LoadContent(Action<long> ntfSizeAct = null,Func<bool> isCancel = null) {                                               //加载子内容以及相关内容;
            if (IsOwnCreate) { return; }                                            //假若是虚构，直接返回;

            _blockGroups = null;
            children = new List<IFile>();

            if (stDirEntryPtr != IntPtr.Zero && Parent != null) {
                var partition = this.GetParent<AndroidPartition>();
                if (partition == null) {
                    Logger.WriteLine($"{nameof(AndroidDirectory)}->{nameof(LoadContent)}:partition can't be null");
                    return;
                }
                try {
                    ParseByDirEntryPtr(stDirEntryPtr,
                                        out stDirEntry,

                                        out stExt4DirEntry,

                                        out stExt4InodePtr,
                                        out stExt4Inode,

                                        out stBlockListPtr,
                                        out _blockGroups);  //解析inode,blockList等;
                    
                    deleted = stDirEntry?.bDel;
                    name = stExt4DirEntry.Value.name;                               //加载名称;
                    var superBlock = partition.TabPartInfo.SuperBlockInfo;          //确定startLBA;
                    if(superBlock != null && superBlock.StSuperBlock != null) {
                        var firstBlock = _blockGroups.FirstOrDefault();
                        if (firstBlock != null) {
                            startLBA = superBlock.StSuperBlock.Value.s_log_block_size * //块大小 = 2 ^ (10 + s_long_block_size)
                                2048 * firstBlock.BlockAddress;                       //块序号;
                        }
                    }
                    
                    stExt4Inode?.GetMacTime(out modifiedTime, out accessedTime, out createTime);        //确定MAC时间;

                    if (stBlockListPtr != IntPtr.Zero ) {
                        #region 加载子文件;
                        if (name != ".." && name != "." && !deleted.Value) {                               //当目录名不为".."或者"."时,当作普通目录处理;
                            var stChildrenDirNode = Cflabqd_Parse_Dir(stBlockListPtr);     //加载子文件的Dir;
                            var stChildrenDirPtr = stChildrenDirNode;

#if DEBUG
                            if(name == "unzip") {

                            }
#endif
                            while (stChildrenDirNode != IntPtr.Zero) {                              //循环获取,直到节点为空;
                                var dirTab = stChildrenDirNode.GetStructure<StDirEntry>();
                                var dirEntity = dirTab.DirInfo.GetStructure<StExt4DirEntry>();

                                IFile file = null;
                                if (dirEntity.file_type == Ext4FileType.Directory) {
                                    var direct = new AndroidDirectory(stChildrenDirNode, this);
                                    direct.LoadContent(ntfSizeAct,isCancel);
                                    file = direct;
                                }
                                else if (dirEntity.file_type == Ext4FileType.RegularFile) {
                                    var regFile = new AndroidRegFile(stChildrenDirNode, this);
                                    file = regFile;
                                }
                                else {
                                    var otherFile = new AndroidOtherFile(stChildrenDirNode, this);
                                    file = otherFile;
                                }
                                ntfSizeAct?.Invoke(file.Size);
                                children.Add(file);
                                stChildrenDirNode = dirTab.Next;

                                if (dirTab.bDel) {
                                    partition.DelFileNum++;
                                }
                                else {
                                    partition.RegFileNum++;
                                }

                                if (isCancel?.Invoke() == true) {
                                    break;
                                }
                            }

                        }    
                        #endregion
                    }
                }
                catch (Exception ex) {
                    Logger.WriteLine($"{nameof(AndroidDirectory)}->{nameof(LoadContent)}:{ex.Message}");
                }
                finally {
                    if (children.Count == 0 && name != "." && name != "..") {
                        if (this.Parent != null) {
                            children.Add(CreateParentDirect(this.Parent));
                            children.Add(CreateSiblingDirect(this.Parent));
                        }
                    }
                }
            }
        }

        
        
        //创建同胞目录".";
        private static AndroidDirectory CreateSiblingDirect(IFile bro) {
            if (bro != null && bro.Parent != null) {
                var itrFile = bro.Parent as IIterableFile;
                var direct = new AndroidDirectory {
                    children = new List<IFile>(itrFile?.Children),
                    name = "."
                };
                return direct;
            }
            return null;
        }

        private AndroidDirectory():base(null) {
            IsOwnCreate = true;
        }

        
        /// <summary>
        /// 创建".."目录;
        /// </summary>
        /// <param name="son"></param>
        /// <returns></returns>
        private AndroidDirectory CreateParentDirect(IFile son) {
            if (son != null) {
                var itrFile = son as IIterableFile;
                var direct = new AndroidDirectory {
                    children = new List<IFile>(itrFile?.Children),
                    name = ".."
                };
                return direct;
            }

            return null;
            
        }

        //目录名称;
        private string name;
        public override string Name => name;

        internal List<IFile> children;           //子文件节点(包含子目录);
        public override IEnumerable<IFile> Children => children;
        
        //public IFile Parent { get;  }                            //从属父文件(兼容接口);
            
        private bool? deleted;                                              //是否被删除;
        public override bool? Deleted => deleted;


        private DateTime? modifiedTime;
        public override DateTime? ModifiedTime {
            get {
                if(modifiedTime == null) {
                    //LoadChildren();
                }
                return modifiedTime;
            }
        }

        private DateTime? accessedTime;
        public override DateTime? AccessedTime {
            get {
                if(accessedTime == null) {
                    //LoadChildren();
                }
                return accessedTime;
            }
        }

        private DateTime? createTime;
        public override DateTime? CreateTime {
            get {
                if(createTime == null) {
                    //LoadChildren();
                }
                return createTime;
            }
        }

        public void Exit() {
            if (!IsOwnCreate) {

            }
        }
    }

    public partial class AndroidDirectory {
        private StDirEntry? stDirEntry;
        //文件入口结构(自定义结构);
        public StDirEntry? StDirEntry {
            get {
                if (stDirEntry == null) {
                    //LoadChildren();
                }
                return stDirEntry;
            }
        }

        //private IntPtr stExt4DirEntryPtr;                                           //文件结构(原生结构)所属非托管指针;
        //文件结构(原生结构);
        private StExt4DirEntry? stExt4DirEntry;
        public StExt4DirEntry? StExt4DirEntry {
            get {
                if (stExt4DirEntry == null) {
                    //LoadChildren();
                }
                return stExt4DirEntry;
            }
        }

        //INode结构;
        private StExt4Inode? stExt4Inode;
        public StExt4Inode? StExt4Inode {
            get {
                if (stExt4Inode == null) {
                    //LoadChildren();
                }
                return stExt4Inode;
            }
        }
    }
    public partial class AndroidDirectory : IDelAndRegNumedFile {
        //删除文件数;
        public long RegFileNum { get; internal set; }

        //正常文件数;
        public long DelFileNum { get; internal set; }

        //正常目录数;
        public long RegDirNum { get; internal set; }

        //删除目录数;
        public long DelDirNum { get; internal set; }

        public long TotalRegFileNum {
            get {
                throw new NotImplementedException();
            }
        }
    }
}
