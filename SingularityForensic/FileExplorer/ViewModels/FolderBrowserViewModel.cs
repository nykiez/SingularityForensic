﻿using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SysIO = System.IO;
using System.Threading;
using CDFCUIContracts.Events;
using System.Windows.Input;
using Prism.Commands;
using CDFCCultures.Helpers;
using SingularityForensic.Controls.FileExplorer.Models;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Controls.FileExplorer.Helpers;
using SingularityForensic.Controls.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.FileExplorer.MessageBoxes;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.App;
using System.Data;
using System.Linq;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.FileExplorer.Events;

namespace SingularityForensic.FileExplorer.ViewModels {
    /// <summary>
    /// 目录/资源浏览器模型;
    /// </summary>
    public partial class FolderBrowserViewModel : DataGridExViewModel {
        /// <summary>
        /// 目录/资源浏览器模型构造方法;
        /// </summary>
        /// <param name="part">模型所属主文件</param>
        public FolderBrowserViewModel(Partition part) {
            if(part == null) {
                throw new ArgumentNullException(nameof(part));
            }
            this.Part = part;
            
            _fileMetaDataProviders = ServiceProvider.Current?.
                GetAllInstances<IFileMetaDataProvider>().
                Where(p => p.MetaDataTypeFor == Contracts.FileExplorer.Constants.FileMetaDataType_File).
                OrderBy(p => p.Order);

            var dt = InitializeDT();
            FillRows(part.Children,dt);
            Files = dt;
        }
        
        public Partition Part { get; }                                        //浏览器所属主文件（分区，设备等);

        private DataTable _files;
        public DataTable Files {
            get => _files;
            set => SetProperty(ref _files, value);
        }
        
        private DataRow _selectedRow;
        public DataRow SelectedRow {
            get => _selectedRow;
            set {
                SetProperty(ref _selectedRow, value);
                if(_selectedRow == null) {
                    return;
                }

                if(_selectedRow[Constants.FileMetaDataName_File] is FileBase file) {
                    SelectedFile = file;
                    PubEventHelper.GetEvent<FocusedFileChangedEvent>().Publish((file, Part));
                }
                
            }
        }

        public FileBase SelectedFile { get; private set; }

        private DataTable InitializeDT() {
            var files = new DataTable();
            files.Columns.Add(new DataColumn(Constants.FileMetaDataName_File, typeof(FileBase)));
            
            if(_fileMetaDataProviders == null) {
                return null;
            }

            foreach (var mProvider in _fileMetaDataProviders) {
                files.Columns.Add(new DataColumn(mProvider.MetaDataName, mProvider.MetaDataType));
            }
            return files;
        }

        /// <summary>
        /// 填充Rows;
        /// </summary>
        /// <param name="files"></param>
        private void FillRows(IEnumerable<FileBase> files,DataTable dt) {
            if (files == null) {
                return;
            }
            
            dt.Rows.Clear();

            foreach (var file in files) {
                var newRow = dt.NewRow();
                foreach (var mProvider in _fileMetaDataProviders) {
                    newRow[mProvider.MetaDataName] = mProvider.GetDataObject(file);
                }
                newRow[Constants.FileMetaDataName_File] = file;

                dt.Rows.Add(newRow);
            }


        }

        private IEnumerable<IFileMetaDataProvider> _fileMetaDataProviders;
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
                    SelectedFileRowChanged?.Invoke(this,new TEventArgs<IFileRow>( _selectedFileRow ));
                }
            }
        }

        private IFileRow _focusRow;                                                  //当前聚焦行，用于滚动视图;
        public IFileRow FocusRow {
            get => _focusRow;
            set => SetProperty(ref _focusRow, value);
        }


        private object _filterSettings;
        public object FilterSettings {
            get => _filterSettings;
            set {
                SetProperty(ref _filterSettings, value);
                
            }
        }

        public event EventHandler<TEventArgs< ViewerProgramMessage >> WatchRequired;
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
                                WatchRequired?.Invoke(this,new TEventArgs<ViewerProgramMessage>(
                                    new ViewerProgramMessage(rModel.Program.ProgramPath, regFile.Name, stream)));
                            });
                        }
                        catch {

                        }
                    }
                    else if (rModel != null) {
                        var dialog = new VistaOpenFileDialog();
                        dialog.Filter = $"({LanguageService.FindResourceString("Executable")})|*.exe";
                        if (dialog.ShowDialog() == true) {
                            var regFile = (SelectedFileRow as IFileRow<FileBase>).File as RegularFile;
                            using (var stream = regFile.GetInputStream()) {
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
        

    }

    public partial class FolderBrowserViewModel : IGridViewDataContext {
        public void NotifyDoubliClickOnRow(object row) {
            if(!(row is DataRow dataRow)) {
                return;
            }

            try {
                var dt = InitializeDT();
                
                var file = dataRow[Constants.FileMetaDataName_File] as FileBase;
                if (!(file is IHaveFileCollection haveFileCollection)) {
                    return;
                }

                if(file is Directory direct) {
                    if (direct.IsBack) {
                        if(direct.Parent is IHaveFileCollection parentCollection
                            && (parentCollection as FileBase)?.Parent is IHaveFileCollection grandCollection) {
                            FillRows(grandCollection.Children,dt);
                        }
                    }
                    else if(!direct.IsLocalBackUp) {
                        FillRows(haveFileCollection.Children, dt);
                    }
                    Files?.Dispose();
                    Files = dt;
                }
            }
            catch(Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
        }
    }

    //目录/资源浏览器模型命令部分;
    public partial class FolderBrowserViewModel {
        public event EventHandler<TEventArgs<IFileRow>> RowEntered;                      //进入了某个文件行;

        //选择子文件时,进入该文件;
        public void EnterRow(IFileRow<FileBase> row) {
            RowEntered?.Invoke(this,new TEventArgs<IFileRow>( row ));
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

                        if ((SelectedFileRow as IFileRow<FileBase>).File != null) {
                            var blockGroupedFile = (SelectedFileRow as IFileRow<FileBase>).File as IBlockGroupedFile;
                            if(blockGroupedFile.BlockGroups == null) {
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
                if((SelectedFileRow as IFileRow<FileBase>).File != null) {
                    //FileDetailMessageBox.Show((SelectedFileRow as IFileRow<IFile>).File);
                }
            }));

        public void OpenFile(IFileRow row) {
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
        
        private DelegateCommand<MouseEventArgs> _mouseEnterCommand;
        public DelegateCommand<MouseEventArgs> MouseEnterCommand => _mouseEnterCommand ??
            (_mouseEnterCommand = new DelegateCommand<MouseEventArgs>(
                arg => {
                    
                }
            ));

    }

    //目录试图资源管理器模型过滤命令部分;
    public partial class FolderBrowserViewModel {
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
