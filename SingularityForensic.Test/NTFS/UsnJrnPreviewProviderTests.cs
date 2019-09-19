using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.NTFS;
using SingularityForensic.NTFS.USN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.NTFS.Test {
    [TestClass()]
    public class UsnJrnPreviewProviderTests {
        private UsnJrnPreviewProvider _provider = new UsnJrnPreviewProvider();
        private const string UsnFileName = "E://anli/$UsnJrnl(1)";
        [TestMethod()]
        public void CreatePreviewerTest() {
            var previewer = _provider.CreatePreviewer(UsnFileName, UsnJrnPreviewProvider.UsnJrnFileName);
            Assert.IsNotNull(previewer);
        }

        [TestMethod()]
        public void CreatePreviewerTest1() {
            Assert.Fail();
        }
    }
}