using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using SingularityForensic.ITunes;
using SingularityForensic.Test.App;
using System.Linq;
using System.Threading;

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
            
            //测试挂载;
            Assert.AreEqual(Contracts.FileSystem.FileSystemService.Current.MountedUnits.Count(), 1);
            var file = Contracts.FileSystem.FileSystemService.Current.MountedUnits.ElementAt(0);
            Assert.AreEqual(file.File.Name, "iosb");

            //查看案件是否被正确挂载;
            Assert.AreEqual(Contracts.Casing.CaseService.Current.CurrentCase.CaseEvidences.Count(), 1);
            var csEvidence = Contracts.Casing.CaseService.Current.CurrentCase.CaseEvidences.ElementAt(0);
            Assert.AreEqual(csEvidence.Name , "iosb");

            Assert.AreEqual(_iTunesBackUpService.Managers.Count(), 1);

            //测试移除案件文件时卸载;
            Contracts.Casing.CaseService.Current.CurrentCase.RemoveCaseEvidence(csEvidence);
            Assert.AreEqual(Contracts.Casing.CaseService.Current.CurrentCase.CaseEvidences.Count(), 0);
            Assert.AreEqual(_iTunesBackUpService.Managers.Count(), 0);
            Assert.AreEqual(Contracts.FileSystem.FileSystemService.Current.MountedUnits.Count(), 0);

            _iTunesBackUpService.AddITunesBackUpDir();

            //测试卸载案件时卸载;
            Contracts.Casing.CaseService.Current.CloseCurrentCase();
            Assert.AreEqual(Contracts.Casing.CaseService.Current.CurrentCase, null);
            Assert.AreEqual(_iTunesBackUpService.Managers.Count(), 0);
            Assert.AreEqual(Contracts.FileSystem.FileSystemService.Current.MountedUnits.Count(), 0);

            csEvidence = null;
            file = null;
            //测试回收;
            for (int i = 0; i < 2; i++) {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
            }

        }

        [TestCleanup]
        public void Clean() {
         
        }

    }
}