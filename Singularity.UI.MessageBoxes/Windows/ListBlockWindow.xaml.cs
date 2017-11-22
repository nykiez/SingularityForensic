using CDFCControls.Controls;

namespace Singularity.UI.MessageBoxes.Windows {
    /// <summary>
    /// 列出簇窗体;
    /// </summary>
    public partial class ListBlocksWindow : CorneredWindow {
        public ListBlocksWindow() {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, System.Windows.RoutedEventArgs e) {
            this.Close();
        }
    }
}
