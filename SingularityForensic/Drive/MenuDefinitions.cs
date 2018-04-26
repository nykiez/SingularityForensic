using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.MainMenu;
using SingularityForensic.Contracts.ToolBar;
using System.ComponentModel.Composition;

namespace SingularityForensic.Drive {
    static class ToolBarDefinitions {
        private static DelegateCommand _addImgCommand;

        public static DelegateCommand AddImgCommand => _addImgCommand ??
            (_addImgCommand = new DelegateCommand(
                () => {
                    ServiceProvider.Current.GetInstance<DriveService>().AddDrive();
                }
            ));

        private static IToolBarButtonItem _addDriveToolBarItem;
        [Export]
        public static IToolBarButtonItem AddDriveToolBarItem {
            get {
                if (_addDriveToolBarItem == null) {
                    _addDriveToolBarItem = ToolBarService.CreateToolBarButtonItem(AddImgCommand, Constants.TBButtonGUID__AddDrive);
                    _addDriveToolBarItem.Icon = IconSources.AddDriveIcon;
                    _addDriveToolBarItem.ToolTip = LanguageService.FindResourceString(Constants.TBButtonToolTip_AddDrive);
                    _addDriveToolBarItem.Sort = 4;
                }
                return _addDriveToolBarItem;
            }
        }

        [Export]
        public static readonly MenuButtonItem AddDriveMenuItem = new MenuButtonItem(
           MenuConstants.MenuMainGroup,
           LanguageService.FindResourceString(Constants.TBButtonToolTip_AddDrive), 4) {
                Command = AddImgCommand,
                IconSource = IconSources.AddDriveIcon
        };
    }
}
