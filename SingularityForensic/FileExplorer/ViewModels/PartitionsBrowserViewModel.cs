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
using SingularityForensic.FileExplorer.Models;
using SingularityForensic.Data;

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

            InitializePartRowDescriptors();
            InitializeColumns();
            FillRows(device.Children.Select(p => p as Partition));
        }

        public Device Device { get; }

        /// <summary>
        /// 分区数据绑定源;
        /// </summary>
        public CustomTypedListSource<Partition> Partitions { get; set; } 
            = new CustomTypedListSource<Partition>();
        
        /// <summary>
        /// 初始化行元数据提供器;
        /// </summary>
        private void InitializePartRowDescriptors() {
            if (PartitionRow.DescriptorsInitialized) {
                return;
            }

            var partMetaDataProviders = ServiceProvider.Current?.GetAllInstances<IPartMetaDataProvider>();
            if (partMetaDataProviders == null) {
                LoggerService.WriteCallerLine($"{nameof(partMetaDataProviders)} can't be null.");
                return;
            }

            PartitionRow.InitializeDescripters(partMetaDataProviders);
        }

        /// <summary>
        /// 初始化列;比如在<see cref="InitializeFileRowDescriptors"/>后执行
        /// </summary>
        private void InitializeColumns() {
            foreach (var descripter in PartitionRow.PropertyDescriptors) {
                Partitions.PropertyDescriptorList.Add(descripter);
            }
        }


        /// <summary>
        /// 填充Rows;
        /// </summary>
        /// <param name="files"></param>
        private void FillRows(IEnumerable<Partition> parts) {
            if(parts == null) {
                return;
            }

            Partitions.Clear();

            foreach (var part in parts) {
                if(part == null) {
                    continue;
                }
                Partitions.Add(part);
            }
        }
        
        private PartitionRow _selectedRow;
        public PartitionRow SelectedRow {
            get => _selectedRow;
            set  {
                SetProperty(ref _selectedRow, value);
                if(value == null) {
                    return;
                }
                
                SelectedPart = SelectedRow.File;
                PubEventHelper.GetEvent<FocusedFileChangedEvent>().Publish((SelectedPart, Device));
            }
        }

        public FileBase SelectedPart { get; private set; }
        
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
