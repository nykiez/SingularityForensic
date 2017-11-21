using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using CDFCUIContracts.Abstracts;
using System;
using System.Collections.ObjectModel;
using EventLogger;
using System.Threading;
using System.Windows;
using System.Linq;
using System.Collections.Generic;
using CDFCControls.Controls;
using CDFCUIContracts.Events;
using static CDFCUIContracts.Helpers.ApplicationHelper;
using CDFCMessageBoxes.MessageBoxes;
using static CDFCCultures.Managers.ManagerLocator;
using Prism.Commands;
using SingularityForensic.Helpers;
using Singularity.UI.FileSystem.Models;
using SingularityForensic.Modules.Shell.Models;
using Singularity.UI.FileSystem.Global.Events;
using Prism.Mvvm;
using Singularity.UI.Controls.Models.Filtering;
using Singularity.UI.Controls.MessageBoxes.Filtering;
using Singularity.UI.Controls.Filtering;
using CDFC.Parse.Signature.DeviceObjects;
using CDFC.Parse.Local.DeviceObjects;

namespace Singularity.UI.FileSystem.ViewModels {
    //对象浏览器主模型;
    public abstract partial class FileBrowserViewModel : BindableBase,IHaveTabModels,IDisposable {
        public event EventHandler<TEventArgs< bool >> IsLoadingRequired;
        /// <summary>
        /// 对象浏览器主模型构造方法;
        /// </summary>
        /// <param name="file">所描述的文件</param>
        public FileBrowserViewModel(IFile file) {
            this.File = file;
            this.CurFile = file;

            if(file != null) {
                if (file.FileType == FileType.BlockDeviceFile) {                //若为块设备文件，则最高等级文件即为本身;
                    OwnerFile = file;
                }
                else {                                                          //若为目录,则最高等级文件为所属分区;
                    var part = file.GetParent<Partition>();
                    if(part != null) {
                        OwnerFile = part;
                    }
                }
            }
            SelectedTabModel = MainHexViewModel;
            ExistingBrowsers.Add(this);
        }
        
        public IFile File { get; private set; }                             //该模型当前所属文件;
        public IFile OwnerFile { get;  }                        //该模型最高等级文件;
        
        //当前呈现的文件;
        //当前展开的文件;
        private IFile _curFile;
        public IFile CurFile {
            get {
                return _curFile;
            }
            protected set {
                //若目标文件不等于当前文件,则跳转;
                if (true) {
                    var itrFile = value as IIterableFile;
                    if (itrFile != null) {
                        _curFile = value;
                        FolderBrowserViewModel?.NavNodes?.Clear();
                        allRows.Clear();

                        if (itrFile.FileType != FileType.BlockDeviceFile) {
                            allRows.AddRange(itrFile.Children.Select(p => new FileRow(p)));
                        }
                        //若为设备;
                        else if(itrFile is Device) {
                            var device = itrFile as Device;
                            var partIndex = 0;
                            allRows.AddRange(itrFile.Children.Select(p => {
                                var row = new FileRow(p);
                                row.PartitionIndex = partIndex++;
                                return row;
                            }));
                        }
                        //若为分区;
                        else {
                            allRows.AddRange(itrFile.Children.Where(p => {
                                if (p.FileType == FileType.Directory) {
                                    if ((p as CDFC.Parse.Abstracts.Directory).IsBackFile()
                                    || (p as CDFC.Parse.Abstracts.Directory).IsBackUpFile()) {
                                        return false;
                                    }
                                }
                                return true;
                            }).Select(p => new FileRow(p)));
                        }
                        ApplyAllRows();

                        var ownNavNode = new NavNodeModel(value);
                        ownNavNode.EscapeRequired += (sender, e) => {
                            CurFile = e;
                        };
                        FolderBrowserViewModel?.NavNodes?.Insert(0, ownNavNode);
                        var pt = value.Parent;
                        while (pt != null && !(pt is Device)) {
                            var navNode = new NavNodeModel(pt);
                            navNode.EscapeRequired += (sender, e) => {
                                CurFile = e;
                            };
                            FolderBrowserViewModel?.NavNodes.Insert(0, navNode);
                            pt = pt.Parent;
                        }
                    }

                    FolderBrowserViewModel.IsExpanded = false;
                }
            }
        }

        //未过滤的所有行;
        private List<FileRow> allRows = new List<FileRow>();                

