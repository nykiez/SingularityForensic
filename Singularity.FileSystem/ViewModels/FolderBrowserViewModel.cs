using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using CDFC.Parse.IO;
using CDFCMessageBoxes.MessageBoxes;
using CDFCUIContracts.Commands;
using Ookii.Dialogs.Wpf;
using Singularity.UI.MessageBoxes.MessageBoxes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SysIO = System.IO;
using static CDFCCultures.Managers.ManagerLocator;
using EventLogger;
using System.Windows;
using System.Threading;
using CDFCUIContracts.Events;
using System.Windows.Input;
using Prism.Commands;
using Singularity.UI.FileSystem.Models;
using SingularityForensic.Modules.Shell.Models;
using CDFCCultures.Helpers;
using Microsoft.Practices.ServiceLocation;
using Singularity.UI.Controls.ViewModels;
using CDFC.Parse.Signature.DeviceObjects;
using Singularity.UI.Case.Helpers;

namespace Singularity.UI.FileSystem.ViewModels {
    //目录/资源浏览器模型;
    public abstract partial class FolderBrowserViewModel : DataGridExViewModel {
        /// <summary>
        /// 目录/资源浏览器模型构造方法;
        /// </summary>
        /// <param name="file">模型所属主文件</param>
        public FolderBrowserViewModel(IFile file) {
            if (file != null) {
                //this.CurFile = file;                            //初始化当前正在浏览的文件;
                if (file.FileType == FileType.BlockDeviceFile) {
                    this.File = file;                               //初始化对象文件;
                }
                else if (file.FileType == FileType.Directory) {
                    var part = file.GetParent<Partition>();
                    if (part != null) {
                        this.File = part;
                    }
                }
            }
            else {
                throw new ArgumentNullException($"{nameof(file)} can't be null");
            }

            
        }

        public readonly IFile File;                                        //浏览器所属主文件（分区，设备等);
        
        public ObservableCollection<NavNodeModel> NavNodes { get; set; } = new ObservableCollection<NavNodeModel>();

        //文件/资源行;(须在外部指定实例);
        public ObservableCollection<FileRow> FileRows { get; set; } 

        public event EventHandler<TEventArgs<FileRow>> SelectedFileRowChanged;               //当当前选择文件变化时触发;

        private FileRow _selectedFileRow;                                    //当前选定的文件行;
        public FileRow SelectedFileRow {
            get {
                return _selectedFileRow;
            }
            set {
                SetProperty(ref _selectedFileRow, value);
                if (_selectedFileRow != null && _selectedFileRow.File != null) {
                    SelectedFileRowChanged?.Invoke(this,new TEventArgs<FileRow>( _selectedFileRow ));
                }
            }
        }

        private FileRow _focusRow;                                                  //当前聚焦行，用于滚动视图;
        public FileRow FocusRow {
            get => _focusRow;
            set => SetProperty(ref _focusRow, value);
        }

        public event EventHandler<TEventArgs< ViewerProgramMessage >> WatchRequired;
        private ObservableCollection<ICommandItem> _viewersCommands;
        public ObservableCollection<ICommandItem> ViewersCommands {
            get {
                if (_viewersCommands == null) {
                    _viewersCommands = new ObservableCollection<ICommandItem>();
                    LoadViewers();
                }
                return _viewersCommands;
            }
            set {
                _viewersCommands = value;
            }
        }
        internal class ViewerProgramCommandItem : ICommandItem {
            public ViewerProgramCommandItem(ViewerProgramModel programModel) {
                if (programModel == null)
                    throw new ArgumentNullException(nameof(programModel));

                this.VProgramModel = programModel;
            }

            public ObservableCollection<ICommandItem> Children { get; set; }

            public ICommand Command {
                get => VProgramModel.WatchCommand;
                set => throw new NotImplementedException();
            }

            public string CommandName {
                get => VProgramModel.Name;
                set => throw new NotImplementedException();
            }

            

