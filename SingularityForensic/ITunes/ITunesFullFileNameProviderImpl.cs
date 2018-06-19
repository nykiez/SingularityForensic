using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.ITunes {
    [Export(typeof(IFullFileNameProvider))]
    class ITunesFullFileNameProviderImpl : IFullFileNameProvider {
        public int Sort => 64;

        public string GetFullFileName(IFile file, bool selfIncluded) {
            if (file == null) {
                throw new ArgumentNullException(nameof(file));
            }
            
            if (file.TypeGuids?.Contains(Constants.RegularFileType_ITunesBackUp) ?? false) {
                return null;
            }

            var iosFileStruct = file.GetInstance<IOSFileStruct?>(Constants.RegularFileTag_ITunesBackUp);
            if (iosFileStruct == null) {
                return null;
            }

            return iosFileStruct.Value.PhonePath;
        }
    }
}
