using CDFCCultures.Helpers;
using CDFCMessageBoxes.MessageBoxes;
using Microsoft.Practices.ServiceLocation;
using Ookii.Dialogs.Wpf;
using Prism.Commands;
using Singularity.UI.Case.Global.Services;
using Singularity.UI.ITunes.Global.Services;
using Singularity.UI.ITunes.Models;
using Singularity.UI.ITunes.Resources;
using SingularityForensic.Modules.MainMenu.Models;
using SingularityForensic.Modules.MainPage;
using System;
using System.ComponentModel.Composition;
using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.ITunes {
    public static class MenuItemDefinitions {
        [Export]
        public static readonly MenuButtonItemModel AddItunesBackUpMI = new MenuButtonItemModel(MenuGroupDefinitions.MainPageMenuGroup,
            FindResourceString("AddITunesBackUp"), 5) {
            IconSource = IconResources.AddITunesIcon,
            //进行Itnues备份文件检索;
            Command = new DelegateCommand(() => {
                if (ServiceLocator.Current.GetInstance<ICaseService>()?.ConfirmCaseLoaded() != true) {
                    return;
                }
                
                //路径
                var dialog = new VistaFolderBrowserDialog();

                while (true) {    
                    if (dialog.ShowDialog() == true) {
                        if (dialog.SelectedPath?.WordsIScn() == true) {
                            CDFCMessageBox.Show(FindResourceString("InvalidItunesBPath"));
                            continue;
                        }
                    }

                    break;
                }
                
                if((!string.IsNullOrWhiteSpace(dialog.SelectedPath) )
                &&(dialog.SelectedPath?.WordsIScn() != true)) {
                    var bPath = dialog.SelectedPath;

                    var frService = ServiceLocator.Current.GetInstance<ForensicService>();
                    var cFile = new ITunesBackUpCaseFile(IOPathHelper.GetFileNameFromUrl(bPath), bPath, DateTime.Now);
                    ServiceLocator.Current.GetInstance<ICaseService>()?.AddNewCaseFile(cFile);

                    ////加入取证信息节点;
                    //var fUnit = ServiceLocator.Current.GetInstance<ICommonForensicService>()?.AddForensicUnit(cFile);

                    //foreach (var infoKind in PinKindsDefinitions.ForensicClassTypes) {
                    //    if (fUnit.Children.FirstOrDefault(p => p is PinTreeUnit pinUnit && pinUnit.ContentId == infoKind) == null) {
                    //        if(infoKind == PinKindsDefinitions.ForensicClassBasic) {
                    //            fUnit.Children.Add(
                    //                new ExtTreeUnit<IOSBasicStruct?>(null, fUnit, infoKind) {
                    //                    Label = PinKindsDefinitions.GetClassLabel(infoKind)
                    //                }
                    //            );
                    //        }
                    //        else {
                    //            fUnit.Children.Add(
                    //                new PinTreeUnit(infoKind, fUnit) {
                    //                    Label = PinKindsDefinitions.GetClassLabel(infoKind)
                    //                }
                    //            );
                    //        }
                            
                    //    }
                    //}

                    //ServiceLocator.Current.GetInstance<ICaseService>()?.AddCaseFile(cFile);
                    //pubeventhelper
                }
            })
        };

        /// <summary>
        /// 判断是否含有非ASCII码;
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static bool WordsIScn(this string str) {
            if(str == null) {
                return false;
            }
            var sa = str.ToCharArray();
            foreach (var ch in sa) {
                if(ch > 127) {
                    return true;
                }
            }
            return false;
        }
    } 
}