        //应用过滤所有行;
        private void ApplyAllRows() {                                       
            var fileRows = FilterRows(allRows);
            Application.Current.Dispatcher.Invoke(() => {
                FileRows.Clear();
            });
            var addedCount = 0;

            foreach (var row in fileRows) {
                Application.Current.Dispatcher.Invoke(() => {
                    FileRows.Add(row);
                });   
                row.CheckChanged += (sender, e) => {
                    CheckAllChecked();
                    (FolderBrowserViewModel as DirectoriesBrowserViewModel)?.RecCheckedCommand.RaiseCanExecuteChanged();
                };
                addedCount++;
                if (addedCount % 10000 == 0) {
                    Thread.Sleep(10);
                }
            }
        }

        //是否已经被展开;
        public bool IsExpanded {
            get {
                return FolderBrowserViewModel.IsExpanded;
            }
            set {
                FolderBrowserViewModel.IsExpanded = value;
            }
        }

        public void EnterTargetFile(IFile targetFile) {
            if(targetFile is IIterableFile) {
                if (targetFile.FileType == FileType.Directory) {    //若需展开文件为目录;
                    var itrDir = targetFile as IIterableFile;
                    if (itrDir.IsBackFile()) {
                        if (CurFile.FileType == FileType.Directory) {
                            CurFile = CurFile.Parent;
                        }
                        else {
                            return;
                        }
                    }
                    else {
                        CurFile = targetFile;
                    }

                }
                else if (targetFile is Partition) {
                    CurFile = targetFile;
                }
                FolderBrowserViewModel.IsExpanded = false;
            }
            else {
                return;
            }
            //else {
            //    FolderBrowserViewModel.SelectedFileRow = FolderBrowserViewModel.FileRows.FirstOrDefault();
            //}
            
        }

        //选择子文件时,进入该文件;
        public void EnterRow(FileRow row) {
            var targetFile = row.File;
            //验证是否属于该文件列表;
            if (FileRows.FirstOrDefault(p => p.File == targetFile) != null) {//验证是否包含文件;
                if (!(OwnerFile is Device)) {
                    EnterTargetFile(targetFile);
                }
            }
            if (row != null && row.File != null) {
                if (row.File is Partition) {
                    AddPartTabRequired?.Invoke(this,new TEventArgs<Partition>( row.File as Partition ));
                }
            }
            AllChecked = false;
        }

        public event EventHandler<TEventArgs< Partition >> AddPartTabRequired;            //通知需增加分区Tab;

