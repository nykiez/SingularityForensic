using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.FileSystem;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SingularityForensic.Test.BaseDevice {
    [TestClass()]
    public class HaveFileCollectionExtensionsTests {
        [TestInitialize]
        public void TestInitialize() {
            TestCommon.InitializeTest();
            FileSystemService.Current.Initialize();
        }
        [TestMethod()]
        public void GetInnerFilesTest() {
            var device = FileSystemService.Current.
                MountStream(System.IO.File.OpenRead("E://anli/Fat32_Test.img"), "mmp", null, null) as IDevice;

            var innerFiles = device.GetInnerFiles();
            var count = 0;
            Trace.WriteLine($"Dir not included");
            
            foreach (var file in innerFiles) {
                Trace.WriteLine(file.Name);
                count++;
            }

            Trace.WriteLine($"{count}");

            count = 0;
            Trace.WriteLine($"Dir included");
            innerFiles = device.GetInnerFiles(true);
            foreach (var file in innerFiles) {
                Trace.WriteLine(file.Name);
                count++;
            }
            Trace.WriteLine($"{count}");

            FileSystemService.Current.UnMountFile(device);
        }

        [TestMethod()]
        public void GetFileFromPathTest() {
            var file = FileSystemService.Current.MountStream(File.OpenRead("E://anli/FAT32.img"), "name", null, null);
            if (!(file is IHaveFileCollection fileCollection)) {
                Assert.Fail();
                return;
            }
            Assert.AreEqual(fileCollection.GetFileByUrl("name"), file);

            var file2 = fileCollection.GetFileByUrl("name\\H264Src\\H264Src.dsp") as IRegularFile;
            Assert.IsNotNull(file2);
            Assert.AreEqual(file2.Name, "H264Src.dsp");

            var file3 = fileCollection.GetFileByUrl("name\\H264Src") as IDirectory;
            Assert.IsNotNull(file3);
            Assert.AreEqual(file3.Name, "H264Src");

            var file4 = fileCollection.GetFileByUrl("name\\åAK\\ADMIN") as IDirectory;
            Assert.IsNotNull(file4);
            Assert.AreEqual(file4.Name, "ADMIN");
        }

        [TestMethod]
        public void CheckOwnTest() {
            var file = FileSystemService.Current.MountStream(File.OpenRead("E://anli/FAT32.img"), "name", null, null);
            if (!(file is IHaveFileCollection fileCollection)) {
                Assert.Fail();
                return;
            }
            var file2 = fileCollection.GetFileByUrl("name\\H264Src\\H264Src.dsp") as IRegularFile;
            Assert.IsTrue(fileCollection.CheckOwn(file2));
            Assert.IsFalse(fileCollection.CheckOwn(FileFactory.CreateRegularFile(string.Empty)));
        }

        [TestMethod]
        public void CheckGetParentFiles() {
            var file = FileSystemService.Current.MountStream(File.OpenRead("E://anli/FAT32.img"), "name", null, null);
            if (!(file is IHaveFileCollection fileCollection)) {
                Assert.Fail();
                return;
            }
            var file2 = fileCollection.GetFileByUrl("name\\H264Src\\H264Src.dsp") as IRegularFile;
            var files = fileCollection.GetParentFiles(file2);
            
            Assert.IsNotNull(files);
            Assert.AreEqual(files.Last(), fileCollection);

            var files2 = fileCollection.GetParentFiles(file2,true);
            Assert.IsNotNull(files2);
            Assert.AreEqual(files2.Last(), fileCollection);
            Assert.AreEqual(files2.First(), file2);
        }

        [TestMethod]
        public void CheckGetUrlByFile() {
            var file = FileSystemService.Current.MountStream(File.OpenRead("E://anli/FAT32.img"), "name", null, null);
            if (!(file is IHaveFileCollection fileCollection)) {
                Assert.Fail();
                return;
            }
            var path = "name\\H264Src\\H264Src.dsp";
            var file2 = fileCollection.GetFileByUrl(path) as IRegularFile;
            var url = fileCollection.GetUrlByFile(file2);
            Assert.AreEqual(url, path);
        }

        [TestCleanup]
        public void Clean() {

        }
    }
}