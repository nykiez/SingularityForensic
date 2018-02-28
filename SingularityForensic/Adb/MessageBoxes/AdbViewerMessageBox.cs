using CDFC.Info.Adb;
using SingularityForensic.Adb.ViewModels.AdbViewer;
using SingularityForensic.Adb.Views.AdbViewer;
using System.Windows;

namespace SingularityForensic.Adb.MessageBoxes {
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
