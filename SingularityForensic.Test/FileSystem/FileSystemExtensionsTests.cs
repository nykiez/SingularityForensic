using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.FileSystem;

namespace SingularityForensic.Test.Contracts.FileSystem {
    [TestClass()]
    public class FileSystemExtensionsTests {

        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            //var elem = new XElement
            //FileSystemService.Current.MountStream(File.OpenRead("E://fat32.img"),"dad",)
        }

        [TestMethod()]
        public void GetFileTest() {
            var path = FileSystemExtensions.GetFile(null, "31313/31231");
        }
    }
}