        public event EventHandler<TEventArgs< ViewerProgramMessage>> WatchRequired;      //查看程序请求事件;
        private FolderBrowserViewModel folderBrowserViewModel;              //文件资源管理器模型;
        public virtual FolderBrowserViewModel FolderBrowserViewModel {
            get {
                if(folderBrowserViewModel == null) {
                    if(File is Device) {
                        folderBrowserViewModel = new PartitionsBrowserViewModel(File as Device);
                    }
                    else if(File is Partition) {
                        folderBrowserViewModel = new DirectoriesBrowserViewModel(File);
                    }
                    else if(File is CDFC.Parse.Abstracts.Directory){
                        folderBrowserViewModel = new DirectoriesBrowserViewModel(File);
                    }

                    folderBrowserViewModel.FilterFileNameNeeded = FilterFileNameNeeded;

                    //为文件行变更时;
                    folderBrowserViewModel.FileRows = FileRows;
                    WeakEventManager <FolderBrowserViewModel,TEventArgs<FileRow>>.
                        AddHandler(folderBrowserViewModel,nameof(folderBrowserViewModel.SelectedFileRowChanged),
                    (sender, e) => {
                        EscapeToFile(e.Target);
                    });

                    WeakEventManager<FolderBrowserViewModel, TEventArgs<long>>.
                        AddHandler(folderBrowserViewModel, nameof(folderBrowserViewModel.FocusAddressChanged),
                        (sender, e) => {
                            Partition part = null;
                            if (OwnerFile.FileType == FileType.BlockDeviceFile) {
                                part = OwnerFile as Partition;
                            }
                            if (part != null) {
                                if (0 <= e.Target && e.Target < part.Size) {
                                    MainHexViewModel.Position = e.Target * 4096;
                                }
                            }
                        });

                    WeakEventManager<FolderBrowserViewModel,TEventArgs<FileRow>>
                        .AddHandler(folderBrowserViewModel,nameof(folderBrowserViewModel.RowEntered),
                        (sender, e) => {
                            EnterRow(e.Target);
                        });
                    
                    WeakEventManager<FolderBrowserViewModel, TEventArgs<ViewerProgramMessage>>
                       .AddHandler(folderBrowserViewModel, nameof(folderBrowserViewModel.WatchRequired),
                       (sender, e) => {
                           WatchRequired?.Invoke(this, e);
                       });

                    WeakEventManager<FolderBrowserViewModel, EventArgs>
                      .AddHandler(folderBrowserViewModel, nameof(folderBrowserViewModel.FilterFileNameRequired),
                      (sender, e) => {
                          var res = FilterStringMessageBox.Show(ref filterFileNameModel);
                          if(res == null) {
                              return;
                          }
                          

                          ExistingBrowsers.ForEach(p => p.FilterFileNameNeeded = filterFileNameModel?.IsEnabled??false);
                          RefilterRows(nameof(FolderBrowserViewModel.FilterFileNameRequired));
                      });

                    WeakEventManager<FolderBrowserViewModel, EventArgs>
                      .AddHandler(folderBrowserViewModel, nameof(folderBrowserViewModel.FilterFileSizeRequired),
                      (sender, e) => {
                          var res = FilterSizeMessageBox.Show(ref filterFileSizeModel);

                          if (res == null) {
                              return;
                          }
                          if (!FilterFileSizeNeeded && !res.Value) {
                              return;
                          }

                          ExistingBrowsers.ForEach(p => p.FilterFileSizeNeeded = res.Value);
                          RefilterRows(nameof(FolderBrowserViewModel.FilterFileSizeRequired));
                      });
                    
                      WeakEventManager<FolderBrowserViewModel, EventArgs>
                      .AddHandler(folderBrowserViewModel, nameof(folderBrowserViewModel.FilterFilePathRequired),
                      (sender, e) => {
                          var res = FilterStringMessageBox.Show(ref filterFilePathModel);
                          if(res == null) {
                              return;
                          }
                          ExistingBrowsers.ForEach(p => p.FilterFilePathNeeded = filterFilePathModel.IsEnabled);
                          RefilterRows(nameof(FolderBrowserViewModel.FilterFilePathRequired));
                      });

                    WeakEventManager<FolderBrowserViewModel, EventArgs>
                     .AddHandler(folderBrowserViewModel, nameof(folderBrowserViewModel.FilterMTimeRequired),
                     (sender, e) => {
                         var res = FilterDTMessageBox.Show(ref filterMTimeModel);
                         if (res == null) {
                             return;
                         }
                         if (!FilterMTimeNeeded && !res.Value) {
                             return;
                         }
                         ExistingBrowsers.ForEach(p => p.FilterMTimeNeeded = res.Value);
                         RefilterRows(nameof(FolderBrowserViewModel.FilterMTimeRequired));
                     });
                    
                      WeakEventManager<FolderBrowserViewModel, EventArgs>
                      .AddHandler(folderBrowserViewModel, nameof(folderBrowserViewModel.FilterATimeRequired),
                      (sender, e) => {
                          var res = FilterDTMessageBox.Show(ref filterATimeModel);
                          if (res == null) {
                              return;
                          }
                          if (!FilterATimeNeeded && !res.Value) {
                              return;
                          }
                          ExistingBrowsers.ForEach(p => p.FilterATimeNeeded = res.Value);
                          RefilterRows(nameof(FolderBrowserViewModel.FilterATimeRequired));
                      });
                    
                      WeakEventManager<FolderBrowserViewModel, EventArgs>
                      .AddHandler(folderBrowserViewModel, nameof(folderBrowserViewModel.FilterCTimeRequired),
                      (sender, e) => {
                          var res = FilterDTMessageBox.Show(ref filterCTimeModel);
                          if (res == null) {
                              return;
                          }
                          if (!FilterCTimeNeeded && !res.Value) {
                              return;
                          }
                          ExistingBrowsers.ForEach(p => p.FilterCTimeNeeded = res.Value);
                          RefilterRows(nameof(FolderBrowserViewModel.FilterCTimeRequired));
                      });

                        WeakEventManager<FolderBrowserViewModel, EventArgs>
                      .AddHandler(folderBrowserViewModel, nameof(folderBrowserViewModel.RefilterRequired),
                      (sender, e) => {
                          if (AnyFiltering) {
                              AnyFiltering = false;
                              FilterFileNameNeeded = false;
                              FilterFilePathNeeded = false;
                              FilterFileSizeNeeded = false;
                              FilterMTimeNeeded = false;
                              FilterATimeNeeded = false;
                              FilterCTimeNeeded = false;
                              RefilterRows("all");
                          }
                      });
                }
                return folderBrowserViewModel;
            }
        }

