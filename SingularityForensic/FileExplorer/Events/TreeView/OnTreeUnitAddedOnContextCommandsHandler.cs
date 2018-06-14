using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.TreeView;
using SingularityForensic.Contracts.TreeView.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer.Events {
    /// <summary>
    /// 为设备/分区案件文件加入右键菜单;
    /// </summary>
    [Export(typeof(ITreeUnitAddedEventHandler))]
    class OnTreeUnitAddedOnContextCommandsHandler : ITreeUnitAddedEventHandler {
        public int Sort => 2;

        public bool IsEnabled => true;

        public void Handle((ITreeUnit unit, ITreeService treeService) tuple) {
            if (tuple.unit == null) {
                return;
            }

            if (tuple.treeService != Contracts.MainPage.MainTreeService.Current) {
                return;
            }

            var csFile = tuple.unit.GetInstance<ICaseEvidence>(Contracts.Casing.Constants.TreeUnitTag_CaseEvidence);
            if (csFile == null) {
                LoggerService.WriteCallerLine($"{nameof(csFile)} can't be null.");
                return;
            }

            var fileTuple = FileSystemService.Current.MountedUnits?.FirstOrDefault(p => p.XElem.GetXElemValue(nameof(ICaseEvidence.EvidenceGUID)) == csFile.EvidenceGUID);
            if (fileTuple == null) {
                LoggerService.WriteCallerLine($"{nameof(fileTuple)} can't be null.");
                return;
            }

            if (!(fileTuple.File is IStreamFile streamFile)) {
                return;
            }

            tuple.unit.AddContextCommand(FileExplorerTreeUnitCommandItemFactory.CreateCustomSignSearchCommandItem(streamFile));
        }
    }
}
