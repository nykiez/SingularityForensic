using SingularityForensic.Contracts.MainPage.Events;
using SingularityForensic.Contracts.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.MainPage.Events {
    /// <summary>
    /// 主页被加载时导航默认视图;
    /// </summary>
    [Export(typeof(IMainPageLoadedEventHandler))]
    class OnMainPageLoadedOnNavigateToDefaultViewHandler : IMainPageLoadedEventHandler {
        public int Sort => 0;

        public bool IsEnabled => true;

        public void Handle() {
            RegionHelper.RequestNavigate(Contracts.MainPage.Constants.NodeTreeRegion, Contracts.MainPage.Constants.UnitTreeView);
            RegionHelper.RequestNavigate(Contracts.MainPage.Constants.MainPageDocumentRegion, Contracts.MainPage.Constants.WelcomeView);
        }
    }
}
