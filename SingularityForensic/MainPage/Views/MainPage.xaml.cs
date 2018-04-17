using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace SingularityForensic.MainPage.Views {
    /// <summary>
    /// Interaction logic for FileSystem.xaml
    /// </summary>
    [Export]
    public partial class MainPage : UserControl{
        public MainPage() {
            InitializeComponent();
        }
        
    }
}
