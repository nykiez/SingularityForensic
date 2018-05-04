using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SingularityForensic.FileExplorer.Models {
    public class FileRow:FileRowProxy<IFile> , IFileRow{
        public FileRow(IFile file):base(file) {
            
        }
        

    }

    public class PartitionRow : FileRowProxy<IPartition>,IPartitionRow {
        public PartitionRow(IPartition part) : base(part){
            
        }
    }
    
    /// <summary>
    /// 文件行泛基类;
    /// </summary>
    /// <typeparam name="TFile"></typeparam>
    public class FileRowProxy<TFile> : CustomTypeDescriptor,IFileRowProxy<TFile> where TFile: IFile {
        public FileRowProxy(TFile file) {
            this.File = file;
        }

        public TFile File { get; }

        /// <summary>
        /// 数据提供器是否被初始化;
        /// </summary>
        internal static bool DescriptorsInitialized { get; private set; }

        /// <summary>
        /// 初始化属性描述器;
        /// </summary>
        /// <param name="metaProviders"></param>
        internal static void InitializeDescriptors(IEnumerable<IFileMetaDataProviderProxy<TFile>> metaProviders) {
            if(metaProviders == null) {
                throw new ArgumentNullException(nameof(metaProviders));
            }

            _filePropDescriptorCollection = new PropertyDescriptorCollection(
                metaProviders.Select(p => new FileRowPropertyDescriptor(p)).ToArray(),true);

            DescriptorsInitialized = true;
        }
        
        /// <summary>
        /// 所拥有的所有属性描述器,当且仅当初始化后才可能返回不为空;
        /// </summary>
        internal static IEnumerable<PropertyDescriptor> PropertyDescriptors {
            get {
                if (!DescriptorsInitialized) {
                    yield return null;
                }

                if (_filePropDescriptorCollection != null) {
                    var propCount = _filePropDescriptorCollection.Count;
                    for (int i = 0; i < propCount; i++) {
                        yield return _filePropDescriptorCollection[i];
                    }
                }
            }
        }

        private static PropertyDescriptorCollection _filePropDescriptorCollection;
        
        public override PropertyDescriptorCollection GetProperties() {
            return _filePropDescriptorCollection;
        }

        public class FileRowPropertyDescriptor  : PropertyDescriptor {
            public override string DisplayName => FileMetaDataProvider.MetaDataName;
            public FileRowPropertyDescriptor(IFileMetaDataProviderProxy<TFile> fileMetaDataProvider) : base(fileMetaDataProvider.GUID, new Attribute[0]) {
                FileMetaDataProvider = fileMetaDataProvider ?? throw new ArgumentNullException(nameof(fileMetaDataProvider));
            }

            public IFileMetaDataProviderProxy<TFile> FileMetaDataProvider { get; }
            public override Type ComponentType => typeof(FileRowProxy<TFile>);

            public override bool IsReadOnly => true;

            public override Type PropertyType => FileMetaDataProvider.MetaDataType;

            public override bool CanResetValue(object component) => false;

            public override object GetValue(object component) {
                if (component is IFileRowProxy<TFile> row) {
                    return FileMetaDataProvider.GetMetaData(row.File);
                }
                throw new InvalidCastException();
            }

            public override void ResetValue(object component) {
                throw new NotImplementedException();
            }

            public override void SetValue(object component, object value) {
                throw new NotImplementedException();
            }

            public override bool ShouldSerializeValue(object component) {
                return false;
            }
        }


    }
    
   
    
    


}
