using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

namespace SingularityForensic.Hex.Views {
    /// <summary>
    /// Interaction logic for ToolTipDataItem.xaml
    /// </summary>
    [
        Export(
            Contracts.Hex.Constants.ToolTipDataItemView,
            typeof(FrameworkElement)
        ),
        PartCreationPolicy(
            CreationPolicy.NonShared
        )
    ]
    public partial class ToolTipDataItem : UserControl {
        public ToolTipDataItem() {
            InitializeComponent();
        }
    }
}
