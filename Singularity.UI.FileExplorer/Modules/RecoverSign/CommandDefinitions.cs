using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using static CDFCCultures.Managers.ManagerLocator;
using CDFC.Parse.Abstracts;
using EventLogger;
using CDFCUIContracts.Commands;
using CDFC.Parse.Contracts;
using CDFCMessageBoxes.MessageBoxes;
using Singularity.UI.MessageBoxes.MessageBoxes;
using System.Threading;
using System.IO;
using Singularity.Contracts.Common;
using Singularity.Contracts.FileSystem;
using Singularity.Contracts.MainPage;
using Singularity.Contracts.Shell;
using Singularity.Contracts.Case;
using CDFC.Parse.Signature.DeviceObjects;
using CDFC.Parse.Signature.Contracts;
using Singularity.Contracts.FileExplorer;
using CDFC.Parse.Signature.Pictures;

namespace Singularity.UI.FileExplorer.Modules.RecoverSign {
    public static class CommandDefinitions {
        private static INodeService _nodeService;
        private static INodeService NodeService => _nodeService ?? (_nodeService = ServiceProvider.Current.GetInstance<INodeService>());


        public static readonly DelegateCommand RecoverSignCommand = new DelegateCommand(
                () => {
                    ServiceProvider.Current.GetInstance<IShellService>()?.ChangeLoadState(true, string.Empty);
                    if (NodeService?.SelectedNode is ICaseEvidenceUnit<ICaseEvidence> haveCaseFile) {
                        try {
                            RecoverSign(haveCaseFile.Evidence, true);
                        }
                        catch (Exception ex) {
                            Logger.WriteCallerLine(ex.Message);
                        }
                    }
                    ServiceProvider.Current.GetInstance<IShellService>()?.ChangeLoadState(false, string.Empty);
                },
                () => (NodeService?.SelectedNode is ICaseEvidenceUnit<ICaseEvidence> csFUnit) && (csFUnit.Evidence is IHaveData<Device>)
            );

        [Export(Constances.DeviceNodeContextCommand)]
        public static readonly ICommandItem RecompositeSignCMI = new CommandItem {
            Command = RecoverSignCommand,
            CommandName = FindResourceString("MobileRecompositeBySign")
        };

        /// <summary>
        /// 重组;
        /// </summary>
        /// <param name="blDevice"></param>
        /// <param name="isReComposite"></param>
        private static void RecoverSign(ICaseEvidence caseFile, bool isReComposite = false) {
            Device device = null;
            long startLBA = 0;
            long endLBA = 0;
            BlockDeviceFile blDevice = null;

            if(caseFile is IHaveData<BlockDeviceFile> haveBLDevice) {
                blDevice = haveBLDevice.Data;

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
            }
            else {
                Logger.WriteCallerLine($"{nameof(caseFile)} is not a valid {nameof(BlockDeviceFile)}");
                return;
            }
            
            if (device != null) {
                var dialog = new ProgressMessageBox {
                    WindowTitle = isReComposite ? FindResourceString("MobileRecompositeBySign") : FindResourceString("RecoveredBySign")
                };

                string[] extensions = null;

                if (isReComposite) {
                    var setting = ReComPreSettingMessageBox.Show();
                    if (setting != null) {
                        extensions = setting.Extesions;
                    }
                    else {
                        return;
                    }
                }

                SearcherPartition part = null;

                dialog.DoWork += (sender, e) => {
                    IFileSearcher searcher = null;
                    if (!isReComposite) {
                        searcher = new PictureSearcher(device, device.SecSize);
                    }
                    else {
                        searcher = new RecompositeSearcher(device, device.SecSize);
                    }

                    bool done = false;
                    ThreadPool.QueueUserWorkItem(callBack => {
                        while (!done) {
                            var percentage = (int)((searcher.CurOffset - startLBA) * 100 / (endLBA - startLBA));
                            if (percentage >= 0 && percentage <= 100) {
                                dialog.ReportProgress(percentage,
                                FindResourceString("SearchingSignFile"),
                                $"{(isReComposite ? FindResourceString("MobileRecompositeBySign") : FindResourceString("RecoveringBySign"))}:{percentage}%");
                            }
                            if (dialog.CancellationPending) {
                                searcher.Stop();
                            }
                            Thread.Sleep(1000);
                        }
                    });

                    searcher.SearchStart(startLBA, endLBA);
                    done = true;

                    try {
                        List<IFileNode> ndList = null;
                        if (isReComposite) {
                            if (extensions != null) {
                                ndList = searcher.GetFileList(string.Empty);
                                ndList.RemoveAll(p => extensions.FirstOrDefault(q =>
                                p.Type == q) == null);
                            }

                        }
                        else {
                            var sr = new StreamReader("Attachments/sign.txt");
                            var line = string.Empty;
                            ndList = new List<IFileNode>();
                            while (!string.IsNullOrEmpty(line = sr.ReadLine()?.Trim())) {
                                ndList.AddRange(searcher.GetFileList(line));
                                dialog.ReportProgress(100, $"{FindResourceString("RestoringData")}",
                                    $"{FindResourceString("Format")}{line}");
                            }
                            sr.Close();
                        }
                        part = SearcherPartition.LoadFromNodeList(blDevice, ndList, $"{blDevice.Name}-{(isReComposite ? FindResourceString("MobileRecompositeBySign") : FindResourceString("RecoveredBySign"))}");
                    }
                    catch (Exception ex) {
                        Logger.WriteCallerLine(ex.Message);
                    }
                    finally {

                        searcher.Dispose();
                    }
                };
                dialog.RunWorkerCompleted += (sender, e) => {
                    ServiceProvider.Current.GetInstance<IFSNodeService>()?.AddShowingFile(
                        part,FileExplorerHelper.GetFileExplorerServiceProvider(caseFile));

                    ServiceProvider.Current.GetInstance<IShellService>()?.Focus();
                };

                dialog.ShowDialog();
            }

        }
    }
}
