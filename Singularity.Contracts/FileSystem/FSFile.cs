using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Singularity.Contracts.FileSystem {
    public class FSFile {
        public FSFile(IFile file) {
            File = file ?? throw new ArgumentNullException(nameof(file));
        }

        public IFile File { get; }

        public IEnumerable<FSFile> Files { get; }

        public virtual string Name => File.Name;

        public virtual FileType Type => File.Type;

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

        public IEnumerable<FSFile> GetDirectories() => Children?.Where(p => p.Type == FileType.Directory);

        public IEnumerable<FSFile> GetFiles() => Children?.Where(p => p.Type == FileType.RegularFile);
    }
}
