
namespace SingularityForensic.Contracts.FileSystem {
    /// <summary>
    /// 文件详细信息提供契约;
    /// </summary>
    public interface IFileDetailInfoProvider {
        //获取文件详细信息;
        string GetAttachedInfo(IFile file);

        //确认是否为可用文件;
        bool CheckIsValidFile(IFile file);
    } 
}
