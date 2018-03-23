using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.MainPage.Events;
using SingularityForensic.Contracts.TreeView;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.FileExplorer {
    /// <summary>
    /// 文件系统资源管理器UI响应单位;
    /// </summary>
    [Export]
    public class FileExplorerUIService {
        public void RegisterEvents() {
            //为设备案件文件节点加入文件系统子节点;
            PubEventHelper.GetEvent<TreeNodeAddedEvent>().Subscribe(OnTreeUnitAdded);
            //加入文件系统节点响应(左键);
            PubEventHelper.GetEvent<TreeNodeClickEvent>().Subscribe(OnTreeUnitClick);
        }

        private void OnTreeUnitAdded(TreeUnit unit) {
            if (unit == null) {
                return;
            }

            if (unit.TypeGuid == Contracts.Casing.Constants.CaseEvidenceUnit
            && unit.Tag is CaseEvidence csFile) {

                var device = FSService.Current.EnumedFiles?.FirstOrDefault(p => p.xElem.GetXElemValue(nameof(CaseEvidence.EvidenceGUID)) == csFile.EvidenceGUID);
                if (device == null) {
                    LoggerService.WriteCallerLine($"{nameof(device)} can't be null.");
                    return;
                }

                var fsUnit = new TreeUnit(Constants.FileSystemTreeUnit, device) {
                    Icon = IconResources.FileSystemIcon,
                    Label = LanguageService.Current?.FindResourceString("FileSystem")
                };
                fsUnit.MoveToUnit(unit);

            }
        }
        private void OnTreeUnitClick(TreeUnit unit) {
            if (unit == null) {
                return;
            }

            if (unit.TypeGuid != Constants.FileSystemTreeUnit) {
                return;
            }

            if (!(unit.Tag is Device device)) {
                return;
            }
        }


    }
}
