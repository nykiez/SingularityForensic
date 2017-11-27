using CDFC.Parse.Contracts;
using System;
using System.IO;

namespace Singularity.UI.FileSystem.Interfaces {
    //将流解析为可用文件;
    public interface IStreamFileParser {
        //检查是否为可用流;
        bool CheckIsValid(Stream stream);
        IFile ParseStream(Stream stream, Action<(int totalPro, int detailPro, string word, string desc)> ntfAct,Func<bool> isCancel);
    }
}
