using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SingularityForensic.FileExplorer.Models {
    public class PartitionRow : FileRowProxy<IPartition>, IPartitionRow {
        public PartitionRow(IPartition part) : base(part) {

        }
    }

    public class FileRow:FileRowProxy<IFile> , IFileRow{
        public FileRow(IFile file):base(file) {
            
        }
#if DEBUG
        ~FileRow() {

        }
#endif
    }




    /// <summary>
    /// 文件行泛基类;
    /// </summary>
    /// <typeparam name="TFile"></typeparam>
    public class FileRowProxy<TFile> : CustomTypeDescriptor,IFileRowProxy<TFile>,INotifyPropertyChanged where TFile: IFile {
#if DEBUG
        public void CheckSubscribed() {
            var notEmpty = this.PropertyChanged != null;
        }


#endif
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
                metaProviders.Select(p => new FileRowPropertyDescriptor(p)).ToArray());

            DescriptorsInitialized = true;
        }
        
        /// <summary>
        /// 添加元数据描述器;
        /// </summary>
        /// <param name="metaProviders"></param>
        internal static void AddDescriptors(IEnumerable<IFileMetaDataProviderProxy<TFile>> metaProviders) {
            if (metaProviders == null) {
                throw new ArgumentNullException(nameof(metaProviders));
            }

            if (!DescriptorsInitialized) {
                throw new InvalidOperationException($"{nameof(FileRowFactory)} hasn't been initialized.");
            }

            foreach (var provider in metaProviders) {
                _filePropDescriptorCollection.Add(new FileRowPropertyDescriptor(provider));
            }
        }

        /// <summary>
        /// 所拥有的所有属性描述器,当且仅当初始化后才可能返回不为空;
        /// </summary>
        internal static IEnumerable<PropertyDescriptor> PropertyDescriptors {
            get {
                if (!DescriptorsInitialized) {
                    yield break;
                }

                if (_filePropDescriptorCollection != null) {
                    var propCount = _filePropDescriptorCollection.Count;
                    for (int i = 0; i < propCount; i++) {
                        yield return _filePropDescriptorCollection[i];
                    }
                }
            }
        }

        public bool IsChecked {
            get {
                var isChecked = File.ExtensibleTag.GetInstance<bool?>(Contracts.FileExplorer.Constants.FileTag_IsChecked);
                return isChecked ?? false;
            }
            set {
                File.ExtensibleTag.SetInstance<bool?>(value, Contracts.FileExplorer.Constants.FileTag_IsChecked);
                NotifyProperty(nameof(IsChecked));
            }
        }

        private static PropertyDescriptorCollection _filePropDescriptorCollection;

        public event PropertyChangedEventHandler PropertyChanged;

        public override PropertyDescriptorCollection GetProperties() {
            return _filePropDescriptorCollection;
        }

        public void NotifyProperty(string propName) {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propName));
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
