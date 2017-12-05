﻿using Singularity.Contracts.Case;
using Singularity.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singularity.Contracts.FileExplorer {
    public static class FileExplorerHelper {
        public static IFileExplorerServiceProvider GetFileExplorerServiceProvider(ICaseEvidence csV) {
            var providers = ServiceProvider.Current.GetAllInstances<IFileExplorerServiceProvider>();
            try {
                return providers.FirstOrDefault(p => p.FileSystemServiceProvider.CheckIsValid(csV));
            }
            catch (Exception ex) {
                EventLogger.Logger.WriteCallerLine(ex.Message);
                return null;
            }
        }
    }
}
