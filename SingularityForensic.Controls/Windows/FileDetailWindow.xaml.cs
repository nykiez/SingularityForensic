using CDFCControls.Controls;
using System.Windows;

namespace SingularityForensic.Controls.Windows {
    /// <summary>
    /// Interaction logic for FileDetailWindow.xaml
    /// </summary>
    public partial class FileDetailWindow : CorneredWindow {
        public FileDetailWindow() {
            InitializeComponent();
        }

        public void LoadString(string txbString) {
            txbMain.Text = txbString;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
