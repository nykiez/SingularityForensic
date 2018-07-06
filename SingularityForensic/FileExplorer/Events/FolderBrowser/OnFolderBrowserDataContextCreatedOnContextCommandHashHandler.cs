using CDFC.Util.IO;
using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Hash;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;

namespace SingularityForensic.FileExplorer.Events.FolderBrowser
{
    /// <summary>
    /// 分区资源管理器加入时加入哈希相关右键菜单;
    /// </summary>
    [Export(typeof(IFolderBrowserDataContextCreatedEventHandler))]
    partial class OnFolderBrowserDataContextCreatedOnContextCommandHashHandler : IFolderBrowserDataContextCreatedEventHandler {
        public int Sort => 2;

        public bool IsEnabled => true;

        public void Handle(IFolderBrowserDataContext dataContext) {
            if (dataContext == null) {
                throw new ArgumentNullException(nameof(dataContext));
            }
            var vm = dataContext.FolderBrowserViewModel;
            //加入计算哈希值菜单;
            vm.AddContextCommand(CreateComputeHashCommandItem(vm));
            //加入加入哈希集菜单;
            vm.AddContextCommand(CreateAddToHashSetCommandItem(vm)); 
        }
    }

    partial class OnFolderBrowserDataContextCreatedOnContextCommandHashHandler {
        /// <summary>
        /// 创建计算哈希值菜单;
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        private static ICommandItem CreateComputeHashCommandItem(IFolderBrowserViewModel vm) {
            var cmi = CommandItemFactory.CreateNew(null);
            cmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_ComputeHash);
            var commandItems = CreateComputeHashCommandItems(vm);
            foreach (var cm in commandItems) {
                cmi.AddChild(cm);
            }
            return cmi;
        }

