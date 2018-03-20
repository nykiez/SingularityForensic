using System.ComponentModel.Composition;
using Prism.Regions;
using Prism.Mvvm;
using SingularityForensic.MainMenu.Events;
using SingularityForensic.MainPage;
using Prism.Commands;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.MainPage.Events;
using SingularityForensic.Contracts.Contracts.MainMenu;
using SingularityForensic.MainMenu;
using SingularityForensic.Document.ViewModels;
using SingularityForensic.Contracts.Document;

namespace SingularityForensic.ViewModels.Modules.MainPage.ViewModels {
    [Export]
    public partial class MainPageViewModel : BindableBase {
        [ImportingConstructor]
        public MainPageViewModel(IDocumentTabService documentTabService) {
            this._documentTabService = documentTabService;
            RegisterEvents();
            
        }
        private IDocumentTabService _documentTabService;
        /// <summary>
        /// 注册事件;
        /// </summary>
        private void RegisterEvents() {
            PubEventHelper.Subscribe<MenuSelectedGroupChangedEvent, MenuItemGroup>(group => {
                if (group == MenuGroupDefinitions.MainPageMenuGroup) {
                    RegionManager.RequestNavigate(SingularityForensic.Shell.RegionNames.MainRegion, "MainPage");
                }

            });

            PubEventHelper.Subscribe<TabClearedEvent>(() => {
                RegionHelper.RequestNavigate(Constants.MainPageDocumentRegion, "WelcomeView");
            });

            _documentTabService.TabAdded += (sender, e) => {
                RegionHelper.RequestNavigate(Constants.MainPageDocumentRegion, "DocumentTab");
            };
        }

        private DelegateCommand _contentRenderedCommand;
        public DelegateCommand ContentRenderedCommand => _contentRenderedCommand ??
            (_contentRenderedCommand = new DelegateCommand(
                () => {
                    RegionHelper.RequestNavigate(Constants.NodeTreeRegion, Constants.UnitTreeView);
                    RegionHelper.RequestNavigate(Constants.MainPageDocumentRegion, Constants.WelcomeView);
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
