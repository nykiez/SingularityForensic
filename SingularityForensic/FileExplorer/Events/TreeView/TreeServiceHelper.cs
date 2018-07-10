using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.TreeView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer.Events.TreeView {
    static class TreeServiceHelper {
        /// <summary>
        /// 从选中节点中获取IStreamFile;
        /// </summary>
        /// <param name="treeService"></param>
        /// <returns></returns>
        public static IStreamFile GetStreamFileFromUnitSelected(ITreeService treeService) {
            if (treeService == null) {
                return null;
            }
            if (treeService.SelectedUnit == null) {
                return null;
            }

            if (treeService.SelectedUnit.TypeGuid == Contracts.Casing.Constants.TreeUnitType_CaseEvidence) {
                return GetStreamFileFromCaseEvidenceUnit(treeService.SelectedUnit);
            }
            if (treeService.SelectedUnit?.TypeGuid == Contracts.FileExplorer.Constants.TreeUnitType_InnerFile) {
                return GetStreamFileFromInnerFileUnit(treeService.SelectedUnit);
            }
            return null;
        }
        
        public static IStreamFile GetStreamFileFromCaseEvidenceUnit(ITreeUnit treeUnit) {
            if (treeUnit == null) {
                return null;
            }

            var csEvidence = treeUnit.GetInstance<ICaseEvidence>(Contracts.Casing.Constants.TreeUnitTag_CaseEvidence);
            if (csEvidence == null) {
                LoggerService.WriteCallerLine($"{nameof(csEvidence)} can't be null.");
                return null;
            }

            var fileTuple = FileSystemService.Current.MountedUnits?.FirstOrDefault(p => p.XElem.GetXElemValue(nameof(ICaseEvidence.EvidenceGUID)) == csEvidence.EvidenceGUID);
            if (fileTuple == null) {
                LoggerService.WriteCallerLine($"{nameof(fileTuple)} can't be null.");
                return null;
            }

            if (fileTuple.File is IStreamFile streamFile) {
                return streamFile;
            }

            return null;
        }
        
        public static IStreamFile GetStreamFileFromInnerFileUnit(ITreeUnit treeUnit) {
            var file = treeUnit.GetInstance<IFile>(Contracts.FileExplorer.Constants.TreeUnitTag_InnerFile);
            if (file == null) {
                LoggerService.WriteCallerLine($"{nameof(file)} can't be null.");
                return null;
            }

            if (file is IStreamFile streamFile) {
                return streamFile;
            }

            return null;
        }
    }
}
