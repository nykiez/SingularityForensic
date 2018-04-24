using CDFCMessageBoxes.MessageBoxes;
using Prism.Commands;
using SingularityForensic.Contracts.MainMenu;
using SingularityForensic.About.MessageBoxes;
using SingularityForensic.About.Resources;
using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.ToolBar;

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

        private static IToolBarButtonItem _calcToolBarItem;
        [Export]
        public static IToolBarButtonItem CalcToolBarItem {
            get {
                if (_calcToolBarItem == null) {
                    _calcToolBarItem = ToolBarService.CreateToolBarButtonItem(
                        new DelegateCommand(OpenCalc), Constants.TBButtonGUID_Calc);
                    _calcToolBarItem.Icon = IconSources.CalcIcon;
                    _calcToolBarItem.ToolTip = LanguageService.FindResourceString(Constants.TBButtonToolTip_Calc);
                    _calcToolBarItem.Sort = 4;
                }
                return _calcToolBarItem;
            }
        }

        [Export]
        public static readonly MenuButtonItem CalcMenuItem =
            new MenuButtonItem(MenuConstants.AboutGroup,
                LanguageService.FindResourceString(Constants.MenuItemText_Calc)){
                Command = new DelegateCommand(OpenCalc),
                IconSource = IconSources.CalcIcon
            };

        /// <summary>
        /// 打开计算器;
        /// </summary>
        private static void OpenCalc() {
            try {
                Process.Start("Calc");
            }
            catch (Exception ex) {
                MsgBoxService.Show(ex.Message);
            }
        }
    }
}
