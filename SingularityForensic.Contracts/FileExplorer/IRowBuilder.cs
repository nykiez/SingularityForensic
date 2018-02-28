using CDFC.Parse.Contracts;

namespace SingularityForensic.Contracts.FileExplorer {
    //行生成器;
    public interface IRowBuilder {
        IFileRow BuildRow(IFile file);
    }
}
