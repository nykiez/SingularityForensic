using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Local {
    /// <summary>
    /// 本地文件流提供者;
    /// </summary>
    [Export(typeof(IFileInputStreamProvider))]
    class LocalRegularFileInputStreamProvider : IFileInputStreamProvider {
        public int Sort => 6;

        public Stream GetInputStream(IFile file) {
            if (file == null) {
                throw new ArgumentNullException(nameof(file));
            }

            if (file.TypeGuid != Constants.RegularFileType_LocalRegFile) {
                return null;
            }

            var fileInfo = file.GetInstance<FileInfo>(Constants.RegularFileTag_LocalFileInfo);
            if(fileInfo == null) {
                LoggerService.WriteCallerLine($"{nameof(fileInfo)} can't be null.");
                return null;
            }

            try {
                return File.OpenRead(fileInfo.FullName);
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
                return null;
            }
        }
    }
}
