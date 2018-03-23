using SingularityForensic.Contracts.App;
using System;
using System.Windows;

namespace SingularityShell {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public App() {
            
            DispatcherUnhandledException += (sender, e) => {
                LoggerService.Current?.WriteCallerLine("主线程错误:" + e.Exception.Message);
                e.Handled = true;
            };
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => {
                LoggerService.Current?.WriteCallerLine("工作线程错误:" + ((Exception)e.ExceptionObject).Message);
                LoggerService.Current?.WriteCallerLine("工作线程错误:" + ((Exception)e.ExceptionObject).StackTrace);
                var ex = e.ExceptionObject as Exception;
                if (ex != null && ex.InnerException != null) {
                    LoggerService.Current?.WriteLine("工作线程错误:" + ex.InnerException.StackTrace);
                    LoggerService.Current?.WriteLine("工作线程错误: " + ex.InnerException.Message);
                }
                var nullex = e.ExceptionObject as NullReferenceException;
                if (nullex != null) {
                    LoggerService.Current?.WriteLine("Source:" + nullex.Source);
                    var enumrator = nullex.Data.GetEnumerator();
                    while (enumrator.MoveNext()) {
                        LoggerService.Current?.WriteLine("Object:" + enumrator.Current.ToString());
                    }
                }
            };
        }

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            
            try {
                new SingularityBootStrapper().Run();
            }
            catch(Exception ex){
                LoggerService.Current?.WriteCallerLine(ex.Message);
            }

        }
    }
}
