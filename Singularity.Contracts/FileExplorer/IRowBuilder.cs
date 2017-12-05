using CDFC.Parse.Contracts;

namespace Singularity.Contracts.FileExplorer {
    //行生成器;
    public interface IRowBuilder {
        IFileRow BuildRow(IFile file);
    }
}
