using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace SingularityForensic.ToolBar.Views {
    /// <summary>
    /// Interaction logic for ToolBar.xaml
    /// </summary>
    [Export]
    public partial class ToolBar : UserControl
    {
        public ToolBar()
        {
            InitializeComponent();
        }
    }
}