        private static IEnumerable<ICommandItem> CreateComputeHashCommandItems(IFolderBrowserViewModel vm) {
            var hashers = GenericServiceStaticInstances<IHasher>.Currents;
            foreach (var hasher in hashers) {
                var comm = CreateComputeHashCommand(vm, new IHasher[] { hasher });
                var cmi = CommandItemFactory.CreateNew(comm);
                cmi.Name = hasher.HashTypeName;
                yield return cmi;
            }
            var allComm = CreateComputeHashCommand(vm, hashers);
            var allCmi = CommandItemFactory.CreateNew(allComm);
            allCmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_ComputeAllHash);
            yield return allCmi;
        }

        private static DelegateCommand CreateComputeHashCommand(IFolderBrowserViewModel vm, IEnumerable<IHasher> hashers) {
            var comm = new DelegateCommand(() => {
                ComputeHashCore(vm, hashers);
            });
            return comm;
        }

        private static void ComputeHashCore(IFolderBrowserViewModel vm, IEnumerable<IHasher> hashers) {
            var slRows = vm.SelectedFileRows;
            if (slRows == null) {
                return;
            }

            var inputStreamAndFileTuples = new List<(IFileRow fileRow, Stream inputStream)>();
            foreach (var row in slRows) {
                var inputStream = row.File?.GetInputStream();
                if (inputStream == null) {
                    continue;
                }

                inputStreamAndFileTuples.Add((row, inputStream));
            }

            var loadingDialog = DialogService.Current.CreateLoadingDialog();
            loadingDialog.DoWork += delegate { ComputeHashOnDialog(loadingDialog, hashers, inputStreamAndFileTuples); };
            loadingDialog.ShowDialog();
        }


        /// <summary>
        /// 在进度窗体当中计算哈希;
        /// </summary>
        /// <param name="loadingDialog"></param>
        /// <param name="hasher"></param>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        private static void ComputeHashOnDialog(
            ILoadingDialog loadingDialog,
            IEnumerable<IHasher> hashers,
            IEnumerable<(IFileRow fileRow, Stream inputStream)> inputStreamAndFileTuples
        ) {
            var hasherIndex = 0;
            var hasherCount = hashers.Count();
            var allStreamSizeSum = inputStreamAndFileTuples.Sum(p => p.inputStream.Length);
            long finishedStreamSizeSum = 0;

            foreach (var hasher in hashers) {
                var metaGUID = $"{Constants.FileHashMetaDataProvider_GUIDPrefix}{hasher.GUID}";

                foreach (var tuple in inputStreamAndFileTuples) {
                    var reporter = ProgessReporterFactory.CreateNew();
                    reporter.ProgressReported += (sender, e) => {
                        var thisStreamFinishedSize = tuple.inputStream.Length * e.totalPer;
                        loadingDialog.ReportProgress((int)((finishedStreamSizeSum + thisStreamFinishedSize) * 100 / allStreamSizeSum));
                    };
                    loadingDialog.Canceld += delegate {
                        reporter.Cancel();
                    };

                    var result = ComputeHashValue(hasher, tuple.inputStream, reporter);
                    if (result != null) {
                        var hashValue = result.BytesToHexString();

                        tuple.fileRow.File.ExtensibleTag.SetInstance(hashValue, metaGUID);
                        tuple.fileRow.NotifyProperty(metaGUID);
                    }

                    finishedStreamSizeSum += tuple.inputStream.Length;
                    if (loadingDialog.CancellationPending) {
                        break;
                    }

                    //尝试释放流;
                    try {
                        tuple.inputStream.Dispose();
                    }
                    catch (Exception ex) {
                        LoggerService.WriteCallerLine(ex.Message);
                    }
                }

                if (loadingDialog.CancellationPending) {
                    break;
                }

                hasherIndex++;
            }

        }

        /// <summary>
        /// 计算哈希;
        /// </summary>
        /// <param name="hasher"></param>
        /// <param name="inputStream"></param>
        /// <param name="progressReporter">通知,取消回调</param>
        /// <returns></returns>
        private static byte[] ComputeHashValue(IHasher hasher, Stream inputStream, IProgressReporter progressReporter) {
            var opStream = new OperatebleStream(inputStream) {
                Position = 0
            };

            progressReporter.Canceld += delegate {
                opStream.Break();
            };

            //订阅流位置变更事件,通知进度;
            opStream.PositionChanged += (sender, e) => {
                progressReporter.ReportProgress((int)(e * 100 / opStream.Length));
            };

            var bts = hasher.ComputeHash(inputStream);
            //若被中止,则返回为空;
            if (opStream.Broken || (progressReporter?.CancelPending ?? false)) {
                return null;
            }
            return bts;
        }
    }

    partial class OnFolderBrowserDataContextCreatedOnContextCommandHashHandler {
        /// <summary>
        /// 创建"加入到哈希集"菜单;
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        private static ICommandItem CreateAddToHashSetCommandItem(IFolderBrowserViewModel vm) {
            if(vm == null) {
                throw new ArgumentNullException(nameof(vm));
            }

            var cmi = CommandItemFactory.CreateNew(CreateAddToHashSetCommand(vm));
            cmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_AddToHashSet);
            return cmi;
        }

        /// <summary>
        /// 创建"加入到哈希集"命令;
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        private static DelegateCommand CreateAddToHashSetCommand(IFolderBrowserViewModel vm) {
            var comm = new DelegateCommand(() => {
                var hashSet = HashSetDialogService.SelectedHashSet();
                if(hashSet == null) {
                    return;
                }

                AddToHashSet(hashSet, vm);
            });

            return comm;
        }

        private static void AddToHashSet(IHashSet hashSet,IFolderBrowserViewModel vm) {
            var loadingDialog = DialogService.Current.CreateLoadingDialog();
            loadingDialog.WindowTitle = LanguageService.FindResourceString(Constants.WindowTitle_AddingToHashSet);
            loadingDialog.DoWork += delegate {
                AddToHashSetCore(loadingDialog,hashSet,vm);
            };

            loadingDialog.IsProgressVisible = false;
            loadingDialog.ShowDialog();
            
            //foreach (var row in vm.SelectedFileRows) {
            //    row.File
            //}
        }

        private static void AddToHashSetCore(ILoadingDialog loadingDialog,IHashSet hashSet,IFolderBrowserViewModel vm) {
            try {
                hashSet.BeginEdit();
                var rows = vm.SelectedFileRows;
                if(rows == null) {
                    return;
                }
                var metaGUID = $"{Constants.FileHashMetaDataProvider_GUIDPrefix}{hashSet.Hasher.GUID}";
                var rowCount = 0;
                var errCount = 0;
                var succeedCount = 0;

                foreach (var row in rows) {
                    var hashValue = row.File.ExtensibleTag.GetInstance<string>(metaGUID);
                    if(hashValue != null && hashValue.Length == hashSet.Hasher.BytesPerHashValue * 2) {
                        hashSet.AddHashPair(row.File.Name, hashValue);
                    }
                    else {
                        errCount ++;
                    }
                    rowCount++;

                    if(rowCount % 100 == 0) {
                        loadingDialog.ReportProgress(0, $"{rowCount}",string.Empty);
                    }
                }
            }
            catch (Exception ex) {
                LoggerService.WriteException(ex);
            }
            finally {
                hashSet.EndEdit();
            }
        }
    }
}
