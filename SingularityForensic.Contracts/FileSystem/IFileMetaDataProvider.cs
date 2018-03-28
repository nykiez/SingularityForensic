using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    //文件元数据提供器(比如列);
    public interface IFileMetaDataProvider {
        //原数据名称;
        string MetaDataName { get; }

        //元数据类型;
        Type MetaDataType { get; }
        
        //获取元数据;
        object GetDataObject(FileBase file);
        
        //GUID;
        string GUID { get; }
    }
}
