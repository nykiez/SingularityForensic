using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem.Tests {
    [TestClass]
    public class FileBaseTest {
        //测试文件是否能够被正常添加并移除;
        [TestMethod]
        public void TestAddAndRemove() {
            var regFile = new RegularFile(string.Empty, null);
            var direct = new Directory(string.Empty, null);

            direct.Children.Add(regFile);

            Assert.IsTrue(direct.Children.Contains(regFile));
            Assert.AreEqual(direct.Children.Count, 1);

            direct.Children.Remove(regFile);
            Assert.IsFalse(direct.Children.Contains(regFile));
            Assert.AreEqual(direct.Children.Count, 0);
        }
    }
}