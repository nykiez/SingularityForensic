using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
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
                LoggerService.Current?.WriteCallerLine(ex.Message);
                return null;
            }
        }
    }
}
