using CDFC.Parse.Contracts;
using System;
using System.Collections.Generic;

namespace CDFC.Parse.Abstracts {
    /// <summary>
    /// 目录类型;
    /// </summary>
    public abstract class Directory: IIterableFile,ITimeable {
        public Directory(IFile parent) {
            this.Parent = parent;
        }
        public abstract string Name { get; }                          //路径名;
        public FileType FileType => FileType.Directory;                 //文件类型为目录;
        public abstract List<IFile> Children { get; }               //子文件;

        public IFile Parent { get; }                           //父文件;

        public abstract long StartLBA { get; }                          //目录起始偏移;
        
        public abstract long Size { get; }                          //目录大小;

        public abstract bool? Deleted { get; }                      //是否被删除;

        public abstract DateTime? ModifiedTime { get; }             //最后修改时间;
        public abstract DateTime? AccessedTime { get; }             //最后访问时间;
        public abstract DateTime? CreateTime { get; }               //创建时间;
    }
}
