﻿using CDFC.Parse.Contracts;
using System.Collections.Generic;

namespace CDFC.Parse.Abstracts {
    //块设备文件抽象类型;可用作描述分区，磁盘等;
    public abstract class BlockDeviceFile : IIterableFile {
        public BlockDeviceFile(BlockDeviceFile parent) {
            this.Parent = parent;
        }
        
        public List<IFile> Children { get; protected set; } = new List<IFile>();                   //子文件;

        public FileType Type {
            get {
                return FileType.BlockDeviceFile;
            }
        }

        public string Name { get; set; }                                        //标识;

        public IFile Parent { get; private set; }                                   //父文件;

        public abstract long Size { get; }                                              //大小;
    }
}
