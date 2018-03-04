using SingularityForensic.Contracts.Parse.Abstracts;
using System.Text;

namespace SingularityForensic.Contracts.Parse.Contracts {
    public interface IFile {
        FileType Type { get; }          //文件类型;
        IFile Parent { get; }               //父类型;
        string Name { get; }                //文件名;
        long Size { get; }                  //文件大小;
    }

    public static class FileHelper {
        public static TFile GetParent<TFile>(this IFile file) where TFile: class,IFile {
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
        public static string GetFilePath(this IFile file) {
            if(file is RegularFile regFile) {
                return regFile.FilePath;
            }

            var pt = file.Parent;
            var sb = new StringBuilder();
            while (pt != null) {
                sb.Insert(0, $"{pt.Name}/");
                if (pt.Type == SingularityForensic.Contracts.Parse.Contracts.FileType.BlockDeviceFile) {
                    break;
                }
                pt = pt.Parent;
            }
            return sb.ToString();
        }
    }

    public class FileEntry {
        public FileEntry() {

        }

        FileEntry Parent { get; }               //父类型;
        string Name { get; }                //文件名;
        long Size { get; }                  //文件大小;
    }
}
