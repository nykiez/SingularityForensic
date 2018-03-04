using SingularityForensic.Contracts.Parse.Abstracts;
using SingularityForensic.Contracts.Parse.Contracts;
using EventLogger;
using System;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using static SingularityForensic.Contracts.Parse.Bytes.BytesExtensions;
using CDFC.Util.PInvoke;
using CDFC.Util.IO;

namespace SingularityForensic.Contracts.Parse.IO {
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
            while((readLen = stream.Read(buffer,pinLen - 1,buffer.Length - pinLen + 1)) != 0) {
                for (int i = 0; i < readLen; i++) {
                    if (IsHeadSame(buffer, key, pinLen, i)) {
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
        public static long SearchBlock(this Stream stream,long beginPosition,int blockSize,
            int blockOffset,byte[] key,Action<long> ntfNowIndex = null,Func<bool> cancelFunc = null) {
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
                        if (IsHeadSame(buffer, key, key.Length, readIndex * blockSize + blockOffset)) {
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
                Logger.WriteLine($"{nameof(StreamExtensions)}->{nameof(SearchBlock)}:{ex.Message}");
                return -1;
            }
            
        }

        //通过文件创建流对象;
        public static Stream CreateStreamByFile(IFile file) {
            if (file != null) {
                try {
                    if (file.Type == FileType.BlockDeviceFile) {                        //若为块文件，则文件为设备或分区;
                        if (file is Partition part) {                                            //若为分区，则根据分区的起始，终止地址构造流;
                            var device = file.GetParent<Device>();
                            if (device != null) {
                                return InterceptStream.CreateFromStream(device.Stream, part.StartLBA, part.Size);
                            }
                        }
                        else if (file is Device device) {                                          //若为设备,直接返回设备流;
                            return device.Stream;
                        }
                    }
                    //否则直接根据StartLBA,EndLBA构建截取流;
                    else if (file is RegularFile regFile) {
                        return regFile.GetStream();
                    }
                    else if (file is IBlockGroupedFile) {
                        var blockGroupedFile = file as IBlockGroupedFile;
                        var part = file.GetParent<Partition>();
                        var device = file.GetParent<Device>();
                        if (part != null && device != null) {
                            var blockSize = part.ClusterSize;
                            var partStream = InterceptStream.CreateFromStream(device.Stream, part.StartLBA, part.Size);
                            //若块组不为空,则遍历块组组成虚拟流;
                            if (blockGroupedFile.BlockGroups != null) {
                                var ranges = blockGroupedFile.BlockGroups.Select(p =>
                                    ValueTuple.Create(p.BlockAddress * blockSize + p.Offset, p.Count * blockSize)).ToArray();

                                var blockSub = ranges.Sum(p => p.Item2) - file.Size;
                                if (ranges?.Count() > 0 && 0 < blockSub && blockSub < blockSize) {
                                    ranges[ranges.Count() - 1].Item2 -= blockSub;
                                }
                                var multiStream = MulPeriodsStream.CreateFromStream(partStream, ranges);
                                return multiStream;
                            }

                        }
                    }
                }
                catch (Exception ex) {
                    Logger.WriteLine($"{nameof(StreamExtensions)}->{nameof(CreateStreamByFile)}:{ex.Message}");
                }

            }

            return null;
        }

        //得到一个可使用的文件流;
        public static FileStream CreateAValidFS(string targetPath,string name) {
            var fullName = targetPath + name;
            try {
                var fs = File.Create(fullName);
                return fs;
            }
            //可能文件正在被占用;
            catch(IOException ex) {
                string preName = string.Empty;
                string ext = string.Empty;

                var lDotIndex = fullName.LastIndexOf(".");
                if(lDotIndex != -1) {
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
            catch(Exception ex) {
                Logger.WriteLine($"{nameof(StreamExtensions)}->{nameof(CreateAValidFS)}:{ex.Message}");
                throw;
            }
            
        }

        [HandleProcessCorruptedStateExceptions]
        public static T? GetStucture<T>(this Stream stream) where T:struct {
            var len = Marshal.SizeOf(typeof(T));
            if(stream.Position + len > stream.Length) {
                return null;
            }
            else {
                var buffer = new byte[len];
                try {
                    stream.Read(buffer, 0, len);
                    return buffer.GetStruture<T>();
                }
                catch(Exception ex) {
                    Logger.WriteLine($"{nameof(StreamExtensions)}->{nameof(GetStucture)}:{ex.Message}");
                    return null;
                }
                
            }
        }

        [HandleProcessCorruptedStateExceptions]
        public static T? GetStruture<T>(this byte[] buffer) where T:struct {
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
