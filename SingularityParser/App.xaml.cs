using CDFCCultures.Managers;
using EventLogger;
using SingularityForensic.Contracts.Common;
using System;
using System.Windows;

namespace SingularityParser {
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
                if (e.ExceptionObject is Exception ex && ex.InnerException != null) {
                    Logger.WriteLine("工作线程错误:" + ex.InnerException.StackTrace);
                    Logger.WriteLine("工作线程错误: " + ex.InnerException.Message);
                }
                if (e.ExceptionObject is NullReferenceException nullex) {
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
