using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using CDFCMessageBoxes.MessageBoxes;
using CDFCUIContracts.Commands;
using EventLogger;
using Ookii.Dialogs.Wpf;
using Prism.Commands;
using SingularityForensic.Contracts.FileExplorer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SysIO = System.IO;
using System.Windows;
using static CDFCCultures.Managers.ManagerLocator;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.App;

namespace SingularityForensic.Controls.FileExplorer.ViewModels {

    //分区-文件视图;
    public partial class DirectoriesBrowserViewModel : FolderBrowserViewModel {
        public DirectoriesBrowserViewModel(IFile file) : base(file) {

        }

        //恢复所有项的命令;
        private DelegateCommand _recCheckedCommand;
        public DelegateCommand RecCheckedCommand =>
            _recCheckedCommand ??
            (_recCheckedCommand = new DelegateCommand(() => {
                RecoverFiles(FileRows.Where(p => p.Checked).Select(p => (p as IFileRow<IFile>).File).ToArray());
            }, () => FileRows.FirstOrDefault(p => p.Checked) != null));

        private void RecoverFiles(IEnumerable<IFile> files) {
            if (files == null)
                throw new ArgumentNullException(nameof(files));

            long readSize = 0;
            long totalSize = 0;
            try {
                #region 统计总大小;
                foreach (var file in files) {
                    if (file.Type == FileType.Directory) {
                        var direc = file as Directory;
                        if (!direc.IsBackFile() && !direc.IsBackUpFile()) {
                            totalSize += direc.GetTotalSize();
                        }
                    }
                    else if (file.Type == FileType.RegularFile) {
                        totalSize += file.Size;
                    }
                }
                #endregion
            }
            catch (Exception ex) {
                Logger.WriteLine($"{nameof(FolderBrowserViewModel)}->{nameof(RecoverFiles)} Computing Size:{ex.Message}");
                RemainingMessageBox.Tell($"{FindResourceString("FaileToToComputeTotalSize")}:{ex.Message}");
                return;
            }


            var proDialog = new ProgressMessageBox {
                WindowTitle = $"{FindResourceString("FilesBeingCopied")}"
            };

            Action<RegularFile, string, string> saveFileFunc = (rFile, drPath, fileName) => {
                try {
                    var fs = SysIO.File.Create($"{drPath}/{fileName ?? rFile.Name}");
                    int read;

                    using (var mulS = rFile.GetStream()) {
                        var buffer = new byte[10485760];
                        mulS.Position = 0;
                        while ((read = mulS.Read(buffer, 0, buffer.Length)) != 0
                        && !proDialog.CancellationPending) {
                            fs.Write(buffer, 0, read);
                            readSize += read;
                            var pro = (int)(readSize * 100 / (totalSize != 0 ? totalSize : 4096));
                            proDialog.ReportProgress(pro <= 100 ? pro : 100, null,
                                $"{FindResourceString("CurExtractingFile")}:{fileName}");
                        }
                    }
                    fs.Close();
                }
                catch (Exception ex) {
                    Logger.WriteLine($"{nameof(FolderBrowserViewModel)}->{nameof(CopyOrRecvCommand)}:{ex.Message}");
                    Application.Current.Dispatcher.Invoke(() => {
                        RemainingMessageBox.Tell($"{FindResourceString("FailedToExtractFile")}:{ex.Message}");
                        //CDFCMessageBox.Show($"{FindResourceString("FailedToExtractFile")}:{ex.Message}");
                    });
                }
            };

            if (files.Count() == 1 && files.ElementAt(0).Type == FileType.RegularFile) {
                var file = files.First() as RegularFile;

                if (file != null) {
                    var dialog = new VistaSaveFileDialog();
                    dialog.FileName = file.Name;
                    if (dialog.ShowDialog() == true) {
                        var fullPath = dialog.FileName;
                        var drPath = fullPath.Substring(0, fullPath.LastIndexOf("\\"));
                        var fileName = fullPath.Substring(fullPath.LastIndexOf("\\") + 1);
                        proDialog.DoWork += (sender, e) => {
                            if (!SysIO.Directory.Exists(drPath)) {
                                SysIO.Directory.CreateDirectory(drPath);
                            }
                            saveFileFunc(file, drPath, fileName);
                        };
                        proDialog.RunWorkerCompleted += (sender, e) => {
                            if (!e.Cancelled) {
                                CDFCMessageBox.Show(FindResourceString("Finished"));
                            }
                        };

                        proDialog.ShowDialog(Application.Current.MainWindow);
                    }
                }
            }
            else {
                var dialog = new VistaFolderBrowserDialog();

                if (dialog.ShowDialog() == true) {
                    var drPath = dialog.SelectedPath;
                    proDialog.DoWork += (sender, e) => {
                        if (!SysIO.Directory.Exists(drPath)) {
                            SysIO.Directory.CreateDirectory(drPath);
                        }
                        foreach (var file in files) {
                            if (file.Type == FileType.Directory) {
                                TraverseSaveDirectory(file as Directory, drPath, saveFileFunc, () => proDialog.CancellationPending);
                            }
                            else if (file.Type == FileType.RegularFile) {
                                saveFileFunc(file as RegularFile, drPath, file.Name);
                            }
                            if (proDialog.CancellationPending) {
                                break;
                            }
                        }
                    };
                    proDialog.RunWorkerCompleted += (sender, e) => {
                        if (!e.Cancelled) {
                            CDFCMessageBox.Show(FindResourceString("Finished"));
                        }
                    };
                    proDialog.ShowDialog();
                }
            }
        }

