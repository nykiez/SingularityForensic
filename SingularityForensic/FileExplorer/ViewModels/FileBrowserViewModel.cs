using CDFCUIContracts.Abstracts;
using System;
using System.Collections.ObjectModel;
using EventLogger;
using System.Threading;
using System.Windows;
using System.Linq;
using System.Collections.Generic;
using CDFCUIContracts.Events;
using static CDFCUIContracts.Helpers.ApplicationHelper;
using CDFCMessageBoxes.MessageBoxes;
using Prism.Commands;
using Prism.Mvvm;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Controls.Models.Filtering;
using SingularityForensic.Controls.MessageBoxes.Filtering;
using SingularityForensic.Contracts.Hex.Events;
using SingularityForensic.Contracts.App;

namespace SingularityForensic.FileExplorer.ViewModels {
    //对象浏览器主模型;
    public abstract partial class FileBrowserViewModel : 
        BindableBase, 
        IDisposable, IFileBrowserDataContext {
        public event EventHandler<TEventArgs< bool >> IsLoadingRequired;
        /// <summary>
        /// 对象浏览器主模型构造方法;
        /// </summary>
        /// <param name="file">所描述的文件</param>
        public FileBrowserViewModel(FileBase file, IFileExplorerServiceProvider expServiceProvider) {
            this.File = file ?? throw new ArgumentNullException(nameof(file));
            this.ExpServiceProvider = expServiceProvider ?? throw new ArgumentNullException(nameof(expServiceProvider));

            this.CurFile = file;

            //if(file != null) {
            //    if (file.Type == CDFC.Parse.Contracts.FileType.BlockDeviceFile) {                //若为块设备文件，则最高等级文件即为本身;
            //        OwnerFile = file;
            //    }
            //    else {                                                          //若为目录,则最高等级文件为所属分区;
            //        var part = file.GetParent<Partition>();
            //        if(part != null) {
            //            OwnerFile = part;
            //        }
            //    }
            //}
            //SelectedTabModel = MainHexViewModel;
            ExistingBrowsers.Add(this);
        }

        public IFileExplorerServiceProvider ExpServiceProvider { get; }
        

        public FileBase File { get; private set; }                             //该模型当前所属文件;
        public FileBase OwnerFile { get;  }                        //该模型最高等级文件;
        
        //当前呈现的文件;
        //当前展开的文件;
        private FileBase _curFile;
        public FileBase CurFile {
            get {
                return _curFile;
            }
            protected set {
                _curFile = value;
                //若目标文件不等于当前文件,则跳转;
                //if (true) {
                //    if (value is IEnumerableFile itrFile) {
                //        _curFile = value;
                //        FolderBrowserViewModel?.NavNodes?.Clear();
                //        allRows.Clear();
                        
                //        if(RowBuilder == null) {
                //            return;
                //        }

                //        //若为目录;
                //        if (itrFile.Type != FileType.BlockDeviceFile) {
                //            allRows.AddRange(itrFile.Children.Select(p => RowBuilder.BuildRow(p)));
                //        }
                //        //若为设备;
                //        else if (itrFile is Device) {
                //            var device = itrFile as Device;
                //            var partIndex = 0;
                //            allRows.AddRange(itrFile.Children.Select(p => {
                //                var row = RowBuilder.BuildRow(p);
                //                row.PartitionIndex = partIndex++;
                //                return row;
                //            }));
                //        }
                //        //若为分区;
                //        else {
                //            allRows.AddRange(itrFile.Children.Where(p => {
                //                if (p.Type == FileType.Directory) {
                //                    if ((p as CDFC.Parse.Abstracts.Directory).IsBackFile()
                //                    || (p as CDFC.Parse.Abstracts.Directory).IsBackUpFile()) {
                //                        return false;
                //                    }
                //                }
                //                return true;
                //            }).Select(p => RowBuilder.BuildRow(p)));
                //        }
                //        ApplyAllRows();

                //        var ownNavNode = new NavNodeModel(value);
                //        ownNavNode.EscapeRequired += (sender, e) => {
                //            CurFile = e;
                //        };
                //        FolderBrowserViewModel?.NavNodes?.Insert(0, ownNavNode);
                //        var pt = value.Parent;
                //        while (pt != null && !(pt is Device)) {
                //            var navNode = new NavNodeModel(pt);
                //            navNode.EscapeRequired += (sender, e) => {
                //                CurFile = e;
                //            };
                //            FolderBrowserViewModel?.NavNodes.Insert(0, navNode);
                //            pt = pt.Parent;
                //        }
                //    }

                //    FolderBrowserViewModel.IsExpanded = false;
                //}
            }
        }

        //未过滤的所有行;
        private List<IFileRow> allRows = new List<IFileRow>();                

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

        public void EnterTargetFile(FileBase targetFile) {
            if(targetFile is IHaveFileCollection enumFile) {
                if (targetFile is Directory itrDir) {    //若需展开文件为目录;
                    if (itrDir.IsBackDir()) {
                        if (CurFile is Directory) {
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
        public void EnterRow(IFileRow row) {
            if(row is IFileRow<FileBase> fileRow) {
                var targetFile = fileRow.File;
                //验证是否属于该文件列表;
                //if (FileRows.FirstOrDefault(p => p.File == targetFile) != null) {//验证是否包含文件
                    if (!(OwnerFile is Device)) {
                        EnterTargetFile(targetFile);
                    }
                //}
                if (fileRow != null && fileRow.File != null) {
                    if (fileRow.File is Partition) {
                        AddPartTabRequired?.Invoke(this, new TEventArgs<Partition>(fileRow.File as Partition));
                    }
                }
                AllChecked = false;
            }
            
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
                    else if(File is Directory){
                        folderBrowserViewModel = new DirectoriesBrowserViewModel(File);
                    }

                    folderBrowserViewModel.FilterFileNameNeeded = FilterFileNameNeeded;

                    //为文件行变更时;
                    folderBrowserViewModel.FileRows = FileRows;
                    WeakEventManager <FolderBrowserViewModel,TEventArgs<IFileRow>>.
                        AddHandler(folderBrowserViewModel,nameof(folderBrowserViewModel.SelectedFileRowChanged),
                    (sender, e) => {
                        EscapeToFile(e.Target);
                    });

                    WeakEventManager<FolderBrowserViewModel, TEventArgs<long>>.
                        AddHandler(folderBrowserViewModel, nameof(folderBrowserViewModel.FocusAddressChanged),
                        (sender, e) => {
                            Partition part = null;
                            if (OwnerFile is IBlockedStream blockDevice) {
                                part = OwnerFile as Partition;
                            }
                            if (part != null) {
                                if (0 <= e.Target && e.Target < part.Size) {
                                    //MainHexViewModel.Position = e.Target * 4096;
                                }
                            }
                        });

                    WeakEventManager<FolderBrowserViewModel,TEventArgs<IFileRow>>
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
        protected virtual void EscapeToFile(IFileRow row) {
            if(row is IFileRow<FileBase> fileRow && fileRow.File != null) {
                var file = fileRow.File;
                if(file is IBlockedStream) {             //若选定单元为块设备文件，则可能为分区;
                    if(file is Partition part) {
                        //MainHexViewModel.Position = part.StartLBA;
                    }
                }
                else if (file is Directory directory) {
                    if(directory.StartLBA != null) {
                        //MainHexViewModel.Position = directory.StartLBA.Value;
                    }
                }
                else if(file is RegularFile) {
                    if (file is RegularFile regFile) {
                        //MainHexViewModel.Position = regFile.StartLBA??0;
                    }
                }
            }
        }
        
        //进入到某个文件(夹)的处理;
        public void EnterFile(FileBase file) {
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
        
        //private FileHexTabViewModel mainHexViewModel;                     //主十六进制查看视图模型;
        //public virtual FileHexTabViewModel MainHexViewModel {
        //    get {
        //        if(OwnerFile != null && mainHexViewModel == null) {
        //            if(OwnerFile is Partition) {
        //                var part = OwnerFile as Partition;
        //                var partHex = new PartitionHexTabViewModel(part);
        //                WeakEventManager<PartitionHexTabViewModel, TEventArgs<long>>.AddHandler(
        //                    partHex,nameof(PartitionHexTabViewModel.FindFsPositionRequired),
        //                    (sender, e) => {
        //                        IHaveFileCollection itera = null;
        //                        if(this.OwnerFile != null && (itera = OwnerFile as IHaveFileCollection) != null) {
        //                            var secSize = (OwnerFile.Parent as Device)?.BlockSize ?? 512;
        //                            secSize = secSize == 0 ? 512 : secSize;

        //                            IsLoadingRequired?.Invoke(this,new TEventArgs<bool>(true));
        //                            //ThreadPool.QueueUserWorkItem(cb => {
        //                            //    try {
        //                            //        IFile innerFile = null;
                                            
        //                            //        if((innerFile = itera.GetInnerFileByPosition(e.Target / secSize * secSize)) != null) {
        //                            //            AppInvoke(() => {
        //                            //                CurFile = innerFile.Parent;
        //                            //                FolderBrowserViewModel.SelectedFileRow = 
        //                            //                FileRows.FirstOrDefault(p => p is IFileRow<IFile> fileRow && fileRow.File == innerFile);
        //                            //            });
        //                            //        }
        //                            //        else {
        //                            //            AppInvoke(() => {
        //                            //                RemainingMessageBox.Tell(FindResourceString("UnableToLocateTheFile"));
        //                            //            });
        //                            //        }
        //                            //    }
        //                            //    catch {
        //                            //        AppInvoke(() => {
        //                            //            RemainingMessageBox.Tell(FindResourceString("FailedToLocateTheFile"));
        //                            //        });
        //                            //    }
        //                            //    finally {
        //                            //        IsLoadingRequired?.Invoke(this, new TEventArgs<bool>(false));
        //                            //    }
        //                            //});
        //                        }
        //                        else {
        //                            Logger.WriteLine($"{nameof(FileBrowserViewModel)}->{nameof(MainHexViewModel)}->{nameof(PartitionHexTabViewModel.FindFsPositionRequired)}: OwnerFile Can't be null!");
        //                        }
        //                    });
                        
        //                mainHexViewModel = partHex;
        //            }
        //            else if(OwnerFile is Device) {
        //                var device = OwnerFile as Device;
        //                mainHexViewModel = new DeviceHexTabViewModel(device);
        //            }
        //        }
        //        else {
        //            Logger.WriteLine($"{nameof(FileBrowserViewModel)}->{nameof(MainHexViewModel)}:{nameof(OwnerFile)} Can't be null!");
        //        }
        //        return mainHexViewModel;
        //    }
        //}

        //private ObservableCollection<ITabModel> _tabViewModels;
        //public virtual ObservableCollection<ITabModel> TabViewModels {
        //    get {
        //        if(_tabViewModels == null) {
        //            _tabViewModels = new ObservableCollection<ITabModel>();
        //            _tabViewModels.Add(MainHexViewModel);
        //        }
        //        return _tabViewModels;
        //    }
        //    set {
        //        _tabViewModels = value;
        //    }
        //} 

        //在当前文件中递归检索;
        public void SearchNameInCurFile(string keyValue) {
            if (CurFile is IHaveFileCollection) {
                ExpandFile(CurFile as IHaveFileCollection,keyValue);
            }
        }
        
        //递归展开某个文件;
        public void ExpandFile(IHaveFileCollection file, string keyValue = null) {
            List<FileBase> fileList = new List<FileBase>();
            //递归获取所有文件;
            TraverGetNormalFile(file, fileList);
            this.allRows.Clear();

            
            //if(RowBuilder == null) {
            //    return;
            //}

            //this.allRows.AddRange(fileList.Select(p => RowBuilder.BuildRow(p)));

            ////匹配搜索;
            //HighlightContent.ToHighlight = keyValue??string.Empty;
            //if(HighlightContent.Mode == HighlightContentMode.AnyKey) {
            //    string[] keys = HighlightContent.ToHighlight.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //    if (keys.Length > 0) {
            //        allRows.RemoveAll(r => 
            //            keys.Any(key => (r.FileName + "\0" + r.FileType).IndexOf(key, StringComparison.OrdinalIgnoreCase) == -1)
            //        );
            //    }
            //}
            //else {
            //    if (!string.IsNullOrEmpty(HighlightContent.ToHighlight)) {
            //        allRows.RemoveAll(r => (r.FileName + "\0" + r.FileType).IndexOf(HighlightContent.ToHighlight, StringComparison.OrdinalIgnoreCase) == -1);
            //    }
            //}
            
            ApplyAllRows();
            IsExpanded = true;
        }

        //填充文件;(适用搜索);
        public void FillFiles(IEnumerable<FileBase> files) {
            if (files == null)
                throw new ArgumentNullException(nameof(files));
            
            allRows.Clear();
            //if(RowBuilder == null) {
            //    return;
            //}

            //var newRows = files.Select(p => RowBuilder.BuildRow(p));
            //allRows.AddRange(newRows);

            //IsExpanded = true;
            //ApplyAllRows();
        }
        
        //递归得到所有子文件;
        public void TraverGetNormalFile(IHaveFileCollection file, List<FileBase> fileList) {
            foreach (var p in file.Children) {
                if (p is Directory itrFile) {
                    if (!itrFile.IsBackDir() && !itrFile.IsBackUpDir()) {
                        TraverGetNormalFile(itrFile, fileList);
                    }
                }
                else if (p is RegularFile) {
                    fileList.Add(p);
                }
            }
            
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
        public ObservableCollection<IFileRow> FileRows { get; set; } = new ObservableCollection<IFileRow>();

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
        public IEnumerable<IFileRow> FilterRows(IEnumerable<IFileRow> obRows) {
            IEnumerable<IFileRow> rows = obRows;

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
        
        private IEnumerable<IFileRow> FilterStringRow(IEnumerable<IFileRow> rows,FilterStringModel fsModel,Func<IFileRow,string> fieldFunc) {
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
                List<IFileRow> rowList = new List<IFileRow>();
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

        private IEnumerable<IFileRow> FilterSizeRow(IEnumerable<IFileRow> rows,FilterSizeModel fzModel) {
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

        private IEnumerable<IFileRow> FilterDTRow(IEnumerable<IFileRow> rows, FilterDateModel fdtModel, Func<IFileRow, DateTime?> dtFunc) {
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
        public DeviceBrowserTabModel(Device file,IFileExplorerServiceProvider fsServiceProvider) : 
            base(file,fsServiceProvider) {
            //TabViewModels.Add(PartHexModel);
            //MainHexViewModel.Data = file;
            //PubEventHelper.GetEvent<HexEditorLoadedEvent>().Publish(MainHexViewModel);
        }
        //private PartitionHexTabViewModel partHexModel;                     //分区十六进制查看视图模型;
        //public PartitionHexTabViewModel PartHexModel {
        //    get {
        //        return partHexModel ??
        //            (partHexModel = new PartitionHexTabViewModel(null));
        //    }
        //}

        protected override void EscapeToFile(IFileRow row) {                  //分区流转化;
            base.EscapeToFile(row);
            if (row is IFileRow<Partition> partRow) {
                //PartHexModel.File = partRow.File;
            }
        }
        
    }

    //分区对象浏览器模型;
    public class PartitionBrowserViewModel : FileBrowserViewModel {
        public PartitionBrowserViewModel(Partition part, IFileExplorerServiceProvider fsServiceProvider) : this(part as FileBase,fsServiceProvider) { }
        public PartitionBrowserViewModel(Directory directory, IFileExplorerServiceProvider fsServiceProvider) : this(directory as FileBase, fsServiceProvider) { }

        public PartitionBrowserViewModel(FileBase file, IFileExplorerServiceProvider fsServiceProvider) : base(file , fsServiceProvider) {
            BuildTabs();
        }

        private void BuildTabs() {
            //TabViewModels.Add(FileHexModel);
            //TabViewModels.Add(PreviewerModel);
            //TabViewModels.Add(DetailModel);
            //TabViewModels.Add(ThumbNailModel);
            //foreach (var item in FakeTabModel.Fakes) {
            //    TabViewModels.Add(item);
            //}
        }

        //private InternalFileHexTabViewModel fileHexModel;
        //public InternalFileHexTabViewModel FileHexModel {
        //    get {
        //        return fileHexModel ??
        //            (fileHexModel = new InternalFileHexTabViewModel(null));
        //    }
        //}

        private FilePreviewerTabModel _previewerModel;
        public FilePreviewerTabModel PreviewerModel => _previewerModel ?? (_previewerModel = new FilePreviewerTabModel());

        private FileDetailTabModel _detailModel;
        public FileDetailTabModel DetailModel => _detailModel ?? (_detailModel = new FileDetailTabModel(ExpServiceProvider));

        private ThumbnailViewModel _thumbNailModel;
        public ThumbnailViewModel ThumbNailModel {
            get {
                if (_thumbNailModel == null) {
                    _thumbNailModel = new ThumbnailViewModel();
                    _thumbNailModel.GetRowsFunc = () => FileRows;

                    WeakEventManager<ThumbnailViewModel, TEventArgs<ObservableCollection<IFileRow>>>.
                        AddHandler(_thumbNailModel, nameof(_thumbNailModel.SetRows), (sender, e) => {
                            FileRows = e.Target;
                        });

                    //进入目录;
                    WeakEventManager<ThumbnailViewModel, TEventArgs<IFileRow>>.AddHandler(_thumbNailModel, nameof(_thumbNailModel.EnterRowed), (sender, e) => {
                        if(e.Target != null) {
                            EnterRow(e.Target);
                        }
                    });

                    //选定时滚动;
                    WeakEventManager<ThumbnailViewModel, TEventArgs<IFileRow>>.AddHandler(_thumbNailModel, nameof(_thumbNailModel.SelectedRowChanged), (sender, e) => {
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
        
        protected override void EscapeToFile(IFileRow row) {                  //分区流转化;
            base.EscapeToFile(row);
            if(row is IFileRow<FileBase> fileRow) {
                var file = fileRow.File;
                //FileHexModel.File = file;


                //预览器跳转;
                if (file is RegularFile) {
                    ThreadPool.QueueUserWorkItem(callBack => {
                        lock (previewLocker) {
                            try {
                                PreviewerModel.LoadPreviewerByFileRow(fileRow);
                            }
                            catch (Exception ex) {
                                LoggerService.Current?.WriteCallerLine(ex.Message);
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
        public LocalDirectoryBrowserViewModel(FileBase file, IFileExplorerServiceProvider fsServiceProvider) : base(file,fsServiceProvider) {
            //TabViewModels.Add(MainHexViewModel);
            //TabViewModels.Add(PreviewerModel);
            //SelectedTabModel = MainHexViewModel;
        }

        private FilePreviewerTabModel _previewerModel;
        public FilePreviewerTabModel PreviewerModel => _previewerModel ?? (_previewerModel = new FilePreviewerTabModel());

        private ObservableCollection<ITabModel> _tabViewModels;
        //public override ObservableCollection<ITabModel> TabViewModels {
        //    get {
        //        if(_tabViewModels == null) {
        //            _tabViewModels = new ObservableCollection<ITabModel>();
        //        }
        //        return _tabViewModels;
        //    }
        //}
        
        protected override void EscapeToFile(IFileRow row) {
            if(row is IFileRow<FileBase> fileRow) {
                var file = fileRow.File;
                //if (file is LocalRegFile localFile) {
                //    try {
                //        PreviewerModel.DisposePreviewer();
                //        MainHexViewModel.Stream = localFile.GetStream();

                //        PreviewerModel.LoadPreviewerByFileName(localFile.FileInfo.FullName);
                //    }
                //    catch (Exception ex) {
                //        Logger.WriteLine($"{nameof(LocalDirectoryBrowserViewModel)}->{nameof(EscapeToFile)}:{ex.Message}");
                //        RemainingMessageBox.Tell(ex.Message);
                //    }
                //}
            }

            
        }

        //private FileHexTabViewModel _mainHexViewModel;
        //public override FileHexTabViewModel MainHexViewModel {
        //    get {
        //        if(_mainHexViewModel == null) {
        //            _mainHexViewModel = new InternalFileHexTabViewModel(null);
        //        }
        //        return _mainHexViewModel;
        //    }
        //}

        public override void Dispose() {
            base.Dispose();
            //MainHexViewModel.Stream?.Close();
            //MainHexViewModel.Stream = null;
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