        //重新过滤委托;
        private static void RefilterRows(string tp) {
            ExistingBrowsers.ForEach(p => {
                p.IsLoadingRequired?.Invoke(p, new TEventArgs<bool>(true));
                ThreadPool.QueueUserWorkItem(callBack => {
                    try {
                        //若已经展开，则递归添加并过滤;
                        if (p.IsExpanded) {
                            //递归添加并过滤;
                            p.ApplyAllRows();
                        }
                        else {
                            Application.Current.Dispatcher.Invoke(() => {
                                p.CurFile = p.CurFile;
                            });
                        }
                    }
                    catch (Exception ex) {
                        Logger.Write($"{nameof(FileBrowserViewModel)}->{nameof(FolderBrowserViewModel)},Event type: {tp}:{ex.Message}");
                    }
                    finally {
                        p.IsLoadingRequired?.Invoke(p, new TEventArgs<bool>(false));
                        p.AnyFiltering = p.FilterFileNameNeeded ||
                        p.FilterFilePathNeeded ||
                        p.FilterFileSizeNeeded ||
                        p.FilterMTimeNeeded ||
                        p.FilterATimeNeeded ||
                        p.FilterCTimeNeeded;
                    }

                });
            });
            
        }

        /// <summary>
        /// 选定文件发生变化时的处理;(Hex跳转)
        /// </summary>
        /// <param name="file">需跳转到的文件</param>
        protected virtual void EscapeToFile(FileRow row) {
            if(row?.File != null) {
                var file = row.File;
                if(file.FileType == FileType.BlockDeviceFile) {             //若选定单元为块设备文件，则可能为分区;
                    if(file is Partition part) {
                        MainHexViewModel.Position = part.StartLBA;
                    }
                }
                else if(file.FileType == FileType.Directory) {
                    if (file is CDFC.Parse.Abstracts.Directory directory) {
                        MainHexViewModel.Position = directory.StartLBA;
                    }
                }
                else if(file.FileType == FileType.RegularFile) {
                    if (file is RegularFile regFile) {
                        if (File is SearcherPartition) {
                            MainHexViewModel.Position = regFile.DeviceStartLBA;
                        }
                        else {
                            MainHexViewModel.Position = regFile.StartLBA;
                        }
                    }
                }
            }
        }
        
        //进入到某个文件(夹)的处理;
        public void EnterFile(IFile file) {
            EnterTargetFile(file);
            //if(file != OwnerFile) {
            //    EscapeToFile(file);
            //}
        }

        public string Header {                                              //头名;
            get {
                return OwnerFile?.Name ?? "Null";
            }
        }

        private ITabModel selectedTabModel;                                 //当前选定的Tab模型;
        public ITabModel SelectedTabModel {
            get {
                return selectedTabModel;
            }
            set {
                SetProperty(ref selectedTabModel, value);
                //发布已选定的事件;
                PubEventHelper.GetEvent<InnerTabSelectedChangedEvent>().Publish(selectedTabModel);
            }
        }
        
        private FileHexTabViewModel mainHexViewModel;                     //主十六进制查看视图模型;
        public virtual FileHexTabViewModel MainHexViewModel {
            get {
                if(OwnerFile != null && mainHexViewModel == null) {
                    if(OwnerFile is Partition) {
                        var part = OwnerFile as Partition;
                        var partHex = new PartitionHexTabViewModel(part);
                        WeakEventManager<PartitionHexTabViewModel, TEventArgs<long>>.AddHandler(
                            partHex,nameof(PartitionHexTabViewModel.FindFsPositionRequired),
                            (sender, e) => {
                                IIterableFile itera = null;
                                if(this.OwnerFile != null && (itera = OwnerFile as IIterableFile) != null) {
                                    var secSize = (OwnerFile.Parent as Device)?.SecSize ?? 512;
                                    secSize = secSize == 0 ? 512 : secSize;

                                    IsLoadingRequired?.Invoke(this,new TEventArgs<bool>(true));
                                    ThreadPool.QueueUserWorkItem(cb => {
                                        try {
                                            IFile innerFile = null;
                                            
                                            if((innerFile = itera.GetInnerFileByPosition(e.Target / secSize * secSize)) != null) {
                                                AppInvoke(() => {
                                                    CurFile = innerFile.Parent;
                                                    FolderBrowserViewModel.SelectedFileRow = FileRows.FirstOrDefault(p => p.File == innerFile);
                                                });
                                            }
                                            else {
                                                AppInvoke(() => {
                                                    RemainingMessageBox.Tell(FindResourceString("UnableToLocateTheFile"));
                                                });
                                            }
                                        }
                                        catch {
                                            AppInvoke(() => {
                                                RemainingMessageBox.Tell(FindResourceString("FailedToLocateTheFile"));
                                            });
                                        }
                                        finally {
                                            IsLoadingRequired?.Invoke(this, new TEventArgs<bool>(false));
                                        }
                                    });
                                }
                                else {
                                    Logger.WriteLine($"{nameof(FileBrowserViewModel)}->{nameof(MainHexViewModel)}->{nameof(PartitionHexTabViewModel.FindFsPositionRequired)}: OwnerFile Can't be null!");
                                }
                            });
                        
                        mainHexViewModel = partHex;
                    }
                    else if(OwnerFile is Device) {
                        var device = OwnerFile as Device;
                        mainHexViewModel = new DeviceHexTabViewModel(device);
                    }
                }
                else {
                    Logger.WriteLine($"{nameof(FileBrowserViewModel)}->{nameof(MainHexViewModel)}:{nameof(OwnerFile)} Can't be null!");
                }
                return mainHexViewModel;
            }
        }

