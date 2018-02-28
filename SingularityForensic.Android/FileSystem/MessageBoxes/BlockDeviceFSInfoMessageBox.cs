using CDFC.Parse.Abstracts;
using SingularityForensic.Android.FileSystem.MessageBoxes.ViewModels;
using SingularityForensic.Android.FileSystem.Views;

namespace SingularityForensic.Android.FileSystem.MessageBoxes.MessageBoxes {
    public class BlockDeviceFSInfoMessageBox {
        public static void Show(BlockDeviceFile file) {
            var vm = new BlockDeviceFSInfoViewModel(file);
            var window = new BlockDeviceFSInfoWindow(vm);
            window.ShowDialog();
        }
    }
}
