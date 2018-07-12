using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Casing.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Casing.Events.Casing {
    /// <summary>
    /// 案件加载后加入到最近打开案件列表中;
    /// </summary>
    [Export(typeof(ICaseLoadedEventHandler))]
    class OnCaseLoadedOnAddToRecentCaseRecordHandler : ICaseLoadedEventHandler {
        public int Sort => 48;

        public bool IsEnabled => true;

        public void Handle() {
            var cs = CaseService.Current?.CurrentCase;
            if(cs == null) {
                return;
            }
            
            RecentCaseRecordManagementService.AddCase(cs);
        }
    }
}
