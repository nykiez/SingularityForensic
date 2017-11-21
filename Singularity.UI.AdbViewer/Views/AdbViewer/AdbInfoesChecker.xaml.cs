using System.Windows.Controls;

namespace Singularity.UI.AdbViewer.Views.AdbViewer {
    /// <summary>
    /// Interaction logic for AdbInfoesChecker.xaml
    /// </summary>
    public partial class AdbInfoesChecker : UserControl {
        public AdbInfoesChecker() {
            InitializeComponent();

            txbDashBoard.TextChanged += delegate {
                txbDashBoard.ScrollToEnd();
            };
        }
    }
}
