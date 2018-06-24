using SingularityForensic.Contracts.Previewers;
using SingularityForensic.Controls.Previewers;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;

namespace SingularityForensic.Previewers {
    /// <summary>
    /// 默认预览器查看器;
    /// </summary>
    [Export(typeof(IPreviewProvider))]
    public class DefaultPreviewProvider : IPreviewProvider {
        public IPreviewer CreatePreviewer(string fileName,string originName) {
            if(string.IsNullOrEmpty(fileName)) {
                return null;
            }

            if (!(File.Exists(fileName))) {
                return null;
            }

            IPreviewer previewer = null;
            //转化为大写,避免出现后缀大小写不一致的问题;
            var uperFileName = originName.ToUpper();
            if (SqliteExtensionNames.FirstOrDefault(p => uperFileName.EndsWith(p)) != null) {
                previewer = new SqlitePreviewer(fileName);
            }
            else if(VlcExtensionNames.FirstOrDefault(p => uperFileName.EndsWith(p)) != null) {
                previewer = new VlcVideoPreviewer(fileName);
            }
            else {
                previewer = new OutsideInPreviewer(fileName);
            }
            return previewer;
        }

        public IPreviewer CreatePreviewer(Stream stream, string originName) {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// VlC所支持后缀;
        /// </summary>
        public static readonly string[] VlcExtensionNames = new string[] {
            "MP4",
            "AVI",
            "OGG",
            "RMVB",
            "MP3"
        };

        /// <summary>
        /// Sqite所支持后缀;
        /// </summary>
        public static readonly string[] SqliteExtensionNames = new string[] {
            "DB",
            "SQLITE"
        };

        public int Order => int.MaxValue;

        public bool NeedSaveToLocal => true;
    }
}
