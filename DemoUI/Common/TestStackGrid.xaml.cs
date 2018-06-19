using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Controls;
using System.Windows;
using System.Windows.Controls;

namespace DemoUI.Common {
    /// <summary>
    /// Interaction logic for TestCommon.xaml
    /// </summary>
    public partial class TestStackGrid : UserControl {
        public TestStackGrid() {
            InitializeComponent();
            var stackGrid = StackGridFactory.CreateNew<IUIObjectProvider>();
            grid.Children.Add(stackGrid.UIObject as UIElement);
        }

        private void AddStarButton_Click(object sender, RoutedEventArgs e) {

        }
    }
}
