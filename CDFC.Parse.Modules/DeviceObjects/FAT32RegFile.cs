using CDFC.Parse.Abstracts;
using CDFC.Parse.Modules.Contracts;
using CDFC.Parse.Modules.Structs;
using CDFC.Parse.Contracts;
using CDFC.Util.PInvoke;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CDFC.Parse.Modules.Static.FAT32Methods;

namespace CDFC.Parse.Modules.DeviceObjects {
    public class FAT32RegFile : RegularFile, IFAT32Node {
        public FAT32RegFile(IntPtr stFat32FilePtr, IIterableFile parent):base(parent) {
            if (stFat32FilePtr == IntPtr.Zero)
                throw new ArgumentException($"{nameof(stFat32FilePtr)} can't be null!");

            this.stFAT32FilePtr = stFat32FilePtr;
        }
        
        private long? _startLBA;
        public override long StartLBA {
            get {
                if(_startLBA == null) {
                    LoadClusterInfo();
                }
                return _startLBA.Value;
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


        public override long Size => StFAT32FileNode?.FileSize??0;

        public override string Name => StFAT32FileNode?.Name;

        public override bool? Deleted => StFAT32FileNode?.Deleted;


        private IntPtr stFAT32FilePtr;
        private StFAT32FileNode? _stFAT32FileNode;
        public StFAT32FileNode? StFAT32FileNode => _stFAT32FileNode ?? (_stFAT32FileNode = stFAT32FilePtr.GetStructure<StFAT32FileNode>());

        public override DateTime? ModifiedTime => StFAT32FileNode?.ChangeTime;

        public override DateTime? AccessedTime => null;

        public override DateTime? CreateTime => StFAT32FileNode?.CreateTime;

        private void LoadClusterInfo() {
            var part = this.GetParent<FAT32Partition>();
            if (StFAT32FileNode != null && part != null) {
                var tuple = LoadClusterInfoFromNode(StFAT32FileNode.Value, part.ClusterStart, (int)part.ClusterSize);
                _startLBA = tuple.startLBA;
                _blockGroups = tuple.groups;
            }

        }
    }
}
