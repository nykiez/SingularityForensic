using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using System;
using System.ComponentModel.Composition;
using System.IO;

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
    }
}
