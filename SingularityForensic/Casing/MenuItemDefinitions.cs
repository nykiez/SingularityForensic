using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Casing.Events;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Contracts.MainMenu;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.MainMenu;
using System.ComponentModel.Composition;
using System.Linq;
using static CDFCCultures.Managers.ManagerLocator;
using static CDFCUIContracts.Helpers.ApplicationHelper;

namespace SingularityForensic.Casing {
    public static class MenuItemDefinitions {
        static MenuItemDefinitions() {
            PubEventHelper.GetEvent<CaseLoadedEvent>().Subscribe(cs => {
                CloseCaseCommand.RaiseCanExecuteChanged();
            });
            PubEventHelper.Subscribe<CaseUnloadedEvent>(() => {
                CloseCaseCommand.RaiseCanExecuteChanged();
            });
        }

        //[ImportMany]
        //static IEnumerable<Lazy<ICaseManager>> CaseManagers;

        //加载案件菜单;
        [Export]
        public static readonly MenuButtonItem OpenCaseMenuItem = new MenuButtonItem(
            MenuConstants.MenuMainGroup,
            LanguageService.Current?.FindResourceString("OpenCase")) {
            Command = new DelegateCommand(() => {
                Contracts.Casing.CaseService.Current?.OpenExistingCase();
            }),
            IconSource = IconSources.OpenCaseIcon
        };

        private static readonly DelegateCommand CloseCaseCommand = new DelegateCommand(
            () => {
                if (MsgBoxService.Current.Show(FindResourceString("ConfirmToCloseCase"), MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                    ServiceProvider.Current.GetInstance<ICaseService>()?.CloseCurrentCase();
                }
            },
            () =>
            Contracts.Casing.CaseService.Current != null);

        //关闭案件菜单;
        [Export]
        public static MenuButtonItem CloseCaseMenuItem = new MenuButtonItem(
                        MenuConstants.MenuMainGroup,
                       LanguageService.Current?.FindResourceString("CloseCase")) {
            Command = CloseCaseCommand,
            IconSource = IconSources.CloseCaseIcon
        };
        

        [Export]
        public static readonly MenuButtonItem CreateCaseMenuItem =
            new MenuButtonItem(
                MenuConstants.MenuMainGroup,
               LanguageService.Current?.FindResourceString("CreateNewCase"), 0) {
                    Command = new DelegateCommand(() => {
                        ServiceProvider.Current.GetInstance<ICaseService>()?.CreateNewCase();
                    }),
                    IconSource = IconSources.CreateCaseIcon
            };
        
    }
}
