using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
