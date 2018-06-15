using SingularityForensic.Contracts.Common;
using System;
using System.Windows;

namespace DemoUI {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App2 : Application {
        public App2() {
            DispatcherUnhandledException += (sender, e) => {
                LoggerService.WriteException(e.Exception);
            };
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => {
                
            };
            
            InitializeComponent();
        }

        protected override void OnStartup(StartupEventArgs e) {
            new DemoBootStrapper().Run();   
        }
    }
}
