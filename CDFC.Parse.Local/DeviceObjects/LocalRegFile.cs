using CDFC.Parse.Abstracts;
using System;
using System.IO;
using EventLogger;

namespace CDFC.Parse.Local.DeviceObjects {
    public class LocalRegFile : RegularFile {
        public LocalRegFile(FileInfo fi, LocalDirectory parent):base(parent) {
            this.FileInfo = fi ?? throw new ArgumentNullException(nameof(fi));
        }

        public FileInfo FileInfo { get; }

        public override bool? Deleted => false;
        
        public override string Name => FileInfo.Name;

        public override long Size => FileInfo.Length;

        public override long StartLBA => 0;
        
        public override Stream GetStream(bool isReadOnly = true) {
            try {
                return new FileStream(FileInfo.FullName,FileMode.Open,isReadOnly? FileAccess.Read:FileAccess.ReadWrite);
            }
            catch (Exception ex) {
                Logger.WriteLine($"{nameof(LocalRegFile)}->{nameof(Stream)}:{ex.Message}");
                return null;
            }
        }
    }

    //public class LocalRegFileEx(bool)
}
