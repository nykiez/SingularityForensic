using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SingularityForensic.Test.Common {
    [TestClass]
    public class AppDataFolderTest {
        [TestMethod]
        public void TestCreateCurrentDomainDirectory() {
            var folder = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/SingularityForensic";
            if (!System.IO.Directory.Exists(folder)) {
                System.IO.Directory.CreateDirectory(folder);
            }

            Assert.IsTrue(Directory.Exists(folder));
        }
    }

}
