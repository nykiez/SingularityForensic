using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using SingularityForensic.Contracts.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EWF;
using SingularityForensic.Contracts.Imaging;
using System.Xml.Linq;
using SingularityForensic.Contracts.App;

namespace SingularityForensic.Imaging {
    /// <summary>
    /// EWF(E01挂载器);
    /// </summary>
    class EWFImgMounter : IImgMounter {
        public EWFImgMounter(string fileName,XElement xElem,FileAccess fileAccess) {
            _rawStream = new EWFStream(fileName, fileAccess);
            var xGroup = xElem.GetGroup(Contracts.Common.Constants.EvidenceProperties);
            foreach (var propItem in _rawStream.Headers) {
                if (string.IsNullOrEmpty(propItem.Value)) {
                    continue;
                }

                xGroup[$"EWF_{propItem.Key}"] = propItem.Value;
            }
            ImgPath = fileName;
        }

        private EWFStream _rawStream;
        public Stream RawStream => _rawStream;

        class EWFStream : Stream {
            public EWFStream(string fileName, FileAccess fileAccess) {
                if (!Handle.CheckFileSignature(fileName)) {
                    throw new InvalidOperationException($"{nameof(fileName)} is not a valid EWF img.");
                }

                _globs = Handle.Glob(fileName);
                if (_globs == null) {
                    throw new Exception($"Unknown error,{nameof(_globs)} is null.");
                }

                Handle = new Handle();

                //使用Read之外的方式打开镜像,EWF取得大小会抛出异常,将暂时使用只读方式打开;
                Handle.Open(_globs, Handle.GetAccessFlagsRead());
                //_fileAccess = fileAccess;
                //switch (_fileAccess) {
                //    case FileAccess.Read:
                        
                //        break;
                //    case FileAccess.Write:
                //        Handle.Open(_globs, Handle.GetAccessFlagsWrite());
                //        break;
                //    case FileAccess.ReadWrite:
                //        Handle.Open(_globs, Handle.GetAccessFlagsReadWrite());
                //        break;
                //    default:
                //        break;
                //}

                if (Handle == null) {
                    throw new Exception($"Unknown error,{nameof(Handle)} is null.");
                }
            }

            public Handle Handle { get; }

            //EWF专用信息键值对;
            private Dictionary<string, string> _headers;
            internal Dictionary<string, string> Headers {
                get {
                    if(_headers == null) {
                        _headers = new Dictionary<string, string>();
                        try {
                            for (int i = 0; i < Handle.GetNumberOfHeaderValues(); i++) {
                                var id = Handle.GetHeaderValueIdentifier(i);
                                _headers.Add(id, Handle.GetHeaderValue(id));
                            }
                        }
                        catch(Exception ex) {
                            LoggerService.Current?.WriteCallerLine(ex.Message);
                        }
                    }
                    return _headers;
                }
            }
            private FileAccess _fileAccess;
            private string[] _globs;

            public override bool CanRead => _fileAccess == FileAccess.Read || _fileAccess == FileAccess.ReadWrite;

            public override bool CanSeek => true;


            public override bool CanWrite => false;
                //=> _fileAccess == FileAccess.Write || _fileAccess == FileAccess.ReadWrite;

            public override long Length {
                get {
                    try {
                        return Handle.SeekOffset(0, SeekOrigin.End);
                    }
                    catch(Exception ex) {
                        LoggerService.Current?.WriteCallerLine(ex.Message);
                        return -1;
                    }
                }
            }

            public override long Position {
                get => Handle.SeekOffset(0, SeekOrigin.Current);
                set => Handle.SeekOffset(value, SeekOrigin.Begin);
            }

            public override void Flush() {
                throw new NotImplementedException();
            }

            public override int Read(byte[] buffer, int offset, int count) {
                if(buffer == null) {
                    throw new ArgumentNullException(nameof(buffer));
                }
                //因为EWF尚未提供针对byte[] + offset的方法,在不使用非安全代码的情况下,此处将做特殊处理;
                //即读取前将起始位置至偏移量的位置的缓冲区保存至临时区域,读取完成后,还原改区域的数据;
                var preBuffer = new byte[offset];
                Buffer.BlockCopy(buffer, 0, preBuffer, 0, offset);

                var readCount = Handle.ReadBuffer(buffer, count + offset);

                Buffer.BlockCopy(preBuffer, 0, buffer, 0, offset);

                return readCount;
            }

            public override long Seek(long offset, SeekOrigin origin) {
                return Handle.SeekOffset(offset, origin);
            }

            public override void SetLength(long value) {
                throw new NotImplementedException();
            }

            public override void Write(byte[] buffer, int offset, int count) {
                if (CanWrite) {
                    throw new InvalidOperationException($"The instance of {nameof(EWFStream)} is readonly.");
                }

                throw new NotImplementedException();
                //_handle.WriteBuffer(buffer,)
            }

            public override void Close() {
                base.Close();
                Handle.Dispose();
            }
        }

        public string Formart => LanguageService.FindResourceString(Constants.EWFStreamFormat);

        public string ImgPath { get; }
        private bool _disposed;
        public void Dispose() {
            if (!_disposed) {
                RawStream.Close();
                _disposed = true;
            }
        }
    }

    /// <summary>
    /// EWF(E01)挂载器提供者;
    /// </summary>
    [Export(typeof(IImgMounterProvider))]
    class EWFImgMounterProvider : IImgMounterProvider {
        public int Sort => 64;

        public string FormatName => LanguageService.Current?.FindResourceString(Constants.EWFStreamFormat);

        public bool CheckIsValidImg(string imgPath) {
            if(imgPath == null) {
                throw new ArgumentNullException(nameof(imgPath));
            }

            if (!File.Exists(imgPath)) {
                throw new FileNotFoundException(imgPath);
            }

            try {
                return Handle.CheckFileSignature(imgPath.Replace("/", "\\"));
            }
            catch(Exception ex) {
                LoggerService.Current?.WriteCallerLine(ex.Message);
                return false;
            }
        }

        public IImgMounter CreateMounter(string imgPath,XElement xElem, FileAccess fileAccess, FileShare fileShare) {
            try {
                var mounter = new EWFImgMounter(imgPath, xElem, fileAccess);
                return mounter;
            }
            catch(Exception ex) {
                LoggerService.Current?.WriteCallerLine(ex.Message);
                throw;
            }
        }
    }
}
