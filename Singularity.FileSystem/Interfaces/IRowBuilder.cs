using CDFC.Parse.Contracts;
using Singularity.UI.FileSystem.Models;

namespace Singularity.UI.FileSystem.Interfaces {
    //行生成器;
    public interface IRowBuilder {
        IFileRow BuildRow(IFile file);
    }
}
