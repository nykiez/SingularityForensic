using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

using System.Threading;
using CDFCUIContracts.Events;
using System.Windows.Input;
using Prism.Commands;
using CDFCCultures.Helpers;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.Controls;

using SingularityForensic.FileExplorer.Models;
using SingularityForensic.FileExplorer.MessageBoxes;
using SingularityForensic.FileExplorer.Helpers;
using SingularityForensic.Controls.GridView;
using SingularityForensic.Controls;

namespace SingularityForensic.FileExplorer.ViewModels {
    /// <summary>
    /// 目录/资源浏览器模型;
    /// </summary>
    public partial class FolderBrowserViewModel : DataGridExViewModel, IFolderBrowserViewModel {
        /// <summary>
        /// 目录/资源浏览器模型构造方法;
        /// </summary>
        /// <param name="part">模型所属主文件</param>
        public FolderBrowserViewModel(Partition part) {
            if (part == null) {
                throw new ArgumentNullException(nameof(part));
            }
            this.Part = part;

            InitializeFileRowDescriptors();
            InitializeColumns();
            FillWithCollection(part);
        }

        /// <summary>
        /// 初始化行元数据提供器;
        /// </summary>
        private void InitializeFileRowDescriptors() {
            if (FileRow.DescriptorsInitialized) {
                return;
            }

            var fileMetaDataProviders = ServiceProvider.Current?.GetAllInstances<IFileMetaDataProvider>().OrderBy(p => p.Order);
            if (fileMetaDataProviders == null) {
                LoggerService.WriteCallerLine($"{nameof(fileMetaDataProviders)} can't be null.");
                return;
            }

#if DEBUG
            //var arr = fileMetaDataProviders.ToArray();
#endif
            FileRow.InitializeDescriptors(fileMetaDataProviders);
        }

        /// <summary>
        /// 初始化列;比如在<see cref="InitializeFileRowDescriptors"/>后执行
        /// </summary>
        private void InitializeColumns() {
            foreach (var descripter in FileRow.PropertyDescriptors) {
                Files.PropertyDescriptorList.Add(descripter);
            }
        }

        public Partition Part { get; }                                        //浏览器所属主文件（分区，设备等);

        public CustomTypedListSource<FileRow> Files { get; set; } = new CustomTypedListSource<FileRow>();

        private FileRow _selectedRow;
        public FileRow SelectedRow {
            get => _selectedRow;
            set {
                SetProperty(ref _selectedRow, value);
                if (_selectedRow == null) {
                    return;
                }

                SelectedFile = _selectedRow.File;
                PubEventHelper.GetEvent<FocusedFileChangedEvent>().Publish((this,SelectedFile));
            }
        }

        public FileBase SelectedFile { get; private set; }

        /// <summary>
        /// 填充行;
        /// </summary>
        ///<param name="fileCollection">母文件</param>
        private void FillWithCollection(IHaveFileCollection fileCollection) {
            if (fileCollection == null) {
                return;
            }

            Files.Clear();

            FillRows(fileCollection.Children);

            this.CurrentFileCollection = fileCollection;
        }


        /// <summary>
        /// 填充行;
        /// </summary>
        /// <param name="files"></param>
        private void FillRows(IEnumerable<FileBase> files) {
            if (files == null) {
                return;
            }

            foreach (var file in files) {
                Files.Add(new FileRow(file));
            }
        }


        public ObservableCollection<NavNodeModel> NavNodes { get; set; } = new ObservableCollection<NavNodeModel>();

        /// <summary>
        /// 文件/资源行;(须在外部指定实例);
        /// </summary>
        public ObservableCollection<IFileRow> FileRows { get; set; }

        public event EventHandler<TEventArgs<IFileRow>> SelectedFileRowChanged;               //当当前选择文件变化时触发;

        private IFileRow _selectedFileRow;                                    //当前选定的文件行;
        public IFileRow SelectedFileRow {
            get {
                return _selectedFileRow;
            }
            set {
                SetProperty(ref _selectedFileRow, value);
                if (_selectedFileRow != null) {
                    SelectedFileRowChanged?.Invoke(this, new TEventArgs<IFileRow>(_selectedFileRow));
                }
            }
        }

        private IFileRow _focusRow;                                                  //当前聚焦行，用于滚动视图;
        public IFileRow FocusRow {
            get => _focusRow;
            set => SetProperty(ref _focusRow, value);
        }


