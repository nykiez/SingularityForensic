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

namespace CDFC.Parse.Modules.DeviceObjects {
    public class Fat32OtherFile : OtherFile, IFAT32Node {
        public Fat32OtherFile(IntPtr stFat32FilePtr, IIterableFile parent):base(parent) {
            if (stFat32FilePtr == IntPtr.Zero)
                throw new ArgumentException($"{nameof(stFat32FilePtr)} can't be null!");

            this.stFAT32FilePtr = stFat32FilePtr;
        }

        private IntPtr stFAT32FilePtr;
        private StFAT32FileNode? _stFAT32FileNode;
        public StFAT32FileNode? StFAT32FileNode => _stFAT32FileNode ?? (_stFAT32FileNode = stFAT32FilePtr.GetStructure<StFAT32FileNode>());

        public override long StartLBA => throw new NotImplementedException();

        public override string Name => StFAT32FileNode?.Name;

        public override long Size => StFAT32FileNode?.FileSize??0;

        public override bool? Deleted => StFAT32FileNode?.Deleted;

        public override DateTime? ModifiedTime => StFAT32FileNode?.ChangeTime;

        public override DateTime? AccessedTime => null;

        public override DateTime? CreateTime => StFAT32FileNode?.CreateTime;
    }
}
