using CDFC.Parse.Contracts;
using CDFC.Util.IO;
using System;
using System.IO;
using System.Text;

namespace CDFC.Parse.Abstracts {
    //常规文件通用结构;
    public abstract class RegularFile: IFile, ITimeable {
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
        public FileType FileType => FileType.RegularFile;                                //文件类型为常规文件;
        public abstract string Name { get;  }                       //文件名;
        
        public abstract bool? Deleted { get; }                      //是否被删除;

        public virtual DateTime? ModifiedTime { get; }             //最后修改时间;
        public virtual DateTime? AccessedTime { get; }             //最后访问时间;

        public virtual DateTime? CreateTime { get; }               //创建时间;

        public virtual Stream GetStream(bool isReadOnly = true) {
            var part = this.GetParent<Partition>();
            var device = this.GetParent<Device>();
            if (part != null && device != null) {
                var blockSize = part.BlockSize ?? 4096;
                var partStream = InterceptStream.CreateFromStream(device.Stream, part.StartLBA, part.EndLBA);
                var fiStream = InterceptStream.CreateFromStream(partStream, StartLBA, StartLBA + Size - 1);
                //var buffer = new byte[128];
                //var read = fiStream.Read(buffer, 0, buffer.Length);
                return fiStream;
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
                if(_filePath == null) {
                    var pt = Parent;
                    var sb = new StringBuilder();
                    while (pt != null) {
                        sb.Insert(0, $"{pt.Name}/");
                        if (pt.FileType == FileType.BlockDeviceFile) {
                            break;
                        }
                        pt = pt.Parent;
                    }
                    _filePath = sb.ToString();
                }
                return _filePath;
            }
        }
    }
}
