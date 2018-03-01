using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System;
using CDFC.Util.PInvoke;
using Microsoft.Win32.SafeHandles;
using System.Management;
using CDFC.Parse.DeviceObjects;
using System.Linq;
using CDFC.Parse.Modules.DeviceObjects;
using CDFC.Parse.Modules.Structs;
using CDFC.Parse.Modules.Pictures;

namespace SingularityForensic.Test {
    [TestClass]
    public class TestPInvoke {

        [TestMethod]
        public void TestDeviceStream() {
            //var fs = new FileStream("dad","Dad");
        }

        [TestMethod]
        public void TestFat() {
            var part = GetFAT();
            //            var device = AndroidDevice.LoadFromPath();
            var direct = part.Children.ElementAt(0) as FAT32Directory;
            var groups = direct.BlockGroups;
        }

        private const string ImgPath = "G:/MobileImgs/Honor/mmcblk0";

        private FAT32Partition GetFAT() {
            var fs = System.IO.File.OpenRead(ImgPath);

            var device = UnKnownDevice.LoadFromFileStream(fs);

            var stTabInfo = new StPartInfo {
                PartTabStartLBA = 201326592,
                
            };
            var tabInfoPtr = stTabInfo.GetPtrFromStructure();

            var stTab = new StTabPartInfo {
                PartInfoPtr = tabInfoPtr
            };
            var stTabPtr = stTab.GetPtrFromStructure();

            
            var tabPartInfo = new TabPartInfo(stTabPtr);

            var part = new FAT32Partition(tabPartInfo, device);

            Marshal.FreeHGlobal(stTabPtr);

            part.LoadChildren();

            return part;
        }

        [TestMethod]
        public void TestFatIntercept() {
            var part = GetFAT();
            var stream = part.GetStream();
        }

        public void TestExplorer() {
            //ExplorerHelper.OpenFile("D://1.psd");
        }

        [TestMethod]
        public void TestSignSearcher() {
            var keyWord = new byte[] {0x00,0x00,0x00,0x02 };
            var fs = System.IO.File.OpenRead("I://ImageFile鹰潭.DD");
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
