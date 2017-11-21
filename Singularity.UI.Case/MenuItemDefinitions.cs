using CDFCCultures.Helpers;
using CDFCMessageBoxes.MessageBoxes;
using EventLogger;
using Microsoft.Practices.ServiceLocation;
using Ookii.Dialogs.Wpf;
using Prism.Commands;
using Singularity.UI.Case;
using Singularity.UI.Case.Contracts;
using Singularity.UI.Case.Events;
using Singularity.UI.Case.Global.Services;
using Singularity.UI.Case.Resources;
using Singularity.UI.MessageBoxes.MessageBoxes;
using SingularityForensic.Helpers;
using SingularityForensic.Modules.MainMenu.Models;
using SingularityForensic.Modules.MainPage;
using SingularityForensic.Modules.Shell.Global.Services;
using System;
using System.ComponentModel.Composition;
using System.Windows;
using static CDFCCultures.Managers.ManagerLocator;
using static CDFCUIContracts.Helpers.ApplicationHelper;

namespace SingularityForensic.Modules.Case {
    public static class MenuItemDefinitions {
        static MenuItemDefinitions() {
            PubEventHelper.Subscribe<CaseLoadedEvent>(() => {
                CloseCaseCommand.RaiseCanExecuteChanged();
            });
            PubEventHelper.Subscribe<CloseCaseEvent>(() => {
                CloseCaseCommand.RaiseCanExecuteChanged();
            });
        }

        //[ImportMany]
        //static IEnumerable<Lazy<ICaseManager>> CaseManagers;

        //加载案件菜单;
        [Export]
        public static readonly MenuButtonItemModel OpenCaseMenuItem = new MenuButtonItemModel(MenuGroupDefinitions.MainPageMenuGroup, FindResourceString("OpenCase")) {
            Command = new DelegateCommand(() => {
                //若已经存在打开的案件;
                if (SingularityCase.Current != null) {
                    //询问是否关闭;
                    if (CDFCMessageBox.Show($"{FindResourceString("ConfirmToCloseAndOpen")}",
                        MessageBoxButton.YesNo) != MessageBoxResult.Yes) {
                        return;
                    }
                }

                var dialog = new VistaOpenFileDialog();
                dialog.Filter = $"({FindResourceString("SupportedCaseFileType")})|*.sfproj|({FindResourceString("AllFiles")})|*.*";
                
                if (dialog.ShowDialog() == true) {
                    var tp = IOPathHelper.GetPathAndFileName(dialog.FileName);
                    if (tp == null) {
                        return;
                    }

                    ServiceLocator.Current.GetInstance<IShellService>()?.ChangeLoadState(true, null);

                    //后台加载
                    var sCase = SingularityCase.LoadFrom(tp.Value.path,tp.Value.fileName);
                    try {
                        if (sCase != null) {
                            //关闭尚未关闭的案件;
                            if (SingularityCase.Current != null) {
                                Application.Current.Dispatcher.Invoke(() => {
                                    PubEventHelper.Publish<CloseCaseEvent>();
                                });
                            }

                            Application.Current.Dispatcher.Invoke(() => {
                                ServiceLocator.Current.GetInstance<ICaseService>()?.LoadCase(sCase);
                            });
                        }

                        SingularityCase.Current = sCase;

                        var msg = new DoubleProcessMessageBox() { Title = FindResourceString("LoadingCase") };
                        msg.DoWork += delegate {
                            foreach (var manager in ServiceLocator.Current.GetAllInstances<ICaseManager>()) {
                                manager.LoadCase((totalPro, pro, capTip, tip) => {
                                    msg.ReportProgress(totalPro, pro, capTip, tip);
                                }, () => msg.CancellationPending);
                            }
                        };

                        msg.ShowDialog();
                        AppInvoke(() => {
                            PubEventHelper.Publish<CaseLoadedEvent>();
                        });
                        
                    }
                    catch (Exception ex) {
                        Logger.WriteLine($"{nameof(OpenCaseMenuItem)}:{ex.Message}");
                        Application.Current.Dispatcher.Invoke(() => {
                            CDFCMessageBox.Show($"{FindResourceString("FailedToOpenCase")}:{ex.Message}");
                        });
                    }
                    finally {
                        sCase?.Save();
                        ServiceLocator.Current.GetInstance<IShellService>()?.ChangeLoadState(false, string.Empty);
                    }

                }
            }),
            IconSource = IconSources.OpenCaseIcon
        };

        private static readonly DelegateCommand CloseCaseCommand = new DelegateCommand(
            () => {
                if (CDFCMessageBox.Show(FindResourceString("ConfirmToCloseCase"), MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                    ServiceLocator.Current.GetInstance<ICaseService>()?.CloseCase();
                }
            },
            () =>
            SingularityCase.Current != null);

        //关闭案件菜单;
        [Export]
        public static MenuButtonItemModel CloseCaseMenuItem = new MenuButtonItemModel(
                        MenuGroupDefinitions.MainPageMenuGroup,
                        FindResourceString("CloseCase")) {
            Command = CloseCaseCommand,
            IconSource = IconSources.CloseCaseIcon
        };
        

        [Export]
        public static readonly MenuButtonItemModel CreateCaseMenuItem =
            new MenuButtonItemModel(
                MenuGroupDefinitions.MainPageMenuGroup,
                FindResourceString("CreateNewCase"), 0) {
                    Command = new DelegateCommand(() => {
                        ServiceLocator.Current.GetInstance<ICaseService>()?.CreateCase();
                    }),
                    IconSource = IconSources.CreateCaseIcon
            };
        
    }
}
