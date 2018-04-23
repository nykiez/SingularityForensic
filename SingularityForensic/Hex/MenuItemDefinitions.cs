using CDFCMessageBoxes.MessageBoxes;
using CDFCUIContracts.Abstracts;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.MainMenu;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Controls.MessageBoxes;
using SingularityForensic.Contracts.App;

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

        private static HexUIReactService _hexUIService;
        private static HexUIReactService HexUIService => 
            _hexUIService ?? (_hexUIService = ServiceProvider.Current?.GetInstance<HexUIReactService>());
    }
}
