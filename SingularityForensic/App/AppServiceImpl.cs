using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using System;
using System.ComponentModel.Composition;
using System.Configuration;
using System.IO;
using System.Text;

namespace SingularityForensic.App {
    [Export(typeof(IAppService))]
    class AppServiceImpl : IAppService {
        public string AppName => "SingularityForensic";

        private string _appDataFolder;
        public string AppDataFolder {
            get {
                if(_appDataFolder == null) {
                    _appDataFolder = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\{AppName}";
                }

                CheckDirectoryExists(_appDataFolder);
                return _appDataFolder;
            }
        }
        
        private string _appTempFolder;
        public string AppTempFolder {
            get {
                if(_appTempFolder == null) {
                    _appTempFolder = $"{Path.GetTempPath()}\\{AppName}";
                }

                CheckDirectoryExists(_appTempFolder);
                return _appTempFolder;
            }
        }

        private string _appResourceFolder;
        public string AppResourceFolder {
            get {
                if(_appResourceFolder == null) {
                    _appResourceFolder = $"{Environment.CurrentDirectory}\\Resources";
                }

                CheckDirectoryExists(_appResourceFolder);
                return _appResourceFolder;
            }
        }

        private Encoding _appEncoding;
        public Encoding AppEncoding => _appEncoding ?? (_appEncoding = Encoding.GetEncoding("GB2312"));

        /// <summary>
        /// 检查指定目录是否存在,若无,则创建;
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        private bool CheckDirectoryExists(string folderPath) {
            if (!Directory.Exists(folderPath)) {
                try {
                    Directory.CreateDirectory(folderPath);
                }
                catch (Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            }

            return Directory.Exists(folderPath);
        }

        public string GetSettingValue(string keyName) {
            var cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if(cfa == null) {
                LoggerService.WriteCallerLine($"{nameof(cfa)} is null.");
                return null;
            }
            try {
                return cfa.AppSettings.Settings[keyName].Value;
            }
            catch (Exception ex) {
                LoggerService.WriteException(ex);
                return null;
            }
        }

        public void SetSettingValue(string keyName,string value) {
            var cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (cfa == null) {
                LoggerService.WriteCallerLine($"{nameof(cfa)} is null.");
                return;
            }

            try {
                var setting = cfa.AppSettings.Settings[keyName];
                if(setting == null) {
                    cfa.AppSettings.Settings.Add(keyName, value);
                }
                else {
                    setting.Value = value;
                }
                
                cfa.Save();
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
            }
        }
    }
}
