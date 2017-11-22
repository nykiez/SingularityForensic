using CDFC.Parse.Contracts;
using System;
using System.IO;

namespace Singularity.Interfaces {
    //将流解析为可用文件;
    public interface IStreamFileParser {
        //检查是否为可用流;
        bool CheckIsValid(Stream stream);
        IFile ParseStream(Stream stream,Action<(double totalPro, double detailPro, string word,string desc)> ntfAct);
    }
}
