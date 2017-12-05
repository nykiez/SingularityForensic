using Singularity.Contracts.Case;
using Singularity.Contracts.Common;
using System;
using System.Linq;

namespace Singularity.Contracts.FileSystem {
    public static class FileSystemHelper {
        public static IFileSystemServiceProvider GetFileSystemServiceProvider(ICaseEvidence caseFile) {
            var providers = ServiceProvider.Current.GetAllInstances<IFileSystemServiceProvider>();
            try {
                return providers.FirstOrDefault(p => p.CheckIsValid(caseFile));
            }
            catch (Exception ex) {
                EventLogger.Logger.WriteCallerLine(ex.Message);
                return null;
            }
        }
    }
}
