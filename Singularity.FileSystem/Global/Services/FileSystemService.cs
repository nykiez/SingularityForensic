using System.ComponentModel.Composition;

namespace Singularity.UI.FileSystem.Global.Services {
    public interface IFileSystemService {
        void AddImg();
    }

    //文件系统(显示)服务;
    [Export(typeof(IFileSystemService))]
    public class FileSystemService:IFileSystemService
    {
        public void AddImg() {

        }
    }
}
