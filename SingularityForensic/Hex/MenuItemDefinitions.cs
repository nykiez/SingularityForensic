using System.ComponentModel.Composition;
using System.Windows.Input;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.MainMenu;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.ToolBar;

namespace SingularityForensic.Hex {

    public static partial class MenuItemDefinitions {
        private static MenuButtonItem _searchKeyMenuItem;
        [Export]
        public static MenuButtonItem SearchKeyMenuItem {
            get {
                if (_searchKeyMenuItem == null) {
                    _searchKeyMenuItem = new MenuButtonItem(MenuConstants.MenuMainGroup, ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("IndexSearch")) {
                        Command = ServiceProvider.GetInstance<HexUIServiceImpl>().SearchKeyConfirmCommand,
                        IconSource = IconSources.FindTextIcon
                    };

                }
                return _searchKeyMenuItem;
            }
        }

        private static IToolBarButtonItem _findTextToolBarButtonItem;
        [Export]
        public static IToolBarButtonItem FindTextToolBarButtonItem {
            get {
                if(_findTextToolBarButtonItem == null) {
                    _findTextToolBarButtonItem = ToolBarService.CreateToolBarButtonItem(
                        ServiceProvider.GetInstance<HexUIServiceImpl>().FindTextCommand, Constants.TBButtonGUID_FindText);
                    _findTextToolBarButtonItem.Icon = IconSources.FindTextIcon;
                    _findTextToolBarButtonItem.ToolTip = LanguageService.FindResourceString(Constants.TBButtonToolTip_FindText);
                    _findTextToolBarButtonItem.Sort = 4;
                }

                return _findTextToolBarButtonItem;
            }
        }

        private static MenuButtonItem _findTextMenuItem;
        [Export]
        public static MenuButtonItem FindTextMenuItem
              => _findTextMenuItem ?? (_findTextMenuItem = new MenuButtonItem(MenuConstants.MenuMainGroup,
                        ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("SearchForText")) {
                  Command = ServiceProvider.GetInstance<HexUIServiceImpl>().FindTextCommand,
                  IconSource = IconSources.FindTextIcon,
                  Modifier = ModifierKeys.Control,
                  Key = Key.F
              });

        private static IToolBarButtonItem _findHexToolBarButtonItem;
        [Export]
        public static IToolBarButtonItem FindHexToolBarButtonItem {
            get {
                if (_findHexToolBarButtonItem == null) {
                    _findHexToolBarButtonItem = ToolBarService.CreateToolBarButtonItem(
                        ServiceProvider.GetInstance<HexUIServiceImpl>().FindHexValueCommand, Constants.TBButtonGUID_FindHex);
                    _findHexToolBarButtonItem.Icon = IconSources.FindHexIcon;
                    _findHexToolBarButtonItem.ToolTip = LanguageService.FindResourceString(Constants.TBButtonToolTip_FindHex);
                    _findHexToolBarButtonItem.Sort = 4;
                }

                return _findHexToolBarButtonItem;
            }
        }

        private static MenuButtonItem _findHexMenuItem;
        [Export]
        public static MenuButtonItem FindHexMenuItem
            => _findHexMenuItem ?? (_findHexMenuItem = new MenuButtonItem(MenuConstants.MenuMainGroup,
                ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString(Constants.ToolBarText_SearchForHex)) {
                Command = ServiceProvider.GetInstance<HexUIServiceImpl>().FindHexValueCommand,
                IconSource = IconSources.FindHexIcon,
                Modifier = ModifierKeys.Alt | ModifierKeys.Control,
                Key = Key.X
            });

        private static IToolBarButtonItem _goToOffsetToolBarButtonItem;
        [Export]
        public static IToolBarButtonItem GoToOffsetToolBarButtonItem {
            get {
                if (_goToOffsetToolBarButtonItem == null) {
                    _goToOffsetToolBarButtonItem = ToolBarService.CreateToolBarButtonItem(
                        ServiceProvider.GetInstance<HexUIServiceImpl>().GoToOffsetCommand, Constants.TBButtonGUID_GoToOffset);
                    _goToOffsetToolBarButtonItem.Icon = IconSources.GotoOffsetIcon;
                    _goToOffsetToolBarButtonItem.ToolTip = LanguageService.FindResourceString(Constants.TBButtonToolTip_GoToOffset);
                    _goToOffsetToolBarButtonItem.Sort = 4;
                }

                return _goToOffsetToolBarButtonItem;
            }
        }

        private static MenuButtonItem _goToOffsetMenuItem;
        [Export]
        public static MenuButtonItem GoToOffsetMenuItem
           => _goToOffsetMenuItem ?? (_goToOffsetMenuItem = new MenuButtonItem(
           MenuConstants.MenuMainGroup,
           ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("GoToOffset")) {
               Command = ServiceProvider.GetInstance<HexUIServiceImpl>().GoToOffsetCommand,
               IconSource = IconSources.GotoOffsetIcon,
               Modifier = ModifierKeys.Alt,
               Key = Key.G
           });

        
        
    }
}
