using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer {
    [Export(typeof(IFileMetaDataProvider))]
    class CategoryNameFileMetaDataProvider : FileMetaDataProvider {
        public override string DisplayName => LanguageService.FindResourceString(Constants.FileMetaDataName_NameCategory);

        public override Type MetaDataType => typeof(string);

        private string _guid;
        public override string GUID => _guid??(_guid = Constants.FileMetaDataGUID_NameCategory);

        public override int Order => 8;

        public override object GetMetaData(IFile file) {
            if(file == null || file.Name == null) {
                return null;
            }

            var descriptor = file.ExtensibleTag.GetInstance<ICategoryDescriptor>(Constants.FileTag_CategoryDescriptor);
            if(descriptor == null || descriptor.IsExpired) {
                descriptor = CategoryNameService.GetNameCategory(file.Name);
                if(descriptor != null) {
                    file.ExtensibleTag.SetInstance(descriptor, Constants.FileTag_CategoryDescriptor);
                    return $"{descriptor.CategoryName}-{descriptor.Key}";
                }
            }
            else {
                return $"{descriptor.CategoryName}-{descriptor.Key}";
            }

            return null;
        }
    }
}
