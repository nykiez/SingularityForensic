using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer {
    /// <summary>
    /// 文件元数据提供器(比如列);
    /// </summary>
    public interface IFileMetaDataProvider {
        //原数据名称;
        string MetaDataName { get; }

        //元数据类型;
        Type MetaDataType { get; }
        
        //数据类型针对对象;
        string MetaDataTypeFor { get; }

        //获取元数据;
        object GetDataObject(FileBase file);
        
        //GUID;
        string GUID { get; }

        //排序;
        int Order { get; }

        /// <summary>
        /// 是否自动转换;
        /// </summary>
        bool AutoConvert { get; }

        
    }
}
