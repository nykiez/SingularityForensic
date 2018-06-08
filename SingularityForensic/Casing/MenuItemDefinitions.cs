using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.MainMenu;
using SingularityForensic.Contracts.ToolBar;
using System.ComponentModel.Composition;

namespace SingularityForensic.Casing {
    public static class MenuItemDefinitions {
        private static IToolBarButtonItem _openCaseToolBarItem;
        [Export]
        public static IToolBarButtonItem OpenCaseToolBarItem {
            get {
                if (_openCaseToolBarItem == null) {
                    _openCaseToolBarItem = ToolBarItemFactory.CreateToolBarButtonItem(CsUIService.OpenCaseCommand, Constants.TBButtonGUID__OpenCase);
                    _openCaseToolBarItem.Icon = IconSources.OpenCaseIcon;
                    _openCaseToolBarItem.ToolTip = LanguageService.FindResourceString(Constants.TBButtonToolTip_OpenCase);
                    _openCaseToolBarItem.Sort = 4;
                }
                return _openCaseToolBarItem;
            }
        }

        [Export]
        public static readonly MenuButtonItem OpenCaseMenuItem = new MenuButtonItem(
            MenuConstants.MenuMainGroup,
            LanguageService.Current?.FindResourceString(Constants.MenuItemText_OpenCase)) {
            Command = CsUIService.OpenCaseCommand,
            IconSource = IconSources.OpenCaseIcon
        };


        private static IToolBarButtonItem _closeCaseToolBarItem;
        [Export]
        public static IToolBarButtonItem CloseCaseToolBarItem {
            get {
                if (_closeCaseToolBarItem == null) {
                    _closeCaseToolBarItem = ToolBarItemFactory.CreateToolBarButtonItem(CsUIService.CloseCaseCommand, Constants.TBButtonGUID__CloseCase);
                    _closeCaseToolBarItem.Icon = IconSources.OpenCaseIcon;
                    _closeCaseToolBarItem.ToolTip = LanguageService.FindResourceString(Constants.TBButtonToolTip_CloseCase);
                    _closeCaseToolBarItem.Sort = 12;
                }
                return _closeCaseToolBarItem;
            }
        }

        //关闭案件菜单;
        [Export]
        public static MenuButtonItem CloseCaseMenuItem = new MenuButtonItem(
                        MenuConstants.MenuMainGroup,
                       LanguageService.Current?.FindResourceString(Constants.MenuItemText_CloseCase)) {
            Command = CsUIService.CloseCaseCommand,
            IconSource = IconSources.CloseCaseIcon
        };

        private static IToolBarButtonItem _createCaseToolBarItem;
        [Export]
        public static IToolBarButtonItem CreateCaseToolBarItem {
            get {
                if (_createCaseToolBarItem == null) {
                    _createCaseToolBarItem = ToolBarItemFactory.CreateToolBarButtonItem(CsUIService.CreateCaseCommand, Constants.TBButtonGUID__CloseCase);
                    _createCaseToolBarItem.Icon = IconSources.OpenCaseIcon;
                    _createCaseToolBarItem.ToolTip = LanguageService.FindResourceString(Constants.TBButtonToolTip__CreateCase);
                    _createCaseToolBarItem.Sort = 0;
                }
                return _createCaseToolBarItem;
            }
        }

        [Export]
        public static readonly MenuButtonItem CreateCaseMenuItem =
            new MenuButtonItem(
               MenuConstants.MenuMainGroup,
               LanguageService.Current?.FindResourceString(Constants.MenuItemText_CreateCase), 0) {
                    Command = CsUIService.CreateCaseCommand,
                    IconSource = IconSources.CreateCaseIcon
            };

        private static CaseUIService _csUIService;
        public static CaseUIService CsUIService => _csUIService ?? (_csUIService = ServiceProvider.GetInstance<CaseUIService>());
    }
}
