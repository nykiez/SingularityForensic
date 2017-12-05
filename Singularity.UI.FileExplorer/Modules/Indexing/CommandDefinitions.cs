using CDFC.Parse.Abstracts;
using Prism.Commands;
using Singularity.UI.MessageBoxes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using static CDFCCultures.Managers.ManagerLocator;
using Microsoft.Practices.ServiceLocation;
using CDFCUIContracts.Commands;
using Singularity.UI.MessageBoxes.MessageBoxes;
using EventLogger;
using CDFC.Parse.Contracts;
using CDFCMessageBoxes.MessageBoxes;
using Singularity.Contracts.FileSystem;
using Singularity.Contracts.Case;
using Singularity.Contracts.Common;
using Singularity.Contracts.MainPage;
using Singularity.Contracts.Shell;
using CDFC.Parse.Signature.DeviceObjects;
using CDFC.Parse.Signature.Pictures;
using Singularity.Contracts.FileExplorer;

namespace Singularity.UI.FileExplorer.Modules.Indexing {
    public static class CommandDefinitions {
        private static INodeService _nodeService;
        private static INodeService NodeService => _nodeService ?? (_nodeService = ServiceProvider.Current.GetInstance<INodeService>());

        public static readonly DelegateCommand CustomSSearchCommand = new DelegateCommand(
                () => {
                    ServiceProvider.Current.GetInstance<IShellService>()?.ChangeLoadState(true, string.Empty);
                    try {
                        var msg = new SignSearchMessageBox();
                        var setting = msg.Show();

                        if (setting != null) {
                            if (NodeService?.SelectedNode is ICaseEvidenceUnit<ICaseEvidence> csFileUnit && 
                            csFileUnit.Evidence is IHaveData<Device> dcsFile) {
                                SignSearch(dcsFile.Data, setting,FileExplorerHelper.GetFileExplorerServiceProvider(csFileUnit.Evidence));
                            }
                        }
                    }
                    catch (Exception ex) {
                        Logger.WriteLine($"{nameof(CommandDefinitions)}-{nameof(CustomSSearchCommand)}:{ex.Message}");
                    }
                    finally {
                        ServiceProvider.Current.GetInstance<IShellService>()?.ChangeLoadState(false, string.Empty);
                    }
                },
                () => (NodeService?.SelectedNode is ICaseEvidenceUnit<ICaseEvidence> csFUnit) && (csFUnit.Evidence is IHaveData<Device>)
            );
        
        [Export(Constances.DeviceNodeContextCommand)]
        public static readonly ICommandItem CustomSSearchMI = new CommandItem {
            Command = CustomSSearchCommand,
            CommandName = FindResourceString("CustomSignSearch")
        };

        /// <summary>
        /// 签名搜索;
        /// </summary>
        /// <param name="blDevice"></param>
        /// <param name="setting"></param>
        private static void SignSearch(BlockDeviceFile blDevice, SignSearchSetting setting, IFileExplorerServiceProvider fsProvider) {
            Device device = null;
            long startLBA = 0;
            long endLBA = 0;
            if (blDevice is Device) {
                device = blDevice as Device;
                endLBA = device.Size - 1;
            }
            else if (blDevice is Partition) {
                var part = blDevice as Partition;
                device = blDevice.GetParent<Device>();
                startLBA = part.StartLBA;
                endLBA = part.EndLBA;

            }

            if (device != null) {
                var dialog = new ProgressMessageBox();
                dialog.WindowTitle = FindResourceString("SignSearch");

                var part = new SearcherPartition(device, blDevice, startLBA, endLBA, $"{blDevice.Name}-{FindResourceString("SignSearch")}");

                dialog.DoWork += (sender, e) => {
                    var searcher = new SignSearcher(device.Stream, setting.KeyWord, setting.MaxSize, setting.SectorSize, setting.SecStartLBA);
                    searcher.AlignToSector = setting.AlignToSec;
                    searcher.FileExtension = setting.FileExtension;

                    searcher.CurOffsetChanged += (insender, curOffset) => {
                        var percentage = (int)((curOffset - startLBA) * 100 / (endLBA - startLBA));
                        if (percentage >= 0 && percentage <= 100) {
                            dialog.ReportProgress(percentage,
                            FindResourceString("SearchingSignFile"),
                            $"{FindResourceString("RecoveringBySign")}:{percentage}%");
                        }
                        if (dialog.CancellationPending) {
                            searcher.Stop();
                        }
                    };

                    searcher.SearchStart(startLBA, endLBA);

                    var fileList = new List<RegularFile>();
                    var shfileList = new List<RegularFile>();

                    try {
                        //遍历获取文件列表;
                        searcher.FileExtension = setting.FileExtension;
                        var ndList = searcher.GetFileList(string.Empty);
                        if (ndList?.Count != 0) {
                            shfileList.AddRange(ndList.Select(p => new SearcherFile(part, p)));
                        }
                        shfileList.ForEach(p => {
                            fileList.Add(p);
                        });
                    }
                    catch (Exception ex) {
                        Logger.WriteLine($"{nameof(CommandDefinitions)} -> {nameof(RecoverSign)}:{ex.Message}");
                    }
                    finally {
                        part.Children.AddRange(fileList);
                        searcher.Dispose();
                    }
                };
                dialog.RunWorkerCompleted += (sender, e) => {
                    ServiceProvider.Current.GetInstance<IFSNodeService>()?.AddShowingFile(part, fsProvider);
                    ServiceProvider.Current.GetInstance<IShellService>()?.Focus();
                };
                dialog.ShowDialog();
            }
        }
    }
}
