using CDFCControls.Controls;

namespace SingularityForensic.FileExplorer.Windows {
    /// <summary>
    /// 列出簇窗体;
    /// </summary>
    public partial class ListBlocksWindow  {
        public ListBlocksWindow() {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, System.Windows.RoutedEventArgs e) {
            this.Close();
        }
    }
}
