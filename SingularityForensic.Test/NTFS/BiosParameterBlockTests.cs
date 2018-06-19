using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using SingularityForensic.NTFS;

namespace SingularityForensic.Test.NTFS {
    [TestClass()]
    public class BiosParameterBlockTests {
        [TestMethod()]
        public void FromBytesTest() {
            using (var fs = System.IO.File.OpenRead("E://NTFS2G.img")) {
                var bts = StreamUtilities.ReadSector(fs);
                var bpb = BiosParameterBlock.FromBytes(bts, 0);
            }
        }
    }
}