using CDFC.Parse.Contracts;
using Singularity.UI.Case.Contracts;
using System;

namespace Singularity.UI.FileSystem.Interfaces {
    //文件系统服务提供者契约;
    public interface IFileSystemServiceProvider {
        //获取文件系统相关服务;
        TService GetService<TService>();
        object GetService(Type serviceType);

        //流解析器;
        IStreamFileParser StreamFileParser { get;}
        //是否为可用的案件文件;
        bool CheckIsValid(ICaseFile file);

        /// <summary>
        /// //向案件中加入文件;
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="interLabel">内部标识</param>
        void AddNewCaseFile(IFile file,string interLabel);

    }
}
