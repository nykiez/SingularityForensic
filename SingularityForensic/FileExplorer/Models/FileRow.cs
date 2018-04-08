﻿using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer.Models {
    public class FileRow:FileRowProxy<FileBase> {
        public FileRow(FileBase file):base(file) {
            
        }
    }
    public class PartitionRow : FileRowProxy<Partition> {
        public PartitionRow(Partition part) : base(part){
            
        }
    }

    /// <summary>
    /// 文件行泛基类;
    /// </summary>
    /// <typeparam name="TFile"></typeparam>
    public class FileRowProxy<TFile> : CustomTypeDescriptor where TFile : FileBase {
        public FileRowProxy(TFile file) {
            this.File = file;
        }

        public TFile File { get; }

        /// <summary>
        /// 初始化数据提供器是否被初始化;
        /// </summary>
        internal static bool DescriptorsInitialized = false;

        /// <summary>
        /// 初始化属性描述器;
        /// </summary>
        /// <param name="metaProviders"></param>
        internal static void InitializeDescripters(IEnumerable<IFileMetaDataProviderProxy<TFile>> metaProviders) {
            if(metaProviders == null) {
                throw new ArgumentNullException(nameof(metaProviders));
            }

            _filePropDescriptorCollection = new PropertyDescriptorCollection(
                metaProviders.Select(p => new FileRowPropertyDescriptor(p)).ToArray(),true);

            DescriptorsInitialized = true;
        }
        

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

        public class FileRowPropertyDescriptor : PropertyDescriptor {
            public FileRowPropertyDescriptor(IFileMetaDataProviderProxy<TFile> fileMetaDataProvider) : base(fileMetaDataProvider.MetaDataName, new Attribute[0]) {
                FileMetaDataProvider = fileMetaDataProvider ?? throw new ArgumentNullException(nameof(fileMetaDataProvider));
            }

            public IFileMetaDataProviderProxy<TFile> FileMetaDataProvider { get; }
            public override Type ComponentType => typeof(FileRow);

            public override bool IsReadOnly => true;

            public override Type PropertyType => FileMetaDataProvider.MetaDataType;

            public override bool CanResetValue(object component) => false;

            public override object GetValue(object component) {
                if (component is FileRowProxy<TFile> row) {
                    return FileMetaDataProvider.GetDataObject(row.File);
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
