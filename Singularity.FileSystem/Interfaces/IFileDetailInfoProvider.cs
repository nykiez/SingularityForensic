using CDFC.Parse.Contracts;

namespace Singularity.UI.FileSystem.Interfaces {
    /// <summary>
    /// 文件详细信息提供契约;
    /// </summary>
    public interface IFileDetailInfoProvider {
        //获取文件详细信息;
        string GetAttachedInfo(IFile file);
    }
}
