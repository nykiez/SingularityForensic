using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Ext {
    class ExtPartInfo {
        /// <summary>
        /// 非托管状态;
        /// </summary>
        public ExtUnmanagedManager ExtUnmanagedManager { get; set; }

        /// <summary>
        /// 超级块;
        /// </summary>
        public SuperBlock SuperBlock { get; set; }

        /// <summary>
        /// 块组描述符;
        /// </summary>
        public StExtGroupDesc[] StExt4GroupDescs { get;set;}

        
    }

    public class SuperBlock: StructFieldDecriptorBase<StSuperBlock>,ICustomMemerDecriptor {
        public SuperBlock(StSuperBlock stSuperBlock):base(stSuperBlock) {

        }
    }
}
