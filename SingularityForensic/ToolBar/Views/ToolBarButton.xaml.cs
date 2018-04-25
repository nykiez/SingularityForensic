using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

namespace SingularityForensic.ToolBar.Views {
    /// <summary>
    /// Interaction logic for ToolBarButton.xaml
    /// </summary>
    [Export(SingularityForensic.ToolBar.Constants.ToolBarButtonView, typeof(FrameworkElement))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ToolBarButton : UserControl {
        public ToolBarButton() {
            InitializeComponent();
        }
    }
}
