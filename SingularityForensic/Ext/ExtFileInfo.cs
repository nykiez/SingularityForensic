using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Ext {
    class ExtFileInfo {
        public StDirEntry? StDirEntry { get; set; }
        public StExt4DirEntry? StExt4DirEntry { get; set; }
        public StExt4Inode? StExt4Inode { get; set; }
        public IntPtr BlockListPtr { get; set; }
        public IEnumerable<StBlockList> BlockList {
            get {
                if(BlockListPtr == IntPtr.Zero) {
                    yield break;
                }

                foreach (var block in BlockListPtr.GetStructs<StBlockList>(p => p.Next)) {
                    yield return block;
                } 
            }
        }
    }
}
