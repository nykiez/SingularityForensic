using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileSystem;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;

namespace SingularityForensic.Test.BaseDevice {
    [TestClass]
    public class FileSystemServiceTest {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _fsService = FileSystemService.Current;
            Assert.IsNotNull(_fsService);
            _fsService.Initialize();
        }

        private IFileSystemService _fsService;

        [TestMethod]
        public void TestMount() {
            var elem = new XElement("dad");
            var reporter = new Mock<IProgressReporter>();
            reporter.Setup(p => p.ReportProgress(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Callback<int, int, string, string>((p1, p2, s1, s2) => {
                Trace.WriteLine($"{p1}\t {p2} \t{s1} \t {s2}");
            });
            _fsService.MountStream(File.OpenRead("E://Ext4G.img"),"name", elem, reporter.Object);
        }
    }
}
