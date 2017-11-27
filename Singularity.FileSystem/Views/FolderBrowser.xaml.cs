using CDFC.Parse.Contracts;
using CDFCMessageBoxes.MessageBoxes;
using CDFCUIContracts.Helpers;
using Singularity.UI.FileSystem.Models;
using Singularity.UI.FileSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Singularity.UI.FileSystem.Views {
    /// <summary>
    /// Interaction logic for FolderBrowser.xaml
    /// </summary>
    [Export(nameof(FolderBrowser))]
    public partial class FolderBrowser : UserControl {
        public FolderBrowser() {
            InitializeComponent();
            this.DataContextChanged += FolderBrowser_DataContextChanged;
            
        }
        private void CheckRows(bool isChecked) {
            var slRows = new List<IFileRow>();
            try {
                foreach (var item in dg.SelectedItems) {
                    slRows.Add(item as IFileRow);
                }
            }
            catch (Exception ex) {
                EventLogger.Logger.WriteLine($"{nameof(FolderBrowser)}->CheckSelected:{ex.Message}");
                CDFCMessageBox.Show(ex.Message);
            }
            finally {
                vm?.CheckRows(slRows, isChecked);
            }
        }

        private void FolderBrowser_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            if(vm != e.NewValue && e.NewValue != null) {
                vm = e.NewValue as FolderBrowserViewModel;
                if(vm != null) {
                    vm.CheckSelectedRequired += (s, args) => {
                        CheckRows(true);
                    };
                    vm.UnCheckSelectedRequired += (s, args) => {
                        CheckRows(false);
                    };
                }
            }
        }

        //~FolderBrowser() {

        //}
        //方便应对双击等动作，编写后台VM;
        private FolderBrowserViewModel vm;
        public FolderBrowserViewModel VM {
            get {
                if(vm == null && DataContext != null) {
                    vm = DataContext as FolderBrowserViewModel;
                }
                return vm;
            }
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
                        VM?.EnterRow(row as IFileRow<IFile>);
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
    }
}
