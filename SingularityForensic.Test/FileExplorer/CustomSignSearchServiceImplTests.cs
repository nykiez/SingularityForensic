using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SingularityForensic.Contracts.FileExplorer;
using System.Diagnostics;
using System.IO;

namespace SingularityForensic.FileExplorer.Tests {
    [TestClass()]
    public class CustomSignSearchServiceImplTests {
        [TestInitialize]
        public void Initialize() {
            _signSearchService = new CustomSignSearchServiceImpl();
        }
        private ICustomSignSearchService _signSearchService;
        [TestMethod()]
        public void GetSettingTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void SearchTest() {
            var settingMocker = new Mock<ICustomSignSearchSetting>();
            var keyWord = new byte[] { 0x4d, 0x5a, 0x90, 0x00 };
            //4D5A9000
            settingMocker.SetupGet(p => p.KeyWord).Returns(
                () => keyWord
            );
            settingMocker.Setup(p => p.AlignToSec).Returns(true);
            settingMocker.Setup(p => p.SectorSize).Returns(512);

            using(var fs = File.OpenRead("E://anli/FAT32.img")) {

                var blocks = _signSearchService.Search(fs, settingMocker.Object, null);
                foreach (var (index, size) in blocks) {
                    Trace.WriteLine($"{index}-{size}");
                }
            }
            
        }
    }
}