using SingularityForensic.Drive.ViewModels;
using SingularityForensic.Drive.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Drive {
    /// <summary>
    /// 设备对话框服务;
    /// </summary>
    public interface IDriveDialogService {
        /// <summary>
        /// 选择一个设备;
        /// </summary>
        /// <returns>
        /// driveType确定设备类型;
        /// entity为设备实体;
        /// </returns>
        (string driveType, object entity)? SelectDrive();
    }


    /// <summary>
    /// 设备对话框服务;
    /// </summary>
    [Export(typeof(IDriveDialogService))]
    public class DriveDialogService : IDriveDialogService {
        [Import]
        Lazy<DriveItemsWindowViewModel> _vm;

        public (string driveType, object entity)? SelectDrive() {
            var window = new DrivesItemsWindow();
            window.DataContext = _vm.Value;
            window.ShowDialog();

            return _vm.Value.SelectedDriveTuple;
        }
    }
}
