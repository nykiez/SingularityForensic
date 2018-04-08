using Prism.Commands;
using Prism.Mvvm;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Converters;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Controls.ViewModels;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using Telerik.Windows.Controls;

namespace SingularityForensic.FileExplorer.ViewModels {
    /// <summary>
    /// 磁盘-分区列表视图模型;
    /// </summary>
    public class PartitionsBrowserViewModel : DataGridExViewModel {
        public PartitionsBrowserViewModel(Device device)  {
            if(device == null) {
                throw new ArgumentNullException(nameof(device));
            }
            this.Device = device;
            _partMetaDataProviders = ServiceProvider.Current?.
                GetAllInstances<IFileMetaDataProvider>().
                Where(p => p.MetaDataTypeFor == Contracts.FileExplorer.Constants.FileMetaDataType_Partition).
                OrderBy(p => p.Order);
            InitializeDT();
            FillRows(device.Children);
        }

        public Device Device { get; }

        /// <summary>
        /// 分区数据绑定源;
        /// </summary>
        public DataTable Partitions { get; private set; }

        private IEnumerable<IFileMetaDataProvider> _partMetaDataProviders;
        
        /// <summary>
        /// 初始化DataTable;
        /// </summary>
        private void InitializeDT() {
            Partitions = new DataTable();
            Partitions.Columns.Add(new DataColumn(Constants.PartMetaDataName_Partition, typeof(FileBase)));

            if (_partMetaDataProviders == null) {
                return;
            }
            
            foreach (var mProvider in _partMetaDataProviders) {
                Partitions.Columns.Add(new DataColumn(mProvider.MetaDataName, mProvider.MetaDataType));
            }
            
        }
        
        /// <summary>
        /// 填充Rows;
        /// </summary>
        /// <param name="files"></param>
        private void FillRows(IEnumerable<FileBase> files) {
            if(files == null) {
                return;
            }

            foreach (var file in files) {
                var newRow = Partitions.NewRow();
                foreach (var mProvider in _partMetaDataProviders) {
                    newRow[mProvider.MetaDataName] = mProvider.GetDataObject(file);
                }

                newRow[Constants.PartMetaDataName_Partition] = file;

                Partitions.Rows.Add(newRow);
            }
        }
        
        private DataRow _selectedRow;
        public DataRow SelectedRow {
            get => _selectedRow;
            set  {
                SetProperty(ref _selectedRow, value);
                if(value == null) {
                    return;
                }

                if(_selectedRow[Constants.PartMetaDataName_Partition] is FileBase file) {
                    SelectedFile = file;
                    PubEventHelper.GetEvent<FocusedFileChangedEvent>().Publish((file, Device));
                }
            }
        }

        public FileBase SelectedFile { get; private set; }

        public FileBase SelectedFileBase { get; private set; }

        private ObservableCollection<CommandItem> _contextCommands;
        public override ObservableCollection<CommandItem> ContextCommands {
            get {
                //if (_contextCommands == null) {
                //    var mainViewerCommandItem = new CommandItem {  CommandName = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("ViewerProgram") };
                //    mainViewerCommandItem.Children.AddRange(ViewersCommands);
                //    _contextCommands = new ObservableCollection<CommandItem> {
                //        new CommandItem{ CommandName=LanguageService.FindResourceString("FileDetailInfo") , Command = ShowFileDetailCommand }
                //    };
                //    _contextCommands.AddRange(base.ContextCommands);

                //}
                return _contextCommands;
            }
            set => _contextCommands = value;
        }

        //GridViewAutoGeneratingColumnEventArgs
        private DelegateCommand<GridViewAutoGeneratingColumnEventArgs> _autoGeneratingColumnCommand;
        public DelegateCommand<GridViewAutoGeneratingColumnEventArgs> AutoGeneratingColumnCommand =>
            _autoGeneratingColumnCommand ??
            (_autoGeneratingColumnCommand = new DelegateCommand<GridViewAutoGeneratingColumnEventArgs>(
                e => {
                    if (e == null) {
                        return;
                    }

                    if (e.ItemPropertyInfo.Name == Constants.PartMetaDataName_Partition) {
                        e.Cancel = true;
                    }

                    if (e.Column is GridViewDataColumn dataColumn
                    && e.ItemPropertyInfo.PropertyType == typeof(long)) {
                        if (dataColumn.DataMemberBinding != null) {
                            dataColumn.DataMemberBinding.Converter = ByteSizeToSizeConverter.StaticInstance;
                        }
                    }
                }
            ));
        
        
    }
}
