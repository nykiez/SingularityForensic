using SingularityForensic.Contracts.Case;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer {
    public static class FileExplorerHelper {
        public static IFileExplorerServiceProvider GetFileExplorerServiceProvider(CaseEvidence csV) {
            var providers = ServiceProvider.Current.GetAllInstances<IFileExplorerServiceProvider>();
            try {
                return providers.FirstOrDefault(p => p.CaseEvidenceServiceProvider.CheckIsValid(csV));
            }
            catch (Exception ex) {
                EventLogger.Logger.WriteCallerLine(ex.Message);
                return null;
            }
        }
    }
}
