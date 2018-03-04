using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;

namespace SingularityForensic.Contracts.FileSystem {
    public enum FsFileType {
        RegularFile,            //常规文件;
        Directory,              //目录;
        Device,                 //设备;
        Partition,               //分区;
        Unknown                 //未知;
    }

    /// <summary>
    /// 为统一编码,为文件设定了一个类型;
    /// 内部将自行管理写入安全(通过key)
    /// </summary>
    [Serializable]
    public class FileEn {
        public string[] TypeGUIDS { get; }
        public FileEn(string[] _typeGuids, string key) {
            this._key = key;
            this.TypeGUIDS = _typeGuids;
        }

        private FileEnInfo _info = new FileEnInfo();

        public FileEnInfo GetFileEnInfo(string key) {
            if(_key != key) {
                throw new AuthenticationException($"{nameof(key)} is not right.");
            }

            return _info;
        }
        

        [Serializable]
        public class FileEnInfo {
            public string Name { get; set; }
            public long Size { get; set; }
            public List<FileEn> Children { get; set; } = new List<FileEn>();
            //实体数据;
            public object Data { get; set; }
        }

        public IEnumerable<FileEn> Children => _info.Children;
        
        private string _key;

        public FileEn Parent { get; }               //父文件;
        public string Name { get; set; }                //文件名;
        public long Size { get; }                  //文件大小;
    }

    public class FSFile {
        
        public FSFile(IFile file) {
            File = file ?? throw new ArgumentNullException(nameof(file));
        }

        public IFile File { get; }
        
        public virtual string Name => File.Name;

        public virtual FsFileType Type {
            get {
                switch (File.Type) {
                    case CDFC.Parse.Contracts.FileType.Directory:
                        return FsFileType.Directory;
                    case CDFC.Parse.Contracts.FileType.RegularFile:
                        return FsFileType.RegularFile;
                    default:
                        if(File is Partition) {
                            return FsFileType.Partition;
                        }
                        else if(File is Device) {
                            return FsFileType.Device;
                        }
                        return FsFileType.Unknown;
                }
            }
        }

        public virtual long Size => File.Size;

        public virtual Stream GetStream() {
            if (File != null) {
                if (File is RegularFile regFile) {
                    return regFile.GetStream();
                }
                else if (File is Device device) {
                    return device.Stream;
                }
            }
            return null;
        }

        private string _path;
        public virtual string Path {
            get {
                if(_path == null) {
                    return (_path = File.GetFilePath());
                }
                return _path;
            }
        }

        public IEnumerable<FSFile> Children {
            get {
                if(File is IIterableFile itrFile){
                    return itrFile.Children.Select(p => new FSFile(p));
                }

                return null;
            }
        }

        public IEnumerable<FSFile> GetDirectories() => Children?.Where(p => p.Type == FsFileType.Directory);

        public IEnumerable<FSFile> GetFiles() => Children?.Where(p => p.Type == FsFileType.RegularFile);
    }
}
