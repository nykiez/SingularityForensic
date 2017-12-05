using CDFCMessageBoxes.MessageBoxes;
using Prism.Commands;
using Singularity.Contracts.Contracts.MainMenu;
using Singularity.Contracts.MainMenu;
using SingularityForensic.Modules.About.MessageBoxes;
using SingularityForensic.Modules.About.Resources;
using SingularityForensic.Modules.HelpAbout;
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
            new MenuButtonItemModel(MenuConstants.AboutGroup, FindResourceString("About")) {
                Command = AboutCommand,
                IconSource = IconSources.AboutIcon
            };
        [Export]
        public static readonly MenuButtonItemModel CalcMenuItem =
            new MenuButtonItemModel(MenuConstants.AboutGroup, FindResourceString("Calculator")) {
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
