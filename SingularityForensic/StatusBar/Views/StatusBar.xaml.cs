using SingularityForensic.Contracts.StatusBar;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace SingularityForensic.StatusBar.Views {
    /// <summary>
    /// Interaction logic for StatusBarModule.xaml
    /// </summary>
    [Export(typeof(IStatusBar))]
    public partial class StatusBar : UserControl, IStatusBar {
        public StatusBar()
        {
            InitializeComponent();
        }

        public Grid Grid => mainGrid;
    }
}
