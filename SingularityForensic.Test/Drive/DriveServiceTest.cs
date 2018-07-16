using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Drive;
using SingularityForensic.Test.Common;
using static SingularityForensic.Drive.Constants;

namespace SingularityForensic.Test.Drive {
    [TestClass]
    public class DriveServiceTest {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            
            _comObject = ComObject.Current;
            //设定选择设备Mocker;
            ExportProviderServiceProviderMocker.StaticInstance.SetInstance(DriveMockers.DriveDialogServiceMocker);
            Assert.IsNotNull(_comObject);

            _dService = ServiceProvider.Current.GetInstance<DriveService>();
            Assert.IsNotNull(_dService);
            _dService.Initialize();
            FileSystemService.Current.Initialize();
        }

        private DriveService _dService;
        private ComObject _comObject;

        [TestMethod]
        public void TestAddHdd() {
            DriveMockers.SLDriveTuple = (DriveType_LocalHDD, _comObject.LocalHdds.First());
            _dService.AddDrive();
            Assert.AreEqual(FileSystemService.Current.MountedUnits.Count(), 1);
            Assert.AreEqual(CaseService.Current.CurrentCase.CaseEvidences.Count(), 1);

            //测试移除证据项;
            CaseService.Current.CurrentCase.RemoveCaseEvidence(CaseService.Current.CurrentCase.CaseEvidences.First());

            Assert.AreEqual(FileSystemService.Current.MountedUnits.Count(), 0);
            Assert.AreEqual(CaseService.Current.CurrentCase.CaseEvidences.Count(), 0);
        }

        private void TestAddVolumeCore() {
            DriveMockers.SLDriveTuple = (DriveType_LocalVolume, _comObject.LocalHdds.First().Volumes.First());
            _dService.AddDrive();
            Assert.AreEqual(FileSystemService.Current.MountedUnits.Count(), 1);
            Assert.AreEqual(CaseService.Current.CurrentCase.CaseEvidences.Count(), 1);
        }
        [TestMethod]
        public void TestAddVolume() {
            TestAddVolumeCore();

            //测试移除证据项;
            CaseService.Current.CurrentCase.RemoveCaseEvidence(CaseService.Current.CurrentCase.CaseEvidences.First());
            
            Assert.AreEqual(FileSystemService.Current.MountedUnits.Count(), 0);
            Assert.AreEqual(CaseService.Current.CurrentCase.CaseEvidences.Count(), 0);

            TestAddVolumeCore();
            //测试关闭案件响应;
            CaseService.Current.CloseCurrentCase();
            Assert.AreEqual(FileSystemService.Current.MountedUnits.Count(), 0);
            Assert.AreEqual(CaseService.Current.CurrentCase, null);
            //CaseService.Current.CloseCurrentCase();
        }

        [TestCleanup]
        public void Clean() {
            _comObject.Dispose();
        } 
    }
}
