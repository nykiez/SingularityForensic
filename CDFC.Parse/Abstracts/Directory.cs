using CDFC.Parse.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using EventLogger;

namespace CDFC.Parse.Abstracts {
    /// <summary>
    /// 目录类型;
    /// </summary>
    public abstract class Directory: IIterableFile,ITimeable,IBlockGroupedFile {
        public Directory(IFile parent) {
            this.Parent = parent;
        }
        public abstract string Name { get; }                          //路径名;
        public FileType Type => FileType.Directory;                 //文件类型为目录;
        public abstract IEnumerable<IFile> Children { get; }               //子文件;

        public IFile Parent { get; }                           //父文件;

        public abstract long StartLBA { get; }                          //目录起始偏移;
        
        public abstract long Size { get; }                          //目录大小;

        public abstract bool? Deleted { get; }                      //是否被删除;

        public abstract DateTime? ModifiedTime { get; }             //最后修改时间;
        public abstract DateTime? AccessedTime { get; }             //最后访问时间;
        public abstract DateTime? CreateTime { get; }               //创建时间;

        public virtual IEnumerable<BlockGroup> BlockGroups { get; }

        
        public bool IsOwnCreate { get; protected set; }              //是否虚构("."以及"..");

        public virtual long ClusterSize {
            get {
                var part = this.GetParent<Partition>();
                if(part != null) {
                    return part.ClusterSize;
                }
                else {
                    return 4096;
                }
                
            }
        }
    }
}
