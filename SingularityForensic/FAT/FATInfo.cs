using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FAT {
    public class FATInfo: StructFieldDecriptorBase<StFatINFO>,ICustomFieldDecriptor {
        public FATInfo(StFatINFO stFatINFO,long offset):base(stFatINFO) {
            this.Offset = offset;
            this.PrefixName = Constants.FATFieldPrefix_Info;
        }
        public long Offset { get; }
    }
}
