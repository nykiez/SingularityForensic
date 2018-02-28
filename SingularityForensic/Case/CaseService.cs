using System;
using System.Windows;
using static CDFCCultures.Managers.ManagerLocator;
using EventLogger;
using Prism.Commands;
using System.Collections.ObjectModel;
using CDFCUIContracts.Commands;
using System.ComponentModel.Composition;
using CDFCMessageBoxes.MessageBoxes;
using SingularityForensic.Case.MessageBoxes;
using SingularityForensic.Contracts.Case;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.MainPage;
using SingularityForensic.Contracts.Case.Events;
using SingularityForensic.Contracts.Shell;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Case.MessageBoxes;
using SingularityForensic.Contracts.App;

namespace SingularityForensic.Case{
    [Export(typeof(ICaseService))]
    public class CaseService:ICaseService {
        [ImportMany]
        private Lazy<ICaseManager>[] CaseManagers;
        
        public ICase CurrentCase => SingularityCase.Current;

        public void CreateCase() {
            if (SingularityCase.Current != null) {
                if(MsgBoxService.Current?.Show(
                    ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("ConfirmToCloseAndCreate"),
                   LanguageService.Current?.FindResourceString("Tip"), MessageBoxButton.YesNo) != MessageBoxResult.Yes) {
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
                    
                    PubEventHelper.Publish<CaseLoadedEvent>();
                }
                catch (Exception ex) {
                    Logger.WriteCallerLine(ex.Message);
                    CDFCMessageBox.Show($"{ex.Message}");
                }
            }
        }

        //载入案件;
        public void LoadCase(ICase sCase) {
            if (sCase == null)
                throw new ArgumentNullException(nameof(sCase));

            //载入案件节点;
            //CaseUnit = new CaseTreeUnit(sCase);
            //TreeUnits.Add(CaseUnit);

            if(sCase != null) {
                //赋值案件字段;
                SingularityCase.Current = sCase as SingularityCase;
            }
            
            ServiceProvider.Current.GetInstance<IShellService>()?.SetTitle(sCase.CaseName);
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
                    cManager.Value?.Clear();
                }
                ServiceProvider.Current.GetInstance<IShellService>()?.SetTitle(null);
                SingularityCase.Current = null;
                PubEventHelper.Publish<CloseCaseEvent>();
            }
        }
        
        ///// <summary>
        ///// 加载案件文件;
        ///// </summary>
        ///// <param name="csFile"></param>
        //public void LoadCaseFile<TCaseFile> (TCaseFile csFile) where TCaseFile:CaseEvidence {
        //    if(SingularityCase.Current == null) {
        //        throw new Exception($"{nameof(SingularityCase)} can't be null");
        //    }

        //    //案件中加入文件;
        //    SingularityCase.Current.LoadCaseFile(csFile);

        //    LoadCaseFileToUnit(csFile);
        //    PubEventHelper.GetEvent<CaseEvidenceLoadedEvent<TCaseFile>>()?.Publish(csFile);
            
        //    ////若为文件案件文件,则加入文件性质的上下文菜单;
        //    //if (csFile is IHaveData<IFile> fcsFile) {
        //    //    //文件系统信息;
        //    //    unit.ContextCommands.AddRange(
        //    //        new CommandItem[]{
        //    //            new CommandItem {
        //    //                Command = new DelegateCommand(() => ShowFileSystem(fcsFile.Data)),
        //    //                CommandName =LanguageService.Current?.FindResourceString("FileSystemInfo")
        //    //            },
        //    //            //重组扫描;
        //    //            new CommandItem {
        //    //                Command = RecompositeSignCommand,
        //    //                CommandName =LanguageService.Current?.FindResourceString("MobileRecompositeBySign")
        //    //            },
        //    //            //自定义签名扫描;
        //    //            new CommandItem {
        //    //                Command = CustomSSearchCommand,
        //    //                CommandName =LanguageService.Current?.FindResourceString("CustomSignSearch")
        //    //            },
        //    //            //不可用;
        //    //            new CommandItem {
        //    //                Command = unAvailebleCommand,
        //    //                CommandName =LanguageService.Current?.FindResourceString("RecoverFileSystemLog")
        //    //            }
        //    //        }
        //    //    );
        //    //}
            
        //}

#pragma warning disable 0169
        [Import]
        private Lazy<INodeService> nodeService;

        [Import]
        private Lazy<IDocumentTabService> documentService;

#pragma warning restore 0169
        //显示案件文件属性;
        //public void ShowCaseFileProperty<TCaseFile>(TCaseFile csFile) where TCaseFile : CaseEvidence {
        //    if (csFile != null) {
        //        csFile = ShowCaseFilePropertyMessageBox.Show(csFile);
        //        SingularityCase.Current.Save();
        //    }
        //}

        /// <summary>
        /// //添加案件文件到案件中;;
        /// </summary>
        /// <param name="csFile"></param>
        public void AddNewCaseFile(CaseEvidence csFile) {
            SingularityCase.Current.AddNewCaseFile(csFile);

            LoadCaseFile(csFile);

            //LoadCaseFileToUnit(csFile);

            PubEventHelper.GetEvent<CaseEvidenceAddedEvent>()?.Publish(csFile);
        }
        
        //加载案件文件到案件中;
        public void LoadCaseFile(CaseEvidence cFile) {
            try {
                //发布正在加载事件;
                PubEventHelper.GetEvent<CaseEvidenceLoadingEvent>().Publish(cFile);
                //案件中加入文件;
                SingularityCase.Current.LoadCaseFile(cFile);
                //发布加载完毕事件;
                PubEventHelper.GetEvent<CaseEvidenceLoadedEvent>().Publish(cFile);
            }
            catch(Exception ex) {
                LoggerService.Current?.WriteCallerLine(ex.Message);
            }
        }

        
        

        public void ShowCaseFileProperty(CaseEvidence csFile) {
            throw new NotImplementedException();
        }
    }
}
