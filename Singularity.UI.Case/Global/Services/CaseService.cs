using Singularity.UI.Case.Events;
using System;
using System.Windows;
using static CDFCCultures.Managers.ManagerLocator;
using SingularityForensic.Helpers;
using Microsoft.Practices.ServiceLocation;
using SingularityForensic.Modules.Shell.Global.Services;
using EventLogger;
using SingularityForensic.Modules.MainPage.Global.Services;
using Prism.Commands;
using System.Collections.ObjectModel;
using CDFCUIContracts.Commands;
using Singularity.UI.Case.Models;
using Singularity.UI.Case.Contracts;
using System.ComponentModel.Composition;
using CDFCMessageBoxes.MessageBoxes;
using Singularity.UI.Case.MessageBoxes;

namespace Singularity.UI.Case.Global.Services {
    public interface ICaseService {
        void CreateCase();
        bool ConfirmCaseLoaded();
        void CloseCase();

        void LoadCase(SingularityCase sCase);
        //加载案件文件;
        void LoadCaseFile<TCaseFile>(TCaseFile cFile) where TCaseFile : ICaseFile;
        //添加案件文件;
        void AddNewCaseFile<TCaseFile>(TCaseFile cFile) where TCaseFile:ICaseFile;
        //获取案件文件相关节点;
        CaseFileUnit<TCaseFile> GetCaseFileUnit<TCaseFile>(TCaseFile cFile) where TCaseFile : class, ICaseFile;

        void ShowCaseFileProperty<TCaseFile>(TCaseFile csFile) where TCaseFile : ICaseFile;
    }

    [Export(typeof(ICaseService))]
    public class CaseService:ICaseService {
        [ImportMany]
        private Lazy<ICaseManager>[] CaseManagers;

        [Import]
        private Lazy<INodeService> nodeService;

        [Import]
        private Lazy<IDocumentTabService> documentService;

        public void CreateCase() {
            if (SingularityCase.Current != null) {
                if (CDFCMessageBox.Show(FindResourceString("ConfirmToCloseAndCreate"), FindResourceString("Tip"), MessageBoxButton.YesNo) != MessageBoxResult.Yes) {
                    return;
                }
            }

            var sCase = CreateCaseMessageBox.Show();
            if (sCase != null) {
                try {
                    if (SingularityCase.Current != null) {
                        PubEventHelper.Publish<CloseCaseEvent>();
                    }
                    sCase.Save();
                    LoadCase(sCase);
                    SingularityCase.Current = sCase;
                    PubEventHelper.Publish<CaseLoadedEvent>();
                }
                catch (Exception ex) {
                    Logger.WriteCallerLine(ex.Message);
                    CDFCMessageBox.Show($"{ex.Message}");
                }
            }
        }

        /// <summary>
        /// 确认案件是否加载;
        /// </summary>
        /// <returns></returns>
        public bool ConfirmCaseLoaded() {
            if (SingularityCase.Current == null) {
                if (CDFCMessageBox.Show(FindResourceString("ConfirmToCreateNewCase"), MessageBoxButton.YesNo) != MessageBoxResult.Yes) {
                    return false;
                }
                else {
                    CreateCase();
                }
            }

            return SingularityCase.Current != null;
        }

        public void CloseCase() {
            if (SingularityCase.Current != null) {
                SingularityCase.Current.Save();
                foreach (var cManager in CaseManagers) {
                    //卸载案件，释放相关资源;
                    cManager.Value?.Uninstall();
                }
                ServiceLocator.Current.GetInstance<IShellService>()?.ChangeTitleWord(null);
                SingularityCase.Current = null;
                PubEventHelper.Publish<CloseCaseEvent>();
                nodeService?.Value?.ClearNodes();
                documentService?.Value?.CloseAllTab();
            }
        }
        
        //载入案件;
        public void LoadCase(SingularityCase sCase) {
            if (sCase == null)
                throw new ArgumentNullException(nameof(sCase));

            //载入案件节点;
            //CaseUnit = new CaseTreeUnit(sCase);
            //TreeUnits.Add(CaseUnit);

            //赋值案件字段;
            SingularityCase.Current = sCase;
            ServiceLocator.Current.GetInstance<IShellService>()?.ChangeTitleWord(sCase.CaseName);
        }

