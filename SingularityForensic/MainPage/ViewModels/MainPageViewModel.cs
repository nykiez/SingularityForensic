using System.ComponentModel.Composition;
using Prism.Regions;
using Prism.Mvvm;
using SingularityForensic.MainMenu.Events;
using Prism.Commands;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.MainMenu;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Document.Events;
using SingularityForensic.Contracts.MainMenu;

namespace SingularityForensic.MainPage.ViewModels {
    [Export]
    public partial class MainPageViewModel : BindableBase {
        [ImportingConstructor]
        public MainPageViewModel() {
            this._documentTabService = DocumentService.MainDocumentService;
            RegisterEvents();
            
        }
        private IDocumentService _documentTabService;
        /// <summary>
        /// 注册事件;
        /// </summary>
        private void RegisterEvents() {
            PubEventHelper.Subscribe<MenuSelectedGroupChangedEvent, MenuItemGroup>(group => {
                if (group == MenuGroupDefinitions.MainPageMenuGroup) {
                    RegionManager.RequestNavigate(
                        SingularityForensic.Contracts.Shell.Constants.MainRegion,
                        SingularityForensic.MainPage.Constants.MainPageView);
                }

            });

            //PubEventHelper.Subscribe<TabsClearedEvent>(() => {
            //    RegionHelper.RequestNavigate(Constants.MainPageDocumentRegion, "WelcomeView");
            //});

            //PubEventHelper.GetEvent<TabAddedEvent>
            PubEventHelper.GetEvent<DocumentAddedEvent>().Subscribe(tuple => {
                RegionHelper.RequestNavigate(
                    Contracts.MainPage.Constants.MainPageDocumentRegion,
                    Contracts.Document.Constants.DocumentTabsView
                );
            });
            //_documentTabService.TabAdded += (sender, e) => {
            //RegionHelper.RequestNavigate(Constants.MainPageDocumentRegion, "DocumentTab");
            //};
        }

        private DelegateCommand _contentRenderedCommand;
        public DelegateCommand ContentRenderedCommand => _contentRenderedCommand ??
            (_contentRenderedCommand = new DelegateCommand(
                () => {
                    RegionHelper.RequestNavigate(Contracts.MainPage.Constants.NodeTreeRegion, Contracts.MainPage.Constants.UnitTreeView);
                    RegionHelper.RequestNavigate(Contracts.MainPage.Constants.MainPageDocumentRegion, Contracts.MainPage.Constants.WelcomeView);
                }
            ));
        
        [Import]
        private IRegionManager regionManager {
            set => RegionManager = value;
        }
        public IRegionManager RegionManager { get; private set; }
        
    }
    
    public partial class MainPageViewModel {
        //[Import]                        //浏览器tab页;
        //private DocumentTabsViewModel browserTabViewModel {
        //    set {
        //        if (value != null) {
                    
        //            BrowserTabViewModel = value;
        //        }
        //    }
        //}
        //public DocumentTabsViewModel BrowserTabViewModel { get; private set; }
    }

    
    
}
