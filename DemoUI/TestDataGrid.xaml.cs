using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace DemoUI {
    /// <summary>
    /// Interaction logic for TestDataGrid.xaml
    /// </summary>
    public partial class TestDataGrid : UserControl {
        public TestDataGrid() {
            InitializeComponent();
            var vm = new ObservableCollection<Model>();
            for (int i = 0; i < 128; i++) {
                vm.Add(new Model());
            }
            this.DataContext = vm;
            
        }
    }
    class Model {
        public string Sec { get; set; } = "512";
    }
}
