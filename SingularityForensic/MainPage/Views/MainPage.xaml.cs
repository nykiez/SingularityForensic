using MahApps.Metro.Controls;
using SingularityForensic.Contracts.Common;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using System.Linq;

namespace SingularityForensic.MainPage.Views {
    /// <summary>
    /// Interaction logic for FileSystem.xaml
    /// </summary>
    [Export]
    public partial class MainPage : UserControl{
        public MainPage() {
            InitializeComponent();
        }


#if DEBUG
        private class SEntity {
            ~SEntity() {

            }
        }
#endif
        private void MainPage_Loaded1(object sender, RoutedEventArgs e) {
            var window = this.TryFindParent<Window>();
            if(window == null) {
                return;
            }
            
            window.Closed += Window_Closed;
        }

        private void CheckFolderExists() {
            var folder = $"{ Environment.CurrentDirectory}\\{ Contracts.Common.Constants.ResourceFolder}";
            try {
                if (Directory.Exists(folder)) {
                    return;
                }
                Directory.CreateDirectory(folder);
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
            }
            
        }

        private void Window_Closed(object sender, EventArgs e) {
            var fsName = $"{Environment.CurrentDirectory}\\{Contracts.Common.Constants.ResourceFolder}\\{Constants.MainPageLayoutFile}";
            CheckFolderExists();

            Stream _layoutStream = null;
            
            try {
                _layoutStream = File.Open(fsName, FileMode.OpenOrCreate);
                docking.SaveLayout(_layoutStream);
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
            }

            _layoutStream?.Dispose();
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e) {
            //var fsName = $"{Environment.CurrentDirectory}\\{Contracts.Common.Constants.ResourceFolder}\\{Constants.MainPageLayoutFile}";
            //CheckFolderExists();
            //if (!File.Exists(fsName)) {
            //    return;
            //}

            //Stream _layoutStream = null;

            //try {
            //    _layoutStream = File.OpenRead(fsName);
            //    //docking.LoadLayout(_layoutStream);
            //    //mainContent.PaneHeaderVisibility = Visibility.Collapsed;
            //}
            //catch (Exception ex) {

            //}
            //_layoutStream?.Dispose();


        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            var dd = this.docking;
#if DEBUG
            var container = (dd.Items[2] as RadSplitContainer);
            var group = container.Items[0] as RadPaneGroup;
            
            //var pane = group.Items[0] as RadPane;
            //container.Visibility = Visibility.Visible;
            //group.Visibility = Visibility.Visible;
            //pane.Visibility = Visibility.Visible;
#endif
        }
    }
}
