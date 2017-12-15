using EventLogger;
using Prism.Mvvm;
using Singularity.Contracts.Helpers;
using Singularity.Contracts.MainPage.Events;
using Singularity.Contracts.TabControl;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.ViewModels.Modules.MainPage.ViewModels {
    //文件/资源浏览器相关;
    [Export]
    public partial class DocumentTabsViewModel: BindableBase {
        public DocumentTabsViewModel() {}

        /// <summary>
        /// 移除主浏览器对象;
        /// </summary>
        /// <param name="browserItem">需移除的浏览器对象</param>
        public void RemoveBrowserItem(TabModel removingItem) {
            if (removingItem != null) {
                try {
                    BrowserItems.Remove(removingItem);
                    if(BrowserItems.FirstOrDefault() == null) {
                        PubEventHelper.Publish<TabClearedEvent>();
                    }
                    else {
                        
                    }
                    //GC.Collect();
                    //GC.WaitForPendingFinalizers();
                }
                catch (Exception ex) {
                    Logger.WriteCallerLine(ex.Message);
                }
            }
        }

        //选定的浏览集合;
        public ObservableCollection<TabModel> BrowserItems { get;
            set; } = new ObservableCollection<TabModel>();
        
        private TabModel selectedBrowserItem;
        public TabModel SelectedBrowserItem {
            get {
                return selectedBrowserItem;
            }
            set {
                SetProperty(ref selectedBrowserItem, value);
                PubEventHelper.GetEvent<SelectedTabChangedEvent>()?.Publish(selectedBrowserItem);
            }
        }
    }

    public partial class DocumentTabsViewModel { 
        public void CloseAllItems() {
            TabModel item = null;
            while ((item = BrowserItems.FirstOrDefault()) != null) {
                RemoveBrowserItem(item);
            }
            
        }

        public void AddTab(TabModel tab) {
            if(tab == null) {
                throw new ArgumentNullException(nameof(tab));
            }

            BrowserItems.Add(tab);
            SelectedBrowserItem = tab;
            PubEventHelper.Publish<TabAddedEvent, TabModel>(tab);
        }
    }
    
}