            public ViewerProgramModel VProgramModel { get; }
            public int SortOrder { get; set; }
        }
        private bool viewerEverLoaded;
        private void LoadViewers() {
            _viewersCommands.Clear();
            var pros = ViewerProgramHelper.ClientPrograms;
            Action<ViewerProgramModel> addEventAct = model => {
                model.WatchRequired += (sender, e) => {
                    var rModel = sender as ViewerProgramModel;
                    if (rModel != ViewerProgramModel.OtherProgramModel) {
                        try {
                            var regFile = SelectedFileRow.File as RegularFile;
                            var stream = StreamExtensions.CreateStreamByFile(regFile);
                            ThreadPool.QueueUserWorkItem(callBack => {
                                WatchRequired?.Invoke(this,new TEventArgs<ViewerProgramMessage>(
                                    new ViewerProgramMessage(rModel.Program.ProgramPath, regFile.Name, stream)));
                            });
                        }
                        catch {

                        }
                    }
                    else if (rModel != null) {
                        var dialog = new VistaOpenFileDialog();
                        dialog.Filter = $"({FindResourceString("Executable")})|*.exe";
                        if (dialog.ShowDialog() == true) {
                            var regFile = SelectedFileRow.File as RegularFile;
                            using (var stream = regFile.GetStream()) {
                                ThreadPool.QueueUserWorkItem(callBack => {
                                    WatchRequired?.Invoke(this, new TEventArgs<ViewerProgramMessage>(
                                        new ViewerProgramMessage(dialog.FileName, regFile.Name, stream)));
                                });

                                var proName = IOPathHelper.GetFileNameFromUrl(dialog.FileName);
                                if (proName != null) {
                                    ViewerProgramHelper.AddProgram(dialog.FileName, proName);
                                    LoadViewers();
                                }
                            }
                        }
                    }
                };
                model.CanSee = () => SelectedFileRow?.File?.FileType == FileType.RegularFile;
            };
            if (pros != null) {
                foreach (var item in pros) {
                    var model = new ViewerProgramModel(item);
                    addEventAct(model);
                    _viewersCommands.Add(new ViewerProgramCommandItem(model));
                }
                if (!viewerEverLoaded) {
                    addEventAct(ViewerProgramModel.OtherProgramModel);
                }
                viewerEverLoaded = true;
                _viewersCommands.Add(new ViewerProgramCommandItem(ViewerProgramModel.OtherProgramModel));
            }
            this.SelectedFileRowChanged += delegate {
                foreach (var command in _viewersCommands) {
                    (command.Command as DelegateCommand)?.RaiseCanExecuteChanged();
                }
            };
        }

        public void Exit() {
            NavNodes.Clear();
        }
        //~FolderBrowserViewModel() {

        //}
    }

    //目录/资源浏览器模型命令部分;
    public abstract partial class FolderBrowserViewModel {
        public event EventHandler<TEventArgs<FileRow>> RowEntered;                      //进入了某个文件行;

        //选择子文件时,进入该文件;
        public void EnterRow(FileRow row) {
            RowEntered?.Invoke(this,new TEventArgs<FileRow>( row ));
            if(row?.File is RegularFile regFile) {
                OpenFile(row);
            }
        }

        
        
        private ListBlockMessageBox listBlockMsg;
        public event EventHandler<TEventArgs< long >> FocusAddressChanged;

        private DelegateCommand listBlocksCommand;                             //列出簇命令;
        public DelegateCommand ListBlocksCommand {
            get {
                return listBlocksCommand ??
                    (listBlocksCommand = new DelegateCommand(() => {
                        if (listBlockMsg != null) {
                            listBlockMsg.Close();
                            listBlockMsg = null;
                        }

                        if (SelectedFileRow.File != null) {
                            var blockGroupedFile = SelectedFileRow.File as IBlockGroupedFile;
                            if(blockGroupedFile.BlockGroups == null) {
                                CDFCMessageBox.Show(FindResourceString("DeletedCannotBeListed"));
                            }
                            else {
                                listBlockMsg = new ListBlockMessageBox(blockGroupedFile.BlockGroups, SelectedFileRow.File);
                                listBlockMsg.SelectedAddressChanged += (sender, e) => {
                                    if (File is SearcherPartition) {
                                        if (listBlockMsg.File is RegularFile) {
                                            var regFile = listBlockMsg.File as RegularFile;

                                            FocusAddressChanged?.Invoke(this,new TEventArgs<long>((regFile.DeviceStartLBA - regFile.StartLBA) / 4096 + e) );
                                        }
                                    }
                                    else {
                                        FocusAddressChanged?.Invoke(this,new TEventArgs<long>( e ));
                                    }

                                };
                                listBlockMsg.Show();
                            }
                        }
                    }, () => SelectedFileRow != null && SelectedFileRow.File is IBlockGroupedFile));
            }
        }

