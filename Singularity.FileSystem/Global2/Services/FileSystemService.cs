using SingularityForensic.Modules.FileSystem.ViewModels;
using System;
using System.ComponentModel.Composition;

namespace SingularityForensic.Modules.FileSystem.Global.Services {
    //文件系统(显示)服务;
    [Export]
    public class FileSystemService
    {
        [Import]
        Lazy<MainPageViewModel> VM;
        
        //public void AddImg() {
        //    VM?.Value?.AddImgCommand?.Execute();
        //}
    }
}
