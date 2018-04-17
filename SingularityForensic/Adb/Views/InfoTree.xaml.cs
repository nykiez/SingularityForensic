using SingularityForensic.Adb.ViewModels;
using SingularityForensic.Contracts.TreeView;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SingularityForensic.Adb.Views {
    /// <summary>
    /// Interaction logic for InfoTree.xaml
    /// </summary>
    [Export(nameof(InfoTree))]
    public partial class InfoTree : UserControl {
        public InfoTree() {
            InitializeComponent();
        }
        [Import]
        private InfoTreeModel vm {
            set {
                this.DataContext = value;
                value.NotifyUnitExpanded += (d, unit) => {
                    var ti = CaseTreeList.GetContainerFromItemEx(unit);
                    if (ti != null) {
                        ti.IsExpanded = true;
                        ti.UpdateLayout();
                    }
                };
            }
        }

        private void CaseTreeList_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            var vm = this.DataContext as InfoTreeModel;
            if (e.ClickCount == 1 && vm != null) {
                var element = e.OriginalSource as FrameworkElement;
                if (element != null) {
                    if (element.DataContext is ITreeUnit unit) {
                        if (e.LeftButton == MouseButtonState.Pressed) {
                            vm.NotifySelectionUnitChanged(unit);
                        }
                        else {
                            vm.SelectedUnit = unit;
                        }
                        //if (unit is StorageTreeUnit) {
                        //    //右键展开显示;
                        //    if (e.RightButton == MouseButtonState.Pressed && (unit as StorageTreeUnit).File?.FileType == FileType.Directory) {
                        //        vm.ExploreRsvCommand.Execute();
                        //    }
                        //}

                    }
                }
            }
        }
    }
}
