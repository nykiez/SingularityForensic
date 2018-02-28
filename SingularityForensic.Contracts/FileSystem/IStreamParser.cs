using CDFC.Parse.Contracts;
using SingularityForensic.Contracts.Case;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    //将镜像解析为可用文件解析器契约;
    public interface IImgParser {
        //检查是否为可用流;
        bool CheckIsValid(string path);
        //解析动作;
        IFile ParseStream(string path, Action<(int totalPro, int detailPro, string word, string desc)> ntfAct, Func<bool> isCancel);

        //案件文件管理器;
        ICaseManager CaseManager { get; }

        //排序号;
        int SortNum { get; }
    }
}
