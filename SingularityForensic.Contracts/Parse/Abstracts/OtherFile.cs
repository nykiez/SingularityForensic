using SingularityForensic.Contracts.Parse.Contracts;
using System;
using System.Collections.Generic;
using EventLogger;

namespace SingularityForensic.Contracts.Parse.Abstracts {
    
    //其他类型的文件;
    public abstract class OtherFile : IFile, IBlockGroupedFile, ITimeable {
        public OtherFile(IFile parent) {
            this.Parent = parent;
        }
        public virtual FileType Type => FileType.Unknown; //文件类型尚不确定;

        public IFile Parent { get; } //父文件尚未确定;

        public abstract long StartLBA { get; } //起始LBA(相对分区);

        //public abstract long EndLBA { get; }    //终止LBA(相对分区);

        public abstract string Name { get; }    //其他文件名称;
        
        public abstract long Size { get; }          //文件大小;

        public abstract bool? Deleted { get; }                      //是否被删除;

        public abstract DateTime? ModifiedTime { get; }             //最后修改时间;
        public abstract DateTime? AccessedTime { get; }             //最后访问时间;
        public abstract DateTime? CreateTime { get; }               //创建时间;

        public virtual IEnumerable<BlockGroup> BlockGroups { get; }

        public virtual long ClusterSize {
            get {
                var part = this.GetParent<Partition>();
                if (part != null) {
                    return (long)part.ClusterSize;
                }
                else {
                    Logger.WriteCallerLine($"{nameof(part)} can't be null!");
                    return 0;
                }
            }
        }
    }
}
