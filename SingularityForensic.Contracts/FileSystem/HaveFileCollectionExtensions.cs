using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    public static class HaveFileCollectionExtensions {
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

        /// <summary>
        /// 得到文件总数;
        /// </summary>
        /// <param name="file"></param>
        /// <param name="searchOption"></param>
        /// <returns></returns>
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
                return (file.Children.ElementAt(partIndex) as IPartition).GetFileByUrlArgs(partArgs.ToArray());
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
                url = url.Replace('/', Constants.Path_SplitChar);
                var urlArgs = url.Split(Constants.Path_SplitChar);
                return GetFileByUrlArgs(part, urlArgs);
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
        public static IFile GetFileByUrlArgs(this IHaveFileCollection enumFile, string[] urlArgs) {
            if (urlArgs == null || urlArgs.Length == 0) {
                return null;
            }
            if (enumFile == null) {
                return null;
            }

            return EntityHelper.GetEntityFromParams<IFile, string>(
                enumFile,
                urlArgs,
                f => {
                    if (f is IHaveFileCollection haveFileCollection) {
                        return haveFileCollection.Children;
                    }
                    return Enumerable.Empty<IFile>();
                },
                (f, param) => f.Name == param
            );
        }

        public static string[] GetUrlArgsByFile(this IHaveFileCollection haveFileCollection, IFile file) {
            if (file == null) {
                throw new ArgumentNullException(nameof(file));
            }

            if (haveFileCollection == null) {
                throw new ArgumentNullException(nameof(haveFileCollection));
            }

            var fileStack = haveFileCollection.GetParentFiles(file, true);
            if (fileStack == null) {
                return null;
            }

            var stackSize = fileStack.Count();
            var array = new string[stackSize];
            var index = 0;
            foreach (var cell in fileStack) {
                array[stackSize - index - 1] = cell.Name;
                
                index++;
            }
            return array;
        }

        /// <summary>
        /// 根据文件得到路径;
        /// </summary>
        /// <param name="haveFileCollection"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetUrlByFile(this IHaveFileCollection haveFileCollection, IFile file) {
            if (file == null) {
                throw new ArgumentNullException(nameof(file));
            }

            if (haveFileCollection == null) {
                throw new ArgumentNullException(nameof(haveFileCollection));
            }

            var sb = new StringBuilder();
            var parentFiles = haveFileCollection.GetParentFiles(file, true);
            if (parentFiles == null) {
                return null;
            }
            var index = 0;
            foreach (var pFile in parentFiles) {
                if (index == 0) {
                    sb.Insert(0, $"{pFile.Name}");
                }
                else {
                    sb.Insert(0, $"{pFile.Name}{Constants.Path_SplitChar}");
                }
                index++;
            }
            return sb.ToString();
        }

        //public static TAggregate GetAggregate<TAggregate,TEntity>(Func<TAggregate> aggreateFactory,IEnumerable<){
        //}

        /// <summary>
        /// 检查某个文件集合中是否包含某个文件;
        /// </summary>
        /// <param name="haveFileCollection"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool CheckOwn(this IHaveFileCollection haveFileCollection,IFile file) {
            if(haveFileCollection == null) {
                throw new ArgumentNullException(nameof(haveFileCollection));
            }

            if(file == null) {
                throw new ArgumentNullException(nameof(file));
            }

            return EntityHelper.CheckOwn(haveFileCollection, file, f => f.Parent);

            //IFile fileNode = file;
            //while (fileNode != null) {
            //    if (fileNode == haveFileCollection) {
            //        break;
            //    }
            //    fileNode = fileNode.Parent;
            //}

            //return fileNode != null;
        }

        /// <summary>
        /// 得到指定文件节点以上所有父节点;
        /// </summary>
        /// <param name="haveFileCollection"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static IEnumerable<IFile> GetParentFiles(this IHaveFileCollection haveFileCollection,IFile file,bool selfIncluded = false) {
            if(haveFileCollection == null) {
                throw new ArgumentNullException(nameof(haveFileCollection));
            }
            if(file == null) {
                throw new ArgumentNullException(nameof(file));
            }
            if (!CheckOwn(haveFileCollection, file)) {
                throw new InvalidOperationException($"{nameof(haveFileCollection)}({haveFileCollection.Name}) doesn't own the file ({file.Name})");
            }

            return EntityHelper.GetParentEntities(haveFileCollection, file, f => f.Parent, selfIncluded);
        }
        
        /// <summary>
        /// 得到内部所有文件的迭代;
        /// </summary>
        /// <param name="haveFileCollection"></param>
        /// <param name="backOrBackUpDirIncluded">文件夹是否包含</param>
        /// <returns></returns>
        public static IEnumerable<IFile> GetInnerFiles(this IHaveFileCollection haveFileCollection,bool isDirIncluded = false) {
            if(haveFileCollection == null) {
                throw new ArgumentNullException(nameof(haveFileCollection));
            }

            foreach (var file in haveFileCollection.Children) {
                if(file is IHaveFileCollection innerCollection) {
                    if(file is IDirectory dir) {
                        if (!dir.IsBack && !dir.IsLocalBackUp && isDirIncluded) {
                            yield return file;
                        }
                    }
                    
                    foreach (var innerFile in innerCollection.GetInnerFiles(isDirIncluded)) {
                        yield return innerFile;
                    }

                    
                }
                else {
                    yield return file;
                }
            }
            
        }
    }
}
