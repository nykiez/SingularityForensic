using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Ext;
using SingularityForensic.Test.Common;
using System.IO;

namespace SingularityForensic.Test.Ext {
    [TestClass()]
    public class ExtStreamParsingProviderTests {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            //设定事件聚合器;
            ExportProviderServiceProviderMocker.StaticInstance.SetInstance(CommonMockers.LoggerServiceMocker);
            //E://Ext4G.img
            _stream = File.OpenRead("E:\\anli\\Ext1g.img");
        }
        
        [TestMethod()]
        public void CheckIsValidStreamTest() {
            Assert.IsTrue(_provider.CheckIsValidStream(_stream));
        }
        Stream _stream;
        ExtStreamParsingProvider _provider = new ExtStreamParsingProvider();

        [TestMethod()]
        public void ParseStreamTest() {
            var reporter = new Mock<IProgressReporter>();
            //reporter.Setup(p => p.ReportProgress(It.IsAny<int>(),It.IsAny<int>(), It.IsAny<string>(),It.IsAny<string>())).Raises);
            var part = _provider.ParseStream(_stream, null, null, null) as IPartition;
            Assert.IsNotNull(part);
        }

        [TestCleanup]
        public void Clean() {
            _stream?.Dispose();
        }
    }
}