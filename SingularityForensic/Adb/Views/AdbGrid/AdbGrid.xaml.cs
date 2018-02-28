using System.Windows.Controls;

namespace SingularityForensic.Adb.Views.AdbGrid {
    /// <summary>
    /// Interaction logic for DefaultAdbGrid.xaml
    /// </summary>
    //[Export(nameof(AdbGrid))]
    public partial class AdbGrid : UserControl {
        public AdbGrid() {
            InitializeComponent();   
        }
        
        //[Import]
        //AdbGridViewModel VM {
        //    set => this.DataContext = value;
        //}
    }
}
