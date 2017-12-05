using CDFC.Parse.Abstracts;
using System.Collections.Generic;

namespace CDFC.Parse.Contracts {
    //具有子文件的文件;
    public interface IIterableFile : IFile {
        List<IFile> Children { get; }       //子文件;
    }

    public static class IIterableHelper{
        /// <summary>
        /// 判定是否为备用文件;(.)
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static bool IsBackUpFile(this IIterableFile dir) {
            if (dir == null)
                return false;

            return dir.Name == ".";
            //return dir.Children != null &&
            //    dir.Children.Count != 0 &&
            //    dir.Children == (dir.Parent as IIterableFile)?.Children;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itrFile"></param>
        /// <returns></returns>
        public static int IndexOf(this IIterableFile itrFile,IFile file) {
            if(itrFile == null && itrFile.Children == null) {
                return -1;
            }
            return itrFile.Children.IndexOf(file);
        }

        /// <summary>
        /// 判定是否为上级文件(..);
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static bool IsBackFile(this IIterableFile dir) {
            if (dir == null)
                return false;

            return dir.Name == "..";
            //return dir.Children != null &&
            //    dir.Children.Count != 0 &&
            //    dir.Children.Contains(dir.Parent);
        }

        //得到内部文件的总大小;
        public static long GetTotalSize(this IIterableFile file) {
            if (file.Children != null) {
                long sum = 0;
                TraverseAddNum(file,ref sum);
                return sum;
            }
            return 0;
        }
        private static void TraverseAddNum(IIterableFile file,ref long sum) {
            foreach (var p in file.Children) {
                if (p.Type == FileType.Directory) {
                    var direc = p as Abstracts.Directory;
                    if (direc?.Deleted == false) {
                        if (direc.Name != ".." && direc.Name != ".") {
                            TraverseAddNum(direc, ref sum);
                        }
                    }

                }
                else if (p.Type == FileType.RegularFile) {
                    sum += p.Size;
                }
            }
            
            
        }

        //通过起始位置找到内部文件;
        public static IFile GetInnerFileByPosition(this IIterableFile iterable, long pos) {
            if (iterable == null || iterable.Children == null)
                return null;

            foreach (var item in iterable.Children) {
                if (item is RegularFile) {
                    if ((item as RegularFile).StartLBA == pos) {
                        return item;
                    }
                }
                else if(item is IIterableFile) {
                    var itera = (item as IIterableFile);
                    if(!(itera.IsBackFile() || itera.IsBackUpFile())) {
                        IFile innerFile = null;
                        if ((innerFile = GetInnerFileByPosition(itera,pos)) != null) {
                            return innerFile;
                        }
                    }
                }
            }
            return null;
        }
    }
}
