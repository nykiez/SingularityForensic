using CDFCMessageBoxes.MessageBoxes;
using SingularityForensic.Contracts.Converters;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.FileExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;

namespace SingularityForensic.Controls.FileExplorer.Views {
    /// <summary>
    /// Interaction logic for PartitionsBrowser.xaml
    /// </summary>
    [
        Export(
            SingularityForensic.FileExplorer.Constants.PartitionsBrowserView,
            typeof(FrameworkElement)
        ),
        PartCreationPolicy(CreationPolicy.NonShared)
    ]
    public partial class PartitionsFolderBrowser : UserControl {
        public PartitionsFolderBrowser() {
            InitializeComponent();
            this.DataContextChanged += FolderBrowser_DataContextChanged;
        }
        private void FolderBrowser_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            if (vm != e.NewValue && e.NewValue != null) {
                vm = e.NewValue as FolderBrowserViewModel;
                if (vm != null) {
                    Action<bool> checkAct = check => {
                        var slRows = new List<IFileRow>();
                        try {
                            //foreach (var item in dg.SelectedItems) {
                            //    slRows.Add(item as IFileRow);
                            //}
                        }
                        catch (Exception ex) {
                            EventLogger.Logger.WriteLine($"{nameof(FolderBrowser)}->CheckSelected:{ex.Message}");
                            CDFCMessageBox.Show(ex.Message);
                        }
                        finally {
                            vm?.CheckRows(slRows, check);
                        }
                    };

                    vm.CheckSelectedRequired += (s, args) => {
                        checkAct(true);
                    };
                    vm.UnCheckSelectedRequired += (s, args) => {
                        checkAct(false);
                    };
                }
            }
        }
        
        //方便应对双击等动作，编写后台VM;
        private FolderBrowserViewModel vm;
        public FolderBrowserViewModel VM {
            get {
                if (vm == null && DataContext != null) {
                    vm = DataContext as FolderBrowserViewModel;
                }
                return vm;
            }
        }
        private void DataGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ClickCount == 2) {
                try {
                    var s = e.OriginalSource as FrameworkElement;
                    if (s.DataContext is IFileRow row) {
                        //VM?.EnterRow(row as IFileRow<IFile>);
                    }
                }
                catch {

                }
                e.Handled = true;
            }
        }

        private void RadGridViewEx_AutoGeneratingColumn(object sender, GridViewAutoGeneratingColumnEventArgs e) {
            if (e.ItemPropertyInfo.Name == SingularityForensic.FileExplorer.Constants.PartMetaDataName_Partition) {
                e.Cancel = true;
            }

            if (e.Column is GridViewDataColumn dataColumn
            && e.ItemPropertyInfo.PropertyType == typeof(long)) {
                if (dataColumn.DataMemberBinding != null) {
                    dataColumn.DataMemberBinding.Converter = ByteSizeToSizeConverter.StaticInstance;
                }
            }

        }
        ~PartitionsFolderBrowser() {

        }
    }

}
