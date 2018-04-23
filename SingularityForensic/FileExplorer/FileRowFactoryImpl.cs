using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.FileExplorer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;

namespace SingularityForensic.FileExplorer {
    [Export(typeof(IFileRowFactory))]
    class FileRowFactoryImpl : IFileRowFactory {
        public bool DescriptorsInitialized => FileRow.DescriptorsInitialized;

        public IEnumerable<PropertyDescriptor> PropertyDescriptors => FileRow.PropertyDescriptors;

        public IFileRow CreateFileRow(IFile file) => new FileRow(file);

        public void InitializeDescriptors(IEnumerable<IFileMetaDataProviderProxy<IFile>> metaProviders) {
            FileRow.InitializeDescriptors(metaProviders);
        }
    }

    [Export(typeof(IPartitionRowFactory))]
    class PartitionRowFactoryImpl : IPartitionRowFactory {
        public bool DescriptorsInitialized => PartitionRow.DescriptorsInitialized;

        public IEnumerable<PropertyDescriptor> PropertyDescriptors => PartitionRow.PropertyDescriptors;

        public IPartitionRow CreateFileRow(IPartition file) => new PartitionRow(file);

        public void InitializeDescriptors(IEnumerable<IFileMetaDataProviderProxy<IPartition>> metaProviders) {
            PartitionRow.InitializeDescriptors(metaProviders);
        }
    }

}
