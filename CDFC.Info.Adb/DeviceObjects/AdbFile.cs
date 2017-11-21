using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using Cflab.DataTransport.Modules.Transport.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace CDFC.Info.Adb.DeviceObjects {
    /// <summary>
    /// Adb文件契约;
    /// </summary>
    public interface IAdbFile  {
        //Adb封装的文件;
        AnFile AnFile { get; }
    }

    /// <summary>
    /// Adb正常文件;
    /// </summary>
    [Serializable]
    public class AdbRegFile:RegularFile,IAdbFile {
        public AdbRegFile(AnFile anFile,IFile parent):base(parent) {
            if (anFile == null)
                throw new ArgumentNullException($"{nameof(anFile)} can't be null!");

            this.AnFile = anFile;
        }

        public AnFile AnFile { get; }
        
        public override bool? Deleted => false;

        public override string Name => AnFile.Name;

        public override long Size => AnFile.Size;

        public override long StartLBA => 0;

        public override Stream GetStream(bool isReadOnly = true) => null;
    }

    /// <summary>
    /// Adb所有文件列表的目录;
    /// </summary>
    [Serializable]
    public class AdbDirectory : CDFC.Parse.Abstracts.Directory, IAdbFile {
        public AdbDirectory(AnFile anfile, IFile parent):base(parent) {
            if(anfile == null) {
                throw new ArgumentNullException(nameof(anfile));
            }

            this.AnFile = anfile;
        }

        public override DateTime? AccessedTime => null;

        public AnFile AnFile { get; }
        
        private List<IFile> _children;
        public override List<IFile> Children {
            get {
                if(_children == null) {
                    LoadContent();
                }
                return _children;
            }
        }

        private void LoadContent() {
            _children = new List<IFile>();
            AnFile.Children?.ForEach(p => {
                if (p.IsDir) {
                    _children.Add(new AdbDirectory(p, this));
                }
                else {
                    _children.Add(new AdbRegFile(p, this));
                }
            });
        }

        public override DateTime? CreateTime => null;

        public override bool? Deleted => false;

        public override DateTime? ModifiedTime => new DateTime( AnFile.DateModif);

        public override string Name => AnFile.Name;

        public override long Size => AnFile.Size;

        public override long StartLBA => 0;
    }

    /// <summary>
    /// Adb链接文件;
    /// </summary>
    [Serializable]
    public class AdbLinkFile : SymbolLink,IAdbFile {
        public AdbLinkFile(AnFile anFile,IFile parent):base(parent) {
            this.AnFile = anFile;
        }
        
        public AnFile AnFile { get; }
        public IFile LinkTarget { get; internal set; }

        public override string Name => AnFile.Name;

        public override long Size => AnFile.Size;
    }

    /// <summary>
    /// Adb虚拟分区;
    /// </summary>
    [Serializable]
    public class AdbAllFilesPartition : Partition{
        public AdbAllFilesPartition(List<AnFile> anFiles):base(null) {
            if (anFiles == null)
                throw new ArgumentNullException(nameof(anFiles));

            this.AnFiles = anFiles;
            LoadContent();
        }

        public List<AnFile> AnFiles { get;  }
        private void LoadContent() {
            Children.Clear();
            AnFiles.ForEach(p => {
                if (p.IsDir) {
                    Children.Add(new AdbDirectory(p, this));
                }
                else {
                    Children.Add(new AdbRegFile(p, this));
                }
                //switch (p.IsDir) {
                //    case AnFile.FileType.Directory:
                        
                //        break;
                //    case AnFile.FileType.File:
                        
                //        break;
                //    case AnFile.FileType.Link:
                //        Children.Add(new AdbDirectory(p, this));
                //        break;
                //}
            });
        }
        public override uint? BlockSize {
            get {
                throw new NotImplementedException();
            }
        }

        public override FileSystemType FSType => FileSystemType.EXT4;
    }

    /// <summary>
    /// 安卓备份分区(存储于本地)
    /// </summary>
    [Serializable]
    public class AdbBackUpPartition : Partition {
        public AdbBackUpPartition(DirectoryInfo di, CDFC.Parse.Abstracts.Device parent):base(parent) {
            if (di == null)
                throw new ArgumentNullException(nameof(di));

            this.DirectoryInfo = di;
        }

        //备份文件存在本地的目录;
        public DirectoryInfo DirectoryInfo { get; }

        public override uint? BlockSize => null;

        public override FileSystemType FSType => FileSystemType.EXT4;
    }
    
}
