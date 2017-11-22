using SingularityForensic.Modules.MainMenu.Models;
using System.ComponentModel.Composition;
using Prism.Regions;
using Prism.Mvvm;
using SingularityForensic.Helpers;
using SingularityForensic.Modules.MainMenu.Global.Events;
using SingularityForensic.Modules.MainPage;
using Prism.Commands;
using SingularityForensic.Modules.MainPage.Global.Events;
using SingularityForensic.Modules.MainPage.Models;

namespace SingularityForensic.ViewModels.Modules.MainPage.ViewModels {
    [Export]
    public partial class MainPageViewModel : BindableBase {
        public MainPageViewModel() {
            RegisterEvents();
            
        }
        
        /// <summary>
        /// 注册事件;
        /// </summary>
        private void RegisterEvents() {
            PubEventHelper.Subscribe<MenuSelectedGroupChangedEvent, MenuItemGroup>(group => {
                if (group == MenuGroupDefinitions.MainPageMenuGroup) {
                    RegionManager.RequestNavigate(SingularityForensic.Modules.Shell.RegionNames.MainRegion, "MainPage");
                }

            });

            PubEventHelper.Subscribe<TabClearedEvent>(() => {
                RegionHelper.RequestNavigate(RegionNames.MainPageDocumentRegion, "WelcomeView");
            });

            PubEventHelper.Subscribe<TabAddedEvent,TabModel>(tab => {
                RegionHelper.RequestNavigate(RegionNames.MainPageDocumentRegion, "DocumentTab");
            });
        }

        private DelegateCommand _contentRenderedCommand;
        public DelegateCommand ContentRenderedCommand => _contentRenderedCommand ??
            (_contentRenderedCommand = new DelegateCommand(
                () => {
                    RegionHelper.RequestNavigate(RegionNames.MainPageNodeRegion, "MainPageNodeManager");
                    RegionHelper.RequestNavigate(RegionNames.MainPageDocumentRegion, "WelcomeView");
                }
            ));
        
        [Import]
        private IRegionManager regionManager {
            set => RegionManager = value;
        }
        public IRegionManager RegionManager { get; private set; }
        
    }
    
    public partial class MainPageViewModel {
        [Import]                        //浏览器tab页;
        private DocumentTabsViewModel browserTabViewModel {
            set {
                if (value != null) {
                    
                    BrowserTabViewModel = value;
                }
            }
        }
        public DocumentTabsViewModel BrowserTabViewModel { get; private set; }
    }

    
    
}
