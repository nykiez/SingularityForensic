using SingularityForensic.Windows;

namespace SingularityForensic.Modules.About.MessageBoxes {
    public class AboutMessageBox {
        public static void Show() {
            var window = new AboutWindow();
            window.ShowDialog();
        }
    }
}
