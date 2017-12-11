using Microsoft.VisualStudio.TestTools.UnitTesting;
using CDFCCultures.Helpers;
using CDFC.Parse.Signature.Pictures;
using System.IO;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System;
using CDFC.Util.PInvoke;
using Microsoft.Win32.SafeHandles;
using System.Management;

namespace CommonTest {
    

    [TestClass]
    public class TestPInvoke {
        

        [TestMethod]
        public void TestFat() {
            
        }

        public void TestExplorer() {
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

        [TestMethod]
        public void TestSerilizable() {
            var s = new ObservableCollection<string>();
            s.Add("dad");
            var formatter = new BinaryFormatter();
            var ms = new MemoryStream();
            formatter.Serialize(ms, s);
            ms.Position = 0;
            var s2 = formatter.Deserialize(ms);

        }
    }
}
