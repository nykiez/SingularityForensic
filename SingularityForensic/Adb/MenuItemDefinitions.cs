namespace SingularityForensic.Adb {
    public static class MenuItemDefinitions {
        //[Export]
        //public static MenuButtonItem ConnectToDeviceMenuItem {
        //    get {
        //        if (_connectToDeviceMenuItem == null) {
        //            _connectToDeviceMenuItem = new MenuButtonItem(
        //                MenuConstants.MenuMainGroup, ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("ConnectToDevice"), 2) {
        //                Command = ConnectToDeviceCommand,
        //                IconSource = Resources.IconSources.ConnectToDeviceIcon
        //            };
        //        }
        //        return _connectToDeviceMenuItem;
        //    }
        //}
        //private static MenuButtonItem _connectToDeviceMenuItem;

        ////连接到设备命令;
        //private static DelegateCommand _connectToDeviceCommand;
        //public static DelegateCommand ConnectToDeviceCommand =>
        //    _connectToDeviceCommand ?? (_connectToDeviceCommand = new DelegateCommand(
        //        () => {
        //            if (ServiceProvider.Current?.GetInstance<ICaseService>()?.CurrentCase == null) {
        //                var res = MsgBoxService.Current?.
        //                Show(
        //                    LanguageService.Current?.
        //                    FindResourceString("ConfirmToCreateNewCase"), 
        //                    MessageBoxButton.YesNo
        //                )??MessageBoxResult.No;

        //                if(res != MessageBoxResult.Yes) {
        //                    return;
        //                }
        //                else {
        //                    ServiceProvider.Current.GetInstance<ICaseService>()?.CreateNewCase();
        //                }
        //            }

        //            if (ServiceProvider.Current?.GetInstance<ICaseService>()?.CurrentCase == null) {
        //                Logger.WriteLine($"{nameof(ConnectToDeviceCommand)}:CaseEntity can't be null.");
        //                return;
        //            }

        //            var container = AdbViewerMessageBox.Show();
        //            if (container == null) {
        //                return;
        //            }

        //            ServiceProvider.Current.GetInstance<AdbViewerService>()?.LoadAdbPhoneContainer(container);
        //            //FsNodeManagerViewModel?.AddAdbUnit(container);
        //            //发布事件;
        //            //Aggregator?.GetEvent<AdbPhoneContainerAquiredEvent>().Publish(container);
        //        }
        //    ));
    }
}
