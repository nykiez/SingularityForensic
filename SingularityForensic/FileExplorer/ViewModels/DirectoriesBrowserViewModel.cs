using CDFCMessageBoxes.MessageBoxes;
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
using SingularityForensic.Contracts.FileSystem;

namespace SingularityForensic.FileExplorer.ViewModels {

    //分区-文件视图;
    public partial class DirectoriesBrowserViewModel : FolderBrowserViewModel {
        public DirectoriesBrowserViewModel(FileBase file) : base(file) {

        }

        //恢复所有项的命令;
        private DelegateCommand _recCheckedCommand;
        public DelegateCommand RecCheckedCommand =>
            _recCheckedCommand ??
            (_recCheckedCommand = new DelegateCommand(() => {
                RecoverFiles(FileRows.Where(p => p.Checked).Select(p => (p as IFileRow<FileBase>).File).ToArray());
            }, () => FileRows.FirstOrDefault(p => p.Checked) != null));

        private void RecoverFiles(IEnumerable<FileBase> files) {
            if (files == null)
                throw new ArgumentNullException(nameof(files));

            long readSize = 0;
            long totalSize = 0;
            try {
                #region 统计总大小;
                foreach (var innerfile in files) {
                    if (innerfile is Directory direc) {
                        if (!direc.IsBackDir() && !direc.IsBackUpDir()) {
                            totalSize += direc.GetSubSize();
                        }
                    }
                    else if (innerfile is RegularFile) {
                        totalSize += innerfile.Size;
                    }
                }
                #endregion
            }
            catch (Exception ex) {
                Logger.WriteLine($"{nameof(FolderBrowserViewModel)}->{nameof(RecoverFiles)} Computing Size:{ex.Message}");
                RemainingMessageBox.Tell($"{LanguageService.FindResourceString("FaileToToComputeTotalSize")}:{ex.Message}");
                return;
            }


            var proDialog = new ProgressMessageBox {
                WindowTitle = $"{LanguageService.FindResourceString("FilesBeingCopied")}"
            };

            Action<RegularFile, string, string> saveFileFunc = (rFile, drPath, fileName) => {
                try {
                    var fs = SysIO.File.Create($"{drPath}/{fileName ?? rFile.Name}");
                    int read;

                    using (var mulS = rFile.GetInputStream()) {
                        var buffer = new byte[10485760];
                        mulS.Position = 0;
                        while ((read = mulS.Read(buffer, 0, buffer.Length)) != 0
                        && !proDialog.CancellationPending) {
                            fs.Write(buffer, 0, read);
                            readSize += read;
                            var pro = (int)(readSize * 100 / (totalSize != 0 ? totalSize : 4096));
                            proDialog.ReportProgress(pro <= 100 ? pro : 100, null,
                                $"{LanguageService.FindResourceString("CurExtractingFile")}:{fileName}");
                        }
                    }
                    fs.Close();
                }
                catch (Exception ex) {
                    Logger.WriteLine($"{nameof(FolderBrowserViewModel)}->{nameof(CopyOrRecvCommand)}:{ex.Message}");
                    Application.Current.Dispatcher.Invoke(() => {
                        MsgBoxService.ShowError($"{LanguageService.FindResourceString("FailedToExtractFile")}:{ex.Message}");
                        //CDFCMessageBox.Show($"{FindResourceString("FailedToExtractFile")}:{ex.Message}");
                    });
                }
            };

            if (files.Count() == 1 && files.FirstOrDefault() is RegularFile file) {
                
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
                                MsgBoxService.Show(LanguageService.FindResourceString("Finished"));
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
                        foreach (var innerfile in files) {
                            if (innerfile is Directory direct) {
                                TraverseSaveDirectory(direct, drPath, saveFileFunc, () => proDialog.CancellationPending);
                            }
                            else if (innerfile is RegularFile regFile) {
                                saveFileFunc(regFile, drPath, innerfile.Name);
                            }
                            if (proDialog.CancellationPending) {
                                break;
                            }
                        }
                    };
                    proDialog.RunWorkerCompleted += (sender, e) => {
                        if (!e.Cancelled) {
                            CDFCMessageBox.Show(LanguageService.FindResourceString("Finished"));
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

                    if (p is Directory direct) {
                        if (!SysIO.Directory.Exists($"{drPath}/{dir.Name}")) {
                            try {
                                SysIO.Directory.CreateDirectory($"{drPath}/{dir.Name}");
                            }
                            catch (Exception ex) {
                                Logger.WriteLine($"{nameof(FolderBrowserViewModel)}->{nameof(TraverseSaveDirectory)} Creating Directory:{ex.Message}");
                                Application.Current.Dispatcher.Invoke(() => {
                                    RemainingMessageBox.Tell($"{LanguageService.FindResourceString("FailedToCreateDirectory")} {drPath}/{dir.Name}:{ex.Message}");
                                });
                            }
                        }
                        if (!direct.IsBackDir() && !direct.IsBackUpDir() && direct.Name != ".." && direct.Name != ".") {
                            TraverseSaveDirectory(direct, $"{drPath}/{dir.Name}", saveFileFunc, isCancel);
                        }
                    }
                    else if (p is RegularFile regFile) {
                        saveFileFunc(regFile, $"{drPath}/{dir.Name}", p.Name);
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
                () => (SelectedFileRow as IFileRow<FileBase>)?.File is RegularFile).
            ObservesProperty(() => SelectedFileRow));

        private ObservableCollection<CommandItem> _contextCommands;
        public override ObservableCollection<CommandItem> ContextCommands {
            get {
                var mainViewerCommandItem = new CommandItem {  CommandName = LanguageService.Current?.FindResourceString("ViewerProgram") };
                mainViewerCommandItem.Children.AddRange(ViewersCommands);
                if (_contextCommands == null) {
                    var navCm = new CommandItem {
                        CommandName = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("Navigation")
                    };
                    navCm.Children.Add(new CommandItem {
                        CommandName = LanguageService.Current?.FindResourceString("ListClusters"),
                        Command = ListBlocksCommand });

                    _contextCommands = new ObservableCollection<CommandItem>() {
                        navCm,
                        new CommandItem {
                            CommandName = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("OpenFile"),
                            Command = OpenFileCommand
                        },
                        new CommandItem{ CommandName = LanguageService.FindResourceString("RecoverChecked"),Command=RecCheckedCommand },
                        mainViewerCommandItem,
                        new CommandItem{ CommandName = LanguageService.FindResourceString("ExtractOrCopy") , Command = CopyOrRecvCommand },
                        new CommandItem{ CommandName = LanguageService.FindResourceString("CheckSelected") , Command = CheckSelectedCommand },
                        new CommandItem{ CommandName = LanguageService.FindResourceString("UnCheckSelected"), Command = UnCheckSelectedCommand },
                        new CommandItem{ CommandName = LanguageService.FindResourceString("FileDetailInfo") , Command = ShowFileDetailCommand }
                    };

                    _contextCommands.AddRange(base.ContextCommands);

                    //var externCommandItems = ServiceProvider.Current.GetAllInstances<CommandItem<IFileRow>>();
                    //var externTupleCommandItems = ServiceProvider.Current.GetAllInstances<CommandItem<(DirectoriesBrowserViewModel, IFileRow)>>();

                    //void SetCommandItems<TData>(IEnumerable<CommandItem<TData>> items, Func<TData> valFunc) {
                    //    if (items != null) {
                    //        foreach (var cmi in items) {
                    //            cmi.GetData = valFunc;
                    //            _contextCommands.Add(cmi);
                    //        }
                    //    }
                    //};

                    //SetCommandItems(externCommandItems, () => SelectedFileRow);
                    //SetCommandItems(externTupleCommandItems, () => (this, SelectedFileRow));
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
                        if ((SelectedFileRow as IFileRow<FileBase>).File is Directory
                        && (SelectedFileRow as IFileRow<FileBase>).File is Directory dir) {
                            if (dir.IsBackDir() || dir.IsBackUpDir()) {
                                MsgBoxService.Show(LanguageService.FindResourceString("RootOrBackNodeFileCannotBeExtracted"));
                            }
                            else {
                                RecoverFiles(new FileBase[] { (SelectedFileRow as IFileRow<FileBase>).File });
                            }
                        }
                        else if ((SelectedFileRow as IFileRow<FileBase>).File is RegularFile) {
                            RecoverFiles(new FileBase[] { (SelectedFileRow as IFileRow<FileBase>).File });
                        }
                    }, () => SelectedFileRow != null 
                    //&& (SelectedFileRow as IFileRow<FileBase>).File is BlockedStreamFile)
                    ));
            }
        }
    }
}
