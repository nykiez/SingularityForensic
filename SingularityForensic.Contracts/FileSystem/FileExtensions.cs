using CDFC.Util.IO;
using SingularityForensic.Contracts.App;
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
        public static TFile GetParent<TFile>(this FileBase file) where TFile:class {
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
        public static string GetFilePath(this FileBase file) {
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
        /// 得到文件流;
        /// </summary>
        /// <param name="groupedFile">块组文件</param>
        /// <returns></returns>
        public static Stream GetInputStream<TFile> (this TFile groupedFile) where TFile:FileBase,IBlockGroupedFile {
            if(groupedFile == null) {
                throw new ArgumentNullException(nameof(groupedFile));
            }

            var streamFile = groupedFile.GetParent<IBlockedStream>();
            if (streamFile == null) {
                LoggerService.Current?.WriteCallerLine($"{nameof(streamFile)} can't be null!");
                return null;
            }

            if (groupedFile.BlockGroups == null) {
                return null;
            }

            //若块组不为空,则取所有的块字段流;
            var blockSize = streamFile.BlockSize;
            var baseStream = streamFile.BaseStream;
            //若块组不为空,则遍历块组组成虚拟流;

            var ranges = groupedFile.BlockGroups.Select(p =>
                ValueTuple.Create(
                    p.BlockAddress * blockSize,
                    p.Count * blockSize)).ToArray();

            var blockSub = ranges.Sum(p => p.Item2) - groupedFile.Size;
            if (ranges?.Count() > 0 && 0 < blockSub && blockSub < blockSize) {
                ranges[ranges.Count() - 1].Item2 -= blockSub;
            }
            return MulPeriodsStream.CreateFromStream(baseStream, ranges);
        }
    }
}
