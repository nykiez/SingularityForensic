using CDFCControls.Controls;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace SingularityForensic.Windows {
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : CorneredWindow {
        public AboutWindow() {
            InitializeComponent();
        }

        private void StackPanel_Click(object sender, RoutedEventArgs e) {
            var link = e.OriginalSource as Hyperlink;
            if(link != null) {
                Process.Start(link.NavigateUri.ToString());
            }
        }

        private void CorneredWindow_KeyDown(object sender, KeyEventArgs e) {
            this.Close();
        }
    }
}
