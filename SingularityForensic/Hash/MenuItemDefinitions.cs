using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hash;
using SingularityForensic.Contracts.MainMenu;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Hash
{
    public static class MenuItemDefinitions
    {
        private static DelegateCommand _hashSetManagementCommand;
        public static DelegateCommand HashSetManagementCommand => _hashSetManagementCommand ??
            (_hashSetManagementCommand = new DelegateCommand(HashSetDialogService.ShowManagementDialog));

        [Export]
        public static readonly MenuButtonItem HashSetManagementMenuItem =
            new MenuButtonItem(MenuConstants.MenuGroupGUID_Tools, 
                ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString(Constants.MenuItemText_HashSetManagement)) {
                Command = HashSetManagementCommand
            };

    }
}
