using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

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

        /// <summary>
        /// 获取元数据;
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        object GetMetaData(TFile file);

        /// <summary>
        /// GUID;
        /// </summary>
        string GUID { get; }

        /// <summary>
        /// 排序;
        /// </summary>
        int Order { get; }
        
        /// <summary>
        /// 转换器,当且仅当DataTemplate为空时才可非空;
        /// </summary>
        IValueConverter Converter { get; }

        /// <summary>
        /// 用于自定义组成;
        /// </summary>
        DataTemplate CellTemplate { get; }
    }

    /// <summary>
    /// 用于文件/目录浏览器的元数据提供器;
    /// </summary>
    public interface IFileMetaDataProvider:IFileMetaDataProviderProxy<FileBase> {
        
    }

    public abstract class FileMetaDataProvider : IFileMetaDataProvider {
        public abstract string MetaDataName { get; }

        public abstract Type MetaDataType { get; }

        public abstract string GUID { get; }

        public abstract int Order { get; }

        public virtual IValueConverter Converter => null;

        public virtual DataTemplate CellTemplate => null;

        public abstract object GetMetaData(FileBase file);
    }

    /// <summary>
    /// 用于设备/分区浏览器的元数据提供器;
    /// </summary>
    public interface IPartitionMetaDataProvider: IFileMetaDataProviderProxy<Partition> {

    }

    public abstract class PartitionMetaDataProvider:IPartitionMetaDataProvider {
        public abstract string MetaDataName { get; }

        public abstract Type MetaDataType { get; }

        public abstract string GUID { get; }

        public abstract int Order { get; }

        public virtual IValueConverter Converter => null;

        public virtual DataTemplate CellTemplate => null;

        public abstract object GetMetaData(Partition file);
    }
}
