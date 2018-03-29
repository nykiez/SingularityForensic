using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SingularityForensic.Contracts.FileSystem {
    
    public interface IStreamParsingProvider {
        //检查是否为可用的流;
        bool CheckIsValidStream(Stream stream);

        /// <summary>
        /// //解析流;
        /// </summary>
        /// <param name="stream">数据</param>
        /// <param name="name">数据名称</param>
        /// <param name="xElem">可拓展描述内容</param>
        /// <param name="reporter">进度通知单位</param>
        /// <returns></returns>
        FileBase ParseStream(Stream stream,string name,XElement xElem, ProgressReporter reporter);

        int Order { get; }

        /// <summary>
        /// 标识GUID;
        /// </summary>
        string GUID { get; }
    }

}