        /// <summary>
        /// 选中目标行;
        /// </summary>
        /// <param name="rows"></param>
        public void CheckRows(IEnumerable<FileRow> rows, bool isCheck = true) {
            if (rows == null)
                throw new ArgumentNullException(nameof(rows));

            foreach (var row in rows) {
                row.Checked = isCheck;
            }
        }
        public event EventHandler CheckSelectedRequired;
        private DelegateCommand checkSelectedCommand;
        public DelegateCommand CheckSelectedCommand {
            get {
                return checkSelectedCommand ??
                    (checkSelectedCommand = new DelegateCommand(() => {
                        CheckSelectedRequired?.Invoke(this, new EventArgs());
                        NotifyCheckStateChanged();
                    }));
            }
        }

        

        //取消所有选中项相关;
        public event EventHandler UnCheckSelectedRequired;
        private DelegateCommand uncheckSelectedCommand;
        public DelegateCommand UnCheckSelectedCommand {
            get {
                return uncheckSelectedCommand ??
                    (uncheckSelectedCommand = new DelegateCommand(() => {
                        UnCheckSelectedRequired?.Invoke(this, new EventArgs());
                        NotifyCheckStateChanged();
                    }));
            }
        }
        protected virtual void NotifyCheckStateChanged() {

        }

        //是否全选;
        private bool? _allChecked = false;
        public bool? AllChecked {
            get {
                return _allChecked;
            }
            set {
                if (value != null) {
                    foreach (var row in FileRows) {
                        row.NotifyChecked(value.Value);
                    }
                }

                SetProperty(ref _allChecked, value);
            }
        }
       
        //显示详细信息相关;
        private DelegateCommand _showFileDetailCommand;
        public DelegateCommand ShowFileDetailCommand =>
            _showFileDetailCommand ?? (_showFileDetailCommand = new DelegateCommand(() => {
                if(SelectedFileRow.File != null) {
                    FileDetailMessageBox.Show(SelectedFileRow.File);
                }
            }));

        public void OpenFile(FileRow row) {
            try {
                
                if(row.LocalPath != null) {
                    if (SysIO.File.Exists(row.LocalPath)) {
                        
                        ExplorerHelper.OpenFile(row.LocalPath);
                    }
                }
            }
            catch {

            }
        }
    }

    //目录试图资源管理器模型过滤命令部分;
    public abstract partial class FolderBrowserViewModel {
        //过滤文件名事件;
        public event EventHandler FilterFileNameRequired;
        //过滤文件名命令;
        private DelegateCommand filterFileNameCommand;
        public DelegateCommand FilterFileNameCommand =>
            filterFileNameCommand ?? (filterFileNameCommand = new DelegateCommand(() => {
                FilterFileNameRequired?.Invoke(this,new EventArgs());
            }));

        //是否开启了过滤文件名;
        private bool _filterFileNameNeeded;
        public bool FilterFileNameNeeded {
            get {
                return _filterFileNameNeeded;
            }
            set {
                SetProperty(ref _filterFileNameNeeded, value);
            }
        }

        //过滤文件大小事件;
        public event EventHandler FilterFileSizeRequired;
        private DelegateCommand _filterFileSizeCommand;
        public DelegateCommand FilterFileSizeCommand =>
            _filterFileSizeCommand ?? (_filterFileSizeCommand = new DelegateCommand(() => {
                FilterFileSizeRequired?.Invoke(this, new EventArgs());
            }));
        //是否开启了过滤大小;
        private bool filterFileSizeNeeded;
        public bool FilterFileSizeNeeded {
            get {
                return filterFileSizeNeeded;
            }
            set {
                SetProperty(ref filterFileSizeNeeded, value);
            }
        }


        public event EventHandler FilterFilePathRequired;

        private DelegateCommand _filterFilePathCommand;
        public DelegateCommand FilterFilePathCommand =>
            _filterFilePathCommand ?? (_filterFilePathCommand = new DelegateCommand(() => {
                FilterFilePathRequired?.Invoke(this, new EventArgs());
            }));
        //public string FilterFileNameKey { get; private set; }
        //是否开启了过滤路径;
        private bool filterFilePathNeeded;
        public bool FilterFilePathNeeded {
            get {
                return filterFilePathNeeded;
            }
            set {
                SetProperty(ref filterFilePathNeeded, value);
            }
        }

