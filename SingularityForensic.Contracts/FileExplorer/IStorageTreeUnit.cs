﻿using CDFCUIContracts.Commands;
using CDFCUIContracts.Models;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer {
    public interface IStorageTreeUnit : ITreeUnit {
        FileBase File { get; }
        IFileExplorerServiceProvider FSProvider { get; }
    }

  
}
