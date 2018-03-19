using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Drive;

namespace SingularityForensic.Test.Drive {
    [TestClass]
    public class DriveDialogServiceTest {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _dDialogService = ServiceProvider.Current.GetInstance<IDriveDialogService>();
        }

        IDriveDialogService _dDialogService;
        [TestMethod]
        public void TestShowDialog() {
            var driveTuple = _dDialogService.SelectDrive();
            
        }


    }
}
