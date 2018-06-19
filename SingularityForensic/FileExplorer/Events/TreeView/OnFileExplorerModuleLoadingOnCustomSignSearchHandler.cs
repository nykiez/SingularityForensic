using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.TreeView;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.FileExplorer.Events {
    /// <summary>
    /// 为设备/分区节点加入时加入自定义签名扫描;
    /// </summary>
    [Export(typeof(IFileExplorerModuleLoadingEventHandler))]
    class OnFileExplorerModuleLoadingOnCustomSignSearchHandler : IFileExplorerModuleLoadingEventHandler {
        public int Sort => 2;

        public bool IsEnabled => true;

        public void Handle() {
            var treeService = Contracts.MainPage.MainTreeService.Current;
            if (treeService == null) {
                return;
            }

            var comm = CommandFactory.CreateDelegateCommand(() => {
                var streamFile = GetStreamFileFromUnitSelected(treeService);
                if(streamFile == null) {
                    return;
                }

                FileExplorerStreamFileExtensions.SignSearch(streamFile);
            });

            var cmi = CommandItemFactory.CreateNew(comm,Constants.ContextCommandItemGUID_CustomSignSearch,() => GetStreamFileFromUnitSelected(treeService) != null);
            cmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_CustomSignSearch);
            cmi.Sort = 12;

            treeService.AddContextCommand(cmi);
        }

        private static IStreamFile GetStreamFileFromUnitSelected(ITreeService treeService) {
            if(treeService == null) {
                return null;
            }
            if(treeService.SelectedUnit == null) {
                return null;
            }

            if(treeService.SelectedUnit.TypeGuid == Contracts.Casing.Constants.TreeUnitType_CaseEvidence) {
                return GetStreamFileFromCaseEvidenceUnit(treeService.SelectedUnit);
            }
            if (treeService.SelectedUnit?.TypeGuid == Contracts.FileExplorer.Constants.TreeUnitType_InnerFile) {
                return GetStreamFileFromInnerFileUnit(treeService.SelectedUnit);
            }
            return null;
        }

        private static IStreamFile GetStreamFileFromCaseEvidenceUnit(ITreeUnit treeUnit) {
            if(treeUnit == null) {
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

        private static IStreamFile GetStreamFileFromInnerFileUnit(ITreeUnit treeUnit) {
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