        private ObservableCollection<ITabModel> _tabViewModels;
        public virtual ObservableCollection<ITabModel> TabViewModels {
            get {
                if(_tabViewModels == null) {
                    _tabViewModels = new ObservableCollection<ITabModel>();
                    _tabViewModels.Add(MainHexViewModel);
                }
                return _tabViewModels;
            }
            set {
                _tabViewModels = value;
            }
        } 

        //在当前文件中递归检索;
        public void SearchNameInCurFile(string keyValue) {
            if (CurFile is IIterableFile) {
                ExpandFile(CurFile as IIterableFile,keyValue);
            }
        }
        
        //递归展开某个文件;
        public void ExpandFile(IIterableFile file, string keyValue = null) {
            List<IFile> fileList = new List<IFile>();
            
            //递归获取所有文件;
            TraverGetNormalFile(file, fileList);
            this.allRows.Clear();
            this.allRows.AddRange(fileList.Select(p => new FileRow(p)));

            //匹配搜索;
            HighlightContent.ToHighlight = keyValue??string.Empty;
            if(HighlightContent.Mode == HighlightContentMode.AnyKey) {
                string[] keys = HighlightContent.ToHighlight.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (keys.Length > 0) {
                    allRows.RemoveAll(r => 
                        keys.Any(key => (r.FileName + "\0" + r.FileType).IndexOf(key, StringComparison.OrdinalIgnoreCase) == -1)
                    );
                }
            }
            else {
                if (!string.IsNullOrEmpty(HighlightContent.ToHighlight)) {
                    allRows.RemoveAll(r => (r.FileName + "\0" + r.FileType).IndexOf(HighlightContent.ToHighlight, StringComparison.OrdinalIgnoreCase) == -1);
                }
            }
            
            ApplyAllRows();
            IsExpanded = true;
        }

        //填充文件;(适用搜索);
        public void FillFiles(List<IFile> files) {
            if (files == null)
                throw new ArgumentNullException(nameof(files));

            allRows.Clear();
            var newRows = files.Select(p => new FileRow(p));
            allRows.AddRange(newRows);

            IsExpanded = true;
            ApplyAllRows();
        }
        
        //递归得到所有子文件;
        public void TraverGetNormalFile(IIterableFile file, List<IFile> fileList) {
            file.Children.ForEach(p => {
                if (p is IIterableFile) {
                    var itrFile = p as IIterableFile;
                    if (!itrFile.IsBackFile() && !itrFile.IsBackUpFile()) {
                        TraverGetNormalFile(itrFile, fileList);
                    }
                }
                else if (p.FileType == FileType.RegularFile) {
                    fileList.Add(p);
                }
            });
        }
    }

    //过滤相关;
    public abstract partial class FileBrowserViewModel {
        //过滤文件名相关;
        //是否开启了过滤文件名;
        public bool FilterFileNameNeeded {
            get {
                return filterFileNameModel?.IsEnabled ?? false;
            }
            set {
                if(filterFileNameModel != null) {
                    filterFileNameModel.IsEnabled = value;
                }
                FolderBrowserViewModel.FilterFileNameNeeded = value;
            }
        }
        private static FilterStringModel filterFileNameModel;
        
        public bool FilterFileSizeNeeded {
            get {
                return FolderBrowserViewModel.FilterFileSizeNeeded;
            }
            set {
                FolderBrowserViewModel.FilterFileSizeNeeded = value;
            }
        }
        private static FilterSizeModel filterFileSizeModel;

        public bool FilterFilePathNeeded {
            get {
                return FolderBrowserViewModel.FilterFilePathNeeded;
            }
            set {
                FolderBrowserViewModel.FilterFilePathNeeded = value;
            }
        }
        private static FilterStringModel filterFilePathModel;

        public bool FilterMTimeNeeded {
            get {
                return FolderBrowserViewModel.FilterMTimeNeeded;
            }
            set {
                FolderBrowserViewModel.FilterMTimeNeeded = value;
            }
        }
        private static FilterDateModel filterMTimeModel;

        public bool FilterCTimeNeeded {
            get {
                return FolderBrowserViewModel.FilterCTimeNeeded;
            }
            set {
                FolderBrowserViewModel.FilterCTimeNeeded = value;
            }
        }
        private static FilterDateModel filterCTimeModel;

