using Prism.Commands;
using System.ComponentModel.Composition;
using SingularityForensic.Contracts;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Controls.Models;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.TreeView;

namespace SingularityForensic.Controls.FileExplorer.Indexing {
    public static class CommandDefinitions {
        public static readonly DelegateCommand CustomSSearchCommand = new DelegateCommand(
                () => {
                    //ServiceProvider.Current.GetInstance<IShellService>()?.ChangeLoadState(true, string.Empty);
                    //try {
                    //    var msg = new SignSearchMessageBox();
                    //    var setting = msg.Show();

                    //    if (setting != null) {
                    //        if (NodeService?.SelectedNode is ICaseEvidenceUnit<CaseEvidence> csFileUnit && 
                    //        csFileUnit.Evidence is IHaveData<Device> dcsFile) {
                    //            SignSearch(dcsFile.Data, setting,
                    //                FileExplorerHelper.GetFileExplorerServiceProvider(csFileUnit.Evidence));
                    //        }
                    //    }
                    //}
                    //catch (Exception ex) {
                    //    Logger.WriteLine($"{nameof(CommandDefinitions)}-{nameof(CustomSSearchCommand)}:{ex.Message}");
                    //}
                    //finally {
                    //    ServiceProvider.Current.GetInstance<IShellService>()?.ChangeLoadState(false, string.Empty);
                    //}
                }
                //() => (NodeService?.SelectedNode is ICaseEvidenceUnit<CaseEvidence> csFUnit) && (csFUnit.Evidence is IHaveData<Device>
                
            );
        
        //[Export(SingularityForensic.FileExplorer.Constants.DeviceNodeContextCommand)]
        //public static readonly ICommandItem CustomSSearchMI = CommandItemFactory.CreateNew(CustomSSearchCommand) {
        //    Name = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("CustomSignSearch")
        //};

        /// <summary>
        /// 签名搜索;
        /// </summary>
        /// <param name="blDevice"></param>
        /// <param name="setting"></param>
        private static void SignSearch(IStreamFile blDevice, ICustomSignSearchSetting setting) {
            //Device device = null;
            //long startLBA = 0;
            //long endLBA = 0;
            //if (blDevice is Device) {
            //    device = blDevice as Device;
            //    endLBA = device.Size - 1;
            //}
            //else if (blDevice is Partition) {
            //    var part = blDevice as Partition;
            //    device = blDevice.GetParent<Device>();
            //    startLBA = part.StartLBA;
            //    endLBA = part.EndLBA;

            //}

            //if (device != null) {
            //    var dialog = new ProgressMessageBox();
            //    dialog.WindowTitle = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("SignSearch");

            //    SearcherPartition part = null;

            //    dialog.DoWork += (sender, e) => {
            //        var searcher = new SignSearcher(device.Stream, setting.KeyWord, setting.MaxSize, setting.SectorSize, setting.SecStartLBA);
            //        searcher.AlignToSector = setting.AlignToSec;
            //        searcher.FileExtension = setting.FileExtension;

            //        searcher.CurOffsetChanged += (insender, curOffset) => {
            //            var percentage = (int)((curOffset - startLBA) * 100 / (endLBA - startLBA));
            //            if (percentage >= 0 && percentage <= 100) {
            //                dialog.ReportProgress(percentage,
            //                ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("SearchingSignFile"),
            //                $"{FindResourceString("RecoveringBySign")}:{percentage}%");
            //            }
            //            if (dialog.CancellationPending) {
            //                searcher.Stop();
            //            }
            //        };

            //        searcher.SearchStart(startLBA, endLBA);

            //        var fileList = new List<RegularFile>();
            //        var shfileList = new List<RegularFile>();

            //        try {
            //            //遍历获取文件列表;
            //            searcher.FileExtension = setting.FileExtension;
            //            var ndList = searcher.GetFileList(string.Empty);
            //            if (ndList?.Count != 0) {
            //                shfileList.AddRange(ndList.Select(p => new SearcherFile(part, p)));
            //            }
            //            fileList.AddRange(shfileList);

            //            part = SearcherPartition.LoadFromNodeList(device,ndList,
            //                $"{blDevice.Name}-{FindResourceString("SignSearch")}(${ByteConverterHelper.ByteToHex(setting.KeyWord)})");
            //        }
            //        catch (Exception ex) {
            //            Logger.WriteLine($"{nameof(CommandDefinitions)} -> {nameof(RecoverSign)}:{ex.Message}");
            //        }
            //        finally {
            //            searcher.Dispose();
            //        }
            //    };
            //    dialog.RunWorkerCompleted += (sender, e) => {
            //        ServiceProvider.Current.GetInstance<IFSNodeService>()?.AddShowingFile(part, fsProvider);
            //        ServiceProvider.Current.GetInstance<IShellService>()?.Focus();
            //    };
            //    dialog.ShowDialog();
            //}
        }
    }
}
