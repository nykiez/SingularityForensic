using System.Configuration;

namespace SingularityUpdater.Helpers {
    public static class CultureHelper {
        //是否被更改;
        private static bool modified = true;
        private static string defaultLanguage = "zh_CN";
        private const string Cons_LanguageType = "LanguageType";
        private static string language;
        public static string Language {
            get {
                //若被修改或者是第一次启动;
                if (modified) {
                    language = ConfigurationManager.AppSettings[Cons_LanguageType];
                    if (language == null) {
                        Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        cfa.AppSettings.Settings.Add(Cons_LanguageType, defaultLanguage);
                        cfa.Save();
                        return defaultLanguage;
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

        public static string TryFindResourceString(string keyName) {
            var resource = System.Windows.Application.Current.TryFindResource(keyName);
            if (resource != null) {
                return resource.ToString();
            }
            else {
                return string.Empty;
            }
        }
    }
}
