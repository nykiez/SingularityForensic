using CDFCMessageBoxes.MessageBoxes;
using Prism.Commands;
using SingularityForensic.Modules.About.MessageBoxes;
using SingularityForensic.Modules.About.Resources;
using SingularityForensic.Modules.HelpAbout;
using SingularityForensic.Modules.MainMenu.Models;
using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using static CDFCCultures.Managers.ManagerLocator;

namespace SingularityForensic.Modules.About {
    public static class MenuItemDefinitions {
        private static DelegateCommand _aboutCommand;
        public static DelegateCommand AboutCommand => _aboutCommand ??
            (_aboutCommand = new DelegateCommand(AboutMessageBox.Show));

        [Export]
        public static readonly MenuButtonItemModel AboutMenuItem =
            new MenuButtonItemModel(MenuGroupDefinitions.HelpMenuGroup, FindResourceString("About")) {
                Command = AboutCommand,
                IconSource = IconSources.AboutIcon
            };
        [Export]
        public static readonly MenuButtonItemModel CalcMenuItem =
            new MenuButtonItemModel(MenuGroupDefinitions.HelpMenuGroup, FindResourceString("Calculator")) {
                Command = new DelegateCommand(() => {
                    try {
                        Process.Start("Calc");
                    }
                    catch(Exception ex) {
                        CDFCMessageBox.Show(ex.Message);
                    }
                }),
                IconSource = IconSources.CalcIcon
            };
    }
}
