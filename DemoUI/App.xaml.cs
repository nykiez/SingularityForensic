using System;
using System.Windows;

namespace DemoUI {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App2 : Application {
        public App2() {
            DispatcherUnhandledException += (sender, e) => {
                
            };
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => {
                
            };
        }

        protected override void OnStartup(StartupEventArgs e) {
            new DemoBootStrapper().Run();   
        }
    }
}
