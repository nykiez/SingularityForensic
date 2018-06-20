using Prism.Mvvm;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls.Data.PropertyGrid;

namespace DemoUI.Controls {
    /// <summary>
    /// Interaction logic for TestPropertyGrid.xaml
    /// </summary>
    public partial class TestPropertyGrid : UserControl {
        public TestPropertyGrid() {
            InitializeComponent();
            this.DataContext = new VM {
                Item = FileRowFactory.Current.CreateFileRow(FileFactory.CreateRegularFile(string.Empty))
            };
        }
    }

    public class VM:BindableBase {
        public object Item { get; set; }

        private PropertyDefinition _selectedProperty;
        public PropertyDefinition SelectedProperty {
            get => _selectedProperty;
            set => SetProperty(ref _selectedProperty, value);
        }

    }
    public class Entity {
        //public string Name { get; set; } = "313";
    }
}
