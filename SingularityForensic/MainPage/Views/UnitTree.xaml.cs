using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel.Composition;
using CDFCUIContracts.Helpers;
using SingularityForensic.Contracts.TreeView;
using SingularityForensic.MainPage.ViewModels;
using Telerik.Windows.Controls;

namespace SingularityForensic.MainPage.Views {
    /// <summary>
    /// Interaction logic for CaseManager.xaml
    /// </summary>
    [Export(Contracts.MainPage.Constants.UnitTreeView)]
    public partial class UnitTree : UserControl {
        public UnitTree() {
            InitializeComponent();
            EventManager.RegisterClassHandler(typeof(RadTreeViewItem),
        Mouse.MouseDownEvent, new MouseButtonEventHandler(OnTreeViewItemMouseDown), false);
        }

        public static void OnTreeViewItemMouseDown(object sender, MouseButtonEventArgs e) {
            var treeViewitem = sender as RadTreeViewItem;
            if (e.RightButton == MouseButtonState.Pressed) {
                treeViewitem.IsSelected = true;
                e.Handled = true;
            }
        }


       
    }

   
}
