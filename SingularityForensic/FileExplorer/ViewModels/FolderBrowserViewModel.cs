using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.Controls;

using SingularityForensic.FileExplorer.Models;
using SingularityForensic.Controls.GridView;
using SingularityForensic.Controls;
using SingularityForensic.Contracts.FileExplorer.Models;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.Contracts.App;

namespace SingularityForensic.FileExplorer.ViewModels {
    /// <summary>
    /// 目录/资源浏览器模型;
    /// </summary>
    public partial class FolderBrowserViewModel : DataGridExViewModel, IFolderBrowserViewModel {
        /// <summary>
        /// 目录/资源浏览器模型构造方法;
        /// </summary>
        /// <param name="part">模型所属主文件</param>
        public FolderBrowserViewModel(IHaveFileCollection part) {
            this.HaveFileCollection = part ?? throw new ArgumentNullException(nameof(part));
        }

        public void Initialize() {
            InitializeColumns();
            this.FillWithCollection(HaveFileCollection);
        }

       
        /// <summary>
        /// 初始化列;比如在<see cref="InitializeFileRowDescriptors"/>后执行
        /// </summary>
        private void InitializeColumns() {
            foreach (var descriptor in FileRowFactory.Current.PropertyDescriptors) {
                FileRows.PropertyDescriptorList.Add(descriptor);
            }
        }

        public IHaveFileCollection HaveFileCollection { get; }                                        //浏览器所属主文件（分区，设备等);

        public CustomTypedListSource<IFileRow> FileRows { get; set; } = new CustomTypedListSource<IFileRow>();
        public IEnumerable<IFileRow> Files  => FileRows;
        
        private IFileRow _selectedFile;
        public IFileRow SelectedFile {
            get => _selectedFile;
            set {
                SetProperty(ref _selectedFile, value);
                if (_selectedFile == null) {
                    return;
                }
                SelectedFileChanged?.Invoke(this, EventArgs.Empty);
                PubEventHelper.PublishEventToHandlers((this as IFolderBrowserViewModel, SelectedFile), GenericServiceStaticInstances<IFocusedFileRowChangedEventHandler>.Currents);
                PubEventHelper.GetEvent<FocusedFileRowChangedEvent>().Publish((this,SelectedFile));

#if DEBUG
                //var fs = SelectedFiles.ToArray();
#endif
            }
        }
        
        /// <summary>
        /// 填充行;
        /// </summary>
        /// <param name="files"></param>
        public void FillRows(IEnumerable<IFile> files) {
            if (files == null) {
                return;
            }
            
            ThreadInvoker.UIInvoke(() => {
                FileRows.Clear();
                MouseService.AppCusor = Cursor.Loading;
            });
            
            var loadingDialog = DialogService.Current?.CreateLoadingDialog();
            if(loadingDialog != null) {
                loadingDialog.IsProgressVisible = false;
            }

            
            loadingDialog.Word = LanguageService.FindResourceString(Constants.MsgText_FileBeingShown);
            loadingDialog.DoWork += (sender, e) => {
                var bufferLength = 10;
                var bufferRows = new IFileRow[bufferLength];

                var index = 0;
                foreach (var file in files) {
                    var fileRow = FileRowFactory.Current.CreateFileRow(file);
                    
                    if (loadingDialog.CancellationPending) {
                        return;
                    }
                    bufferRows[index] = fileRow;
                    index++;
                    if (index == bufferLength) {
                        ThreadInvoker.UIInvoke(() => {
                            foreach (var row in bufferRows) {
                                FileRows.Add(row);
                            }
                        });
                        System.Threading.Thread.Sleep(1);
                        index = 0;
                    }
                }
                ThreadInvoker.UIInvoke(() => {
                    for (int i = 0; i < index; i++) {
                        FileRows.Add(bufferRows[i]);
                    }

                    MouseService.AppCusor = Cursor.Normal;
                });
                
            };

            loadingDialog.ShowDialog();
            
            this.FilesChanged?.Invoke(this, EventArgs.Empty);
            //RaisePropertyChanged(nameof(FilterSettings));

#if DEBUG
            if(FileRows.Count != 0) {
                ((FileRow)FileRows[0]).CheckSubscribed();
            }
#endif
        }

        public event EventHandler FilesChanged;
        
        private ObservableCollection<INavNodeModel> NavNodeModels { get; set; } = new ObservableCollection<INavNodeModel>();
        
        public ICollection<INavNodeModel> NavNodes {
            get => NavNodeModels;
            set {
                NavNodeModels = value as ObservableCollection<INavNodeModel>;
            }
        }

        public IEnumerable<IFileRow> SelectedFiles {
            get {
                if(GetSelectedRows == null) {
                    yield break;
                }
                
                foreach (var row in GetSelectedRows.Invoke()) {
                    if(row is IFileRow fileRow) {
                        yield return fileRow;
                    }
                }
            }
        }
        
        public event EventHandler SelectedFileChanged;
        

#if DEBUG
        ~FolderBrowserViewModel() {

        }
#endif
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
            if (!(row is IFileRow fileRow)) {
                return;
            }

            try {
                var file = fileRow.File;
                if (!(file is IHaveFileCollection haveFileCollection)) {
                    return;
                }
                if (file is IDirectory direct) {
                    if (direct.IsBack) {
                        if (direct.Parent is IHaveFileCollection parentCollection
                            && (parentCollection as IFile)?.Parent is IHaveFileCollection grandCollection) {
                            this.FillWithCollection(grandCollection);
                        }
                    }
                    else if (!direct.IsLocalBackUp) {
                        this.FillWithCollection(haveFileCollection);
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
            var descriptor = FileRowFactory.Current.PropertyDescriptors.FirstOrDefault(p => p.Name == e.ItemPropertyInfo.Name);
            if (!(descriptor is FileRow.FileRowPropertyDescriptor fileRowPropDescriptor)) {
                return;
            }
            
            e.CellTemplate = fileRowPropDescriptor.FileMetaDataProvider.CellTemplate;
            e.Cancel = fileRowPropDescriptor.FileMetaDataProvider.IsHidden;
            e.Converter = fileRowPropDescriptor.FileMetaDataProvider.Converter;
            e.ShowDistinctFilters = fileRowPropDescriptor.FileMetaDataProvider.ShowDistinctFilters;
        }
    }
    
    ////目录视图资源管理器模型过滤命令部分;
    public partial class FolderBrowserViewModel {
        //一键取消所有的过滤;
        private DelegateCommand _cancelFilteringCommand;
        public DelegateCommand CancelFilteringCommand =>
            _cancelFilteringCommand ?? (_cancelFilteringCommand = new DelegateCommand(() => {
                FilterSettings = Enumerable.Empty<FilterSetting>();
            }));
    }

    
}
