using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Contracts.MainMenu;
using SingularityForensic.Contracts.MainMenu;
using System.ComponentModel.Composition;

namespace SingularityForensic.MainMenu {
    public static class MenuGroupDefinitions {
        [Export]
        public static readonly MenuItemGroup MainPageMenuGroup = new MenuItemGroupEx(MenuConstants.MenuMainGroup, 0) {
            Text = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("MainPageMenuText"),
            IconSource = IconSources.MenuHomeIcon
        };
    }
}