        public event EventHandler FilterMTimeRequired;

        private DelegateCommand _filterMTimeCommand;
        public DelegateCommand FilterMTimeCommand =>
            _filterMTimeCommand ?? (_filterMTimeCommand = new DelegateCommand(() => {
                FilterMTimeRequired?.Invoke(this, new EventArgs());
            }));

        //是否开启了修改时间过滤;
        private bool filterMTimeNeeded;
        public bool FilterMTimeNeeded {
            get {
                return filterMTimeNeeded;
            }
            set {
                SetProperty(ref filterMTimeNeeded, value);
            }
        }
        
        public event EventHandler FilterATimeRequired;

        private DelegateCommand _filterATimeCommand;
        public DelegateCommand FilterATimeCommand =>
            _filterATimeCommand ?? (_filterATimeCommand = new DelegateCommand(() => {
                FilterATimeRequired?.Invoke(this, new EventArgs());
            }));
        //是否开启了访问时间过滤;
        private bool filterATimeNeeded;
        public bool FilterATimeNeeded {
            get {
                return filterATimeNeeded;
            }
            set {
                SetProperty(ref filterATimeNeeded, value);
            }
        }

        public event EventHandler FilterCTimeRequired;

        private DelegateCommand _filterCTimeCommand;
        public DelegateCommand FilterCTimeCommand =>
            _filterCTimeCommand ?? (_filterCTimeCommand = new DelegateCommand(() => {
                FilterCTimeRequired?.Invoke(this, new EventArgs());
            }));
        //是否开启了创建时间过滤;
        private bool filterCTimeNeeded;
        public bool FilterCTimeNeeded {
            get {
                return filterCTimeNeeded;
            }
            set {
                SetProperty(ref filterCTimeNeeded, value);
            }
        }

        

        ////是否开启了任何过滤;
        private bool anyFiltering;
        public bool AnyFiltering {
            get {
                return anyFiltering;
            }
            set {
                SetProperty(ref anyFiltering, value);
            }
        }

        //要求重新过滤事件;
        public event EventHandler RefilterRequired;         
        //一键开合(取消)所有的过滤;
        private DelegateCommand switchFilteringCommand;
        public DelegateCommand SwitchFilteringCommand =>
            switchFilteringCommand ?? (switchFilteringCommand = new DelegateCommand(() => {
                RefilterRequired?.Invoke(this, new EventArgs());
            }));
    }

    //递归展开视图部分;
    public abstract partial class FolderBrowserViewModel {
        //是否展开(递归浏览);
        private bool _isExpanded;
        public bool IsExpanded {
            get {
                return _isExpanded;
            }
            set {
                SetProperty(ref _isExpanded, value);
            }
        }
    }

    //磁盘-分区列表视图;
    public class PartitionsBrowserViewModel:FolderBrowserViewModel{
        public PartitionsBrowserViewModel(Device device):base(device) {

        }
        
        private ObservableCollection<ICommandItem> _contextCommands;
        public override ObservableCollection<ICommandItem> ContextCommands {
            get {
                if(_contextCommands == null) {
                    var mainViewerCommandItem = new CommandItem { Children = ViewersCommands, CommandName = FindResourceString("ViewerProgram") };
                    _contextCommands = new ObservableCollection<ICommandItem> {
                        new CommandItem{ CommandName=FindResourceString("FileDetailInfo") , Command = ShowFileDetailCommand }
                    };
                    _contextCommands.AddRange(base.ContextCommands);
                    
                }
                return _contextCommands;
            }
            set => _contextCommands = value;
        }
    }

    //分区-文件视图;
    public partial class DirectoriesBrowserViewModel : FolderBrowserViewModel {
        public DirectoriesBrowserViewModel(IFile file):base(file){
            
        }

        //恢复所有项的命令;
        private DelegateCommand _recCheckedCommand;
        public DelegateCommand RecCheckedCommand =>
            _recCheckedCommand ??
            (_recCheckedCommand = new DelegateCommand(() => {
                RecoverFiles(FileRows.Where(p => p.Checked).Select(p => p.File).ToArray());
            }, () => FileRows.FirstOrDefault(p => p.Checked) != null));

