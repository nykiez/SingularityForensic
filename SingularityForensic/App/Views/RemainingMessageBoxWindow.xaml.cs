using CDFCControls.Controls;
using System;

namespace SingularityForensic.App.Views {
    /// <summary>
    /// Interaction logic for RemainingMessageBox.xaml
    /// </summary>
    public partial class RemainingMessageBoxWindow : CorneredWindow {
        public RemainingMessageBoxWindow() {
            InitializeComponent();
        }
        public void AddWord(string word) {
            this.mainTxb.Text += word + Environment.NewLine;
            mainTxb.ScrollToEnd();
        }
    }
}
