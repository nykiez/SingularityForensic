using System.Windows.Controls;

namespace Singularity.Previewers.Views {
    /// <summary>
    /// Interaction logic for VideoPreviewer.xaml
    /// </summary>
    public partial class VideoPreviewer : UserControl {
        public VideoPreviewer() {
            InitializeComponent();
        }

        public object ScreenPanelContent {
            get => ScreenPanel.Content;
            set => ScreenPanel.Content = value;
        }
    }
}
