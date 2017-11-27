using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Singularity.Interfaces;
using SingularityForensic.Modules.MainPage.Global.Services;
using SingularityForensic.Modules.Shell.Global.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using static CDFCCultures.Managers.ManagerLocator;
using CDFC.Parse.Abstracts;
using EventLogger;
using CDFCUIContracts.Commands;
using Singularity.UI.Case.Contracts;
using CDFC.Parse.Contracts;
using CDFCMessageBoxes.MessageBoxes;
using Singularity.UI.MessageBoxes.MessageBoxes;
using CDFC.Parse.Signature.DeviceObjects;
using CDFC.Parse.Signature.Contracts;
using CDFC.Parse.Signature.Pictures;
using System.Threading;
using System.IO;
using Singularity.UI.FileSystem.Global.Services;
using Singularity.UI.FileSystem.Helpers;
using Singularity.UI.FileSystem.Global;

namespace Singularity.UI.FileSystem.Modules.RecoverSign {
    public static class CommandDefinitions {
        private static INodeService _nodeService;
        private static INodeService NodeService => _nodeService ?? (_nodeService = ServiceLocator.Current.GetInstance<INodeService>());


        public static readonly DelegateCommand RecoverSignCommand = new DelegateCommand(
                () => {
                    ServiceLocator.Current.GetInstance<IShellService>()?.ChangeLoadState(true, string.Empty);
                    if (NodeService?.SelectedNode is IHaveData<ICaseFile> haveCaseFile) {
                        try {
                            RecoverSign(haveCaseFile.Data, true);
                        }
                        catch (Exception ex) {
                            Logger.WriteCallerLine(ex.Message);
                        }
                    }
                    ServiceLocator.Current.GetInstance<IShellService>()?.ChangeLoadState(false, string.Empty);
                },
                () => (NodeService?.SelectedNode is IHaveData<ICaseFile> csFUnit) && (csFUnit.Data is IHaveData<Device>)
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
        private static void RecoverSign(ICaseFile caseFile, bool isReComposite = false) {
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
                    ServiceLocator.Current.GetInstance<IFSNodeService>()?.AddShowingFile(part,FileSystemHelper.GetFileSystemServiceProvider(caseFile));

                    ServiceLocator.Current.GetInstance<IShellService>()?.Focus();
                };

                dialog.ShowDialog();
            }

        }
    }
}
