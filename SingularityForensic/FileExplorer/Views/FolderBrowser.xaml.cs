using CDFCUIContracts.Helpers;
using SingularityForensic.Controls;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace SingularityForensic.FileExplorer.Views {
    /// <summary>
    /// Interaction logic for FolderBrowser.xaml
    /// </summary>
    [Export(Constants.FolderBrowserView,typeof(FrameworkElement))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class FolderBrowser : UserControl {
        public FolderBrowser() {
            SingularityForensic.Contracts.Common.LoggerService.WriteCallerLine("Folder initializing");
            InitializeComponent();
            this.DataContextChanged += FolderBrowser_DataContextChanged;
            //this.Unloaded += FolderBrowser_Unloaded;
        }

        //private void FolderBrowser_Unloaded(object sender, RoutedEventArgs e) {
        //    if(this.DataContext is IInteractionGridViewModel vm) {
        //        vm.GetSelectedRows = null;
        //    }
        //}

        private void FolderBrowser_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            if(e.NewValue is IInteractionGridViewModel newVM) {
                newVM.GetSelectedRows = () => gridView.SelectedItems;
            }

            if (e.OldValue is IInteractionGridViewModel oldVM){
                oldVM.GetSelectedRows = null;
            }
        }


        
        private void RadGridView_AutoGeneratingColumn(object sender,GridViewAutoGeneratingColumnEventArgs e) {
            var args = new Contracts.Controls.GridViewAutoGeneratingColumnEventArgs(e.ItemPropertyInfo);
            (this.DataContext as IInteractionGridViewModel)?.NotifyAutoGeneratingColumns(args);

            e.Cancel = args.Cancel;
            e.Column.CellTemplate = args.CellTemplate;
            e.Column.ShowDistinctFilters = args.ShowDistinctFilters;
            
            if(e.Column is GridViewDataColumn dataColumn
                && dataColumn.DataMemberBinding != null) {
                dataColumn.DataMemberBinding.Converter = args.Converter;
            }
        }

        private void RadGridViewEx_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if(!(e.OriginalSource is FrameworkElement elem)) {
                return;
            }
            
            var cell = VisualHelper.GetVisualParent<GridViewCell>(elem);
            if(cell == null) {
                return;
            }
            
            (this.DataContext as IInteractionGridViewModel)?.NotifyDoubleClickOnRow(cell.DataContext);
        }

#if DEBUG
        ~FolderBrowser() {

        }
#endif

        private void RadGridViewEx_SelectionChanged(object sender, SelectionChangeEventArgs e) {

        }

    }
}