        /// <summary>
        /// 递归保存目录;
        /// </summary>
        /// <param name="dir">目录本体</param>
        /// <param name="drPath">目标路径</param>
        /// <param name="saveFileFunc">文件保存通知进度委托</param>
        /// <param name="isCancel">动作是否取消委托</param>
        private void TraverseSaveDirectory(Directory dir, string drPath,
            Action<RegularFile, string, string> saveFileFunc, Func<bool> isCancel = null) {
            if(dir.Children != null) {
                foreach (var p in dir.Children) {
                    if (isCancel?.Invoke() == true) { return; }

                    if (p.Type == FileType.Directory) {
                        var direct = p as Directory;
                        if (!SysIO.Directory.Exists($"{drPath}/{dir.Name}")) {
                            try {
                                SysIO.Directory.CreateDirectory($"{drPath}/{dir.Name}");
                            }
                            catch (Exception ex) {
                                Logger.WriteLine($"{nameof(FolderBrowserViewModel)}->{nameof(TraverseSaveDirectory)} Creating Directory:{ex.Message}");
                                Application.Current.Dispatcher.Invoke(() => {
                                    RemainingMessageBox.Tell($"{FindResourceString("FailedToCreateDirectory")} {drPath}/{dir.Name}:{ex.Message}");
                                });
                            }
                        }
                        if (!direct.IsBackFile() && !direct.IsBackUpFile() && direct.Name != ".." && direct.Name != ".") {
                            TraverseSaveDirectory(direct, $"{drPath}/{dir.Name}", saveFileFunc, isCancel);
                        }
                    }
                    else if (p.Type == FileType.RegularFile) {
                        saveFileFunc(p as RegularFile, $"{drPath}/{dir.Name}", p.Name);
                    }
                }
            }
            
        }

        private DelegateCommand _openFileCommand;
        public DelegateCommand OpenFileCommand =>
            _openFileCommand ?? (_openFileCommand = new DelegateCommand(
                () => {
                    if (SelectedFileRow != null) {
                        OpenFile(SelectedFileRow);
                    }
                },
                () => (SelectedFileRow as IFileRow<IFile>)?.File is RegularFile).
            ObservesProperty(() => SelectedFileRow));

        private ObservableCollection<ICommandItem> _contextCommands;
        public override ObservableCollection<ICommandItem> ContextCommands {
            get {
                var mainViewerCommandItem = new CommandItem { Children = ViewersCommands, CommandName = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("ViewerProgram") };
                if (_contextCommands == null) {
                    _contextCommands = new ObservableCollection<ICommandItem>() {
                            new CommandItem {
                                CommandName = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("Navigation"),
                                Children = new ObservableCollection<ICommandItem> {
                                    new CommandItem{ CommandName=FindResourceString("ListClusters"),Command=ListBlocksCommand}
                                }
                            },
                            new CommandItem {
                                CommandName = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("OpenFile"),
                                Command = OpenFileCommand
                            },
                            new CommandItem{ CommandName=FindResourceString("RecoverChecked"),Command=RecCheckedCommand },
                            mainViewerCommandItem,
                            new CommandItem{ CommandName=FindResourceString("ExtractOrCopy") , Command = CopyOrRecvCommand },
                            new CommandItem{ CommandName=FindResourceString("CheckSelected") , Command = CheckSelectedCommand },
                            new CommandItem{ CommandName=FindResourceString("UnCheckSelected"), Command = UnCheckSelectedCommand },
                            new CommandItem{ CommandName=FindResourceString("FileDetailInfo") , Command = ShowFileDetailCommand }
                    };

                    _contextCommands.AddRange(base.ContextCommands);

                    var externCommandItems = ServiceProvider.Current.GetAllInstances<CommandItem<IFileRow>>();
                    var externTupleCommandItems = ServiceProvider.Current.GetAllInstances<CommandItem<(DirectoriesBrowserViewModel, IFileRow)>>();

                    void SetCommandItems<TData>(IEnumerable<CommandItem<TData>> items, Func<TData> valFunc) {
                        if (items != null) {
                            foreach (var cmi in items) {
                                cmi.GetData = valFunc;
                                _contextCommands.Add(cmi);
                            }
                        }
                    };

                    SetCommandItems(externCommandItems, () => SelectedFileRow);
                    SetCommandItems(externTupleCommandItems, () => (this, SelectedFileRow));
                }

                return _contextCommands;
            }
            set => _contextCommands = value;
        }

        protected override void NotifyCheckStateChanged() {
            base.NotifyCheckStateChanged();
            RecCheckedCommand.RaiseCanExecuteChanged();
        }

        private DelegateCommand copyOrRecvCommand;                             //恢复或复制方法;
        public virtual DelegateCommand CopyOrRecvCommand {
            get {
                return copyOrRecvCommand ??
                    (copyOrRecvCommand = new DelegateCommand(() => {
                        if ((SelectedFileRow as IFileRow<IFile>).File.Type == FileType.Directory
                        && (SelectedFileRow as IFileRow<IFile>).File is Directory dir) {
                            if (dir.IsBackFile() || dir.IsBackUpFile()) {
                                CDFCMessageBox.Show(FindResourceString("RootOrBackNodeFileCannotBeExtracted"));
                            }
                            else {
                                RecoverFiles(new IFile[] { (SelectedFileRow as IFileRow<IFile>).File });
                            }
                        }
                        else if ((SelectedFileRow as IFileRow<IFile>).File.Type == FileType.RegularFile) {
                            RecoverFiles(new IFile[] { (SelectedFileRow as IFileRow<IFile>).File });
                        }
                    }, () => SelectedFileRow != null && (SelectedFileRow as IFileRow<IFile>).File.Type != FileType.BlockDeviceFile));
            }
        }
    }
}
