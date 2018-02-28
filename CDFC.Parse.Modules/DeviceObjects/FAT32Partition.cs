using CDFC.Parse.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDFC.Parse.Contracts;
using static CDFC.Parse.Modules.Static.FAT32Methods;
using CDFC.Parse.Modules.Abstracts;
using CDFC.Parse.Modules.Structs;
using CDFC.Util.PInvoke;
using EventLogger;
using System.Diagnostics;

namespace CDFC.Parse.Modules.DeviceObjects {
    /// <summary>
    /// FAT32分区;
    /// </summary>
    public class FAT32Partition : TabPartPartition {
        public FAT32Partition(TabPartInfo tabPartInfo, Device parent):base(tabPartInfo,parent) {
            LoadStFileSys();
        }

        public StFatFileSys StFileSys { get; private set; }

        private void LoadStFileSys() {
            if (TabPartInfo != null && TabPartInfo.StTabPartInfoPtr != IntPtr.Zero) {
                try {
                    var device = this.GetParent<Device>();                                       //确定StartLBA/EndLBA;
                    if (Parent is IHaveHandle haveHandle) {
                        var sysPtr = fat_init(haveHandle.Handle, (ulong)StartLBA, FsType.FAT32);
                        if (sysPtr == IntPtr.Zero) {
                            throw new InvalidOperationException($"{sysPtr} can't be zero.");
                        }
                        StFileSys = sysPtr.GetStructure<StFatFileSys>();
                    }
                }
                catch (Exception ex) {
                    Logger.WriteCallerLine(ex.Message);
                    throw;
                }
            }
        }

        private StFatDBR? _stFatDBR;
        public StFatDBR? StFatDBR {
            get {
                if(_stFatDBR == null) {
                    if (StFileSys.stFatDBR != IntPtr.Zero) {
                        _stFatDBR = StFileSys.stFatDBR.GetStructure<StFatDBR>();
                    }
                    return null;
                }
                else {
                    return _stFatDBR.Value;
                }
            }
        }

        private StFatFSINFO? _stFsInfo;
        public StFatFSINFO? StFSINFO {
            get {
                if(_stFsInfo == null) {
                    if(StFileSys.stFSINFO != IntPtr.Zero) {
                        _stFsInfo = StFileSys.stFSINFO.GetStructure<StFatFSINFO>();
                    }
                    return null;
                }
                else {
                    return _stFsInfo;
                }
            }
        }

        private List<IFile> _children;
        public override IEnumerable<IFile> Children => _children;
        /// <summary>
        /// 加载子文件，并获取相关信息(如Ext4Node);
        /// </summary>
        public void LoadChildren(Action<long> ntfSizeAct = null, Func<bool> isCancel = null) {
            _children = new List<IFile>();

            try {
                var root = fat_get_rootdir(StFileSys.stFatDBR);

                void PrintLevel(int level) {
                    for (int i = 0; i < level; i++) {
                        Console.Write($"\t");
                    }
                }

                void PrintNode(StFAT32FileNode fileNode, int level) {
                    PrintLevel(level);


                    Console.WriteLine(fileNode.Name);

                    var clusterNode = fileNode.stClusterList;
                    while (clusterNode != IntPtr.Zero) {
                        var cluster = clusterNode.GetStructure<StFatCluster>();
                        //Console.WriteLine($"{cluster.nClusterNum} - {cluster.nLBAByte}");
                        clusterNode = cluster.next;
                    }

                    if ((fileNode._fileAttrib & Fat32FileAttr.Directory) == Fat32FileAttr.Directory
                        && !fileNode.Deleted
                        && fileNode.Name != "." && fileNode.Name != "..") {
                        var fileList = fat_parse_dir(fileNode.stClusterList);

                        while (fileList != IntPtr.Zero) {
                            var stFileList = fileList.GetStructure<StFAT32FileNode>();
                            PrintNode(stFileList, level + 1);
                            fileList = stFileList._next;
                        }
                        
                    }
                    else {

                    }
                }
                if (root != IntPtr.Zero) {
                    var node = root;
                    while (node != IntPtr.Zero) {
                        IFile file = null;

                        var rootNode = node.GetStructure<StFAT32FileNode>();

                        //若为目录,则加入目录;
                        if((rootNode._fileAttrib & Fat32FileAttr.Directory) == Fat32FileAttr.Directory) {
                            var direct = new FAT32Directory(node, this);
                            direct.LoadChildren(ntfSizeAct, isCancel);
                            file = direct;
                        }

                        else if ((rootNode._fileAttrib & Fat32FileAttr.SDocument) == Fat32FileAttr.SDocument) {
                            var regFile = new FAT32RegFile(node, this);
                            file = regFile;
                        }
                        else {
                            file = new Fat32OtherFile(node, this);
                        }
                        _children.Add(file);
                        //PrintNode(rootNode,0);
                        node = rootNode._next ;
                    }

                }
            }
            catch(Exception ex) {
                Logger.WriteCallerLine(ex.Message);
            }
        }

        public override FileSystemType FSType => FileSystemType.FAT32;

        public override uint ClusterSize => (uint)((StFatDBR?.sectors_per_cluster ?? 8) * StFatDBR?.sector_size ?? 512);

        /// <summary>
        /// 簇起始位置;
        /// </summary>
        public long ClusterStart {
            get {
                if(StFatDBR != null) {
                    var stFatDBR = StFatDBR.Value;
                    //获得簇起始地址;
                    long iniLBA = (stFatDBR.reserved + stFatDBR.fats * stFatDBR.fat32_length) * (long)stFatDBR.sector_size;
                    return (stFatDBR.reserved + stFatDBR.fats * stFatDBR.fat32_length) * stFatDBR.sector_size;
                }
                else {
                    Logger.WriteCallerLine($"{nameof(StFatDBR)} can't be null.");
                    return 0;
                }
                
            }
        }
    }
}
