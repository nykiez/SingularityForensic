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
using CDFC.Parse.Android.DeviceObjects;
using CDFC.Parse.DeviceObjects;
using CDFC.Parse.Android.Structs;
using System.Linq;

namespace CommonTest {
    [TestClass]
    public class TestPInvoke {
        [TestMethod]
        public void TestFat() {
            var fs = File.OpenRead("E://fat32.img");

            var device = UnKnownDevice.LoadFromFileStream(fs);

            var stTabInfo = new StPartInfo {
                PartTabStartLBA = 0,
                PartTabEndLBA = 1231414
            };
            var tabInfoPtr = stTabInfo.GetPtrFromStructure();

            var stTab = new StTabPartInfo {
                PartInfoPtr = tabInfoPtr
            };
            var stTabPtr = stTab.GetPtrFromStructure();

            var tabPartInfo = new TabPartInfo(stTabPtr);

            var part = new FAT32Partition(tabPartInfo,device);

            part.LoadChildren();
            //            var device = AndroidDevice.LoadFromPath();
            var direct = part.Children.ElementAt(0) as FAT32Directory;
            var groups = direct.BlockGroups;
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
