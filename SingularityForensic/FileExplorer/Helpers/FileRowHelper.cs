using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using System.IO;

namespace SingularityForensic.Controls.FileExplorer.Helpers {
    public static class FileRowHelper {
        private const int limSize = 10485760 * 10;
        /// <summary>
        /// 缓存文件到本地;
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static FileStream SaveFileToTemp(IFileRow<FileBase> row) {
            //var file = row.File;
            //if (!Directory.Exists($"{AppDomain.CurrentDomain.BaseDirectory}/Tmp")) {
            //    System.IO.Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}/Tmp");
            //}
            //var targetPath = $"{AppDomain.CurrentDomain.BaseDirectory}/Tmp/";
            //var targetName = $"{file.Name}";
            //FileStream fs = null;

            //try {
            //    fs = StreamExtensions.CreateAValidFS(targetPath, targetName);

            //    if (fs == null) {
            //        return null;
            //    }

            //    var stream = StreamExtensions.CreateStreamByFile(file);

            //    if (stream != null) {
            //        if (stream.Length < limSize) {
            //            stream.Position = 0;
            //            stream.CopyTo(fs);
            //        }
            //        stream.Close();
            //    }


            //    row.LocalPath = fs.Name;
            //    return fs;
            //}
            //catch (Exception ex) {
            //    LoggerService.Current?.WriteCallerLine(ex.Message);
            //    Application.Current.Dispatcher.Invoke(() => {
            //        RemainingMessageBox.Tell($"{ex.Message}");
            //    });
            //}

            return null;
        }
    }
}
