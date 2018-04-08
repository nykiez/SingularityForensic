using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace SingularityForensic.MainPage.Views {
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    [Export(Contracts.MainPage.Constants.WelcomeView)]
    public partial class Welcome : UserControl {
        public Welcome() {
            InitializeComponent();
        }
    }
}