        public bool FilterATimeNeeded {
            get {
                return FolderBrowserViewModel.FilterATimeNeeded;
            }
            set {
                FolderBrowserViewModel.FilterATimeNeeded = value;
            }
        }
        private static FilterDateModel filterATimeModel;


        public bool AnyFiltering {
            get {
                return FolderBrowserViewModel?.AnyFiltering??false;
            }
            set {
                if (FolderBrowserViewModel != null) {
                    FolderBrowserViewModel.AnyFiltering = value;
                }
                
            }
        }

        //文件/资源行;(须在外部指定实例);
        public ObservableCollection<FileRow> FileRows { get; set; } = new ObservableCollection<FileRow>();

        //是否全选;
        public bool? AllChecked {
            get {
                return FolderBrowserViewModel?.AllChecked;
            }
            set {
                if(FolderBrowserViewModel != null) {
                    FolderBrowserViewModel.AllChecked = value;
                }
            }
        }

        private void CheckAllChecked() {
            var hasChecked = FolderBrowserViewModel?.FileRows.FirstOrDefault(p => p.Checked) != null;
            var hasUnchecked = FolderBrowserViewModel?.FileRows.FirstOrDefault(p => !p.Checked) != null;
            if (hasChecked && hasUnchecked) {
                AllChecked = null;
            }
            else if (hasChecked) {
                AllChecked = true;
            }
            else {
                AllChecked = false;
            }
        }

        /// <summary>
        /// 过滤得到满足条件的项,最基本方法;
        /// </summary>
        /// <param name="obRows"></param>
        /// <returns></returns>
        public IEnumerable<FileRow> FilterRows(IEnumerable<FileRow> obRows) {
            IEnumerable<FileRow> rows = obRows;

            if (FilterFileNameNeeded) {
                rows = FilterStringRow(rows, filterFileNameModel,row => row.FileName);
            }

            if (rows.Count() == 0) {
                return rows;
            }

            if (FilterFileSizeNeeded) {
                rows = FilterSizeRow(rows, filterFileSizeModel);
            }

            if(rows.Count() == 0) {
                return rows;
            }

            if (FilterFilePathNeeded) {
                rows = FilterStringRow(rows, filterFilePathModel,row => row.FilePath);
            }

            if(FilterMTimeNeeded) {
                rows = FilterDTRow(rows, filterMTimeModel, row => row.ModifiedTime);
            }
            if (FilterATimeNeeded) {
                rows = FilterDTRow(rows, filterATimeModel, row => row.AccessedTime);
            }
            if (FilterCTimeNeeded) {
                rows = FilterDTRow(rows, filterCTimeModel, row => row.CreateTime);
            }

            if (rows.Count() == 0) {
                return rows;
            }

            return rows;
        }
        
        private IEnumerable<FileRow> FilterStringRow(IEnumerable<FileRow> rows,FilterStringModel fsModel,Func<FileRow,string> fieldFunc) {
            if (fsModel == null)
                return rows;

            if (fsModel.MatchWay == StringMatchWay.AnyKey) {
                var keys = fsModel.Keys.ToList();
                keys.RemoveAll(p => string.IsNullOrWhiteSpace(p));
                if (keys.Count > 0) {
                    rows = rows.Where(r =>
                        keys.Any(key => fieldFunc(r).IndexOf(key,fsModel.MatchCase?StringComparison.Ordinal:StringComparison.OrdinalIgnoreCase) != -1)
                    );
                }
            }
            else if (fsModel.MatchWay == StringMatchWay.FullMatch) {
                List<FileRow> rowList = new List<FileRow>();
                fsModel.Keys = fsModel.Keys?.Where(p => !string.IsNullOrWhiteSpace(p.Trim()))?.ToArray()??null;

                foreach (var row in rows) {
                    if(fsModel.Keys.FirstOrDefault(key => fieldFunc(row).IndexOf(key,fsModel.MatchCase?
                        StringComparison.Ordinal:StringComparison.OrdinalIgnoreCase) != -1) != null){
                        rowList.Add(row);
                    }
                }
                
                rows = rowList;
            }

            return rows;
         }

        private IEnumerable<FileRow> FilterSizeRow(IEnumerable<FileRow> rows,FilterSizeModel fzModel) {
            if (fzModel.Condition == TwoConditionRule.MinOnly) {
                rows = rows.Where(p => p.FileSize >= (fzModel.MinSize ?? 0));
            }
            else if (fzModel.Condition == TwoConditionRule.MaxOnly) {
                rows = rows.Where(p => p.FileSize <= (fzModel.MaxSize ?? 0));
            }
            else if (fzModel.Condition == TwoConditionRule.MinOrMax) {
                rows = rows.Where(p => p.FileSize <= (fzModel.MaxSize ?? 0) || p.FileSize >= (fzModel.MinSize));
            }
            else if (fzModel.Condition == TwoConditionRule.MinAndMax) {
                rows = rows.Where(p => p.FileSize <= (fzModel.MaxSize ?? 0) && p.FileSize >= (fzModel.MinSize));
            }
            return rows;
        }

