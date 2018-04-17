using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer {
    public interface IFileRowProxy<TFile> where TFile : IFile {
        TFile File { get; }
        //xbool Checked { get; }
    }

    public interface IFileRow : IFileRowProxy<IFile> {

    }

    public interface IPartitionRow : IFileRowProxy<IPartition> {

    }
}
