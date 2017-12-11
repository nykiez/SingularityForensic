using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using EventLogger;
using Microsoft.Win32.SafeHandles;
using System;
using System.IO;

namespace CDFC.Parse.DeviceObjects {
    public class UnKnownDevice : Device, IHaveHandle {
        public override PartsType PartsType => PartsType.Unknown;

        private int? secSize;
        public override int SecSize {
            get {
                return secSize != null ? secSize.Value : 512;
            }
            protected set {
                secSize = value;
            }
        }

        private long? _size;
        public override long Size {
            get {
                if (_size == null) {
                    if (Stream != null) {
                        _size = Stream.Length;
                    }
                }
                return _size ?? 0;
            }
        }

        private Stream _stream;
        public override Stream Stream => _stream;

        public SafeFileHandle Handle {
            get {
                if (Stream is FileStream) {
                    return (Stream as FileStream).SafeFileHandle;
                }
                return null;
            }
        }

        public static UnKnownDevice LoadFromPath(string path, bool readOnly = true) {
            FileStream fs = null;

            try {
                fs = File.Open(path, FileMode.Open, readOnly ? FileAccess.Read : FileAccess.ReadWrite, FileShare.ReadWrite);
                return LoadFromFileStream(fs);
            }
            catch (Exception ex) {
                Logger.WriteLine($"{nameof(UnKnownDevice)} -> {nameof(LoadFromPath)}:{ex.Message}");
                fs?.Close();
                throw new Exception("Failed to load img!", ex);
            }

        }

        public static UnKnownDevice LoadFromFileStream(FileStream fs) {
            var device = new UnKnownDevice {
                _stream = fs,
                Name = fs.Name.Substring(fs.Name.LastIndexOf("\\") + 1),
                Children = new System.Collections.Generic.List<IFile>()
            };

            return device;
        }

        public override void Exit() {
            Stream?.Close();
            _stream = null;
            //throw new NotImplementedException();
        }
    }
}
