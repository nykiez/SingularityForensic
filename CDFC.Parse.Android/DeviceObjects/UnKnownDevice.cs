using CDFC.Parse.Abstracts;
using System;
using CDFC.Parse.Contracts;
using System.IO;
using EventLogger;
using Microsoft.Win32.SafeHandles;

namespace CDFC.Parse.Android.DeviceObjects {
    public class UnKnownDevice : Device ,IHandleDevice{
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
                if(_size == null) {
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
                if(Stream is FileStream) {
                    return (Stream as FileStream).SafeFileHandle;
                }
                return null;
            }
        }

        public static UnKnownDevice LoadFromPath(string path, bool readOnly = true) {
            FileStream fs = null;

            try {
                fs = File.Open(path, FileMode.Open, readOnly ? FileAccess.Read : FileAccess.ReadWrite, FileShare.ReadWrite);
                var device = new UnKnownDevice {
                    _stream = fs,
                    Name = path.Substring(path.LastIndexOf("\\") + 1),
                    Children = new System.Collections.Generic.List<IFile>()
                };
                
                return device;
            }
            catch (Exception ex) {
                Logger.WriteLine($"{nameof(AndroidDevice)} -> {nameof(LoadFromPath)}:{ex.Message}");
                fs?.Close();
                throw new Exception("Failed to load img!", ex);
            }

        }

        public override void Exit() {
            Stream?.Close();
            _stream = null;
            //throw new NotImplementedException();
        }
    }
}
