using Moq;
using SingularityForensic.Drive;

namespace SingularityForensic.Test.Drive {
    static class DriveMockers {
        //选择设备对话框Mocker;
        private static IDriveDialogService _driveDialogServiceMocker;
        internal static IDriveDialogService DriveDialogServiceMocker {
            get {
                if(_driveDialogServiceMocker == null) {
                    var drdMocker = new Mock<IDriveDialogService>();
                    drdMocker.Setup(p => p.SelectDrive()).Returns(() => SLDriveTuple);
                    _driveDialogServiceMocker = drdMocker.Object;
                }
                return _driveDialogServiceMocker;
            }
        }

        internal static (string driveType, object entity)? SLDriveTuple { get; set; }
    }
}
