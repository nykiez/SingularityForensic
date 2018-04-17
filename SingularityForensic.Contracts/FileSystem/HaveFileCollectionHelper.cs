using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    public static class HaveFileCollectionHelper {
        /// <summary>
        /// 查找索引号;
        /// </summary>
        /// <param name="itrFile"></param>
        /// <returns></returns>
        public static int IndexOf(this IHaveFileCollection itrFile, IFile file) {
            if (itrFile == null && itrFile.Children == null) {
                return -1;
            }
            var idx = 0;
            foreach (var item in itrFile.Children) {
                if (item == file) {
                    return idx;
                }
                idx++;
            }
            return -1;
        }

        /// <summary>
        /// //得到内部文件的总大小;
        /// </summary>
        /// <param name="file">目标文件</param>
        /// <param name="subDicIncluded">是否包含子文件夹</param>
        /// <returns></returns>
        public static long GetSubSize(this IHaveFileCollection file, SearchOption searchOption = SearchOption.AllDirectories) {
            if (file == null) {
                throw new ArgumentNullException(nameof(file));
            }

            if (file.Children == null) {
                return 0;
            }

            long sumSize = 0;

            foreach (var child in file.Children) {
                if (searchOption == SearchOption.AllDirectories && child is IHaveFileCollection eumFile) {
                    sumSize += GetSubSize(eumFile);
                }

                sumSize += child.Size;
            }

            return sumSize;
        }

        //得到文件总数;
        private static long GetSubFileNum(IHaveFileCollection file, SearchOption searchOption = SearchOption.AllDirectories) {
            if (file == null) {
                throw new ArgumentNullException(nameof(file));
            }

            if (file.Children == null) {
                return 0;
            }

            long sumNum = 0;

            foreach (var child in file.Children) {
                if (searchOption == SearchOption.AllDirectories && child is IHaveFileCollection enumFile) {
                    sumNum += GetSubFileNum(enumFile);
                }
                sumNum++;
            }


            return sumNum;
        }

        /// <summary>
        /// 通过Url找到子文件;
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static IFile GetFileByUrl(this IDevice file, string url) {
            if (string.IsNullOrEmpty(url)) {
                return file;
            }

            url = url.Replace('\\', '/');
            if (url == "/") {
                return file;
            }

            try {
                var args = url.Split('/');
                var partArgs = args.ToList().GetRange(1, args.Length - 1);

                var partIndex = int.Parse(args[0]);
                return (file.Children.ElementAt(partIndex) as IPartition).GetFileByUrl(partArgs.ToArray());
            }
            catch (Exception ex) {
                LoggerService.Current?.WriteCallerLine(ex.Message);
            }
            return null;
        }

        /// <summary>
        /// 通过Url找到子文件;
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static IFile GetFileByUrl(this IHaveFileCollection part, string url) {
            try {
                url = url.Replace('\\', '/');
                var urlArgs = url.Split('/');
                return GetFileByUrl(part, urlArgs);
            }
            catch (Exception ex) {
                LoggerService.Current?.WriteCallerLine(ex.Message);
            }

            return null;
        }

        /// <summary>
        /// 通过Url参数获得子文件;
        /// </summary>
        /// <param name="enumFile"></param>
        /// <param name="urlArgs"></param>
        /// <returns></returns>
        public static IFile GetFileByUrl(this IHaveFileCollection enumFile, string[] urlArgs) {
            if (urlArgs == null) {
                return null;
            }
            if (urlArgs.Length == 1 && string.IsNullOrEmpty(urlArgs[0])) {
                return enumFile as IFile;
            }

            IHaveFileCollection fileNode = enumFile;
            for (int index = 0; index < urlArgs.Length - 1; index++) {
                if (fileNode == null) {
                    throw new FileNotFoundException($"Can't find file {urlArgs[index]}-{urlArgs.Aggregate((a, b) => $"{a}/{b}")}");
                }
                fileNode = fileNode.Children.FirstOrDefault(p => p.Name == urlArgs[index]) as IHaveFileCollection;
            }
            return fileNode.Children.FirstOrDefault(p => p.Name == urlArgs[urlArgs.Length - 1]);
        }

        //通过起始位置找到内部文件;
        //public static IFile GetInnerFileByPosition(this IIterable iterable, long pos) {
        //    if (iterable == null || iterable.Children == null)
        //        return null;

        //    foreach (var item in iterable.Children) {
        //        if (item is RegularFile) {
        //            if ((item as RegularFile).StartLBA == pos) {
        //                return item;
        //            }
        //        }
        //        else if (item is IIterable) {
        //            var itera = (item as IIterable);
        //            if (!(itera.IsBackFile() || itera.IsBackUpFile())) {
        //                IFile innerFile = null;
        //                if ((innerFile = GetInnerFileByPosition(itera, pos)) != null) {
        //                    return innerFile;
        //                }
        //            }
        //        }
        //    }
        //    return null;
        //}
    }
}
