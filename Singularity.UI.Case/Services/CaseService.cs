using System;
using System.Windows;
using static CDFCCultures.Managers.ManagerLocator;
using Microsoft.Practices.ServiceLocation;
using EventLogger;
using Prism.Commands;
using System.Collections.ObjectModel;
using CDFCUIContracts.Commands;
using System.ComponentModel.Composition;
using CDFCMessageBoxes.MessageBoxes;
using Singularity.UI.Case.MessageBoxes;
using Singularity.Contracts.Case;
using Singularity.Contracts.Helpers;
using Singularity.Contracts.MainPage;
using Singularity.Contracts.Case.Events;
using Singularity.Contracts.Shell;
using Singularity.Contracts.Common;

namespace Singularity.UI.Case.Services {
    [Export(typeof(ICaseService))]
    public class CaseService:ICaseService {
        [ImportMany]
        private Lazy<ICaseManager>[] CaseManagers;
        
        public ICase CurrentCase => SingularityCase.Current;

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
                    cManager.Value?.Uninstall();
                }
                ServiceProvider.Current.GetInstance<IShellService>()?.SetTitle(null);
                SingularityCase.Current = null;
                PubEventHelper.Publish<CloseCaseEvent>();
            }
        }
        
        /// <summary>
        /// 加载案件文件;
        /// </summary>
        /// <param name="csFile"></param>
        public void LoadCaseFile<TCaseFile> (TCaseFile csFile) where TCaseFile:ICaseEvidence {
            if(SingularityCase.Current == null) {
                throw new Exception($"{nameof(SingularityCase)} can't be null");
            }

            //案件中加入文件;
            SingularityCase.Current.LoadCaseFile(csFile);

            LoadCaseFileToUnit(csFile);
            PubEventHelper.GetEvent<CaseFileLoadedEvent<TCaseFile>>()?.Publish(csFile);
            
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

        [Import]
        private Lazy<INodeService> nodeService;

        [Import]
        private Lazy<IDocumentTabService> documentService;

        //显示案件文件属性;
        public void ShowCaseFileProperty<TCaseFile>(TCaseFile csFile) where TCaseFile : ICaseEvidence {
            if (csFile != null) {
                csFile = ShowCaseFilePropertyMessageBox.Show(csFile);
                SingularityCase.Current.Save();
            }
        }

        /// <summary>
        /// 加入案件文件;
        /// </summary>
        /// <param name="csFile"></param>
        public void AddNewCaseFile<TCaseFile> (TCaseFile csFile) where TCaseFile:ICaseEvidence {
            SingularityCase.Current.AddNewCaseFile(csFile);

            LoadCaseFileToUnit(csFile);

            PubEventHelper.GetEvent<CaseFileLoadedEvent<TCaseFile>>()?.Publish(csFile);
            PubEventHelper.GetEvent<CaseEvidenceAddedEvent<TCaseFile>>()?.Publish(csFile);
        }

        private void LoadCaseFileToUnit<TCaseFile>(TCaseFile csFile) where TCaseFile : ICaseEvidence {
            var unit = new CaseEvidenceUnit<TCaseFile>(csFile, null) { Label = csFile.Name };

            try {
                //设定上下文菜单;
                unit.ContextCommands = new ObservableCollection<ICommandItem> { 
                    //显示案件文件属性;
                    new CommandItem {
                        Command = new DelegateCommand(() => ShowCaseFileProperty(csFile)),
                        CommandName = FindResourceString("Properties")
                    }
                };
            }
            catch {

            }
            

            nodeService?.Value?.AddUnit(unit);
        }
    }
}
