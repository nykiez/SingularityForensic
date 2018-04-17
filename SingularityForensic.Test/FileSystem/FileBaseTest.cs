using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SingularityForensic.Contracts.FileSystem.Tests {
    [TestClass]
    public class FileBaseTest {
        //测试文件是否能够被正常添加并移除;
        [TestMethod]
        public void TestAddAndRemove() {
            var regFile = FileFactory.CreateRegularFile(string.Empty);
            var direct = FileFactory.CreateDirectory(string.Empty);

            direct.Children.Add(regFile);

            Assert.IsTrue(direct.Children.Contains(regFile));
            Assert.AreEqual(direct.Children.Count, 1);

            direct.Children.Remove(regFile);
            Assert.IsFalse(direct.Children.Contains(regFile));
            Assert.AreEqual(direct.Children.Count, 0);
        }
    }
}