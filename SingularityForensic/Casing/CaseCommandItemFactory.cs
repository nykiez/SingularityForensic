using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.TreeView;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace SingularityForensic.Casing {
    public static partial class CaseCommandItemFactory {
        /// <summary>
        /// 创建打开案件路径命令;
        /// </summary>
        /// <param name="treeService"></param>
        /// <returns></returns>
        public static ICommandItem CreateOpenCasePathCommandItem(ITreeService treeService) {
            if (treeService == null) {
                throw new ArgumentNullException(nameof(treeService));
            }

            var command = CreateOpenCasePathCommand(treeService);

            var cmi = CommandItemFactory.CreateNew(command, Constants.ContextCommandItemGUID_OpenCasePathFolder, () => CheckCaseUnitSelected(treeService));
            cmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_OpenCasePathFolder);
            cmi.Sort = 128;

            return cmi;
        }

        private static ICommand CreateOpenCasePathCommand(ITreeService treeService) {
            if (treeService == null) {
                throw new ArgumentNullException(nameof(treeService));
            }
            var comm = CommandFactory.CreateDelegateCommand(() => {
                if (!CheckCaseUnitSelected(treeService)) {
                    return;
                }

                var slUnit = treeService.SelectedUnit;
                if (slUnit == null) {
                    return;
                }

                var cs = slUnit.GetInstance<ICase>(Contracts.Casing.Constants.TreeUnitTag_Case);
                if (cs == null) {
                    return;
                }

                try {
                    var direct = Path.GetFullPath(cs.Path);
                    Process.Start("explorer.exe", direct);
                }
                catch (Exception ex) {
                    LoggerService.WriteException(ex);
                }
            });
            return comm;
        }

        /// <summary>
        /// 创建显示案件属性命令;
        /// </summary>
        /// <param name="treeService"></param>
        /// <returns></returns>
        public static ICommandItem CreateShowCasePropertyCommandItem(ITreeService treeService) {
            var comm = CommandFactory.CreateDelegateCommand(
                () => {
                    ServiceProvider.Current?.GetInstance<ICaseDialogService>()?.ShowCaseProperty(GetCaseFromSelectedUnit(treeService));
                }
            );

            var cmi = CommandItemFactory.CreateNew(comm, Constants.ContextCommandItemGUID_ShowCaseProperty, () => CheckCaseUnitSelected(treeService));
            cmi.Name = LanguageService.FindResourceString(Constants.ShowCaseProperty);
            cmi.Sort = 256;
            return cmi;
        }

        /// <summary>
        /// 创建显示证据项命令;
        /// </summary>
        /// <param name="treeService"></param>
        /// <returns></returns>
        public static ICommandItem CreateShowCaseEvidencePropertyCommandItem(ITreeService treeService) {
            var cmi = CommandItemFactory.CreateNew(
                CreateShowCaseEvidencePropertyCommand(treeService),
                Constants.ContextCommandItemGUID_ShowCaseEvidenceProperty,
                () => CheckEvidenceUnitSelected(treeService)
            );
            cmi.Name = LanguageService.Current?.FindResourceString("Properties");
            cmi.Sort = 128;
            return cmi;
        }
        private static ICommand CreateShowCaseEvidencePropertyCommand(ITreeService treeService) {
            var comm = CommandFactory.CreateDelegateCommand(
                () => {
                    var evidence = GetEvidenceFromSelectedUnit(treeService);
                    if (evidence == null) {
                        return;
                    }

                    CaseDialogService.Current?.ShowCaseEvidenceProperty(evidence);
                }
            );
            //PubEventHelper.GetEvent<Contracts.TreeView.Events.TreeUnitSelectedChangedEvent>().Subscribe()
            return comm;
        }

        /// <summary>
        /// 创建移除证据项命令;
        /// </summary>
        /// <param name="treeService"></param>
        /// <returns></returns>
        public static ICommandItem CreateRemoveCaseEvidenceCommandItem(ITreeService treeService) {
            if (treeService == null) {
                throw new ArgumentNullException(nameof(treeService));
            }

            var comm = CommandFactory.CreateDelegateCommand(
                () => {
                    if(MsgBoxService.Show(
                        LanguageService.FindResourceString(Constants.MsgText_ConfirmToRemoveEvidence),
                        MessageBoxButton.YesNo) != MessageBoxResult.Yes) {
                        return;
                    }

                    var cs = CaseService.Current?.CurrentCase;
                    if (cs == null) {
                        LoggerService.WriteCallerLine($"{nameof(cs)} can't be null.");
                        return;
                    }

                    var evidence = GetEvidenceFromSelectedUnit(treeService);
                    if (evidence == null) {
                        return;
                    }

                    cs.RemoveCaseEvidence(evidence);
                }
            );

            var cmi = CommandItemFactory.CreateNew(
                comm,
                Constants.ContextCommandItemGUID_RemoveCaseProperty,
                () => CheckEvidenceUnitSelected(treeService)
            );
            cmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_RemoveCaseEvidence);
            cmi.Sort = 64;
            return cmi;
        }
    }

    public static partial class CaseCommandItemFactory {
        /// <summary>
        /// 检查是否是案件节点被选中;
        /// </summary>
        /// <param name="treeService"></param>
        /// <returns></returns>
        private static bool CheckCaseUnitSelected(ITreeService treeService) => treeService.CheckTypedUnitSelected(Contracts.Casing.Constants.TreeUnitType_Case);
        /// <summary>
        /// 检查证据项节点是否被选中;
        /// </summary>
        /// <param name="treeService"></param>
        /// <returns></returns>
        private static bool CheckEvidenceUnitSelected(ITreeService treeService) => treeService.CheckTypedUnitSelected(Contracts.Casing.Constants.TreeUnitType_CaseEvidence);
        /// <summary>
        /// 从案件节点中获取案件;
        /// </summary>
        /// <param name="treeService"></param>
        /// <returns></returns>
        private static ICase GetCaseFromSelectedUnit(ITreeService treeService) {
            if (treeService == null) {
                throw new ArgumentNullException(nameof(treeService));
            }

            if (!CheckCaseUnitSelected(treeService)) {
                return null;
            }

            return treeService.SelectedUnit?.GetInstance<ICase>(Contracts.Casing.Constants.TreeUnitTag_Case);
        }
        /// <summary>
        /// 从证据项节点中获取证据项;
        /// </summary>
        /// <param name="treeService"></param>
        /// <returns></returns>
        private static ICaseEvidence GetEvidenceFromSelectedUnit(ITreeService treeService) {
            if (treeService == null) {
                throw new ArgumentNullException(nameof(treeService));
            }

            if (!CheckEvidenceUnitSelected(treeService)) {
                return null;
            }
            return treeService.SelectedUnit?.GetInstance<ICaseEvidence>(Contracts.Casing.Constants.TreeUnitTag_CaseEvidence);
        }
    }
}
