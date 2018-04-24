using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Imaging;
using SingularityForensic.Contracts.MainMenu;
using SingularityForensic.Contracts.ToolBar;
using System.ComponentModel.Composition;

namespace SingularityForensic.Imaging {
    public static class MenuDefinitions {
        private static DelegateCommand _addImgCommand;

        public static DelegateCommand AddImgCommand => _addImgCommand ??
            (_addImgCommand = new DelegateCommand(() => ImgService.Current?.AddImg()));

        private static IToolBarButtonItem _addImgToolBarItem;
        [Export]
        public static IToolBarButtonItem AddImgToolBarItem {
            get {
                if (_addImgToolBarItem == null) {
                    _addImgToolBarItem = ToolBarService.CreateToolBarButtonItem(AddImgCommand, Constants.TBButtonGUID__AddImg);
                    _addImgToolBarItem.Icon = IconSources.AddImgIcon;
                    _addImgToolBarItem.ToolTip = LanguageService.FindResourceString(Constants.TBButtonToolTip_AddImg);
                    _addImgToolBarItem.Sort = 4;
                }
                return _addImgToolBarItem;
            }
        }

        [Export]
        public static readonly MenuButtonItem AddImgMenuItem = new MenuButtonItem(
            MenuConstants.MenuMainGroup,
            LanguageService.FindResourceString(Constants.MenuItemText_AddImg), 4) {
            Command = AddImgCommand,
            IconSource = IconSources.AddImgIcon
        };
    }
}
