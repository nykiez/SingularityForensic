using CDFCControls.Controls;
using System.Windows;

namespace SingularityForensic.Controls.Windows {
    /// <summary>
    /// Interaction logic for InputStringWindow.xaml
    /// </summary>
    public partial class InputStringWindow : CorneredWindow {

        public InputStringWindow(string title = "",string desc = "") {
            InitializeComponent();
            this.Title = title;
            this.txbDesc.Text = desc;
        }

        public string Val {
            get => txbVal.Text;
            set => txbVal.Text = value;
        }

        public bool? InputResult;
        private void Button_Click(object sender, RoutedEventArgs e) {
            
            InputResult = true;
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            txbVal.Text = null;
            this.Close();
        }
    }
}
