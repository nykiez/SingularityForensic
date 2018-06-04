using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using SingularityForensic.NTFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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