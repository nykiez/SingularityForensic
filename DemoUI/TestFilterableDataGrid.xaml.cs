using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace DemoUI {
    /// <summary>
    /// Interaction logic for TestFilterableDataGrid.xaml
    /// </summary>
    public partial class TestFilterableDataGrid : UserControl {
        public TestFilterableDataGrid() {
            var items = new ObservableCollection<DGModel>();
            for (int i = 0; i < 100; i++) {
                items.Add(new DGModel());
            }
            this.DataContext = new { Items = items, Type = typeof(DGModel) };
            InitializeComponent();
        }
    }

    public class DGModel {
        public string Name { get; set; } = "3131";
        public bool Sex { get; set; } = false;
    }
}
