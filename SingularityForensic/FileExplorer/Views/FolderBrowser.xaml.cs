using CDFCUIContracts.Helpers;
using SingularityForensic.Contracts.Converters;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Controls.ViewModels;
using SingularityForensic.FileExplorer;
using SingularityForensic.FileExplorer.ViewModels;
using System;
using System.Collections.Generic;
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
       
        private void DataGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            if(e.ClickCount == 2) {
                try {
                    IFileRow row = null;
                   
                    var s = e.OriginalSource as FrameworkElement;
                    if(s == null) {
                        var parent = VisualHelper.GetVisualParent<FrameworkElement>(e.OriginalSource as DependencyObject);
                        row = parent.DataContext as IFileRow;
                    }
                    else {
                        row = s.DataContext as IFileRow;
                    }
                    if(row != null) {
                        //VM?.EnterRow(row as IFileRow<FileBase>);
                    }
                }
                catch {

                }
                e.Handled = true;
            }
        }

        private void dg_LoadingRow(object sender, DataGridRowEventArgs e) {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        
        private void RadGridView_AutoGeneratingColumn(object sender, GridViewAutoGeneratingColumnEventArgs e) {
            if (e.ItemPropertyInfo.Name == SingularityForensic.FileExplorer.Constants.FileMetaDataName_File) {
                e.Cancel = true;
            }
            
            if (e.Column is GridViewDataColumn dataColumn
            && e.ItemPropertyInfo.PropertyType == typeof(long)) {
                if (dataColumn.DataMemberBinding != null) {
                    dataColumn.DataMemberBinding.Converter = ByteSizeToSizeConverter.StaticInstance;
                }
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

            if(DataContext is IInteractionDataGridViewModel dt) {

                dt.NotifyDoubleClickOnRow(cell.DataContext);
            }
            
        }

        

        
    }
}