        /// <summary>
        /// 加载案件文件;
        /// </summary>
        /// <param name="csFile"></param>
        public void LoadCaseFile<TCaseFile> (TCaseFile csFile) where TCaseFile:ICaseFile {
            if(SingularityCase.Current == null) {
                throw new Exception($"{nameof(SingularityCase)} can't be null");
            }

            //案件中加入文件;
            SingularityCase.Current.LoadCaseFile(csFile);
            PubEventHelper.GetEvent<CaseFileLoadedEvent<TCaseFile>>()?.Publish(csFile);

            LoadCaseFileToUnit(csFile);
            ////若为文件案件文件,则加入文件性质的上下文菜单;
            //if (csFile is IHaveData<IFile> fcsFile) {
            //    //文件系统信息;
            //    unit.ContextCommands.AddRange(
            //        new CommandItem[]{
            //            new CommandItem {
            //                Command = new DelegateCommand(() => ShowFileSystem(fcsFile.Data)),
            //                CommandName = FindResourceString("FileSystemInfo")
            //            },
            //            //重组扫描;
            //            new CommandItem {
            //                Command = RecompositeSignCommand,
            //                CommandName = FindResourceString("MobileRecompositeBySign")
            //            },
            //            //自定义签名扫描;
            //            new CommandItem {
            //                Command = CustomSSearchCommand,
            //                CommandName = FindResourceString("CustomSignSearch")
            //            },
            //            //不可用;
            //            new CommandItem {
            //                Command = unAvailebleCommand,
            //                CommandName = FindResourceString("RecoverFileSystemLog")
            //            }
            //        }
            //    );
            //}
            
        }

        private void LoadCaseFileToUnit<TCaseFile>(TCaseFile csFile) where TCaseFile : ICaseFile{
            var unit = new CaseFileUnit<TCaseFile>(csFile, null) { Label = csFile.Name };

            //设定上下文菜单;
            unit.ContextCommands = new ObservableCollection<ICommandItem> { 
                //显示案件文件属性;
                new CommandItem {
                    Command = new DelegateCommand(() => ShowCaseFileProperty(csFile)),
                    CommandName = FindResourceString("Properties")
                }
            };
            
            nodeService?.Value?.AddUnit(unit);
        }

        //显示案件文件属性;
        public void ShowCaseFileProperty<TCaseFile>(TCaseFile csFile) where TCaseFile : ICaseFile {
            if (csFile != null) {
                csFile = ShowCaseFilePropertyMessageBox.Show(csFile);
                SingularityCase.Current.Save();
            }
        }

        /// <summary>
        /// 加入案件文件;
        /// </summary>
        /// <param name="csFile"></param>
        public void AddNewCaseFile<TCaseFile> (TCaseFile csFile) where TCaseFile:ICaseFile {
            SingularityCase.Current.AddNewCaseFile(csFile);
            LoadCaseFileToUnit(csFile);

            PubEventHelper.GetEvent<CaseFileLoadedEvent<TCaseFile>>()?.Publish(csFile);
            PubEventHelper.GetEvent<CaseFileAddedEvent<TCaseFile>>()?.Publish(csFile);
        }

        /// <summary>
        /// 获得案件文件相关节点;
        /// </summary>
        /// <typeparam name="TCaseFile">案件文件类型</typeparam>
        /// <param name="cFile">案件文件</param>
        /// <returns></returns>
        public CaseFileUnit<TCaseFile> GetCaseFileUnit<TCaseFile>(TCaseFile cFile) where TCaseFile : class, ICaseFile {
            if (nodeService?.Value != null) {
                return null;
            }

            foreach (var theUnit in nodeService.Value.CurrentUnits) {
                if (theUnit is CaseFileUnit<TCaseFile> csUnit && csUnit.Data == cFile) {
                    return csUnit;
                }
            }
            return null;
        }
    }
}
