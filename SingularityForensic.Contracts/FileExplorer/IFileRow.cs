using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer {
    public interface IFileRowProxy<TFile> where TFile : IFile {
        TFile File { get; }
    }

    public interface IFileRow : IFileRowProxy<IFile> {
        
    }

    public interface IPartitionRow : IFileRowProxy<IPartition> {

    }

    public interface IFileRowFactoryProxy<TFile,TFileRow> where TFile : IFile where TFileRow:IFileRowProxy<TFile> {
        /// <summary>
        /// 创建一个行单元;
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        TFileRow CreateFileRow(TFile file);
        /// <summary>
        /// 初始化属性描述器;
        /// </summary>
        /// <param name="metaProviders"></param>
        void InitializeDescriptors(IEnumerable<IFileMetaDataProviderProxy<TFile>> metaProviders);
        /// <summary>
        /// 属性描述器是否已经被初始化;
        /// </summary>
        bool DescriptorsInitialized { get; }
        /// <summary>
        /// 所有属性描述器;
        /// </summary>
        IEnumerable<PropertyDescriptor> PropertyDescriptors { get; }
    }

    public interface IFileRowFactory:IFileRowFactoryProxy<IFile,IFileRow> {
        
    }

    public class FileRowFactory: GenericServiceStaticInstance<IFileRowFactory> {

    }

    public interface IPartitionRowFactory : IFileRowFactoryProxy<IPartition,IPartitionRow> {

    }

    public class PartitionRowFactory : GenericServiceStaticInstance<IPartitionRowFactory> {

    }

}
