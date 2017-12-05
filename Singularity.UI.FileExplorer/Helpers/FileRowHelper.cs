using CDFC.Parse.Contracts;
using CDFC.Parse.IO;
using CDFCMessageBoxes.MessageBoxes;
using EventLogger;
using Singularity.Contracts.FileExplorer;
using System;
using System.IO;
using System.Windows;

namespace Singularity.UI.FileExplorer.Helpers {
    public static class FileRowHelper {
        private const int limSize = 10485760 * 10;
        /// <summary>
        /// 缓存文件到本地;
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static FileStream SaveFileToTemp(IFileRow<IFile> row) {
            var file = row.File;
            if (!Directory.Exists($"{AppDomain.CurrentDomain.BaseDirectory}/Tmp")) {
                System.IO.Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}/Tmp");
            }
            var targetPath = $"{AppDomain.CurrentDomain.BaseDirectory}/Tmp/";
            var targetName = $"{file.Name}";
            FileStream fs = null;

            try {
                fs = StreamExtensions.CreateAValidFS(targetPath, targetName);

                if (fs == null) {
                    return null;
                }

                var stream = StreamExtensions.CreateStreamByFile(file);

                if (stream != null) {
                    if (stream.Length < limSize) {
                        stream.Position = 0;
                        stream.CopyTo(fs);
                    }
                    stream.Close();
                }


                row.LocalPath = fs.Name;
                return fs;
            }
            catch (Exception ex) {
                Logger.WriteCallerLine(ex.Message);
                Application.Current.Dispatcher.Invoke(() => {
                    RemainingMessageBox.Tell($"{ex.Message}");
                });
            }

            return null;
        }
    }
}
