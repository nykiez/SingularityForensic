using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileSystem;
using System.ComponentModel.Composition;

namespace SingularityForensic.FAT {
    [Export(typeof(IPartitionType))]
    class FAT32PartitionType : IPartitionType {
        public string PartTypeName => LanguageService.FindResourceString(Constants.PartitionType_FAT32);

        public string GUID => Constants.PartitionType_FAT32;
    }

    [Export(typeof(IPartitionType))]
    class FAT16PartitionType : IPartitionType {
        public string PartTypeName => LanguageService.FindResourceString(Constants.PartitionType_FAT16);

        public string GUID => Constants.PartitionType_FAT16;
    }
}
