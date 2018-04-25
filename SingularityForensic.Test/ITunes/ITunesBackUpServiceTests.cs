using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using SingularityForensic.ITunes;
using SingularityForensic.Test.App;
using System.Linq;

namespace SingularityForensic.Test.ITunes {
    [TestClass()]
    public class ITunesBackUpServiceTests {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _iTunesBackUpService = ServiceProvider.GetInstance<ITunesBackUpService>();
        }
        private ITunesBackUpService _iTunesBackUpService;

        [TestMethod()]
        public void InitializeTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddITunesBackUpDirTest() {
            AppMockers.OpenDirName =  "H://iosb";
            _iTunesBackUpService.AddITunesBackUpDir();
            Assert.AreEqual(Contracts.FileSystem.FileSystemService.Current.MountedFiles.Count(), 1);
            var file = Contracts.FileSystem.FileSystemService.Current.MountedFiles.ElementAt(0);

        }
    }
}