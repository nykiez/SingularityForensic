using SingularityForensic.Contracts.Previewers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.NTFS.USN {
    /// <summary>
    /// NTFS-Usn日志预览器;
    /// </summary>
    [Export(typeof(IPreviewProvider))]
    public class UsnJrnPreviewProvider : IPreviewProvider {
        public bool NeedSaveToLocal => false;

        public int Order => 123;

        public IPreviewer CreatePreviewer(Stream stream, string originName) {
            if(originName == null) {
                return null;
            }

            if(!originName.StartsWith(UsnJrnFileName)) {
                return null;
            }

            return new UsnJrnlPreviewer(stream);
        }
        public const string UsnJrnFileName = "$UsnJrnl";

        public IPreviewer CreatePreviewer(string fileName, string originName) {
            var fs = File.OpenRead(fileName);
            return CreatePreviewer(fs, originName);
        }
    }
}
