using SingularityUpdater.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Windows;

namespace SingularityUpdater {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public App() {
            DispatcherUnhandledException += (sender, e) => {
                UpdateLogger.WriteLine("主线程错误:" + e.Exception.Message);
            };
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => {
                UpdateLogger.WriteLine("工作线程错误:" + ((Exception)e.ExceptionObject).Message);
                UpdateLogger.WriteLine("工作线程错误:" + ((Exception)e.ExceptionObject).StackTrace);
                var ex = e.ExceptionObject as Exception;
                if (ex != null && ex.InnerException != null) {
                    UpdateLogger.WriteLine("工作线程错误:" + ex.InnerException.StackTrace);
                    UpdateLogger.WriteLine("工作线程错误: " + ex.InnerException.Message);
                }
                var nullex = e.ExceptionObject as NullReferenceException;
                if (nullex != null) {
                    UpdateLogger.WriteLine("Source:" + nullex.Source);

                    var enumrator = nullex.Data.GetEnumerator();
                    while (enumrator.MoveNext()) {
                        UpdateLogger.WriteLine("Object:" + enumrator.Current.ToString());
                    }
                }
            };
        }
        protected override void OnStartup(StartupEventArgs e) {
            //检查启动参数以指定版本;
            if (e.Args.Count() != 0) {
                VersionHelper.VersionBranch = e.Args[0];
            }
            else {
#if DEBUG
                VersionHelper.VersionBranch = "DaHua";
#else
                VersionHelper.VersionBranch = "DaHua";
#endif
            }
            LoadLanguage(this.Resources.MergedDictionaries, "SingularityUpdater");
        }
        public static void LoadLanguage(Collection<ResourceDictionary> dictArr, string moduleName) {
            ResourceDictionary localLanResr = new ResourceDictionary();
            try {
                var curlan = Language;
                var lanPath = AppDomain.CurrentDomain.BaseDirectory + "Languages/";
                if (curlan != DefaultLanguage) {
                    ResourceDictionary curLanResr = null;
                    foreach (var item in dictArr) {
                        if (item.Source.ToString().EndsWith($"{moduleName}.xaml")) {
                            curLanResr = item;
                        }
                    }
                    localLanResr.Source = new Uri($"{lanPath}{curlan}/{moduleName}.xaml");
                    if (curLanResr != null) {
                        dictArr.Remove(curLanResr);
                    }
                    dictArr.Add(localLanResr);
                }
            }
            catch {

            }
        }

        //是否被更改;
        private static bool modified = true;
        public const string DefaultLanguage = "zh_CN";
        private const string Cons_LanguageType = "LanguageType";
        private static string language;
        public static string Language {
            get {
                //若被修改或者是第一次启动;
                if (modified) {
                    language = ConfigurationManager.AppSettings[Cons_LanguageType];
                    if (language == null) {
                        Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        cfa.AppSettings.Settings.Add(Cons_LanguageType, DefaultLanguage);
                        cfa.Save();
                        return DefaultLanguage;
                    }
                    modified = false;
                }
                return language;
            }
            set {
                language = value;
                try {
                    Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    cfa.AppSettings.Settings[Cons_LanguageType].Value = value;
                    cfa.Save();
                    modified = true;
                }
                catch {

                }

            }
        }

    }

}
