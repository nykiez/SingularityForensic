using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.MainMenu;
using SingularityForensic.Contracts.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.MainMenu {
    static class MenuItemDefinitions {
        [Export]
        public static readonly MenuButtonItem CloseShellMI = new MenuButtonItem(MenuConstants.MenuGroupGUID_File,
            LanguageService.FindResourceString(Constants.MenuItemText_Exit),int.MaxValue) {
            Command = new DelegateCommand(() => {
                ShellService.Current.Close();
            })
            };
        }

    }

