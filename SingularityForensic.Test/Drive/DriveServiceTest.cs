using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Drive;
using SingularityForensic.Drive.DeviceObjects;
using SingularityForensic.Test.App;
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
        }

        private DriveService _dService;
        private ComObject _comObject;

        [TestMethod]
        public void TestAddHdd() {
            DriveMockers.SLDriveTuple = (DriveType_LocalHDD, _comObject.LocalHdds.First());
            _dService.AddDrive();
            Assert.AreEqual(FSService.Current.EnumedFiles.Count(), 1);
            Assert.AreEqual(CaseService.Current.CurrentCase.CaseEvidences.Count(), 1);
            
            
        }


        [TestCleanup]
        public void Clean() {
            _comObject.Dispose();
        } 
    }
}