        private IEnumerable<FileRow> FilterDTRow(IEnumerable<FileRow> rows, FilterDateModel fdtModel, Func<FileRow, DateTime?> dtFunc) {
            if (fdtModel.Condition == TwoConditionRule.MinOnly) {
                rows = rows.Where(p => dtFunc(p) >= fdtModel.MinTime);
            }
            else if (fdtModel.Condition == TwoConditionRule.MaxOnly) {
                rows = rows.Where(p => dtFunc(p) <= fdtModel.MaxTime);
            }
            else if(fdtModel.Condition == TwoConditionRule.MinOrMax) {
                
                rows = rows.Where(p => dtFunc(p) <= fdtModel.MaxTime || dtFunc(p) >= fdtModel.MinTime);
            }
            else if(fdtModel.Condition == TwoConditionRule.MinAndMax) {
                rows = rows.Where(p => dtFunc(p) <= fdtModel.MaxTime && dtFunc(p) >= fdtModel.MinTime);
            }
            return rows;
        }
    }
    
    //析构,预览相关;
    public abstract partial class FileBrowserViewModel {
        //~FileBrowserTabModel() {

        //}
        
    }
    
    //对象浏览器的命令部分;
    public abstract partial class FileBrowserViewModel {
        public event EventHandler<TEventArgs<IClosableTabModel>> TabClosed;                                  //关闭事件;
        private DelegateCommand closeThisCommand;                               //关闭当前对象浏览器的命令;
        public DelegateCommand CloseThisCommand {
            get {
                return closeThisCommand ??
                    (closeThisCommand = new DelegateCommand(() => {
                        Dispose();
                        TabClosed?.Invoke(this,new TEventArgs<IClosableTabModel>( this as IClosableTabModel ));
                    }));
            }
        }

        public event EventHandler<EventArgs> CloseAllRequired;
        private DelegateCommand _closeAllCommand;
        public DelegateCommand CloseAllCommand{
            get {
                return _closeAllCommand ??
                    (_closeAllCommand = new DelegateCommand(
                        () => {
                            ExistingBrowsers.Clear();
                            CloseAllRequired?.Invoke(this, EventArgs.Empty);
                        }
                    ));
            }
        }

        //关闭方法;
        public virtual void Dispose() {
            FolderBrowserViewModel.Exit();
            FileRows.Clear();
            allRows.Clear();
            ExistingBrowsers.Remove(this);
        }

        private static List<FileBrowserViewModel> ExistingBrowsers { get; } = new List<FileBrowserViewModel>();
    }

    //设备对象浏览器模型;
    public class DeviceBrowserTabModel : FileBrowserViewModel {
        public DeviceBrowserTabModel(Device file) : base(file) {
            TabViewModels.Add(PartHexModel);
        }
        private PartitionHexTabViewModel partHexModel;                     //分区十六进制查看视图模型;
        public PartitionHexTabViewModel PartHexModel {
            get {
                return partHexModel ??
                    (partHexModel = new PartitionHexTabViewModel(null));
            }
        }

        protected override void EscapeToFile(FileRow row) {                  //分区流转化;
            base.EscapeToFile(row);
            if (row?.File is Partition) {
                PartHexModel.File = row.File;
            }
        }
        
    }

    //分区对象浏览器模型;
    public class PartitionBrowserViewModel : FileBrowserViewModel {
        public PartitionBrowserViewModel(Partition part) : this(part as IFile) { }
        public PartitionBrowserViewModel(CDFC.Parse.Abstracts.Directory directory) : this(directory as IFile) { }

        public PartitionBrowserViewModel(IFile file) : base(file) {
            TabViewModels.Add(FileHexModel);
            TabViewModels.Add(PreviewerModel);
            TabViewModels.Add(DetailModel);
            TabViewModels.Add(ThumbNailModel);
            foreach (var item in FakeTabModel.Fakes) {
                TabViewModels.Add(item);
            }
        }

        private InternalFileHexTabViewModel fileHexModel;
        public InternalFileHexTabViewModel FileHexModel {
            get {
                return fileHexModel ??
                    (fileHexModel = new InternalFileHexTabViewModel(null));
            }
        }

        private FilePreviewerTabModel _previewerModel;
        public FilePreviewerTabModel PreviewerModel => _previewerModel ?? (_previewerModel = new FilePreviewerTabModel());

