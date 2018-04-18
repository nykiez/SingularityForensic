using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.FileExplorer.MessageBoxes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer {
    [Export(typeof(ICustomSignSearchService))]
    public class CustomSignSearchServiceImpl : ICustomSignSearchService {
        public ICustomSignSearchSetting GetSetting() {
            var msg = new SignSearchMessageBox();
            return msg.Show();
        }

        public IEnumerable<(long index, long size)> Search(Stream stream, ICustomSignSearchSetting setting, IProgressReporter reporter) {
            if(setting == null) {
                throw new ArgumentNullException(nameof(setting));
            }

            if(stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            if (setting.AlignToSec) {
                return SearchWithAlignment(stream, setting, reporter);
            }
            else {
                return SearchWithoutAlignment(stream, setting, reporter);
            }
        }

        /// <summary>
        /// 通过对齐的方式搜索;
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="setting"></param>
        /// <param name="reporter"></param>
        /// <returns></returns>
        private IEnumerable<(long index, long size)> SearchWithAlignment(Stream stream, ICustomSignSearchSetting setting, IProgressReporter reporter) {
            var len = stream.Length;
            stream.Position = setting.SecStartLBA * setting.SectorSize;
            
            var loc = stream.Position;
            var buffer = new byte[4096 * setting.SectorSize];
            long lastPosition = -1;
            var keyWord = setting.KeyWord;

            while (loc < len) {
                var readLen = stream.Read(buffer, 0, buffer.Length);
                var readTime = readLen / setting.SectorSize;
                var readIndex = 0;
                while (readIndex < readTime) {
                    if (ByteExtensions.IsHeadSame(buffer,
                        keyWord, keyWord.Length, 
                        readIndex * setting.SectorSize + setting.SecStartLBA)) {
                        if(lastPosition != -1) {
                            yield return (lastPosition, readIndex * setting.SectorSize + loc - lastPosition);
                        }
                        lastPosition = readIndex * setting.SectorSize + loc;
                    }
                    readIndex++;
                }

                loc += readLen;

                if (loc % 104857600 == 0) {
                    reporter?.ReportProgress((int)(loc * 100 / stream.Length));
                }

                if (reporter?.CancelPending??false) {
                    break;
                }
            }
        }

        /// <summary>
        /// 不通过对齐的方式搜索;
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="setting"></param>
        /// <param name="reporter"></param>
        /// <returns></returns>
        private IEnumerable<(long index, long size)> SearchWithoutAlignment(Stream stream, ICustomSignSearchSetting setting, IProgressReporter reporter) {
            stream.Position = setting.SecStartLBA * setting.SectorSize;

            
            var pinLen = setting.KeyWord.Length;						//获得标识长度;
            var readLen = 0;
            var keyWord = setting.KeyWord;

            var bufferLen = 1024 * 1024 * 4 + pinLen - 1;        //缓冲区大小为预缓冲区大小+(pinLen - 1);
            var buffer = new byte[bufferLen];
            long lastPosition = -1;
            long curOffset = 0;

            //将前pinLen - 1位 置为与Ke不等的数字,确保第一次循环时前几位不会影响结果;
            for (var i = 0; i < pinLen - 1; i++) {
                //对于pin[i],遍历所有字节,找到不满足的;
                for (byte j = 0; j < 0xff; j++) {
                    //是否找到相等的结果;
                    if (!keyWord.Any(p => p == j)) {
                        buffer[i] = j;
                        break;
                    }
                }
            }

            
            while ((readLen = stream.Read(buffer, pinLen - 1, bufferLen - pinLen + 1)) != 0) {
                //轮询缓冲区pin剩余长度以后的内容;
                for (var i = 0; i < readLen; i++) {
                    if (buffer[i] != keyWord[0]) {
                        continue;
                    }

                    if (ByteExtensions.IsHeadSame(buffer,keyWord, pinLen - 1, i + 1, 1)) {
                        if (lastPosition != -1) {
                            yield return (lastPosition, curOffset + i - pinLen + 1 - lastPosition);
                        }
                        lastPosition = curOffset + i - pinLen + 1;
                        i += pinLen;
                    }
                }

                //后面未检索的几位向前推移至缓冲区首;
                Buffer.BlockCopy(buffer, readLen, buffer, 0, pinLen - 1);
                curOffset += readLen;
                reporter?.ReportProgress((int)(curOffset * 100 / stream.Length));
                if (reporter?.CancelPending??false) {
                    break;
                }
            }
            
           
        }
    }
}
