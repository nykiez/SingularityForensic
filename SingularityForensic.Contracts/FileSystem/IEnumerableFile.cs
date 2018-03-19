using SingularityForensic.Contracts.App;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    //具有子级的可迭代文件;
    public interface IHaveFileCollection  {
        FileBaseCollection Children { get; }
    }

    //子文件集;
    public class FileBaseCollection : IEnumerable<FileBase> {
        /// <summary>
        /// 文件子集的构造方法;
        /// </summary>
        /// <param name="parent">父文件</param>
        public FileBaseCollection(FileBase parent) {
            if(parent == null) {
                throw new ArgumentNullException(nameof(parent));
            }

            this._parent = parent;
        }

        private FileBase _parent;
        //核心文件集合;
        private List<FileBase> _children = new List<FileBase>();

        public IEnumerator<FileBase> GetEnumerator() => _children.GetEnumerator();

        public bool Contains(FileBase file) => _children.Contains(file);

        public void Add(FileBase file) {
            if (file == null) {
                throw new ArgumentNullException(nameof(file));
            }

            if (file == _parent) {
                throw new InvalidOperationException($"The file can't own itself as a child.");
            }

            if (_children.Contains(file)) {
                throw new InvalidOperationException($"The file has already been added to the collection.");
            }

            file.InternalParent = _parent;
            _children.Add(file);
        }

        public void Remove(FileBase file) {
            if(file == null) {
                throw new ArgumentNullException(nameof(file));
            }

            if (!_children.Contains(file)) {
                throw new InvalidOperationException($"The {nameof(file)} is not contained in the collection.");
            }
            
            _children.Remove(file);
            
            file.InternalParent = null;
        }
        IEnumerator IEnumerable.GetEnumerator() => _children.GetEnumerator();

        public int Count => _children.Count;
    }

    public static class EnumerableFileHelper {
        /// <summary>
        /// 判定是否为备用文件;(.)
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static bool IsBackUpDir(this Directory dir) {
            if (dir == null)
                return false;

            return dir.Name == ".";
        }

        /// <summary>
        /// 判定是否为上级文件(..);
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static bool IsBackDir(this Directory dir) {
            if (dir == null)
                return false;

            return dir.Name == "..";

        }

        /// <summary>
        /// 查找索引号;
        /// </summary>
        /// <param name="itrFile"></param>
        /// <returns></returns>
        public static int IndexOf(this IHaveFileCollection itrFile, FileBase file) {
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
        public static long GetSubSize(this IHaveFileCollection file,SearchOption searchOption = SearchOption.AllDirectories) {
            if(file == null) {
                throw new ArgumentNullException(nameof(file));
            }

            if (file.Children == null) {
                return 0;
            }

            long sumSize = 0;

            foreach (var child in file.Children) {
                if(searchOption == SearchOption.AllDirectories && child is IHaveFileCollection eumFile) {
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
        public static FileBase GetFileByUrl(this Device file, string url) {
            try {
                if (string.IsNullOrEmpty(url)) {
                    return file;
                }

                url = url.Replace('\\', '/');
                if (url == "/") {
                    return file;
                }

                var args = url.Split('/');
                var partArgs = args.ToList().GetRange(1, args.Length - 1);

                var partIndex = int.Parse(args[0]);
                return (file.Children.ElementAt(partIndex) as Partition).GetFileByUrl(partArgs.ToArray());
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
        public static FileBase GetFileByUrl(this IHaveFileCollection part, string url) {
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
        public static FileBase GetFileByUrl(this IHaveFileCollection enumFile, string[] urlArgs) {
            if (urlArgs == null) {
                return null;
            }
            if (urlArgs.Length == 1 && string.IsNullOrEmpty(urlArgs[0])) {
                return enumFile as FileBase;
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
