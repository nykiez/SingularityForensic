using CDFC.Parse.Modules.Contracts;
using System;
using System.Collections.Generic;
using static CDFC.Parse.Bytes.BytesExtensions;
using EventLogger;
using System.IO;
using System.Linq;

namespace CDFC.Parse.Modules.Pictures {
    /// <summary>
    /// 自定义签名搜索器;
    /// </summary>
    public class SignSearcher : IFileSearcher {
        public SignSearcher(Stream stream,byte[] keyWord,int maxSize,int searchBlockSize = 512,int offset = 0) {
            if(searchBlockSize <= 0) {
                throw new ArgumentException(nameof(searchBlockSize));
            }
            
            this.SearchSectorSize = searchBlockSize;
            this.KeyWord = keyWord;
            this.BaseStream = stream;
            this.MaxSize = maxSize;
            this.SecStartLBA = offset;
        }
        public Stream BaseStream { get; }
        public int SecStartLBA { get; }                         //扇区起始偏移;
        public int MaxSize { get; }
        public byte[] KeyWord;
        public int SearchSectorSize { get; }

        public bool AlignToSector { get; set; }                 //是否按照扇区对齐的方式搜索;

        private string fileExtension;
        public string FileExtension {
            get {
                return fileExtension;
            }
            set {
                fileExtension = value;
            }
        }

        private int curFileCount = 0;
        public int CurFileCount => curFileCount;
        public event EventHandler<int> CurFileCountChanged;

        private long curOffset;
        public long CurOffset => curOffset;
        public event EventHandler<long> CurOffsetChanged;

        public void Dispose() {
            
        }

        private List<long> poses;
        public List<IFileNode> GetFileList(string extensionName) {
            try {
                var nodes = new List<IFileNode>();
                
                var shIndex = 0;

                for (int i = 0; i < poses.Count; i++) {
                    ulong? end;
                    if (i == poses.Count - 1) {
                        end = (ulong)(poses[i] + Math.Min(BaseStream.Length - poses[i], MaxSize));
                    }
                    else if (i != 0) {
                        end = (ulong)(poses[i] + Math.Min(poses[i + 1] - poses[i], MaxSize));
                    }
                    else {
                        if(poses.Count == 1) {
                            end = (ulong)(poses[i] + Math.Min(BaseStream.Length - poses[i], MaxSize)); 
                        }
                        else {
                            end = (ulong)(poses[i] + Math.Min(poses[i + 1] - poses[i], MaxSize)); 
                        }
                    }
                    if(end != null) {
                        
                        var stNode = new StPhotoFileNode {
                            Start = (ulong)poses[i],
                            End = end.Value,
                            Name = $"Searched_File{shIndex++}.{FileExtension}",
                            FileSize = (ulong)(end - (ulong)poses[i])
                        };
                        var node = new FileNode { StPhotoFileNode = stNode };
                        nodes.Add(node);
                    }
                }
                
                return nodes;
            }
            catch(Exception ex) {
                Logger.WriteLine($"{nameof(SignSearcher)}->{nameof(GetFileList)}:{ex.Message}");
                return null;
            }
            
        }
        
        public bool SearchStart(long startLBA, long byteCount) {
            var stream = BaseStream;
            if(stream != null) {
                if (AlignToSector) {
                    return DoSearchAlignToOffset(stream, startLBA);
                }
                else {
                    return DoSearch(stream, startLBA);
                }
            }
            else {
                Logger.WriteLine($"{nameof(SignSearcher)}->{nameof(SearchStart)}:{nameof(stream)} can't be null!");
                return false;
            }
        }
        
        /// <summary>
        /// 按照扇区对齐的方式搜索;
        /// </summary>
        private bool DoSearchAlignToOffset(Stream stream, long startLBA) {
            var len = stream.Length;
            stream.Position = startLBA;
            var loc = stream.Position;
            var buffer = new byte[4096 * SearchSectorSize];

            poses = new List<long>();

            try {
                while (loc < len) {
                    var readLen = stream.Read(buffer, 0, buffer.Length);
                    var readTime = readLen / SearchSectorSize;
                    var readIndex = 0;
                    while (readIndex < readTime) {
                        if (IsHeadSame(buffer, KeyWord, KeyWord.Length, readIndex * SearchSectorSize + SecStartLBA)) {
                            poses.Add(readIndex * SearchSectorSize + loc);
                            CurFileCountChanged?.Invoke(this, poses.Count);
                        }
                        readIndex++;
                    }

                    loc += readLen;
                    curOffset = loc;

                    if (loc % 104857600 == 0) {
                        CurOffsetChanged?.Invoke(this, loc);
                    }

                    if (stopped || readLen == 0) {
                        break;
                    }
                }
                return true;
            }
            catch (Exception ex) {
                Logger.WriteLine($"{nameof(SignSearcher)}->{nameof(SearchStart)}:{ex.Message}");
                return false;
            }
            finally {

            }
        }

        /// <summary>
        /// 不按照扇区对齐的方式搜索;
        /// </summary>
        private bool DoSearch(Stream stream, long startLBA) {
            stream.Position = startLBA;
            var pinLen = KeyWord.Length;						//获得标识长度;
            var readLen = 0;

            var bufferLen = 1024 * 1024 * 16 + pinLen - 1;        //缓冲区大小为预缓冲区大小+(pinLen - 1);
            var buffer = new byte[bufferLen];

            poses = new List<long>();

            //将前pinLen - 1位 置为与Ke不等的数字,确保第一次循环时前几位不会影响结果;
            for (var i = 0; i < pinLen - 1; i++) {
                //对于pin[i],遍历所有字节,找到不满足的;
                for (byte j = 0; j < 0xff; j++) {
                    //是否找到相等的结果;
                    if(!KeyWord.Any(p => p == j)) {
                        buffer[i] = j;
                        break;
                    }
                }
            }

            try {
                while ((readLen = stream.Read(buffer, pinLen - 1, bufferLen - pinLen + 1)) != 0) {
                    //轮询缓冲区pin剩余长度以后的内容;
                    for (var i = 0; i < readLen; i++) {
                        //if(curOffset + i - pinLen + 1 == 11117056) {
                        //    var s = buffer[i];
                        //}
                        if (buffer[i] == KeyWord[0]) {
                            if (IsHeadSame(buffer, KeyWord, pinLen - 1, i + 1, 1)) {
                                poses.Add(curOffset + i - pinLen + 1);
                                i += pinLen;
                            }
                        }
                    }
                    
                    //后面未检索的几位向前推移至缓冲区首;
                    Buffer.BlockCopy(buffer, readLen, buffer, 0, pinLen - 1);
                    curOffset += readLen;
                    CurOffsetChanged?.Invoke(this, curOffset);
                    if (stopped) {
                        break;
                    }
                }
                return true;
            }
            catch(Exception ex) {
                LoggerService.Current?.WriteCallerLine(ex.Message);
                return false;
            }
        }
        
        private bool stopped = false;
        public bool Stop() {
            stopped = true;
            return true;
        }
    }
    
}
