using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.FileExplorer.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.FileExplorer {
    [Export(typeof(IFileRowFactory))]
    class FileRowFactoryImpl : IFileRowFactory {
        [ImportingConstructor]
        public FileRowFactoryImpl([ImportMany]IEnumerable<IFileMetaDataProvider> fileMetaDataProviders) {
            FileRow.InitializeDescriptors(fileMetaDataProviders.OrderBy(p => p.Order));
        }

        public bool DescriptorsInitialized => FileRow.DescriptorsInitialized;
        
        public IEnumerable<PropertyDescriptor> PropertyDescriptors => FileRow.PropertyDescriptors;

        public void AddDescriptors(IEnumerable<IFileMetaDataProviderProxy<IFile>> metaProviders) {
            FileRow.AddDescriptors(metaProviders);
        }

        public IFileRow CreateFileRow(IFile file) => new FileRow(file);
    }

    [Export(typeof(IPartitionRowFactory))]
    class PartitionRowFactoryImpl : IPartitionRowFactory {
        [ImportingConstructor]
        public PartitionRowFactoryImpl([ImportMany] IEnumerable<IPartitionMetaDataProvider> partitionMetaDataProviders) {
            PartitionRow.InitializeDescriptors(partitionMetaDataProviders.OrderBy(p => p.Order));
        }
        public bool DescriptorsInitialized => PartitionRow.DescriptorsInitialized;

        public IEnumerable<PropertyDescriptor> PropertyDescriptors => PartitionRow.PropertyDescriptors;

        public void AddDescriptors(IEnumerable<IFileMetaDataProviderProxy<IPartition>> metaProviders) {
            PartitionRow.AddDescriptors(metaProviders);
        }

        public IPartitionRow CreateFileRow(IPartition file) => new PartitionRow(file);

        public void Initialize(IEnumerable<IFileMetaDataProviderProxy<IPartition>> metaProviders) {
            PartitionRow.AddDescriptors(metaProviders);
        }
    }

}
