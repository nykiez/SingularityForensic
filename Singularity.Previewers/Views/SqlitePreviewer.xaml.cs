﻿using Singularity.Previewers.Models;
using Singularity.Previewers.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Singularity.Previewers.Views {
    /// <summary>
    /// Interaction logic for SqlitePreviewer.xaml
    /// </summary>
    public partial class SqlitePreviewer : UserControl {
        public SqlitePreviewer() {
            InitializeComponent();
            
        }
        private SqlitePreviewerModel vm;
        public SqlitePreviewerModel VM {
            get {
                if (vm == null) {
                    vm = this.DataContext as SqlitePreviewerModel;
                }
                return vm;
            }
        }

        private void TreeViewEx_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            if(e.ClickCount == 1 && VM != null) {
                var element = e.OriginalSource as FrameworkElement;
                if(element != null) {
                    if (element.DataContext is DBUnit unit) {
                        VM.NotifySelectedUnitChanged(unit);
                    }
                }
            }
        }

        
    }
}
