using Singularity.Contracts.TabControl;
using System.Collections.Generic;

namespace Singularity.Contracts.MainPage {
    public interface IDocumentTabService {
        void ChangeSelectedTab(TabModel tabModel);
        void AddTab(TabModel tabModel);
        IEnumerable<TabModel> CurrentTabs { get; }
        void CloseAllTab();
        void CloseTab(TabModel tabModel);
        TabModel SelectedTab { get; }
    }
}
