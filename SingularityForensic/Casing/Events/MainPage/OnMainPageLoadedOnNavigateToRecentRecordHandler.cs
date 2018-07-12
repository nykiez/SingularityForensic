using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.MainPage.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Casing.Events.MainPage {
    /// <summary>
    /// 主页被加载时加载最近案件(如果存在)视图;
    /// </summary>
    [Export(typeof(IMainPageLoadedEventHandler))]
    class OnMainPageLoadedOnNavigateToRecentRecordHandler : IMainPageLoadedEventHandler {
        public int Sort => 2;

        public bool IsEnabled => true;

        public void Handle() {
            var records = RecentCaseRecordManagementService.GetAllRecentRecords();
            if(records == null || records.Count() == 0) {
                return;
            }

            RegionHelper.RequestNavigate(Contracts.MainPage.Constants.MainPageDocumentRegion, Contracts.Casing.Constants.RecentCaseRecordsView);
        }
    }
}