        private void RecoverFiles(IEnumerable<IFile> files) {
            if (files == null)
                throw new ArgumentNullException(nameof(files));

            long readSize = 0;
            long totalSize = 0;
            try {
                #region 统计总大小;
                foreach (var file in files) {
                    if (file.FileType == FileType.Directory) {
                        var direc = file as Directory;
                        if (!direc.IsBackFile() && !direc.IsBackUpFile()) {
                            totalSize += direc.GetTotalSize();
                        }
                    }
                    else if (file.FileType == FileType.RegularFile) {
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

            if (files.Count() == 1 && files.ElementAt(0).FileType == FileType.RegularFile) {
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
                            if (file.FileType == FileType.Directory) {
                                TraverseSaveDirectory(file as Directory, drPath, saveFileFunc, () => proDialog.CancellationPending);
                            }
                            else if (file.FileType == FileType.RegularFile) {
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
            dir.Children?.ForEach(p => {
                if (isCancel?.Invoke() == true) { return; }

                if (p.FileType == FileType.Directory) {
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
                else if (p.FileType == FileType.RegularFile) {
                    saveFileFunc(p as RegularFile, $"{drPath}/{dir.Name}", p.Name);
                }
            });
        }

        private DelegateCommand _openFileCommand;
        public DelegateCommand OpenFileCommand =>
            _openFileCommand ?? (_openFileCommand = new DelegateCommand(
                () => {
                    if (SelectedFileRow != null) {
                        OpenFile(SelectedFileRow);
                    }
                },
                () => SelectedFileRow?.File is RegularFile).
            ObservesProperty(() => SelectedFileRow));

        private ObservableCollection<ICommandItem> _contextCommands;
        public override ObservableCollection<ICommandItem> ContextCommands {
            get {
                var mainViewerCommandItem = new CommandItem { Children = ViewersCommands, CommandName = FindResourceString("ViewerProgram") };
                if(_contextCommands == null) {
                    _contextCommands = new ObservableCollection<ICommandItem>() {
                            new CommandItem {
                                CommandName = FindResourceString("Navigation"),
                                Children = new ObservableCollection<ICommandItem> {
                                    new CommandItem{ CommandName=FindResourceString("ListClusters"),Command=ListBlocksCommand}
                                }
                            },
                            new CommandItem {
                                CommandName = FindResourceString("OpenFile"),
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

                    var externCommandItems = ServiceLocator.Current.GetAllInstances<CommandItem<FileRow>>();
                    var externTupleCommandItems = ServiceLocator.Current.GetAllInstances<CommandItem<(DirectoriesBrowserViewModel, FileRow)>>();

                    void SetCommandItems<TData>(IEnumerable<CommandItem<TData>> items,Func<TData> valFunc) {
                        if (items != null) {
                            foreach (var cmi in items) {
                                cmi.GetData = valFunc;
                                _contextCommands.Add(cmi);
                            }
                        }
                    };
                    
                    SetCommandItems(externCommandItems,() => SelectedFileRow);
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
                        if (SelectedFileRow.File.FileType == FileType.Directory
                        && SelectedFileRow.File is Directory dir) {
                            if (dir.IsBackFile() || dir.IsBackUpFile()) {
                                CDFCMessageBox.Show(FindResourceString("RootOrBackNodeFileCannotBeExtracted"));
                            }
                            else {
                                RecoverFiles(new IFile[] { SelectedFileRow.File });
                            }
                        }
                        else if (SelectedFileRow.File.FileType == FileType.RegularFile) {
                            RecoverFiles(new IFile[] { SelectedFileRow.File });
                        }
                    }, () => SelectedFileRow != null && SelectedFileRow.File.FileType != FileType.BlockDeviceFile));
            }
        }
    }

    ////Adb分区-文件列表视图;
    //public class AdbDirectoriesBrowserViewModel:FolderBrowserViewModel {
    //    public AdbDirectoriesBrowserViewModel(IFile file):base(file) {

    //    }

    //    //上下文命令集合;
    //    private ObservableCollection<ICommandItem> _contextCommands;
    //    public override ObservableCollection<ICommandItem> ContextCommands {
    //        get {
    //            if(_contextCommands == null) {
    //                _contextCommands = new ObservableCollection<ICommandItem>();

    //            }
    //            return _contextCommands;
    //        }

    //        set {
    //            _contextCommands = value;
    //        }
    //    }
    //}
    
}
