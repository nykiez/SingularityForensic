using CDFC.Util.IO;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {

    ////文件契约;
    //public interface FileBase {
    //    IEnumerable<string> TypeGuids { get; }          //文件类型(应对模块);
    //    FileBase Parent { get; }               //父类型;
    //    string Name { get; }                //文件名;
    //    long Size { get; }                  //文件大小;
    //}

    public static class FileExtensions {
        public static TFile GetParent<TFile>(this IFile file) where TFile:class {
            while (file != null && file.Parent != null) {
                file = file.Parent;
                if (file is TFile) {
                    return file as TFile;
                }
            }
            return null;
        }

        /// <summary>
        /// 得到文件路径;
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetTotalFilePath(this IFile file) {
            if(file == null) {
                throw new ArgumentNullException(nameof(file));
            }


            var fileNode = file.Parent;

            var sb = new StringBuilder();
            while (fileNode != null) {
                sb.Insert(0, $"{fileNode.Name}/");
                fileNode = fileNode.Parent;
            }

            return sb.ToString();
        }


        /// <summary>
        /// 获取输入文件的输入流;
        /// </summary>
        /// <param name="blockGrouped"></param>
        /// <returns></returns>
        public static Stream GetInputStream(this IFile file) {
            var streamProviders = ServiceProvider.GetAllInstances<IFileInputStreamProvider>();
            foreach (var provider in streamProviders) {
                try {
                    var stream = provider.GetInputStream(file);
                    if (stream != null) {
                        return stream;
                    }
                }
                catch(Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            }

            return null;
        }
    }
}
