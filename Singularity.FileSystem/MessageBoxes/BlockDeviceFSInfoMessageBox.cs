using CDFC.Parse.Abstracts;
using Singularity.UI.FileSystem.ViewModels;
using Singularity.UI.FileSystem.Views;

namespace Singularity.UI.FileSystem.MessageBoxes {
    public class BlockDeviceFSInfoMessageBox {
        public static void Show(BlockDeviceFile file) {
            var vm = new BlockDeviceFSInfoViewModel(file);
            var window = new BlockDeviceFSInfoWindow(vm);
            window.ShowDialog();
        }
    }
}
