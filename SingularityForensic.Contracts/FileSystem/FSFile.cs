using CDFC.Util.IO;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;

namespace SingularityForensic.Contracts.FileSystem {
    //用于某块内部使用的文件信息(模块可自定义,建议外部需要根据Key拿到所需数据);
    public abstract class FileStokenBase:SecurityStoken {
        public IEnumerable<string> TypeGuids { get; set; }          //文件类型;
        public FileBase Parent { get; set; }           //父类型;
        public string Name { get; set; }                //文件名;
        public long Size { get; set; }                  //文件大小;
    }
    
    //用于描述文件,文件夹等具有时间,块组特性的文件的信息;
    public abstract class FileStokenBase2 : FileStokenBase, ITimeable, IBlockGroupedFile {
        public DateTime? ModifiedTime { get; set; }

        public DateTime? AccessedTime { get; set; }

        public DateTime? CreateTime { get; set; }

        public Dictionary<string, DateTime?> ExtendedTimes { get; } = new Dictionary<string, DateTime?>();

        public IEnumerable<BlockGroup> BlockGroups { get; set; }

        //拓展时间获取;
        public DateTime? GetExtensionTime(string timeLabel) {
            if (!ExtendedTimes.ContainsKey(timeLabel)) {
                return null;
            }

            return ExtendedTimes[timeLabel];
        }

        //是否被删除;
        public bool? Deleted { get; set; }                      //是否被删除;
    }

    public abstract class FileBase {
        public abstract IEnumerable<string> TypeGuids { get; }

        public FileBase Parent => InternalParent;

        public abstract string Name { get; }

        public abstract long Size { get; }

        internal FileBase InternalParent { get; set; }

        internal void ChangeParent(FileBase parent) {
            InternalParent = parent;
        }
    }

    /// <summary>
    /// 文件内部信息基类;
    /// </summary>
    /// <typeparam name="TStoken"></typeparam>
    public abstract class FileBase<TStoken> : 
        FileBase,IHaveStoken<TStoken>
        where TStoken : FileStokenBase, new() {

        private string _key;
        protected TStoken _stoken;

        public TStoken GetStoken(string key) {
            if (key != _key) {
                throw new AuthenticationException($"{nameof(key)} is not matched.");
            }
            return _stoken;
        }

        public FileBase(string key,TStoken stoken = null) {
            this._key = key;
        }
        
        public override IEnumerable<string> TypeGuids => _stoken?.TypeGuids;

        public FileBase Parent => _stoken?.Parent;

        public override string Name => _stoken?.Name;

        public override long Size => _stoken?.Size ?? throw new InvalidOperationException($"{nameof(_stoken)} can't be null");
        
    }

    //用于描述文件,文件夹等具有时间,块组特性的文件;
    public abstract class BlockGroupedFileBase<TStoken> : FileBase<TStoken>, ITimeable, IBlockGroupedFile where TStoken : FileStokenBase2, new() {
        public BlockGroupedFileBase(string key, TStoken stoken = null) : base(key, stoken) {

        }

        public DateTime? ModifiedTime => _stoken?.ModifiedTime;

        public DateTime? AccessedTime => _stoken?.AccessedTime;

        public DateTime? CreateTime => _stoken?.CreateTime;

        public IEnumerable<BlockGroup> BlockGroups => _stoken?.BlockGroups;

        public DateTime? GetExtensionTime(string timeLabel) => _stoken?.GetExtensionTime(timeLabel);
        
        public long? StartLBA {
            get {
                var firstBlock = _stoken?.BlockGroups?.FirstOrDefault();
                if(firstBlock != null) {
                    return firstBlock.BlockAddress * firstBlock.BlockSize;
                }
                return null;
            }
            
        }

        public Stream GetInputStream() {
            var blockedFile = this.GetParent<IBlockedStream>();
            if (blockedFile == null) {
                LoggerService.Current?.WriteCallerLine($"{nameof(blockedFile)} can't be null!");
            }

            //若块组不为空,则取所有的块字段流;
            if (BlockGroups != null) {
                if (blockedFile != null) {
                    var blockSize = blockedFile.BlockSize;
                    var partStream = blockedFile.BaseStream;
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
            else if(StartLBA != null){
                var blockSize = blockedFile.BlockSize;

                var partStream = blockedFile.BaseStream;
                if (partStream != null) {
                    var fiStream = InterceptStream.CreateFromStream(partStream, StartLBA.Value, Size);
                    //var buffer = new byte[128];
                    //var read = fiStream.Read(buffer, 0, buffer.Length);
                    return fiStream;
                }
            }
            return null;
        }
    }

    

    //常规文件内部信息;
    public sealed class RegularFileStoken : FileStokenBase2  {

    }

    //文件夹内部信息;

    public sealed class DirectoryStoken : FileStokenBase2  {
        public List<FileBase> Children { get; set; } 
    }
    
    /// <summary>
    /// 正常文件入口
    /// </summary>
    [Serializable]
    public sealed class RegularFile : BlockGroupedFileBase<RegularFileStoken>  {
        /// <summary>
        /// 常规文件构造方法;
        /// </summary>
        /// <param name="parent"></param>
        public RegularFile(string key, RegularFileStoken stoken = null) :base(key,stoken) {
            
        }
    }

    [Serializable]
    public sealed class Directory : BlockGroupedFileBase<DirectoryStoken>, IHaveFileCollection {
        public Directory(string key, DirectoryStoken stoken = null) : base(key, stoken) {
            Children = new FileBaseCollection(this);
        }

        //public IEnumerable<FileBase> Children => _stoken?.Children?.Select(p => p);

        
        public FileBaseCollection Children { get; set; }
    }
    
    //public class FSFile {
    //    public FSFile(IFile file) {
    //        File = file ?? throw new ArgumentNullException(nameof(file));
    //    }

    //    public IFile File { get; }
        
    //    public virtual string Name => File.Name;

    //    public virtual FileAttributes Type {
    //        get {
    //            switch (File.Type) {
    //                case CDFC.Parse.Contracts.FileType.Directory:
    //                    return FileAttributes.Directory;
    //                case CDFC.Parse.Contracts.FileType.RegularFile:
    //                    return FileAttributes.RegularFile;
    //                default:
    //                    if(File is Partition) {
    //                        return FileAttributes.Partition;
    //                    }
    //                    else if(File is Device) {
    //                        return FileAttributes.Device;
    //                    }
    //                    return FileAttributes.Unknown;
    //            }
    //        }
    //    }

    //    public virtual long Size => File.Size;

    //    public virtual Stream GetStream() {
    //        if (File != null) {
    //            if (File is RegularFile regFile) {
    //                return regFile.GetStream();
    //            }
    //            else if (File is Device device) {
    //                return device.Stream;
    //            }
    //        }
    //        return null;
    //    }

    //    private string _path;
    //    public virtual string Path {
    //        get {
    //            if(_path == null) {
    //                return (_path = File.GetFilePath());
    //            }
    //            return _path;
    //        }
    //    }

    //    public IEnumerable<FSFile> Children {
    //        get {
    //            if(File is IIterableFile itrFile){
    //                return itrFile.Children.Select(p => new FSFile(p));
    //            }

    //            return null;
    //        }
    //    }

    //    public IEnumerable<FSFile> GetDirectories() => Children?.Where(p => p.Type == FileAttributes.Directory);

    //    public IEnumerable<FSFile> GetFiles() => Children?.Where(p => p.Type == FileAttributes.RegularFile);
    //}
}
