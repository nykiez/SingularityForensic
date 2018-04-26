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
            
            Assert.AreEqual(Contracts.FileSystem.FileSystemService.Current.MountedEntities.Count(), 1);
            var file = Contracts.FileSystem.FileSystemService.Current.MountedEntities.ElementAt(0);
            Assert.AreEqual(file.file.Name, "iosb");

            Assert.AreEqual(Contracts.Casing.CaseService.Current.CurrentCase.CaseEvidences.Count(), 1);
            var csEvidence = Contracts.Casing.CaseService.Current.CurrentCase.CaseEvidences.ElementAt(0);
            Assert.AreEqual(csEvidence.Name , "iosb");

            Assert.AreEqual(_iTunesBackUpService.Managers.Count(), 1);
            
        }
    }
}