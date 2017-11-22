using CDFC.Parse.Contracts;
using System;
using System.Collections.Generic;

namespace CDFC.Parse.Abstracts {
    
    //其他类型的文件;
    public abstract class OtherFile : IFile, IBlockGroupedFile, ITimeable {
        public abstract FileType FileType { get; } //文件类型尚不确定;

        public abstract IFile Parent { get;} //父文件尚未确定;

        public abstract long StartLBA { get; } //起始LBA(相对分区);

        //public abstract long EndLBA { get; }    //终止LBA(相对分区);

        public abstract string Name { get; }    //其他文件名称;

        public abstract List<BlockGroup> BlockGroups { get; }       //文件所占块组;

        public abstract long Size { get; }          //文件大小;

        public abstract bool? Deleted { get; }                      //是否被删除;

        public abstract DateTime? ModifiedTime { get; }             //最后修改时间;
        public abstract DateTime? AccessedTime { get; }             //最后访问时间;
        public abstract DateTime? CreateTime { get; }               //创建时间;
    }
}
