using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.ITunes {
    [Export(typeof(IFileInputStreamProvider))]
    class ITunesRegularFileInputStreamProvider : IFileInputStreamProvider {
        public int Sort => 64;

        public Stream GetInputStream(IFile file) {
            if(file == null) {
                throw new ArgumentNullException(nameof(file));
            }

            if (file.TypeGuids?.Contains(Constants.RegularFileType_ITunesBackUp) ?? false) {
                return null;
            }

            var iosFileStruct = file.GetIntance<IOSFileStruct?>(Constants.RegularFileTag_ITunesBackUp);
            if(iosFileStruct == null) {
                LoggerService.WriteCallerLine($"{nameof(iosFileStruct)} can't be null.");
                return null;
            }

            if (string.IsNullOrEmpty(iosFileStruct.Value.LocalPath)) {
                LoggerService.WriteCallerLine($"{nameof(IOSFileStruct)}->{nameof(IOSFileStruct.LocalPath)} can't be null.");
                return null;
            }

            try {
                return File.OpenRead(iosFileStruct.Value.LocalPath);
            }
            catch(Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
                return null;
            }
        }
    }
}
