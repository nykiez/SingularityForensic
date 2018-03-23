using CDFCMessageBoxes.MessageBoxes;
using Prism.Commands;
using SingularityForensic.Contracts.Contracts.MainMenu;
using SingularityForensic.Contracts.MainMenu;
using SingularityForensic.About.MessageBoxes;
using SingularityForensic.About.Resources;
using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;

namespace SingularityForensic.About {
    public static class MenuItemDefinitions {
        private static DelegateCommand _aboutCommand;
        public static DelegateCommand AboutCommand => _aboutCommand ??
            (_aboutCommand = new DelegateCommand(AboutMessageBox.Show));

        [Export]
        public static readonly MenuButtonItem AboutMenuItem =
            new MenuButtonItem(MenuConstants.AboutGroup, ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("About")) {
                Command = AboutCommand,
                IconSource = IconSources.AboutIcon
            };
        [Export]
        public static readonly MenuButtonItem CalcMenuItem =
            new MenuButtonItem(MenuConstants.AboutGroup, ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("Calculator")) {
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
