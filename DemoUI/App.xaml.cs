using System;
using System.Windows;

namespace DemoUI {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public App() {
            DispatcherUnhandledException += (sender, e) => {
                
            };
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => {
                
            };
        }
    }
}
