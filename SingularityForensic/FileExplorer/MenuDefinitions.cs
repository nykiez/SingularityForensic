using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using CDFCMessageBoxes.MessageBoxes;
using EventLogger;
using Microsoft.Practices.ServiceLocation;
using Ookii.Dialogs.Wpf;
using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Case;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Contracts.MainMenu;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.MainMenu;
using SingularityForensic.Contracts.Shell;
using SingularityForensic.MainMenu;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows;
using static CDFCCultures.Managers.ManagerLocator;

namespace SingularityForensic.FileSystem {
    public static class MenuDefinitions {
        private static DelegateCommand _addImgCommand;

        public static DelegateCommand AddImgCommand => _addImgCommand ??
            (_addImgCommand = new DelegateCommand(
                () => {
                    var csService = ServiceProvider.Current.GetInstance<ICaseService>();
                    if (csService == null || !csService.ConfirmCaseLoaded()) {
                        return;
                    }

                    if (ServiceProvider.Current?.GetInstance<ICaseService>()?.CurrentCase == null) {
                        Logger.WriteCallerLine("null case entity!", "AddImgCommand");
                        return;
                    }

                    var openPath = ServiceProvider.Current?.GetInstance<IDialogService>()?.OpenFile();

                    if (string.IsNullOrEmpty(openPath)) {
                        return;
                    }

                    var shellService = ServiceProvider.Current.GetInstance<IShellService>();
                    shellService?.ChangeLoadState(true, string.Empty);

                    try {
                        ServiceProvider.Current?.GetInstance<IFileSystemService>().MountImg(openPath);
                    }
                    catch (Exception ex) {
                        Logger.WriteCallerLine(ex.Message, "AddImgCommand");
                    }
                    finally {
                        shellService?.ChangeLoadState(false, null);
                    }
                }
            ));

        [Export]
        public static readonly MenuButtonItemModel AddImgMenuItem = new MenuButtonItemModel(
            MenuConstants.MenuMainGroup,
            ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("AddImg"), 4) {
            Command = AddImgCommand,
            IconSource = IconSources.AddImgIcon
        };
        
        
    }
    
}
