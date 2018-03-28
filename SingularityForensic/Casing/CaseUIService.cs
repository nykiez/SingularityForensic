using SingularityForensic.Casing.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Casing.Events;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.MainPage;
using SingularityForensic.Contracts.Shell;
using SingularityForensic.Contracts.TreeView;
using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.Casing {
    /// <summary>
    /// 案件相关的UI响应服务;
    /// </summary>
    [Export]
    public class CaseUIService {
        public void RegisterEvents() {
            //加入案件节点;
            PubEventHelper.GetEvent<CaseLoadedEvent>().Subscribe(OnCaseLoaded);
            
            //订阅案件关闭事件;
            PubEventHelper.Subscribe<CaseUnloadedEvent>(OnCaseUnloaded);

            //当案件文件被加载时,向树形节点中加入案件文件;
            PubEventHelper.GetEvent<CaseEvidenceLoadedEvent>().Subscribe(OnEvidenceLoaded);

            PubEventHelper.GetEvent<CaseEvidenceRemovedEvent>().Subscribe(OnCaseEvidenceRemoved);
        }

        /// <summary>
        /// 案件文件被移除时发生;
        /// </summary>
        /// <param name="obj"></param>
        private void OnCaseEvidenceRemoved(CaseEvidence obj) {
            
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

            var nodeService = NodeService.Current;
            if (nodeService == null) {
                return;
            }

            //加入案件节点;
            var csUnit = new TreeUnit(Constants.CaseEvidenceUnitType, cs) {
                Label = cs.CaseName
            };

            //打开案件位置命令;
            csUnit.AddCommandItem(new OpenCasePathCommandItem(cs));
            //显示案件文件信息命令;
            csUnit.AddCommandItem(new ShowCasePropertyCommandItem(cs));
            
            nodeService.AddUnit(null, csUnit);
        }

        //证据项被加载完成时发生;
        private void OnEvidenceLoaded(CaseEvidence evidence) {
            var nodeService = NodeService.Current;
            if (nodeService == null) {
                LoggerService.WriteCallerLine($"{nameof(nodeService)} can't be null.");
                return;
            }

            var unit = new TreeUnit(Contracts.Casing.Constants.CaseEvidenceUnit, evidence) { Label = evidence.Name };
            try {
                //设定上下文菜单;
                unit.ContextCommands.Add(
                    //显示案件文件属性;
                    new CommandItem {
                            //Command = new DelegateCommand(() => ShowCaseFileProperty(csFile)),
                            CommandName = LanguageService.Current?.FindResourceString("Properties")
                    }
                );
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
                ThreadInvoker.UIInvoke(() => {
                    MsgBoxService.ShowError(ex.Message);
                });
            }
            ThreadInvoker.UIInvoke(() => {
                nodeService.AddUnit(nodeService.CurrentUnits.FirstOrDefault(), unit);
            });
        }

        //案件被卸载时发生;
        private void OnCaseUnloaded() {
            //清空Tab;
            DocumentTabService.Current.CloseAllTabs();
            //清空树形;
            NodeService.Current?.ClearNodes();
            //重置标题;
            ShellService.Current?.SetTitle(string.Empty);
        }
    }
}
