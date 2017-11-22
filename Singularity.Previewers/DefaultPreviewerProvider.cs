using Singularity.Interfaces;
using System.ComponentModel.Composition;
using System.Linq;

namespace Singularity.Previewers {
    /// <summary>
    /// 默认预览器查看器;
    /// </summary>
    [Export(typeof(IPreviewerProvider))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DefaultPreviewerProvider : IPreviewerProvider {
        public IPreviewer GetPreviewer(string fileName) {
            if(string.IsNullOrEmpty(fileName)) {
                return null;
            }

            IPreviewer previewer = null;
            //转化为大写,避免出现后缀大小写不一致的问题;
            var uperFileName = fileName.ToUpper();
            if (SqliteExtensions.FirstOrDefault(p => uperFileName.EndsWith(p)) != null) {
                previewer = new SqlitePreviewer(uperFileName);
            }
            else if(VlcExtensions.FirstOrDefault(p => uperFileName.EndsWith(p)) != null) {
                previewer = new VlcVideoPreviewer(uperFileName);
            }
            else {
                previewer = new OutsideInPreviewer(uperFileName);
            }
            return previewer;
        }

        /// <summary>
        /// VlC所支持后缀;
        /// </summary>
        public static readonly string[] VlcExtensions = new string[] {
            "MP4",
            "AVI",
            "RMVB",

            "MP3"
        };

        /// <summary>
        /// Sqite所支持后缀;
        /// </summary>
        public static readonly string[] SqliteExtensions = new string[] {
            "DB",
            "SQLITE"
        };

    }
}
