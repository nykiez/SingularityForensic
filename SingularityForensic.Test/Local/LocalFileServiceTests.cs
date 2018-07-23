using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Local;
using SingularityForensic.Test.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.Local {
    [TestClass()]
    public class LocalFileServiceTests {

        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _localFileService = ServiceProvider.GetInstance<LocalFileService>();
            _localFileService.Initialize();
        }

        private LocalFileService _localFileService;

        [TestMethod()]
        public void InitializeTest() {
            _localFileService.Initialize();
        }

        [TestMethod()]
        private void AddLocalDirTest() {
            AppMockers.OpenDirName = "E:\\anli";
            _localFileService.AddLocalDir();
            Assert.AreEqual(FileSystemService.Current.MountedUnits.Count(), 1);
            Assert.AreEqual(CaseService.Current.CurrentCase.CaseEvidences.Count(), 1);
        }

        [TestMethod()]
        public void UninstallTest() {
            AddLocalDirTest();

            //测试证据项移除后是否能够正常卸载;
            CaseService.Current.CurrentCase.RemoveCaseEvidence(CaseService.Current.CurrentCase.CaseEvidences.First());
            Assert.AreEqual(FileSystemService.Current.MountedUnits.Count(), 0);
            Assert.AreEqual(CaseService.Current.CurrentCase.CaseEvidences.Count(), 0);

            AddLocalDirTest();
            //测试关闭案件后是否能够正常卸载;
            CaseService.Current.CloseCurrentCase();
            Assert.AreEqual(FileSystemService.Current.MountedUnits.Count(), 0);
            Assert.AreEqual(CaseService.Current.CurrentCase, null);

            //测试GC是否能够正常回收某些对象;
            for (int i = 0; i < 2; i++) {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
            }
        }

        
    }
}