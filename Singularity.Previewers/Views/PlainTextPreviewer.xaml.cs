using System.Windows.Controls;

namespace Singularity.Previewers.Views {
    /// <summary>
    /// Interaction logic for PlainTextPreviewer.xaml
    /// </summary>
    public partial class PlainTextPreviewer : UserControl {
        public PlainTextPreviewer() {
            InitializeComponent();
        }

        public void LoadString(string txt) {
            txtMain.Text = txt;
        }

        public void Clear() {
            txtMain.Text = string.Empty;
        }
    }
}
