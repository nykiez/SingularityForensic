using Singularity.Contracts.MainPage;
using Singularity.Contracts.TabControl;
using SingularityForensic.ViewModels.Modules.MainPage.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.Modules.MainPage.Global {
    [Export(typeof(IDocumentTabService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DocumentTabService : IDocumentTabService {
        [Import]
        Lazy<DocumentTabsViewModel> BrowserTabsVM;

        //变更选择的Tab;
        public void ChangeSelectedTab(TabModel tabModel) {
            if (BrowserTabsVM?.Value != null) {
                BrowserTabsVM.Value.SelectedBrowserItem = tabModel;
            }
        }

        //增加Tab;
        public void AddTab(TabModel tabModel) {
            BrowserTabsVM?.Value.AddTab(tabModel);
        }

        //所有的Tab;
        public IEnumerable<TabModel> CurrentTabs => BrowserTabsVM?.Value.BrowserItems.Select(p => p);

        //关闭所有Tab;
        public void CloseAllTab() {
            BrowserTabsVM?.Value.CloseAllItems();
        }

        //关闭Tab
        public void CloseTab(TabModel tabModel) {
            BrowserTabsVM?.Value.RemoveBrowserItem(tabModel);
        }

        public TabModel SelectedTab => BrowserTabsVM?.Value?.SelectedBrowserItem;
    }
}
