using SingularityForensic.Contracts.FileExplorer;
using CDFC.Util;

namespace SingularityForensic.Android.FileSystem.Models {
    public class AndroidExt4RowBuilder : GenericStaticInstance<AndroidExt4RowBuilder>,IRowBuilder {
        public IFileRow BuildRow(IFile file) {
            if(file != null) {
                return new AndroidFileRow(file);
            }
            return null;
        }
    }
}
