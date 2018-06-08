using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.FileSystem;

namespace SingularityForensic.Test.BaseDevice {
    [TestClass]
    public class FileSystemServiceTest {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _fsService = FileSystemService.Current;
            Assert.IsNotNull(_fsService);
            
        }

        private IFileSystemService _fsService;

        [TestMethod]
        public void TestMount() {
            //_fsService.MountStream()
        }
    }
}
