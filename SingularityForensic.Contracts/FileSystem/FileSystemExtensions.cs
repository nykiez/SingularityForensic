using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
     public static class FileSystemExtensions {
        public static IFile GetFile(this IFileSystemService fsService, string path) {
            if (fsService == null) {
                throw new ArgumentNullException(nameof(fsService));
            }

            try {
                var fullPath = Path.GetFullPath(path);
                
            }
            catch (Exception ex) {
                LoggerService.WriteException(ex);
            }

            return null;
        }

    }
}
