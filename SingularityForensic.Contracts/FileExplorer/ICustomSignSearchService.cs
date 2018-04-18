using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer {
    /// <summary>
    /// 自定义签名扫描设定;
    /// </summary>
    public interface ICustomSignSearchSetting {
        int MaxSize { get;  }
        byte[] KeyWord { get; }

        bool AlignToSec { get; } 
        int SectorSize { get; }
        int SecStartLBA { get; }

        string FileExtension { get; }
    }

    /// <summary>
    /// 自定义签名扫描设定契约;
    /// </summary>
    public interface ICustomSignSearchService {
        /// <summary>
        /// 获得签名设(对话框);
        /// </summary>
        /// <returns></returns>
        ICustomSignSearchSetting GetSetting();
        /// <summary>
        /// 开始搜索;
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="setting"></param>
        /// <param name="reporter"></param>
        /// <returns></returns>
        IEnumerable<(long index,long size)> Search(Stream stream,ICustomSignSearchSetting setting,IProgressReporter reporter);
    }

    public class CustomSignSearchService:GenericServiceStaticInstance<ICustomSignSearchService> {
        public static ICustomSignSearchSetting GetSetting() => Current?.GetSetting();
        public static IEnumerable<(long index, long size)> Search(Stream stream, ICustomSignSearchSetting setting, IProgressReporter reporter)
            => Current?.Search(stream, setting, reporter);
    }
}
