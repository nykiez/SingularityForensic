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


        private void CaseTreeList_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            //if (e.ClickCount == 1 && DataContext is UnitTreeViewModel vm) {
            //    if (e.OriginalSource is DependencyObject dpo) {
            //        object dt = null;
            //        if (dpo is FrameworkElement element) {
            //            dt = element.DataContext;
            //        }
            //        else {
            //            dt = VisualHelper.GetVisualParent<FrameworkElement>(dpo)?.DataContext;
            //        }

            //        if (dt is ITreeUnit unit) {
            //            if (unit is ITreeUnit tUnit) {
            //                //通知右键点击;
            //                if (e.RightButton == MouseButtonState.Pressed) {
            //                    vm.NotifyRightClick(tUnit);
            //                }
            //            }

            //        }
            //    }
            //}
        }
    }

   
}
