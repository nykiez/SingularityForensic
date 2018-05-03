using System.ComponentModel.Composition;
using Prism.Mvvm;
using Prism.Commands;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Document.Events;
using System.IO;
using SingularityForensic.Contracts.Shell.Events;
using System;

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
            PubEventHelper.GetEvent<DocumentAddedEvent>().Subscribe(tuple => {
                RegionHelper.RequestNavigate(
                    Contracts.MainPage.Constants.MainPageDocumentRegion,
                    Contracts.Document.Constants.DocumentTabsView
                );
            });

            PubEventHelper.GetEvent<ShellClosedEvent>().Subscribe(() => {
                
            });
        }

        private DelegateCommand _loadedCommand;
        public DelegateCommand LoadedCommand => _loadedCommand ??
            (_loadedCommand = new DelegateCommand(
                () => {
                    RegionHelper.RequestNavigate(Contracts.MainPage.Constants.NodeTreeRegion, Contracts.MainPage.Constants.UnitTreeView);
                    RegionHelper.RequestNavigate(Contracts.MainPage.Constants.MainPageDocumentRegion, Contracts.MainPage.Constants.WelcomeView);
                }
            ));
        

    }
    
    
    
    
    
}
