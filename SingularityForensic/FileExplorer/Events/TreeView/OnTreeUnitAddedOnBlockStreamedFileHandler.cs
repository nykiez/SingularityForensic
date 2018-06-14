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
    /// 为设备/分区节点加入时加入右键菜单;
    /// </summary>
    [Export(typeof(ITreeUnitAddedEventHandler))]
    class OnTreeUnitAddedOnBlockStreamedFileHandler : ITreeUnitAddedEventHandler {
        public int Sort => 2;

        public bool IsEnabled => true;

        public void Handle((ITreeUnit unit, ITreeService treeService) tuple) {
            if (tuple.unit == null) {
                return;
            }

            if (!(tuple.unit.TypeGuid == Contracts.FileExplorer.Constants.TreeUnitType_InnerFile)) {
                return;
            }

            var file = tuple.unit.GetInstance<IFile>(Contracts.FileExplorer.Constants.TreeUnitTag_InnerFile);
            if (file == null) {
                LoggerService.WriteCallerLine($"{nameof(file)} can't be null.");
                return;
            }

            if (!(file is IStreamFile streamFile)) {
                return;
            }

            tuple.unit.AddContextCommand(FileExplorerTreeUnitCommandItemFactory.CreateCustomSignSearchCommandItem(streamFile));
        }
    }
}
