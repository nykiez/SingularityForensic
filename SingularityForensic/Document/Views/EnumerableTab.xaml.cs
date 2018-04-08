using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

namespace SingularityForensic.Document.Views {
    /// <summary>
    /// Interaction logic for EnumerableTab.xaml
    /// </summary>
    [Export(Constants.EnumerableTabView,typeof(FrameworkElement)) , PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class EnumerableTab : UserControl {
        public EnumerableTab() {
            InitializeComponent();
        }
    }
}
