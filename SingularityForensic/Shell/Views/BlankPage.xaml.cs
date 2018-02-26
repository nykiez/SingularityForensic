using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace SingularityForensic.Shell.Views {
    /// <summary>
    /// Interaction logic for BlankPage.xaml
    /// </summary>
    [Export(nameof(BlankPage))]
    public partial class BlankPage : UserControl
    {
        public BlankPage()
        {
            InitializeComponent();
        }
    }
}
