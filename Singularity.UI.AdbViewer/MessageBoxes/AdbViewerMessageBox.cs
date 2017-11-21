using CDFC.Info.Adb;
using Singularity.UI.AdbViewer.ViewModels.AdbViewer;
using Singularity.UI.AdbViewer.Views.AdbViewer;
using System.Windows;

namespace Singularity.UI.AdbViewer.MessageBoxes {
    public class AdbViewerMessageBox {
        public static PhoneFullInfoContainer Show() {
            var vm = new AdbViewerViewModel();
            var window = new AdbPhoneViewer(vm) {
                Owner = Application.Current.MainWindow
            };
            window.ShowDialog();
            if(window.AquireResult == true) {
                return vm.FullPhoneInfoContainer;
            }
            return null;
        }
    }
}
