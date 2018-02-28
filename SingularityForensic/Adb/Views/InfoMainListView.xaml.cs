using System.Windows.Controls;

namespace SingularityForensic.Adb.Views {
    /// <summary>
    /// Interaction logic for InfoMainListView.xaml
    /// </summary>
    public partial class InfoMainListView : UserControl
    {
        public InfoMainListView()
        {
            InitializeComponent();
        }

        private void FilterableDataGrid_LoadingRow(object sender, DataGridRowEventArgs e) {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
    }
}
