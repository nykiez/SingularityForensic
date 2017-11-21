using CDFCMessageBoxes.MessageBoxes;
using Singularity.UI.FileSystem.Models;
using Singularity.UI.FileSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Singularity.UI.FileSystem.Views {
    /// <summary>
    /// Interaction logic for PartitionsBrowser.xaml
    /// </summary>
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
                        List<FileRow> slRows = new List<FileRow>();
                        try {
                            foreach (var item in dg.SelectedItems) {
                                slRows.Add(item as FileRow);
                            }
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
                    var row = s.DataContext as FileRow;
                    if (row != null) {
                        VM?.EnterRow(row);
                    }
                }
                catch {

                }
                e.Handled = true;
            }
        }

        private void dg_LoadingRow(object sender, DataGridRowEventArgs e) {
            e.Row.Header = e.Row.GetIndex();
        }
    }
}
