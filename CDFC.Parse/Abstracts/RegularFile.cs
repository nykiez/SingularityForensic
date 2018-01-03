using CDFC.Parse.Contracts;
using CDFC.Util.IO;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using EventLogger;
using System.Linq;

namespace CDFC.Parse.Abstracts {
    //常规文件通用结构;
    public abstract class RegularFile : IFile, ITimeable, IBlockGroupedFile {
        /// <summary>
        /// 常规文件构造方法;
        /// </summary>
        /// <param name="parent"></param>
        public RegularFile(IFile parent) {
            this.Parent = parent;
        }
        public abstract long StartLBA { get; }                      //起始LBA(相对分区);
        //public abstract long EndLBA { get; }                        //终止LBA(相对分区);
        public abstract long Size { get; }                          //文件大小;
        public virtual IFile Parent { get; private set; }                   //父文件;
        public FileType Type => FileType.RegularFile;                                //文件类型为常规文件;
        public abstract string Name { get; }                       //文件名;

        public abstract bool? Deleted { get; }                      //是否被删除;

        public abstract DateTime? ModifiedTime { get; }             //最后修改时间;
        public abstract DateTime? AccessedTime { get; }             //最后访问时间;
               
        public abstract DateTime? CreateTime { get; }               //创建时间;

        public virtual Stream GetStream(bool isReadOnly = true) {
            var part = this.GetParent<Partition>();
            if(part == null) {
                Logger.WriteCallerLine($"{nameof(part)} can't be null!");
            }

            //若块组不为空,则取所有的块字段流;
            if (BlockGroups != null) {
                if (part != null) {
                    var blockSize = part.ClusterSize;
                    var partStream = part.GetStream();
                    //若块组不为空,则遍历块组组成虚拟流;
                    
                    var ranges = BlockGroups.Select(p =>
                        ValueTuple.Create(
                            p.BlockAddress * blockSize,
                            p.Count * blockSize)).ToArray();

                    var blockSub = ranges.Sum(p => p.Item2) - Size;
                    if (ranges?.Count() > 0 && 0 < blockSub && blockSub < blockSize) {
                        ranges[ranges.Count() - 1].Item2 -= blockSub;
                    }
                    var multiStream = MulPeriodsStream.CreateFromStream(partStream, ranges);
                    return multiStream;

                }
            }
            //否则直接取连续的流;
            else {
                var blockSize = part.ClusterSize;

                var partStream = part.GetStream();
                if (partStream != null) {
                    var fiStream = InterceptStream.CreateFromStream(partStream, StartLBA , Size);
                    //var buffer = new byte[128];
                    //var read = fiStream.Read(buffer, 0, buffer.Length);
                    return fiStream;
                }
            }
            return null;
        }

        //起始LBA(相对设备);
        public long DeviceStartLBA {
            get {
                var part = this.GetParent<Partition>();
                if (part != null) {
                    return part.StartLBA + StartLBA;
                }
                else {
                    return StartLBA;
                }
            }
        }

        private string _filePath;
        public virtual string FilePath {
            get {
                if (_filePath == null) {
                    var pt = Parent;
                    var sb = new StringBuilder();
                    while (pt != null) {
                        sb.Insert(0, $"{pt.Name}/");
                        if (pt.Type == FileType.BlockDeviceFile) {
                            break;
                        }
                        pt = pt.Parent;
                    }
                    _filePath = sb.ToString();
                }
                return _filePath;
            }
        }

        public virtual IEnumerable<BlockGroup> BlockGroups { get; }

        public virtual long ClusterSize {
            get {
                var part = this.GetParent<Partition>();
                if(part != null) {
                    return (long) part.ClusterSize;
                }
                else {
                    Logger.WriteCallerLine($"{nameof(part)} can't be null!");
                    return 0;
                }
            }
        }
    }
}
