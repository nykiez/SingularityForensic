using Singularity.Previewers.ViewModels;
using System.Windows.Controls;

namespace DemoUI {
    /// <summary>
    /// Interaction logic for TestSqlitePreviewer.xaml
    /// </summary>
    public partial class TestSqlitePreviewer : UserControl {
        public TestSqlitePreviewer() {
            InitializeComponent();
            this.DataContext = new SqlitePreviewerModel("D://SingularitySolution//SingularityShell//bin//Debug//Tmp/download_p2p.db");
        }
    }
}
