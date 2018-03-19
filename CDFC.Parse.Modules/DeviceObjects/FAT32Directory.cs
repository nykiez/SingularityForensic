using CDFC.Parse.Abstracts;
using CDFC.Parse.Modules.Contracts;
using CDFC.Parse.Modules.Structs;
using CDFC.Parse.Contracts;
using CDFC.Util.PInvoke;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using static CDFC.Parse.Modules.Static.FAT32Methods;

namespace CDFC.Parse.Modules.DeviceObjects {
    public class FAT32Directory : Directory, IFAT32Node {
        public FAT32Directory(IntPtr stFAT32FilePtr, IIterableFile parent) : base(parent) {
            if (stFAT32FilePtr == IntPtr.Zero)
                throw new ArgumentException($"{nameof(stFAT32FilePtr)} can't be null!");

            this.stFAT32FilePtr = stFAT32FilePtr;
        }

        private IntPtr stFAT32FilePtr;
        private StFAT32FileNode? _stFAT32FileNode;
        public StFAT32FileNode? StFAT32FileNode => _stFAT32FileNode ?? (_stFAT32FileNode = stFAT32FilePtr.GetStructure<StFAT32FileNode>());

        public override string Name => StFAT32FileNode?.Name;

        private List<IFile> _children;
        public override IEnumerable<IFile> Children => _children;

        private long? _startLBA;
        public override long StartLBA {
            get {
                if(_startLBA == null) {
                    LoadClusterInfo();
                }
                return _startLBA ?? 0;
            }
        }

        public override long Size => StFAT32FileNode?.FileSize ?? 0;

        public override bool? Deleted => StFAT32FileNode?.Deleted;
        
        public override DateTime? ModifiedTime => StFAT32FileNode?.ChangeTime;

        public override DateTime? AccessedTime => null;

        public override DateTime? CreateTime => StFAT32FileNode?.CreateTime;

        [HandleProcessCorruptedStateExceptions]
        internal void LoadChildren(Action<long> ntfSizeAct = null, Func<bool> isCancel = null) {
            _children = new List<IFile>();

            if (StFAT32FileNode == null) {
                return;
            }

            //若已经删除,则不进入;
            if (StFAT32FileNode.Value.Deleted) {
                return;
            }

            try {
                var root = fat_parse_dir(StFAT32FileNode.Value.stClusterList);
                var node = root;

                while(node != IntPtr.Zero) {
                    IFilefile = null;

                    var rootNode = node.GetStructure<StFAT32FileNode>();

                    //若为目录,则加入目录;
                    if ((rootNode._fileAttrib & Fat32FileAttr.Directory) == Fat32FileAttr.Directory
                        && !rootNode.Deleted
                        && rootNode.Name != "." && rootNode.Name != "..") {
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
                    node = rootNode._next;
                }
            }
            catch(Exception ex) {
                LoggerService.Current?.WriteCallerLine(ex.Message);
            }
            
        }
        
        private List<BlockGroup> _blockGroups;
        public override IEnumerable<BlockGroup> BlockGroups {
            get {
                if(_blockGroups == null) {
                    LoadClusterInfo();
                }
                return _blockGroups;
            }
        }

        private void LoadClusterInfo() {
            var part = this.GetParent<FAT32Partition>();
            if(StFAT32FileNode != null && part != null) {
                var tuple = LoadClusterInfoFromNode(StFAT32FileNode.Value, part.ClusterStart,(int)part.ClusterSize);
                _startLBA = tuple.startLBA;
                _blockGroups = tuple.groups;
            }
            
        }

    }
}
