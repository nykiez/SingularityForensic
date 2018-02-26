using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace SingularityForensic.Modules.MainPage.Views {
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    [Export(nameof(WelcomeView))]
    public partial class WelcomeView : UserControl {
        public WelcomeView() {
            InitializeComponent();
        }
    }
}
