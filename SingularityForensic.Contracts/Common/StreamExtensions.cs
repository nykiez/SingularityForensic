using CDFC.Util.PInvoke;
using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static SingularityForensic.Contracts.Common.ByteExtensions;

namespace SingularityForensic.Contracts.Common {
    //流拓展方法;
    public static class StreamExtensions {
        private const int bufferConstSize = 10485760;                           //单次轮询的吞吐量;

        public static long Search(this Stream stream, long beginPosition, byte[] key, Action<long> ntfNowIndex = null,
            Func<bool> isCancelFunc = null) {
            if (stream == null || stream.Length <= beginPosition)
                return -1;
            if (key == null || stream.Length < key.Length)
                return -1;
            if (!stream.CanRead) {
                return -1;
            }

            var pinLen = key.Length;
            var buffer = new byte[bufferConstSize + pinLen - 1];
            var readLen = 0;
            long readIndex = 0;

            stream.Position = beginPosition;

            //初始化,确定前key.Length - 1位不满足条件;
            for (int i = 0; i < pinLen - 1; i++) {
                buffer[i] = 0xff;
            }

            //轮询查找;通过前后错位进行搜索;
            while ((readLen = stream.Read(buffer, pinLen - 1, buffer.Length - pinLen + 1)) != 0) {
                for (int i = 0; i < readLen; i++) {
                    if (buffer.IsHeadSame(key, pinLen, i)) {
                        return beginPosition + readIndex + i - pinLen + 1;
                    }
                }

                if (isCancelFunc?.Invoke() ?? false) {                            //若已经取消，则立即退出;
                    return -1;
                }

                readIndex += readLen;
                Buffer.BlockCopy(buffer, 0, buffer, readLen, pinLen - 1);
                ntfNowIndex?.Invoke(readIndex + beginPosition);
            }

            return -1;
        }

        private const int bufferBlockTime = 20480;
        public static long SearchBlock(this Stream stream, long beginPosition, int blockSize,
            int blockOffset, byte[] key, Action<long> ntfNowIndex = null, Func<bool> cancelFunc = null) {
            var buffer = new byte[bufferBlockTime * blockSize];
            try {
                var len = stream.Length;
                beginPosition += blockSize - (beginPosition % blockSize != 0 ? beginPosition % blockSize : blockSize);

                stream.Position = beginPosition;

                var loc = stream.Position;

                while (loc < len) {
                    var readLen = stream.Read(buffer, 0, buffer.Length);
                    var readTime = readLen / blockSize;
                    var readIndex = 0;
                    while (readIndex < readTime) {
                        if (buffer.IsHeadSame(key, key.Length, readIndex * blockSize + blockOffset)) {
                            return loc + readIndex * blockSize + blockOffset;
                        }
                        readIndex++;
                    }

                    loc += readLen;



                    if ((loc - beginPosition) % 104857600 == 0) {
                        ntfNowIndex?.Invoke(loc);
                    }

                    if (cancelFunc?.Invoke() ?? false || readLen == 0) {
                        return -1;
                    }
                }
                return -1;
            }
            catch (Exception ex) {
                LoggerService.Current?.WriteLine($"{nameof(StreamExtensions)}->{nameof(SearchBlock)}:{ex.Message}");
                return -1;
            }

        }

        //得到一个可使用的文件流;
        public static FileStream CreateAValidFS(string targetPath, string name) {
            var fullName = targetPath + name;
            try {
                var fs = File.Create(fullName);
                return fs;
            }
            //可能文件正在被占用;
            catch (IOException ex) {
                string preName = string.Empty;
                string ext = string.Empty;

                var lDotIndex = fullName.LastIndexOf(".");
                if (lDotIndex != -1) {
                    preName = fullName.Substring(0, lDotIndex);
                    ext = fullName.Substring(lDotIndex);

                }
                else {
                    preName = fullName;
                }

                for (int i = 0; i < 16; i++) {
                    try {
                        var fs = File.Create($"{preName}_{i}.{ext}");
                        return fs;
                    }
                    catch {

                    }
                }

                return null;
            }
            catch (Exception ex) {
                LoggerService.Current?.WriteLine($"{nameof(StreamExtensions)}->{nameof(CreateAValidFS)}:{ex.Message}");
                throw;
            }

        }

        [HandleProcessCorruptedStateExceptions]
        public static T? GetStucture<T>(this Stream stream) where T : struct {
            var len = Marshal.SizeOf(typeof(T));
            if (stream.Position + len > stream.Length) {
                return null;
            }
            else {
                var buffer = new byte[len];
                try {
                    stream.Read(buffer, 0, len);

                    return buffer.GetStruture<T>();
                }
                catch (Exception ex) {
                    LoggerService.Current?.WriteLine($"{nameof(StreamExtensions)}->{nameof(GetStucture)}:{ex.Message}");
                    return null;
                }

            }
        }

        [HandleProcessCorruptedStateExceptions]
        public static T? GetStruture<T>(this byte[] buffer) where T : struct {
            var len = Marshal.SizeOf(typeof(T));
            if (buffer?.Length != len) {
                return null;
            }

            var ptr = Marshal.AllocHGlobal(len);

            try {
                Marshal.Copy(buffer, 0, ptr, len);
                return ptr.GetStructure<T>();
            }
            catch {
                return null;
            }
            finally {
                Marshal.FreeHGlobal(ptr);
            }
        }

    }
}
