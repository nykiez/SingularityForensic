using CDFCUIContracts.Helpers;
using SingularityForensic.Contracts.Controls;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace SingularityForensic.Controls.FileExplorer.Views {
    /// <summary>
    /// Interaction logic for FolderBrowser.xaml
    /// </summary>
    [Export(SingularityForensic.FileExplorer.Constants.FolderBrowserView,typeof(FrameworkElement))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class FolderBrowser : UserControl {
        public FolderBrowser() {
            InitializeComponent();
        }
       
        private void RadGridView_AutoGeneratingColumn(object sender,Telerik.Windows.Controls.GridViewAutoGeneratingColumnEventArgs e) {
            var args = new Contracts.Controls.GridViewAutoGeneratingColumnEventArgs(e.ItemPropertyInfo);
            (this.DataContext as IInteractionGridViewModel)?.NotifyAutoGeneratingColumns(args);
            
            
            e.Column.CellTemplate = args.CellTemplate;
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
        
    }
}
