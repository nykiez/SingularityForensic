using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.FileSystem {
    [TestClass]
    public class FileSystemServiceTest {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _fsService = FSService.Current;
            Assert.IsNotNull(_fsService);

        }

        private IFileSystemService _fsService;

        [TestMethod]
        public void TestMount() {
            //_fsService.MountStream()
        }
    }
}
