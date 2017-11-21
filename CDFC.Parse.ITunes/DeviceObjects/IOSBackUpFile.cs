using CDFC.Parse.Abstracts;
using CDFC.Parse.ITunes.Models;
using CDFC.Parse.Local.DeviceObjects;
using CDFCCultures.Helpers;
using System;
using System.IO;
using CDFC.Parse.Contracts;

namespace CDFC.Parse.ITunes.DeviceObjects {
    /// <summary>
    /// 临时分区仓储;
    /// </summary>
    public class ITunesFilePartition : Partition {
        public ITunesFilePartition():base(null) {
            
        }
        public override uint? BlockSize => throw new NotImplementedException();

        public override FileSystemType FSType => FileSystemType.Unknown;

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
