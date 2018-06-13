using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Hex;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer.Events {
    /// <summary>
    /// 设备选中行发生变更时十六进制变化;
    /// </summary>
    [Export(typeof(IFocusedPartitionChangedEventHandler))]
    class OnFocusedPartChangedOnDeviceHexHandler : IFocusedPartitionChangedEventHandler {
        public int Sort => 2;

        public bool IsEnabled => true;

        public void Handle((IPartitionsBrowserViewModel sender, IPartitionRow part) tuple) {
            if (!(tuple.sender is IPartitionsBrowserViewModel vm)) {
                return;
            }

            if (vm.Device == null) {
                return;
            }

            var tab = DocumentService.MainDocumentService.CurrentDocuments.
               FirstOrDefault(p => p.GetIntance<IFile>(Contracts.FileExplorer.Constants.DocumentTag_File) == vm.Device);
            if (tab == null) {
                return;
            }

            vm.Device.GetStartLBA(tuple.part.File);

            var deviceHexDataContext = tab.GetIntance<IHexDataContext>(Constants.HexDataContext_PartitionBrowser_Device);
            var partHexDataContext = tab.GetIntance<IHexDataContext>(Constants.HexDataContext_PartitionBrowser_Partition);

            if (deviceHexDataContext != null) {
                deviceHexDataContext.Position = vm.Device.GetStartLBA(tuple.part.File);
            }

            if (partHexDataContext != null) {
                partHexDataContext.Stream = tuple.part.File.BaseStream;
            }
        }
    }
}
