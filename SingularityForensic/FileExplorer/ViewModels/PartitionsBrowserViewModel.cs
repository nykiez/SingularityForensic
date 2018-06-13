using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using SingularityForensic.FileExplorer.Models;
using SingularityForensic.Contracts.Controls;
using SingularityForensic.Controls;
using SingularityForensic.Contracts.FileExplorer.ViewModels;

namespace SingularityForensic.FileExplorer.ViewModels {
    /// <summary>
    /// 磁盘-分区列表视图模型;
    /// </summary>
    public partial class PartitionsBrowserViewModel : DataGridExViewModel,IPartitionsBrowserViewModel {
        public PartitionsBrowserViewModel(IDevice device)  {
            if(device == null) {
                throw new ArgumentNullException(nameof(device));
            }
            this.Device = device;
            
            InitializeColumns();
            FillRows(device.Children.Select(p => p as IPartition));
        }

        public IDevice Device { get; }

        /// <summary>
        /// 分区数据绑定源;
        /// </summary>
        public CustomTypedListSource<IPartitionRow> Partitions { get; set; } 
            = new CustomTypedListSource<IPartitionRow>();
        
        /// <summary>
        /// 初始化列;比如在<see cref="InitializeFileRowDescriptors"/>后执行
        /// </summary>
        private void InitializeColumns() {
            foreach (var descripter in PartitionRowFactory.Current.PropertyDescriptors) {
                Partitions.PropertyDescriptorList.Add(descripter);
            }
        }


        /// <summary>
        /// 填充Rows;
        /// </summary>
        /// <param name="files"></param>
        private void FillRows(IEnumerable<IPartition> parts) {
            if(parts == null) {
                return;
            }

            Partitions.Clear();

            foreach (var part in parts) {
                if(part == null) {
                    continue;
                }
                Partitions.Add(PartitionRowFactory.Current.CreateFileRow(part));
            }
        }
        
        private IPartitionRow _selectedRow;
        public IPartitionRow SelectedRow {
            get => _selectedRow;
            set  {
                SetProperty(ref _selectedRow, value);
                if(value == null) {
                    return;
                }

                SelectedPart = _selectedRow;
                PubEventHelper.PublishEventToHandlers((this as IPartitionsBrowserViewModel, SelectedPart),GenericServiceStaticInstances<IFocusedPartitionChangedEventHandler>.Currents);
                PubEventHelper.GetEvent<FocusedPartitionChangedEvent>().Publish((this, SelectedPart));
            }
        }

        public IPartitionRow SelectedPart {
            get;
            private set;
        }
        
        private IEnumerable<ICommandItem> _contextCommands;
        public override IEnumerable<ICommandItem> ContextCommands {
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
            var descriptor = PartitionRowFactory.Current.PropertyDescriptors.FirstOrDefault(p => p.Name == e.ItemPropertyInfo.Name);
            if (!(descriptor is PartitionRow.FileRowPropertyDescriptor partRowPropDescriptor)) {
                return;
            }

            e.CellTemplate = partRowPropDescriptor.FileMetaDataProvider.CellTemplate;
            e.Converter = partRowPropDescriptor.FileMetaDataProvider.Converter;
        }

        public override void NotifyDoubleClickOnRow(object row) {
            if (!(row is IPartitionRow partRow)) {
                return;
            }

            try {
                var part = partRow.File;
                PubEventHelper.GetEvent<PartitionDoubleClickedEvent>().Publish((this,part));
                PubEventHelper.PublishEventToHandlers((this as IPartitionsBrowserViewModel, part), GenericServiceStaticInstances<IPartitionDoubleClickedEventHandler>.Currents);
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
        }
    }
}
