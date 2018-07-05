using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.MainMenu;
using System.ComponentModel.Composition;

namespace SingularityForensic.MainMenu {
    public static class MenuGroupDefinitions {
        [Export]
        public static readonly MenuItemGroup MainPageMenuGroup = new MenuItemGroupEx(MenuConstants.MenuGroupGUID_File, 0) {
            Text = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString(Constants.MenuGroupName_File),
            IconSource = IconSources.MenuHomeIcon
        };

        [Export]
        public static readonly MenuItemGroup OptionsMenuGroup = new MenuItemGroup(MenuConstants.MenuGroupGUID_Tools, 36) {
            Text = LanguageService.FindResourceString(Constants.MenuGroupName_Tools)
        };
    }
}
