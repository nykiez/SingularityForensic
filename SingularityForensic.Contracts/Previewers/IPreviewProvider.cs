using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.Contracts.Previewers {
    /// <summary>
    /// 预览器提供器;
    /// </summary>
    public interface IPreviewProvider  {
        /// <summary>
        /// 创建预览器;
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns>若失败则返回为空</returns>
        IPreviewer CreatePreviewer(Stream stream,string originName);

        /// <summary>
        /// 创建预览器;
        /// </summary>
        /// <param name="fileName">本地文件名</param>
        /// <param name="originName">原始文件名</param>
        /// <remarks>本方法必须在UI线程调用,否则将会产生跨线程调用的异常</remarks>
        /// <returns>若失败则返回为空</returns>
        IPreviewer CreatePreviewer(string fileName,string originName);

        /// <summary>
        /// 是否需要保存到本地;
        /// </summary>
        bool NeedSaveToLocal { get; }

        /// <summary>
        /// 排序;
        /// </summary>
        int Order { get; }
    }

    public interface IPreviewer : IDisposable,IUIObjectProvider {
        /// <summary>
        /// UI元素;
        /// </summary>
        object UIObject { get; }
    }
}
