using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
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
            _fsService = ServiceProvider.GetInstance<IFileSystemService>();
            _fsService.Initialize();
            Assert.IsNotNull(_fsService);
            _fsService.Initialize();
        }

        private IFileSystemService _fsService;
        private readonly string _eviGUID = "da31313";
        private const string testImgPath = "E:\\anli\\FAT32.img";
        [TestMethod]
        public void TestMount() {
            var elem = new XElement("dad");
            elem.SetXElemValue(_eviGUID, Contracts.Common.Constants.EvidenceGUID);
            var reporter = new Mock<IProgressReporter>();
            reporter.Setup(p => p.ReportProgress(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Callback<int, int, string, string>((p1, p2, s1, s2) => {
                Trace.WriteLine($"{p1}\t {p2} \t{s1} \t {s2}");
            });
            //G:\\MobileImgs\\Z0176-809.dd
            //E:\\anli\\Ext4G.img
            var file = _fsService.MountStream(File.OpenRead(testImgPath),"name", _eviGUID, reporter.Object);
        }

        [TestMethod()]
        public void PathTest() {
            TestMount();
            var sw = new Stopwatch();
            var testPath = $"{_eviGUID}\\孔";
            sw.Start();
            var file = FileSystemService.Current.GetFile(testPath);
            sw.Stop();
            Trace.WriteLine($"{nameof(FileSystemService.Current.GetFile)} elapsed time:{sw.ElapsedMilliseconds}");
            Assert.IsNotNull(file);
            sw.Restart();
            var path = FileSystemService.Current.GetPath(file);
            sw.Stop();
            Trace.WriteLine($"{nameof(FileSystemService.Current.GetPath)} elapsed time:{sw.ElapsedMilliseconds}");
            Assert.AreEqual(path, testPath);
        }
    }
}
