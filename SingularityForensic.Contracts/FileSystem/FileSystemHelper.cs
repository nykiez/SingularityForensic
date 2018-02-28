using SingularityForensic.Contracts.Case;
using SingularityForensic.Contracts.Common;
using System;
using System.Linq;

namespace SingularityForensic.Contracts.FileSystem {
    public static class FileSystemHelper {
        public static ICaseEvidenceServiceProvider GetFileSystemServiceProvider(CaseEvidence caseFile) {
            var providers = ServiceProvider.Current.GetAllInstances<ICaseEvidenceServiceProvider>();
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
