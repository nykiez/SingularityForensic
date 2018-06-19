using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileSystem;
using System.ComponentModel.Composition;

namespace SingularityForensic.Ext {
    [Export(typeof(IPartitionType))]
    class ExtPartitionType : IPartitionType {
        public string PartTypeName => LanguageService.FindResourceString(Constants.PartitionType_Ext);

        public string GUID => Constants.PartitionType_Ext;
    }
}
