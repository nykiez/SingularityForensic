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
    /// <typeparam name="TFile">文件本体类型</typeparam>
    /// <typeparam name="TOwnerFile">从属文件本体类型</typeparam>
    public interface IFileMetaDataProviderProxy<TFile> where TFile:FileBase { 
        //原数据名称;
        string MetaDataName { get; }

        //元数据类型;
        Type MetaDataType { get; }

        //获取元数据;
        object GetDataObject(TFile file);
        
        //GUID;
        string GUID { get; }

        //排序;
        int Order { get; }

        /// <summary>
        /// 是否自动转换;
        /// </summary>
        bool AutoConvert { get; }

        
    }

    /// <summary>
    /// 用于文件/目录浏览器的元数据提供器;
    /// </summary>
    public interface IFileMetaDataProvider:IFileMetaDataProviderProxy<FileBase> {

    }

    /// <summary>
    /// 用于设备/分区浏览器的元数据提供器;
    /// </summary>
    public interface IPartMetaDataProvider: IFileMetaDataProviderProxy<Partition> {

    }
}
