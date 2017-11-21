using System.Windows.Controls;

namespace Singularity.UI.AdbViewer.Views.AdbGrid {
    /// <summary>
    /// Interaction logic for AdbDataGrid.xaml
    /// </summary>
    public partial class AdbDataGrid : UserControl {
        public AdbDataGrid() {
            InitializeComponent();
        }

        private void FilterableDataGrid_LoadingRow(object sender, DataGridRowEventArgs e) {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
    }
}
