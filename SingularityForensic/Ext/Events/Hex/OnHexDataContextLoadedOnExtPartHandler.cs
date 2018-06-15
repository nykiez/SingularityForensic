using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Contracts.Hex.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Ext.Events.Hex {
    /// <summary>
    /// 高亮Ext分区信息;
    /// </summary>
    [Export(typeof(IHexDataContextLoadedEventHandler))]
    class OnHexDataContextLoadedOnExtPartHandler : IHexDataContextLoadedEventHandler {
        public int Sort => 629;

        public bool IsEnabled => true;

        public void Handle(IHexDataContext hexDataContext) {
            if (hexDataContext == null) {
                return;
            }

            var part = hexDataContext.GetInstance<IFile>(Contracts.FileExplorer.Constants.HexDataContextTag_File) as IPartition;
            if (part == null) {
                return;
            }

            if (!part.TypeGuids?.Contains(Constants.PartitionType_Ext) ?? false) {
                return;
            }

            var extPartInfo = part.GetInstance<ExtPartInfo>(Constants.PartitionStokenTag_ExtPartInfo);
            if(extPartInfo == null) {
                LoggerService.WriteCallerLine($"{nameof(extPartInfo)} can't be null.");
                return;
            }

            if(extPartInfo.SuperBlock == null) {

            }
        }
    }
}
