using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.Local {
    [TestClass]
    public class LocalDirParserTest {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
        }

        [TestMethod]
        public void TestGetDir() {
            var dirPath = "D://";
            var dirName = "C# Console";
            var dir = LocalDirParser.GetDirectory($"{dirPath}{dirName}");
            Assert.IsNotNull(dir);

            Assert.AreEqual(dir.Name, dirName);


        }
    }
}
