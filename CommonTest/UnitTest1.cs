using Microsoft.VisualStudio.TestTools.UnitTesting;
using CDFCCultures.Helpers;
using CDFC.Parse.Signature.Pictures;
using System.IO;

namespace CommonTest {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void TestMethod1() {
            ExplorerHelper.OpenFile("D://1.psd");
        }

        [TestMethod]
        public void TestSignSearcher() {
            var keyWord = new byte[] {0x00,0x00,0x00,0x02 };
            var fs = File.OpenRead("I://ImageFile鹰潭.DD");
            var searcher = new SignSearcher(fs , keyWord, 10 * 1024 * 1024);
            searcher.AlignToSector = false;
            Assert.IsTrue(searcher.SearchStart(0, fs.Length));
            var fList = searcher.GetFileList(".dav");
        }
    }
}
