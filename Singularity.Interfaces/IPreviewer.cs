using System;
using System.Windows;

namespace Singularity.Interfaces {
    /// <summary>
    /// 预览器契约;
    /// </summary>
    public interface IPreviewer : IDisposable {
        //UI元素;
        UIElement View { get; }
    }

    /// <summary>
    /// 预览器提供器;
    /// </summary>
    public interface IPreviewerProvider {
        IPreviewer GetPreviewer(string fileName);
    }
}
