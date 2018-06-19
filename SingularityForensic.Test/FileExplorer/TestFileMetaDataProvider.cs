using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;

namespace SingularityForensic.Test.FileExplorer {
    [TestClass]
    public class TestFileMetaDataProvider {

        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
        }

        [TestMethod]
        public void TestProviders() {
            var providers = ServiceProvider.GetAllInstances<IFileMetaDataProvider>();
        }
    }
}
