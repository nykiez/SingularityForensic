using System.ComponentModel.Composition;
using Prism.Mvvm;
using Prism.Commands;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.MainPage.Events;
using System;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.App;

namespace SingularityForensic.MainPage.ViewModels {
    [Export]
    public partial class MainPageViewModel : BindableBase {
        /// <summary>
        /// 主页被加载时命令;
        /// </summary>
        private DelegateCommand _loadedCommand;
        public DelegateCommand LoadedCommand => _loadedCommand ??
            (_loadedCommand = new DelegateCommand(
                () => {
                    try {
                        PubEventHelper.Publish<MainPageLoadedEvent>();
                        PubEventHelper.PublishEventToHandlers<IMainPageLoadedEventHandler>();
                    }
                    catch(Exception ex) {
                        LoggerService.WriteException(ex);
                        MsgBoxService.Show(ex.Message);
                    }
                }
            ));
    }
    
    
    
    
    
}
