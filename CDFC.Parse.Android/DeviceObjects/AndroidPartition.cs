using CDFC.Parse.Android.Structs;
using CDFC.Parse.Contracts;
using System;
using System.Collections.Generic;
using EventLogger;
using CDFC.Util.PInvoke;
using static CDFC.Parse.Android.Static.Ext4Methods;
using CDFC.Parse.Abstracts;
using CDFC.Parse.Android.Abstracts;

namespace CDFC.Parse.Android.DeviceObjects {
    /// <summary>
    /// 安卓分区结构(Ext4)
    /// </summary>
    public class AndroidPartition : TabPartPartition {
        /// <summary>
        /// 安卓分区的构造方法;
        /// </summary>
        /// <param name="tabPartInfo">分区结构(自定义)</param>
        /// <param name="parent">父文件</param>
        public AndroidPartition(TabPartInfo tabPartInfo, Device parent) : base(tabPartInfo,parent) {
            
            Index++;
        }

        public static int Index = 0;
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
                    var device = this.GetParent<Device>();                                       //确定StartLBA/EndLBA;

                    if(device is IHaveHandle haveHandle) {
                        Cflabqd_Partition_Init(TabPartInfo.StTabPartInfoPtr, haveHandle.Handle);                   //初始化选定当前分区;
                    }
                    else {
                        throw new InvalidOperationException($"{nameof(IHaveHandle)} parent can't be found.");
                    }

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
                    
                    #region 加载子文件信息;
                    var stDirNode = stDirEntryPtr;
                    //var savedDirects = new List<AndroidDirectory>();            //为名称为".."或"."的目录所保存的目录列表;

                    while (stDirNode != IntPtr.Zero) {                           //获取DirEntity信息;
                        var dirTab = stDirNode.GetStructure<StDirEntry>();
                        var dirEntity = dirTab.DirInfo.GetStructure<StExt4DirEntry>();

                        IFile file = null;
                        if (dirEntity.file_type == Ext4FileType.Directory) {
                            var direct = new AndroidDirectory(stDirNode, this);
                            direct.LoadChildren(ntfSizeAct,isCancel);

                            file = direct;
                        }
                        else if (dirEntity.file_type ==  Ext4FileType.RegularFile) {
                            var regFile = new AndroidRegFile(stDirNode, this);
                            file = regFile;
                        }
                        else {
                            var otherFile = new AndroidOtherFile(stDirNode, this);
                            file = otherFile;
                        }

                        ntfSizeAct?.Invoke(file.Size);
                        _children.Add(file);
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
            foreach (var p in Children) {
                if (p is AndroidDirectory) {
                    (p as AndroidDirectory).Exit();
                }
            }
            
        }

        private List<IFile> _children = new List<IFile>();
        public override IEnumerable<IFile> Children => _children;

        //Inode数量;
        public uint INodeCount {
            get {
                return TabPartInfo?.SuperBlockInfo?.StSuperBlock?.s_inodes_count??0;
            }
        }

    }
}
