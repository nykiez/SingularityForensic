using CDFCUIContracts.Commands;
using Prism.Commands;
using static CDFCCultures.Managers.ManagerLocator;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using CDFC.Parse.Abstracts;
using CDFCMessageBoxes.MessageBoxes;
using CDFC.Util.IO;
using System.Threading;
using CDFCCultures.Helpers;
using Singularity.UI.FileExplorer.ViewModels;
using Singularity.UI.MessageBoxes.MessageBoxes;
using CDFC.Hasher;
using CDFC.Hasher.Interfaces;
using Singularity.Contracts.FileExplorer;

namespace Singularity.UI.FileExplorer.Modules.Hash {
    [Export(typeof(CommandItem<(DirectoriesBrowserViewModel, IFileRow)>))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ComputeHashCommandItem : CommandItem<(DirectoriesBrowserViewModel Dvm, IFileRow Row)> {
        public ComputeHashCommandItem() {
            CommandName = FindResourceString("ComputeHashCommandItem");
            LoadChildren();
        }

        private void LoadChildren() {
            Children = new ObservableCollection<ICommandItem>();
            AddHasher(MD5Hasher.StaticInstance, FindResourceString("MD5HashCommandItem"));
            AddHasher(SHA1Hasher.StaticInstance, FindResourceString("SHA1HashCommandItem"));
            AddHasher(SHA256Hasher.StaticInstance, FindResourceString("SHA256HashCommandItem"));
            AddHasher(SHA512Hasher.StaticInstance, FindResourceString("SHA512HashCommandItem"));
        }

        private void AddHasher(IHasher hasher, string commandName) {
            if (Children == null) {
                return;
            }

            var comm = new DelegateCommand(
                () => {
                    if (GetData != null) {
                        var data = GetData();
                        if (data.Row is IFileRow<RegularFile> regFRow) {
                            var regFile = regFRow.File;
                            using (var stream = regFile.GetStream()) {
                                if (stream.Length == 0) {
                                    return;
                                }

                                var msg = new ProgressMessageBox {
                                    WindowTitle = CommandName
                                };

                                var canceld = false;
                                var done = false;
                                var operatebleStream = new OperatebleStream(stream);
                                operatebleStream.Position = 0;
                                var res = string.Empty;

                                msg.DoWork += (sender, e) => {
                                    operatebleStream.PositionChanged += (se, pos) => {
                                        msg.ReportProgress((int)(pos * 1000 / operatebleStream.Length));
                                    };

                                    ThreadPool.QueueUserWorkItem(cb => {
                                        var bts = hasher.ComputeStream(operatebleStream);
                                        res = ByteConverterHelper.ByteToHex(bts);
                                        done = true;
                                    });

                                    while (!done) {
                                        if (msg.CancellationPending) {
                                            canceld = true;
                                            operatebleStream.Break();
                                        }
                                        Thread.Sleep(100);
                                    }


                                };

                                msg.RunWorkerCompleted += (sender, e) => {
                                    if (!canceld) {
                                        InputValueMessageBox.Show(CommandName, string.Empty, res);
                                    }
                                };

                                msg.ShowDialog();
                            }

                        }
                    }

                }
            );

            Children.Add(new CommandItem {
                CommandName = commandName,
                Command = comm
            });
        }
    }
}
