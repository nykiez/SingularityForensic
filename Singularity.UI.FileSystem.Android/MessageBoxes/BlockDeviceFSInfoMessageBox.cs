using CDFC.Parse.Abstracts;
using Singularity.UI.FileSystem.Android.ViewModels;
using Singularity.Android.Views;

namespace Singularity.UI.FileSystem.Android.MessageBoxes {
    public class BlockDeviceFSInfoMessageBox {
        public static void Show(BlockDeviceFile file) {
            var vm = new BlockDeviceFSInfoViewModel(file);
            var window = new BlockDeviceFSInfoWindow(vm);
            window.ShowDialog();
        }
    }
}
