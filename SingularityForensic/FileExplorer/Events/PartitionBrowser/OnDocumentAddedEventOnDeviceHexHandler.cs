using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Document.Events;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.FileExplorer.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer.Events {
    /// <summary>
    /// 设备加入文档时呈现十六进制;
    /// </summary>
    [Export(typeof(IDocumentAddedEventHandler))]
    class OnDocumentAddedEventOnDeviceHexHandler : IDocumentAddedEventHandler {
        public int Sort => 2;

        public bool IsEnabled => true;

        public void Handle((IDocumentBase tab, IDocumentService owner) tuple) {
            if (!(tuple.tab is IEnumerableDocument enumDoc)) {
                return;
            }

            var device = enumDoc.GetInstance<IFile>(Contracts.FileExplorer.Constants.DocumentTag_File) as IDevice;
            if (device == null) {
                return;
            }

            var hexDeviceTuple = FileExplorerUIHelper.GetStreamHexDocument(device);
            if (hexDeviceTuple == null) {
                return;
            }

            hexDeviceTuple.Value.doc.Title =
                LanguageService.FindResourceString(Constants.DocumentTitle_HexDevice);


            var hexPartTuple = FileExplorerUIHelper.GetStreamHexDocument(null);
            if (hexPartTuple == null) {
                return;
            }
            hexPartTuple.Value.doc.Title = LanguageService.FindResourceString(Constants.DocumentTitle_HexPartition);


            enumDoc.AddDocument(hexDeviceTuple.Value.doc);
            enumDoc.AddDocument(hexPartTuple.Value.doc);

            //设备-分区十六进制加入拓展;
            enumDoc.SetInstance(hexDeviceTuple.Value.hexDataContext, Constants.HexDataContext_PartitionBrowser_Device);
            enumDoc.SetInstance(hexPartTuple.Value.hexDataContext, Constants.HexDataContext_PartitionBrowser_Partition);

            enumDoc.SelectedDocument = hexDeviceTuple.Value.doc;
        }
    }
}
