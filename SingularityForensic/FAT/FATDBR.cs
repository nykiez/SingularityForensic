using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FAT {
    public class FATDBR : StructFieldDecriptorBase<StFatDBR>,ICustomFieldDecriptor {
        public FATDBR(StFatDBR stFatDBR,long offset):base(stFatDBR) {
            this.Offset = offset;
            this.PrefixName = Constants.FATFieldPrefix_DBR;
        }

        public long Offset { get; }
    }
}