        private FileDetailTabModel _detailModel;
        public FileDetailTabModel DetailModel => _detailModel ?? (_detailModel = new FileDetailTabModel());

        private ThumbnailViewModel _thumbNailModel;
        public ThumbnailViewModel ThumbNailModel {
            get {
                if (_thumbNailModel == null) {
                    _thumbNailModel = new ThumbnailViewModel();
                    _thumbNailModel.GetRowsFunc = () => FileRows;

                    WeakEventManager<ThumbnailViewModel, TEventArgs<ObservableCollection<FileRow>>>.
                        AddHandler(_thumbNailModel, nameof(_thumbNailModel.SetRows), (sender, e) => {
                            FileRows = e.Target;
                        });

                    //进入目录;
                    WeakEventManager<ThumbnailViewModel, TEventArgs<FileRow>>.AddHandler(_thumbNailModel, nameof(_thumbNailModel.EnterRowed), (sender, e) => {
                        if(e.Target != null) {
                            EnterRow(e.Target);
                        }
                    });

                    //选定时滚动;
                    WeakEventManager<ThumbnailViewModel, TEventArgs<FileRow>>.AddHandler(_thumbNailModel, nameof(_thumbNailModel.SelectedRowChanged), (sender, e) => {
                        if(e.Target != null) {
                            FolderBrowserViewModel.FocusRow = e.Target;
                            FolderBrowserViewModel.SelectedFileRow = e.Target;
                        }
                    });
                }
                return _thumbNailModel;
            }
        }

        private object previewLocker = new object();
        
        protected override void EscapeToFile(FileRow row) {                  //分区流转化;
            base.EscapeToFile(row);

            var file = row.File;
            FileHexModel.File = file;


            //预览器跳转;
            if (file.FileType == FileType.RegularFile) {
                ThreadPool.QueueUserWorkItem(callBack => {
                    lock (previewLocker) {
                        try {
                            PreviewerModel.LoadPreviewerByFileRow(row);
                        }
                        catch(Exception ex) {
                            Logger.WriteCallerLine(ex.Message);
                            AppInvoke(() => {
                                RemainingMessageBox.Tell(ex.Message);
                            });
                        }
                    }
                });
            }

            //详细信息跳转;
            DetailModel.File = file;
            
        }
        //~PartitionBrowserTabModel() {

        //}

        public override void Dispose() {
            base.Dispose();
            PreviewerModel?.Dispose();
        }
    }
    
    //本地目录浏览器;
    public class LocalDirectoryBrowserViewModel : FileBrowserViewModel {
        public LocalDirectoryBrowserViewModel(IFile file) : base(file) {
            TabViewModels.Add(MainHexViewModel);
            TabViewModels.Add(PreviewerModel);
            SelectedTabModel = MainHexViewModel;
        }

        private FilePreviewerTabModel _previewerModel;
        public FilePreviewerTabModel PreviewerModel => _previewerModel ?? (_previewerModel = new FilePreviewerTabModel());

        private ObservableCollection<ITabModel> _tabViewModels;
        public override ObservableCollection<ITabModel> TabViewModels {
            get {
                if(_tabViewModels == null) {
                    _tabViewModels = new ObservableCollection<ITabModel>();
                }
                return _tabViewModels;
            }
        }
        
        protected override void EscapeToFile(FileRow row) {
            if(row == null) {
                return;
            }

            var file = row.File;
            if (file is LocalRegFile localFile) {
                try {
                    PreviewerModel.DisposePreviewer();
                    MainHexViewModel.Stream = localFile.GetStream();

                    PreviewerModel.LoadPreviewerByFileName(localFile.FileInfo.FullName);
                }
                catch (Exception ex) {
                    Logger.WriteLine($"{nameof(LocalDirectoryBrowserViewModel)}->{nameof(EscapeToFile)}:{ex.Message}");
                    RemainingMessageBox.Tell(ex.Message);
                }
            }
        }

        private FileHexTabViewModel _mainHexViewModel;
        public override FileHexTabViewModel MainHexViewModel {
            get {
                if(_mainHexViewModel == null) {
                    _mainHexViewModel = new InternalFileHexTabViewModel(null);
                }
                return _mainHexViewModel;
            }
        }

        public override void Dispose() {
            base.Dispose();
            MainHexViewModel.Stream?.Close();
            MainHexViewModel.Stream = null;
            PreviewerModel.Dispose();
        }
    }
    
    public class FakeTabModel : ITabModel {
        public string Header { get; set; }
        private static FakeTabModel[] fakes;
        public static FakeTabModel[] Fakes {
            get {
                if(fakes == null) {
                    fakes = new FakeTabModel[] {
                        new FakeTabModel {Header = "时间轴" }
                    };
                }
                return fakes;
            }
        }
    }
}
