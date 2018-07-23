using System.ComponentModel.Composition;
using Prism.Mvvm;
using Prism.Commands;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.MainPage.Events;
using System;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.App;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace SingularityForensic.MainPage.ViewModels {
    [Export]
    public partial class MainPageViewModel : BindableBase {
        /// <summary>
        /// 主页被加载时命令;
        /// </summary>
        private Prism.Commands.DelegateCommand _loadedCommand;
        public Prism.Commands.DelegateCommand LoadedCommand => _loadedCommand ??
            (_loadedCommand = new Prism.Commands.DelegateCommand(
                () => {
                    try {
                        CommonEventHelper.Publish<MainPageLoadedEvent>();
                        CommonEventHelper.PublishEventToHandlers<IMainPageLoadedEventHandler>();
                    }
                    catch(Exception ex) {
                        LoggerService.WriteException(ex);
                        MsgBoxService.Show(ex.Message);
                    }
                }
            ));

        public ObservableCollection<RadPane> Panes { get; } = new ObservableCollection<RadPane>();

        private DockingPanesFactory _panesFactory;
        public DockingPanesFactory PanesFactory {
            get => _panesFactory ?? (_panesFactory = new DockingPanesFactory());
            set {
                SetProperty(ref _panesFactory, value);
            }
        }
       
    }
    
    
    
    
    
}
