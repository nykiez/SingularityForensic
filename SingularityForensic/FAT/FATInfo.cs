using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FAT {
    public class FATInfo: StructFieldDecriptorBase<StFatINFO>,ICustomMemerDecriptor {
        public FATInfo(StFatINFO stFatINFO,long offset):base(stFatINFO) {
            this.Offset = offset;
            
        }
        public long Offset { get; }
    }
}
