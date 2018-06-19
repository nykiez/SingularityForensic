using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Casing.Events;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.MainPage;
using SingularityForensic.Contracts.Shell;
using SingularityForensic.Contracts.TreeView;
using static SingularityForensic.Casing.Constants;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.Casing {
    /// <summary>
    /// 案件相关的UI响应服务;
    /// </summary>
    [Export(typeof(ICaseUIService)),Export]
    public partial class CaseUIService : ICaseUIService {
        public void Initialize() {
            AddContextCommands();
            RegisterEvents();
            RegisterEventsForCommands();
        }

        /// <summary>
        /// 加入上下文菜单;
        /// </summary>
        private void AddContextCommands() {
            var treeService = MainTreeService.Current;
            if (treeService == null) {
                LoggerService.WriteCallerLine($"{nameof(treeService)} can't be null.");
                return;
            }
            //打开案件位置命令;
            treeService.AddContextCommand(CaseCommandItemFactory.CreateOpenCasePathCommandItem(treeService));
            //显示案件文件信息命令;
            treeService.AddContextCommand(CaseCommandItemFactory.CreateShowCasePropertyCommandItem(treeService));
            //显示案件文件属性;
            treeService.AddContextCommand(CaseCommandItemFactory.CreateShowCaseEvidencePropertyCommandItem(treeService));
            //移除案件;
            treeService.AddContextCommand(CaseCommandItemFactory.CreateRemoveCaseEvidenceCommandItem(treeService));
        }
        private void RegisterEvents() {
            //加入案件节点;
            PubEventHelper.GetEvent<CaseLoadedEvent>().Subscribe(OnCaseLoaded);
            
            //订阅案件关闭事件;
            PubEventHelper.Subscribe<CaseUnloadedEvent>(OnCaseUnloaded);

            //当案件文件被加载时,向树形节点中加入案件文件;
            PubEventHelper.GetEvent<CaseEvidenceLoadedEvent>().Subscribe(OnEvidenceLoaded);

            PubEventHelper.GetEvent<CaseEvidenceRemovedEvent>().Subscribe(OnCaseEvidenceRemoved);
        }

        /// <summary>
        /// 案件文件被移除时,移除案件文件节点;
        /// </summary>
        /// <param name="evidence"></param>
        private void OnCaseEvidenceRemoved(ICaseEvidence evidence) {
            var treeService = MainTreeService.Current;
            if (treeService == null) {
                LoggerService.WriteCallerLine($"{nameof(treeService)} can't be null.");
                return;
            }

            var caseUnit = treeService.CurrentUnits.FirstOrDefault();
            if(caseUnit == null) {
                LoggerService.WriteCallerLine($"{nameof(caseUnit)} can't be null.");
                return;
            }

            var evidenceUnit = caseUnit.Children.FirstOrDefault(p => p.TypeGuid == Contracts.Casing.Constants.TreeUnitType_CaseEvidence
            && p.GetInstance<ICaseEvidence>(Contracts.Casing.Constants.TreeUnitTag_CaseEvidence) == evidence);

            if(evidenceUnit == null) {
                LoggerService.WriteCallerLine($"{nameof(evidenceUnit)} can't be null.");
                return;
            }

            treeService.RemoveUnit(evidenceUnit);
        }

        /// <summary>
        /// //案件加载完成时发生;
        /// </summary>
        /// <param name="cs"></param>
        private void OnCaseLoaded(ICase cs) {
            if(cs == null) {
                return;
            }

            //设定标题;
            ServiceProvider.Current.GetInstance<IShellService>()?.SetTitle(cs.CaseName);

            var nodeService = MainTreeService.Current;
            if (nodeService == null) {
                return;
            }

            //加入案件节点;
            var csUnit = TreeUnitFactory.CreateNew(Contracts.Casing.Constants.TreeUnitType_Case);
            csUnit.Label = cs.CaseName;

            csUnit.SetInstance(cs,Contracts.Casing.Constants.TreeUnitTag_Case);

            nodeService.AddUnit(null, csUnit);
            
            
        }

        //证据项被加载完成时发生;
        private void OnEvidenceLoaded(ICaseEvidence evidence) {
            LoadEvidenceUnitToTree(evidence);
        }

        private void LoadEvidenceUnitToTree(ICaseEvidence evidence) {
            var treeService = MainTreeService.Current;
            if (treeService == null) {
                LoggerService.WriteCallerLine($"{nameof(treeService)} can't be null.");
                return;
            }

            var unit = TreeUnitFactory.CreateNew(Contracts.Casing.Constants.TreeUnitType_CaseEvidence);
            unit.Label = evidence.Name;

            unit.SetInstance(evidence, Contracts.Casing.Constants.TreeUnitTag_CaseEvidence);
            treeService.AddUnit(treeService.CurrentUnits.FirstOrDefault(), unit);
        }

        //案件被卸载时发生;
        private void OnCaseUnloaded() {
            //清空Tab;
            DocumentService.MainDocumentService.CloseAllDocuments();
            //清空树形;
            MainTreeService.Current?.ClearNodes();
            //重置标题;
            ShellService.Current?.SetTitle(string.Empty);
        }
    }

    public partial class CaseUIService {
        private DelegateCommand _closeCaseCommand;
        public DelegateCommand CloseCaseCommand => _closeCaseCommand ??(
            _closeCaseCommand = new DelegateCommand(
                () => {
                    if (MsgBoxService.Current.Show(LanguageService.FindResourceString("ConfirmToCloseCase"), MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                        ServiceProvider.Current.GetInstance<ICaseService>()?.CloseCurrentCase();
                    }
                },
                () => Contracts.Casing.CaseService.Current != null
            ));
        
        private DelegateCommand _createCaseCommand;
        public DelegateCommand CreateCaseCommand => _createCaseCommand ??
            (_createCaseCommand = new DelegateCommand(
                () => {
                    var cs = Contracts.Casing.CaseService.Current.CreateNewCase();
                    if (cs != null) {
                        Contracts.Casing.CaseService.Current.LoadCase(cs);
                    }
                }
            ));
        
        private DelegateCommand _openCaseCommand;
        public DelegateCommand OpenCaseCommand => _openCaseCommand ??
            (_openCaseCommand = new DelegateCommand(
                () => {
                    //若已经存在打开的案件;
                    if (Contracts.Casing.CaseService.Current.CurrentCase != null) {
                        //询问是否关闭;
                        if (
                            MsgBoxService.Current.Show(
                            $"{LanguageService.FindResourceString(ConfirmToCloseAndOpen)}",
                            MessageBoxButton.YesNo
                            ) != MessageBoxResult.Yes) {
                            return;
                        }
                    }

                    var filter =
                        $"({LanguageService.FindResourceString(SupportedCaseFileType)})|*.sfproj |" +
                        $"({LanguageService.FindResourceString("AllFiles")})| *.* ";

                    var fileName = DialogService.Current.OpenFile(filter);

                    if (string.IsNullOrEmpty(fileName)) {
                        return;
                    }


                    Contracts.Casing.CaseService.Current.LoadCase(fileName);
                }
            ));

        private void RegisterEventsForCommands() {
            PubEventHelper.GetEvent<CaseLoadedEvent>().Subscribe(cs => {
                CloseCaseCommand.RaiseCanExecuteChanged();
            });
            PubEventHelper.Subscribe<CaseUnloadedEvent>(() => {
                CloseCaseCommand.RaiseCanExecuteChanged();
            });
        }
    }
}
