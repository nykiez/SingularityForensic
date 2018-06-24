using MahApps.Metro.Controls;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace SingularityForensic.MainPage.Views {
    /// <summary>
    /// Interaction logic for FileSystem.xaml
    /// </summary>
    [Export]
    public partial class MainPage : UserControl{
        public MainPage() {
            InitializeComponent();
            //this.Loaded += MainPage_Loaded;
            //this.Loaded += MainPage_Loaded1;
        }

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
            //    mainContent.PaneHeaderVisibility = Visibility.Collapsed;
            //}
            //catch (Exception ex) {

            //}
            //_layoutStream?.Dispose();

            
        }


    }
}
