using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using SingularityForensic.FileExplorer.Models;
using SingularityForensic.Contracts.Controls;
using SingularityForensic.Controls;
using System.ComponentModel.Composition;

namespace SingularityForensic.FileExplorer.ViewModels {
    /// <summary>
    /// 磁盘-分区列表视图模型;
    /// </summary>
    public partial class PartitionsBrowserViewModel : DataGridExViewModel,IPartitionsBrowserViewModel {
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
        public CustomTypedListSource<PartitionRow> Partitions { get; set; } 
            = new CustomTypedListSource<PartitionRow>();
        
        /// <summary>
        /// 初始化行元数据提供器;
        /// </summary>
        private void InitializePartRowDescriptors() {
            if (PartitionRow.DescriptorsInitialized) {
                return;
            }

            var partMetaDataProviders = ServiceProvider.Current?.GetAllInstances<IPartitionMetaDataProvider>();
            if (partMetaDataProviders == null) {
                LoggerService.WriteCallerLine($"{nameof(partMetaDataProviders)} can't be null.");
                return;
            }

            PartitionRow.InitializeDescriptors(partMetaDataProviders);
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
                Partitions.Add(new PartitionRow(part));
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
                PubEventHelper.GetEvent<FocusedPartitionChangedEvent>().Publish((this, SelectedPart));
            }
        }

        public Partition SelectedPart {
            get;
            private set;
        }
        
        private IList<CommandItem> _contextCommands;
        public override IList<CommandItem> ContextCommands {
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

       
    }

    public partial class PartitionsBrowserViewModel {
        public override void NotifyAutoGeneratingColumns(GridViewAutoGeneratingColumnEventArgs e) {
            var descriptor = PartitionRow.PropertyDescriptors.FirstOrDefault(p => p.Name == e.ItemPropertyInfo.Name);
            if (!(descriptor is PartitionRow.FileRowPropertyDescriptor partRowPropDescriptor)) {
                return;
            }

            e.CellTemplate = partRowPropDescriptor.FileMetaDataProvider.CellTemplate;
            e.Converter = partRowPropDescriptor.FileMetaDataProvider.Converter;
        }

        public override void NotifyDoubleClickOnRow(object row) {
            if (!(row is PartitionRow partRow)) {
                return;
            }

            try {
                var part = partRow.File;
                PubEventHelper.GetEvent<PartitionDoubleClickedEvent>().Publish((this,part));
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
        }
    }
}
