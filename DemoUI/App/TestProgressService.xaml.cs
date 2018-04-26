using SingularityForensic.Contracts.App;
using System.Threading;
using System.Windows.Controls;

namespace DemoUI.App {
    /// <summary>
    /// Interaction logic for TestProgressWindow.xaml
    /// </summary>
    public partial class TestProgressService : UserControl {
        public TestProgressService() {
            InitializeComponent();
            this.Loaded += delegate {
                var loadingDialog = DialogService.Current.CreateLoadingDialog();
                
                
                loadingDialog.DoWork += (sender, e) => {
                    for (int i = 0; i < 100; i++) {
                        loadingDialog.ReportProgress(i);
                        Thread.Sleep(100);
                    }
                };
                loadingDialog.Canceld += delegate {

                };
                loadingDialog.RunWorkerCompleted += delegate {

                };

                loadingDialog.ShowDialog();
            };
        }
    }
}
