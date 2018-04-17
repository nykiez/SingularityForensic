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
        static MenuItemDefinitions() {
            //PubEventHelper.Subscribe<InnerTabSelectedChangedEvent, ITabModel>(tab => {
            //    CurHexViewModel = tab as IHexDataContext;
            //    RaiseCanExcute();
            //});

            //PubEventHelper.Subscribe<SelectedTabChangedEvent, DocumentModel>(tab => {
            //    if(tab is IHaveTabModels haveTbs) {
                    
            //    }
            //});

            //PubEventHelper.Subscribe<SelectedTabChangedEvent, DocumentModel>(tab => {
            //    SearchKeyConfirmCommand.RaiseCanExecuteChanged();
            //});
        }

       
    }

    public static partial class MenuItemDefinitions {
        private static MenuButtonItem _searchKeyMenuItem;
        [Export]
        public static MenuButtonItem SearchKeyMenuItem {
            get {
                if (_searchKeyMenuItem == null) {
                    _searchKeyMenuItem = new MenuButtonItem(MenuConstants.MenuMainGroup, ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("IndexSearch")) {
                        Command = HexUIService.SearchKeyConfirmCommand,
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
                  Command = HexUIService.FindTextCommand,
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
               Command = HexUIService.GoToOffsetCommand,
               IconSource = IconSources.GotoOffsetIcon,
               Modifier = ModifierKeys.Alt,
               Key = Key.G
           });

        private static MenuButtonItem _findHexMenuItem;
        [Export]
        public static MenuButtonItem FindHexMenuItem
            => _findHexMenuItem ?? (_findHexMenuItem = new MenuButtonItem(MenuConstants.MenuMainGroup,
                ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("SearchForHex")) {
                Command = HexUIService.FindHexValueCommand,
                IconSource = IconSources.FindHexIcon,
                Modifier = ModifierKeys.Alt | ModifierKeys.Control,
                Key = Key.X
            });

        private static HexUIService _hexUIService;
        private static HexUIService HexUIService => 
            _hexUIService ?? (_hexUIService = ServiceProvider.Current?.GetInstance<HexUIService>());
    }
}