        public event EventHandler<TEventArgs<ViewerProgramMessage>> WatchRequired;
        private ObservableCollection<CommandItem> _viewersCommands;
        public virtual ObservableCollection<CommandItem> ViewersCommands {
            get {
                if (_viewersCommands == null) {
                    _viewersCommands = new ObservableCollection<CommandItem>();
                    LoadViewers();
                }
                return _viewersCommands;
            }
            set {
                _viewersCommands = value;
            }
        }
        internal class ViewerProgramCommandItem : CommandItem {
            public ViewerProgramCommandItem(ViewerProgramModel programModel) {
                if (programModel == null)
                    throw new ArgumentNullException(nameof(programModel));

                this.VProgramModel = programModel;
            }

            public ICommand Command {
                get => VProgramModel.WatchCommand;
                set => throw new NotImplementedException();
            }

            public string CommandName {
                get => VProgramModel.Name;
                set => throw new NotImplementedException();
            }



            public ViewerProgramModel VProgramModel { get; }

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
                            var regFile = (SelectedFileRow as IFileRow<FileBase>).File as RegularFile;
                            var stream = regFile.GetInputStream();
                            ThreadPool.QueueUserWorkItem(callBack => {
                                WatchRequired?.Invoke(this, new TEventArgs<ViewerProgramMessage>(
                                    new ViewerProgramMessage(rModel.Program.ProgramPath, regFile.Name, stream)));
                            });
                        }
                        catch {

                        }
                    }
                    else if (rModel != null) {
                        var fileName = DialogService.Current?.OpenFile($"({LanguageService.FindResourceString("Executable")})|*.exe");
                        if (string.IsNullOrEmpty(fileName)) {
                            return;
                        }
                        var regFile = (SelectedFileRow as IFileRow<FileBase>).File as RegularFile;
                        using (var stream = regFile.GetInputStream()) {
                            ThreadPool.QueueUserWorkItem(callBack => {
                                WatchRequired?.Invoke(this, new TEventArgs<ViewerProgramMessage>(
                                    new ViewerProgramMessage(fileName, regFile.Name, stream)));
                            });

                            var proName = IOPathHelper.GetFileNameFromUrl(fileName);
                            if (proName != null) {
                                ViewerProgramHelper.AddProgram(fileName, proName);
                                LoadViewers();
                            }
                        }

                    }
                };
                model.CanSee = () => (SelectedFileRow as IFileRow<FileBase>)?.File is RegularFile;
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

        /// <summary>
        /// 当前展开的文件;
        /// </summary>
        /// <param name="_fileCollection"></param>
        private IHaveFileCollection _currentFileCollection;
        public IHaveFileCollection CurrentFileCollection {
            get => _currentFileCollection;
            set {
                _currentFileCollection = value;
            }
        }
    }

    /// <summary>
    /// 前台通知相关;
    /// </summary>
    public partial class FolderBrowserViewModel {
        /// <summary>
        /// 双击进入目录动作;
        /// </summary>
        /// <param name="row"></param>
        public override void NotifyDoubleClickOnRow(object row) {
            if (!(row is FileRow fileRow)) {
                return;
            }

            try {
                var file = fileRow.File;
                if (!(file is IHaveFileCollection haveFileCollection)) {
                    return;
                }
                if (file is Directory direct) {
                    if (direct.IsBack) {
                        if (direct.Parent is IHaveFileCollection parentCollection
                            && (parentCollection as FileBase)?.Parent is IHaveFileCollection grandCollection) {
                            FillWithCollection(grandCollection);

                        }
                    }
                    else if (!direct.IsLocalBackUp) {
                        FillWithCollection(haveFileCollection);
                    }

                }
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
        }

        /// <summary>
        /// 自动生成动作;
        /// </summary>
        /// <param name="e"></param>
        public override void NotifyAutoGeneratingColumns(GridViewAutoGeneratingColumnEventArgs e) {
            var descriptor = FileRow.PropertyDescriptors.FirstOrDefault(p => p.Name == e.ItemPropertyInfo.Name);
            if (!(descriptor is FileRow.FileRowPropertyDescriptor fileRowPropDescriptor)) {
                return;
            }
            
            e.CellTemplate = fileRowPropDescriptor.FileMetaDataProvider.CellTemplate;
            e.Converter = fileRowPropDescriptor.FileMetaDataProvider.Converter;
        }
    }

    /// <summary>
    /// 目录/资源浏览器模型命令部分;
    /// </summary>
    public partial class FolderBrowserViewModel {
        public event EventHandler<TEventArgs<IFileRow>> RowEntered;                      //进入了某个文件行;

        //选择子文件时,进入该文件;
        public void EnterRow(IFileRow<FileBase> row) {
            RowEntered?.Invoke(this, new TEventArgs<IFileRow>(row));
            if (row?.File is RegularFile regFile) {
                OpenFile(row);
            }
        }
        
        private ListBlockMessageBox listBlockMsg;


        private DelegateCommand listBlocksCommand;                              //列出簇命令;
        public DelegateCommand ListBlocksCommand {
            get {
                return listBlocksCommand ??
                    (listBlocksCommand = new DelegateCommand(() => {
                        if (listBlockMsg != null) {
                            listBlockMsg.Close();
                            listBlockMsg = null;
                        }

                        if ((SelectedFileRow as IFileRow<FileBase>).File != null) {
                            var blockGroupedFile = (SelectedFileRow as IFileRow<FileBase>).File as IBlockGroupedFile;
                            if (blockGroupedFile.BlockGroups == null) {
                                MsgBoxService.Show(LanguageService.FindResourceString("DeletedCannotBeListed"));
                            }
                            else {
                                listBlockMsg = new ListBlockMessageBox(blockGroupedFile.BlockGroups, (SelectedFileRow as IFileRow<FileBase>).File);
                                listBlockMsg.SelectedAddressChanged += (sender, e) => {
                                    //if (File is SearcherPartition) {
                                    //    if (listBlockMsg.File is RegularFile) {
                                    //        var regFile = listBlockMsg.File as RegularFile;

                                    //        FocusAddressChanged?.Invoke(this,new TEventArgs<long>((regFile.DeviceStartLBA - regFile.StartLBA) / 4096 + e) );
                                    //    }
                                    //}
                                    //else {
                                    //    FocusAddressChanged?.Invoke(this,new TEventArgs<long>( e ));
                                    //}

                                };
                                listBlockMsg.Show();
                            }
                        }
                    }, () => SelectedFileRow != null && (SelectedFileRow as IFileRow<FileBase>).File is IBlockGroupedFile));
            }
        }

        /// <summary>
        /// 选中目标行;
        /// </summary>
        /// <param name="rows"></param>
        public void CheckRows(IEnumerable<IFileRow> rows, bool isCheck = true) {
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
                        row.SetChecked(value.Value);
                    }
                }

                SetProperty(ref _allChecked, value);
            }
        }

        //显示详细信息相关;
        private DelegateCommand _showFileDetailCommand;
        public DelegateCommand ShowFileDetailCommand =>
            _showFileDetailCommand ?? (_showFileDetailCommand = new DelegateCommand(() => {
                if ((SelectedFileRow as IFileRow<FileBase>).File != null) {
                    //FileDetailMessageBox.Show((SelectedFileRow as IFileRow<IFile>).File);
                }
            }));

        public void OpenFile(IFileRow row) {
            try {

                //if (row.LocalPath != null) {
                //    if (SysIO.File.Exists(row.LocalPath)) {

                //        ExplorerHelper.OpenFile(row.LocalPath);
                //    }
                //}
            }
            catch {

            }
        }

        private DelegateCommand<MouseEventArgs> _mouseEnterCommand;
        public DelegateCommand<MouseEventArgs> MouseEnterCommand => _mouseEnterCommand ??
            (_mouseEnterCommand = new DelegateCommand<MouseEventArgs>(
                arg => {

                }
            ));

    }

    ////目录试图资源管理器模型过滤命令部分;
    public partial class FolderBrowserViewModel {
        //一键取消所有的过滤;
        private DelegateCommand _cancelFilteringCommand;
        public DelegateCommand CancelFilteringCommand =>
            _cancelFilteringCommand ?? (_cancelFilteringCommand = new DelegateCommand(() => {
                FilterSettings = Enumerable.Empty<FilterSetting>();
            }));
    }


    //递归展开视图部分;
    public partial class FolderBrowserViewModel {
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

}
