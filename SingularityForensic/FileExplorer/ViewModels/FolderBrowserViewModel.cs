using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using System.Threading;

namespace SingularityForensic.FileExplorer.ViewModels {
    /// <summary>
    /// 目录/资源浏览器模型;
    /// </summary>
    public partial class FolderBrowserViewModel : DataGridExViewModel, IFolderBrowserViewModel {
        /// <summary>
        /// 目录/资源浏览器模型构造方法;
        /// </summary>
        /// <param name="haveFileCollection">模型所属主文件</param>
        public FolderBrowserViewModel(IHaveFileCollection haveFileCollection) {
            this.OwnedFileCollection = haveFileCollection ?? throw new ArgumentNullException(nameof(haveFileCollection));

            Initialize();
        }

        public void Initialize() {
            InitializeColumns();
            this.FillWithCollection(OwnedFileCollection);
        }


        /// <summary>
        /// 初始化列;比如在<see cref="InitializeFileRowDescriptors"/>后执行
        /// </summary>
        private void InitializeColumns() {
            _customColumns = new List<CustomColumn>();
#if DEBUG
            int colIndex = 0;
#endif
            foreach (var descriptor in FileRowFactory.Current.PropertyDescriptors) {

                if (descriptor is FileRow.FileRowPropertyDescriptor fileRowPropDescriptor) {
                    _customColumns.Add(new CustomDataColumn {
                        Binding = new System.Windows.Data.Binding {
                            Path = new System.Windows.PropertyPath(descriptor.Name),
                            Converter = fileRowPropDescriptor.FileMetaDataProvider.Converter,
                        },
                        Header = descriptor.DisplayName,
                        CellTemplate = fileRowPropDescriptor.FileMetaDataProvider.CellTemplate,
                        ShowDistinctFilters = true,
                        ColumnDataType = descriptor.PropertyType
                    });
                }
#if DEBUG
                colIndex++;
                if(colIndex >= 1) {
                    //break;
                }
#endif
            }
        }

        private List<CustomColumn> _customColumns;
        public override IEnumerable<CustomColumn> CustomColumns => _customColumns;

        /// <summary>
        /// 所属主文件（分区，设备等);
        /// </summary>
        public IHaveFileCollection OwnedFileCollection { get; }                                        



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
                PubEventHelper.PublishEventToHandlers((this as Contracts.FileExplorer.ViewModels.IFolderBrowserViewModel, SelectedFile: SelectedFile), GenericServiceStaticInstances<IFocusedFileRowChangedEventHandler>.Currents);
                PubEventHelper.GetEvent<FocusedFileRowChangedEvent>().Publish((this,SelectedFile));

#if DEBUG
                //var fs = SelectedFiles.ToArray();
#endif
            }
        }

        private long fillPriLevel = 0;
        private AutoResetEvent fillEvt = new AutoResetEvent(true);
        /// <summary>
        /// 填充行;
        /// </summary>
        /// <param name="files"></param>
        public void FillRows(IEnumerable<IFile> files) {
            if (files == null) {
                return;
            }

            var thisPriLevel = ++fillPriLevel;
            
            BusyWord = LanguageService.FindResourceString(Constants.BusyWord_FileBeingShown);
            IsBusy = true;
            MouseService.AppCusor = Cursor.Loading;

            ThreadInvoker.BackInvoke(() => {
                fillEvt.WaitOne();
                ThreadInvoker.UIInvoke(() => {
                    FileRows.Clear();
                });
                var bufferLength = 10;
                var bufferRows = new IFileRow[bufferLength];

                var index = 0;
                foreach (var file in files) {
                    var fileRow = FileRowFactory.Current.CreateFileRow(file);
                    
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
                    if(thisPriLevel != fillPriLevel) {
                        break;
                    }
                }
                ThreadInvoker.UIInvoke(() => {
                    for (int i = 0; i < index; i++) {
                        FileRows.Add(bufferRows[i]);
                    }

                    MouseService.AppCusor = Cursor.Normal;
                });
#if DEBUG
                //Thread.Sleep(3000);
#endif
                IsBusy = false;
                fillEvt.Set();
            });
            this.FileCollectionChanged?.Invoke(this, EventArgs.Empty);
            //RaisePropertyChanged(nameof(FilterSettings));

#if DEBUG
            if(FileRows.Count != 0) {
                ((FileRow)FileRows[0]).CheckSubscribed();
            }
#endif
        }

        public event EventHandler FileCollectionChanged;
        
        private ObservableCollection<INavNodeModel> NavNodeModels { get; set; } = new ObservableCollection<INavNodeModel>();


        private bool _isBusy;
        public bool IsBusy {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }


        private string _busyWord;
        public string BusyWord {
            get => _busyWord;
            set => SetProperty(ref _busyWord, value);
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

        /// <summary>
        /// 当前路径;
        /// </summary>
        private string _currentPath;
        public string CurrentPath {
            get => _currentPath;
            set {
                try {
                    if (!(OwnedFileCollection.GetFileByUrl(value) is IHaveFileCollection collection)) {
                        throw new ArgumentException($"{nameof(_currentPath)} doesn't aim to a valid {nameof(IHaveFileCollection)}.");
                    }
                    this.FillWithCollection(collection);
                }
                catch(Exception ex) {
                    LoggerService.WriteException(ex);
                    throw;
                }

                var pathChanged = _currentPath != value;
                _currentPath = value;
                if(pathChanged) {
                    CurrentPathChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        public event EventHandler CurrentPathChanged;

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
                if (haveFileCollection is IDirectory direct) {
                    if (direct.IsBack) {
                        if (direct.Parent is IHaveFileCollection parentCollection
                            && (parentCollection as IFile)?.Parent is IHaveFileCollection grandCollection
                            && !(grandCollection is IDevice)) {
                            CurrentPath = OwnedFileCollection.GetUrlByFile(grandCollection);
                        }
                    }
                    else if (!direct.IsLocalBackUp) {
                        CurrentPath = OwnedFileCollection.GetUrlByFile(haveFileCollection);
                    }
                }
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
        }

        
    }
    
    ////目录视图资源管理器模型过滤命令部分;
    public partial class FolderBrowserViewModel {
        //一键取消所有的过滤;
        private IDelegateCommand _cancelFilteringCommand;
        public IDelegateCommand CancelFilteringCommand =>
            _cancelFilteringCommand ?? (_cancelFilteringCommand = CommandFactory.CreateDelegateCommand(() => {
                FilterSettings = Enumerable.Empty<FilterSetting>();
            }));
    }

    
}
