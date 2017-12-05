using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using CDFCUIContracts.Commands;
using CDFCUIContracts.Models;
using Singularity.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singularity.Contracts.FileExplorer {
    public interface IStorageTreeUnit : ITreeUnit {
        IFile File { get; }
        IFileExplorerServiceProvider FSProvider { get; }
    }

  
}
