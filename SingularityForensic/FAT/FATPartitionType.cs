using CDFC.Util;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FAT {
    [Export,Export(typeof(IPartitionType))]
    public class FAT32PartitionType : IPartitionType {
        public string PartTypeName => LanguageService.FindResourceString(Constants.PartitionType_FAT32);

        public string GUID => Constants.PartitionType_FAT32;
    }

    [Export, Export(typeof(IPartitionType))]
    public class FAT16PartitionType : IPartitionType {
        public string PartTypeName => LanguageService.FindResourceString(Constants.PartitionType_FAT16);

        public string GUID => Constants.PartitionType_FAT16;
    }
}
