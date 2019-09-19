using SingularityForensic.Contracts.Previewers;
using SingularityForensic.NTFS;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace DemoUI.NTFS {
    /// <summary>
    /// Interaction logic for TestUsnJrnlPreviewer.xaml
    /// </summary>
    public partial class TestUsnJrnlPreviewer : UserControl {
        public TestUsnJrnlPreviewer() {
            InitializeComponent();
        }

        private IPreviewer _previewer;
     

        private void Button_Click(object sender, RoutedEventArgs e) {
            //this._previewer = new UsnJrnlPreviewer(File.OpenRead("E://anli/UsnJrnl(1)"));
            bd.Child = _previewer.UIObject as UIElement;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            bd.Child = null;
            _previewer?.Dispose();
            _previewer = null;
#if DEBUG
            for (int i = 0; i < 2; i++) {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
            }
#endif
        }
    }
}
