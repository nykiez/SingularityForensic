using CDFC.Parse.Abstracts;
using CDFC.Parse.Modules.Structs;
using CDFC.Parse.Modules.DeviceObjects;
using CDFCCultures.Helpers;
using System;
using System.IO;
using CDFC.Parse.Contracts;
using System.Collections.Generic;

namespace CDFC.Parse.Modules.DeviceObjects {
    /// <summary>
    /// 临时分区仓储;
    /// </summary>
    public class ITunesFilePartition : Partition {
        public ITunesFilePartition():base(null) {
            
        }
        
        public override FileSystemType FSType => FileSystemType.Unknown;

        private List<IFile> _children = new List<IFile>();
        public override IEnumerable<IFile> Children => _children;

        public override uint ClusterSize => 0;

        public void AddChildren(IEnumerable<IFile> children) =>  _children.AddRange(_children);
    }
    /// <summary>
    /// IOS备份文件
    /// </summary>
    public class IOSBackUpFile:LocalRegFile {
        public IOSBackUpFile(IOSFileStruct st):base(GetFileInfoByStruct(st),null) {
            this.FileStruct = st;
        }

        public static FileInfo GetFileInfoByStruct(IOSFileStruct st) {
            try {
                return new FileInfo(st.strLocalPath);
            }
            catch {
                
            }
            return null;
        }

        public IOSFileStruct FileStruct { get; }

        public override string Name => IOPathHelper.GetFileNameFromUrl(FileStruct.strPhonePath);

        private long? _size;
        public override long Size {
            get {
                if(_size == null) {
                    try {
                         _size = base.Size;
                    }
                    catch {
                        _size = 0;
                    }
                }
                return _size.Value;
            }
        }

        private string _filePath;
        public override string FilePath => _filePath??(_filePath = FileStruct.strPhonePath.Replace("\\","/"));
    }
}
