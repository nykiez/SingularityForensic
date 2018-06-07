using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Ext {
    [Export(typeof(IPartitionType))]
    class ExtPartitionType : IPartitionType {
        public string PartTypeName => LanguageService.FindResourceString(Constants.PartitionType_Ext);

        public string GUID => Constants.PartitionType_Ext;
    }
}
