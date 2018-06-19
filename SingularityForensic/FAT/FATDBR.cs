using SingularityForensic.Contracts.Common;

namespace SingularityForensic.FAT {
    public class FATDBR : StructFieldDecriptorBase<StFatDBR>,ICustomMemerDecriptor {
        public FATDBR(StFatDBR stFatDBR,long offset):base(stFatDBR) {
            this.Offset = offset;
        }

        public long Offset { get; }
    }
}
