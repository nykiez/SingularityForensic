using EventLogger;
using System;
using System.Windows;
using CDFCCultures.Managers;

namespace SingularityShell {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public App() {
            DispatcherUnhandledException += (sender, e) => {
                Logger.WriteCallerLine("主线程错误:" + e.Exception.Message);
                e.Handled = true;
            };
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => {
                Logger.WriteCallerLine("工作线程错误:" + ((Exception)e.ExceptionObject).Message);
                Logger.WriteCallerLine("工作线程错误:" + ((Exception)e.ExceptionObject).StackTrace);
                var ex = e.ExceptionObject as Exception;
                if (ex != null && ex.InnerException != null) {
                    Logger.WriteLine("工作线程错误:" + ex.InnerException.StackTrace);
                    Logger.WriteLine("工作线程错误: " + ex.InnerException.Message);
                }
                var nullex = e.ExceptionObject as NullReferenceException;
                if (nullex != null) {
                    Logger.WriteLine("Source:" + nullex.Source);
                    var enumrator = nullex.Data.GetEnumerator();
                    while (enumrator.MoveNext()) {
                        Logger.WriteLine("Object:" + enumrator.Current.ToString());
                    }
                }
            };
        }

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            
            this.Resources.MergedDictionaries.LoadLanguage("SingularityForensic");
            this.Resources.MergedDictionaries.LoadLanguage("CDFCMessageBoxes");
            try {
                new SingularityBootStrapper().Run();
            }
            catch {

            }
            
        }
    }
}
