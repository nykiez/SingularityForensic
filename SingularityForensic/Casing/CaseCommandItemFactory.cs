using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Casing {
    public static class CaseCommandItemFactory {
        /// <summary>
        /// 创建打开案件路径命令;
        /// </summary>
        /// <param name="cs"></param>
        /// <returns></returns>
        public static ICommandItem CreateOpenCasePathCommandItem(ICase cs) {
            if (cs == null) {
                throw new ArgumentNullException(nameof(cs));
            }

            var command = new OpenPathCommand(cs.Path);

            var cmi = CommandItemFactory.CreateNew(command);
            cmi.Name = LanguageService.FindResourceString(Constants.OpenCasePathFolder);
            cmi.Sort = 128;

            return cmi;
        }

        /// <summary>
        /// 创建显示案件树形命令;
        /// </summary>
        /// <param name="cs"></param>
        /// <returns></returns>
        public static ICommandItem CreateShowCasePropertyCommandItem(ICase cs) {
            var comm = new DelegateCommand(
                () => {
                    ServiceProvider.Current?.GetInstance<ICaseDialogService>()?.ShowCaseProperty(cs);
                }
            );

            var cmi = CommandItemFactory.CreateNew(comm);
            cmi.Name = LanguageService.FindResourceString(Constants.ShowCaseProperty);
            cmi.Sort = 256;
            return cmi;
        }

        /// <summary>
        /// 创建显示证据项命令;
        /// </summary>
        /// <param name="evidence"></param>
        /// <returns></returns>
        public static ICommandItem CreateShowCaseEvidencePropertyCommandItem(ICaseEvidence evidence) {
            var cmi = CommandItemFactory.CreateNew(CreateShowCaseEvidencePropertyCommand(evidence));
            cmi.Name = LanguageService.Current?.FindResourceString("Properties");
            return cmi;
        }

        private static DelegateCommand CreateShowCaseEvidencePropertyCommand(ICaseEvidence evidence) {
            var comm = new DelegateCommand(
                () => {
                    CaseDialogService.Current?.ShowCaseEvidenceProperty(evidence);
                }
            );

            return comm;
        }
    }
}
