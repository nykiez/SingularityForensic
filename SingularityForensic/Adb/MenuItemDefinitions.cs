using CDFCMessageBoxes.MessageBoxes;
using EventLogger;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Singularity.UI.AdbViewer.MessageBoxes;
using System.ComponentModel.Composition;
using System.Windows;
using static CDFCCultures.Managers.ManagerLocator;
using Singularity.UI.AdbViewer.Global.Services;
using Singularity.UI.Case;
using Singularity.Contracts.Contracts.MainMenu;
using Singularity.Contracts.Case;
using Singularity.Contracts.MainMenu;
using Singularity.Contracts.Common;

namespace Singularity.UI.AdbViewer {
    public static class MenuItemDefinitions {
        [Export]
        public static MenuButtonItemModel ConnectToDeviceMenuItem {
            get {
                if (_connectToDeviceMenuItem == null) {
                    _connectToDeviceMenuItem = new MenuButtonItemModel(
                        MenuConstants.MenuMainGroup, FindResourceString("ConnectToDevice"), 2) {
                        Command = ConnectToDeviceCommand,
                        IconSource = Resources.IconSources.ConnectToDeviceIcon
                    };
                }
                return _connectToDeviceMenuItem;
            }
        }
        private static MenuButtonItemModel _connectToDeviceMenuItem;

        //连接到设备命令;
        private static DelegateCommand _connectToDeviceCommand;
        public static DelegateCommand ConnectToDeviceCommand =>
            _connectToDeviceCommand ?? (_connectToDeviceCommand = new DelegateCommand(
                () => {
                    if (ServiceProvider.Current?.GetInstance<ICaseService>()?.CurrentCase == null) {
                        if (CDFCMessageBox.Show(FindResourceString("ConfirmToCreateNewCase"), MessageBoxButton.YesNo) != MessageBoxResult.Yes) {
                            return;
                        }
                        else {
                            ServiceProvider.Current.GetInstance<ICaseService>()?.CreateCase();
                        }
                    }

                    if (ServiceProvider.Current?.GetInstance<ICaseService>()?.CurrentCase == null) {
                        Logger.WriteLine($"{nameof(ConnectToDeviceCommand)}:CaseEntity can't be null.");
                        return;
                    }

                    var container = AdbViewerMessageBox.Show();
                    if (container == null) {
                        return;
                    }

                    ServiceProvider.Current.GetInstance<AdbViewerService>()?.LoadAdbPhoneContainer(container);
                    //FsNodeManagerViewModel?.AddAdbUnit(container);
                    //发布事件;
                    //Aggregator?.GetEvent<AdbPhoneContainerAquiredEvent>().Publish(container);
                }
            ));
    }
}
