using SingularityForensic.Drive.ViewModels;
using SingularityForensic.Drive.Views;
using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Data;

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
        [ImportingConstructor]
        internal DriveDialogService(DriveItemsWindowViewModel vm) {
            this._vm = vm;
        }
        
        private DriveItemsWindowViewModel _vm;

        public (string driveType, object entity)? SelectDrive() {
            var window = new DrivesItemsWindow {
                DataContext = _vm
            };
            window.ShowInTaskbar = false;
           if ((Contracts.Shell.ShellService.Current.Shell as Window)?.IsLoaded ?? false) {
                window.Owner = Contracts.Shell.ShellService.Current.Shell as Window;
            }
            
            window.ShowDialog();

            window.DataContext = null;

#if DEBUG
            Contracts.App.ThreadInvoker.BackInvoke(() => {
                System.Threading.Thread.Sleep(1000);
                for (int i = 0; i < 2; i++) {
                    System.GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            });
#endif
            return _vm.SelectedDriveTuple;
        }
    }
}
