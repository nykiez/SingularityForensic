using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Case;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    public interface IFileSystemService {
        /// <summary>
        /// 打开某个文件;
        /// </summary>
        /// <param name="evidence">证据</param>
        /// <param name="fileName"></param>
        /// GUID为案件文件的GUID;
        /// <example>{GUID}/"" 空字符串:可能返回设备</example>
        /// <example>{GUID}/"0" 返回第一个分区</example>
        /// <example>{GUID}/"0/file.ext" 返回第一个分区下的一个叫做file.ext的文件</example>
        /// <returns>{GUID}/</returns>
        FSFile OpenFile(string fileName);

        //挂载(到案件上);
        IFile MountImg(string path);

        //脱机(从案件中);
        //void UnMount(CaseEvidence csFile);
    }

    
}
