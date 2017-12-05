using CDFC.Parse.Contracts;
using CDFC.Util;

namespace Singularity.Contracts.FileExplorer {
    public class DefaultRowBuilder : GenericStaticInstance<DefaultRowBuilder>, IRowBuilder {
        public IFileRow BuildRow(IFile file) {
            return new FileRow<IFile>(file);
        }
    }
}
