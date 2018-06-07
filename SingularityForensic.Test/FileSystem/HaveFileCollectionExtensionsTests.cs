using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.FileSystem;
using System.Diagnostics;

namespace SingularityForensic.Test.BaseDevice {
    [TestClass()]
    public class HaveFileCollectionExtensionsTests {
        [TestInitialize]
        public void TestInitialize() {
            TestCommon.InitializeTest();
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
        }
    }
}