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
            Assert.IsNotNull(_iTunesBackUpService);
            _iTunesBackUpService.Initialize();
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
            
            Assert.AreEqual(SingularityForensic.Contracts.FileSystem.FileSystemService.Current.MountedUnits.Count(), 1);
            var file = SingularityForensic.Contracts.FileSystem.FileSystemService.Current.MountedUnits.ElementAt(0);
            Assert.AreEqual(file.File.Name, "iosb");

            Assert.AreEqual(SingularityForensic.Contracts.Casing.CaseService.Current.CurrentCase.CaseEvidences.Count(), 1);
            var csEvidence = SingularityForensic.Contracts.Casing.CaseService.Current.CurrentCase.CaseEvidences.ElementAt(0);
            Assert.AreEqual(csEvidence.Name , "iosb");

            Assert.AreEqual(_iTunesBackUpService.Managers.Count(), 1);
            
        }
    }
}