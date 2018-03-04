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

    public interface IStreamParser {
        bool CheckIsValidStream(Stream stream);
        //解析文件;
        FileEn ParseStream(Stream stream,Action<(int pro,int detailPro)> ntfAct);

        
    }


    //介质->流服务器;
    public interface IStreamProvider {
        //检查是否为可用的证据项;
        bool CheckIsValidCaseEvidence(CaseEvidence csEvidence);

        //得到流;
        Stream GetStream(CaseEvidence csEvidence);

        string StreamTypeGuid { get; }
    }

    //镜像流提供者;
    public interface IStreamProvider {
        //检查是否为可用的证据项;
        bool CheckIsValidCaseEvidence(CaseEvidence csEvidence);

    }

}
