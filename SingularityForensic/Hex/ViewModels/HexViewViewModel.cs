using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Hex.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace SingularityForensic.Hex.ViewModels {
    public partial class HexViewViewModel : BindableBase {
        public HexViewViewModel() {
            InitializeToolTips();
        }
        private bool readOnlyMode;
        public bool ReadOnlyMode {
            get {
                if (Stream != null) {
                    return !Stream.CanWrite;
                }
                return false;
            }
        }

        private Stream stream;                      //所描述的流对象;
        public Stream Stream {
            get {
                return stream;
            }
            set {
                SetProperty(ref stream, value);
                RaisePropertyChanged(nameof(ReadOnlyMode));
            }
        }

        private long selectionStart = -1L;           //选择起始位置;
        public long SelectionStart {
            get {
                return selectionStart;
            }
            set {
                SetProperty(ref selectionStart, value);
                this.SelectionStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private long _selectionLength = -1L;                   //控制选定终止处;
        public long SelectionLength {
            get {
                return _selectionLength;
            }
            set {
                SetProperty(ref _selectionLength, value);
                this.SelectionStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler SelectionStateChanged;

        private long position = 0;
        public long Position {                      //当前位置;
            get {
                return position;
            }
            set {
                SetProperty(ref position, value);
            }
        }

        private long _focusPosition = -1;
        public long FocusPosition {
            get {
                return _focusPosition;
            }
            set {
                SetProperty(ref _focusPosition, value);
                FocusPositionChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler FocusPositionChanged;

        public ObservableCollection<WpfHexaEditor.Core.Interfaces.IBrushBlock> CustomBackgroundBlocks { get; set; }
            = new ObservableCollection<WpfHexaEditor.Core.Interfaces.IBrushBlock>();

        public void UpdateCustomBackgroundBlocks() {
            UpdateBackgroundRequest.Raise(new Notification());
        }

        public InteractionRequest<Notification> UpdateBackgroundRequest { get; set; } = new InteractionRequest<Notification>();

        /// <summary>
        /// 失去焦点命令;
        /// </summary>
        private DelegateCommand _lostFocusCommand;
        public DelegateCommand LostFocusCommand => _lostFocusCommand ??
            (_lostFocusCommand = new DelegateCommand(
                () => {
                    LostFocus?.Invoke(this, EventArgs.Empty);
                }
            ));
        public event EventHandler LostFocus;
    }

    /// <summary>
    /// ToolTip
    /// </summary>
    public partial class HexViewViewModel {
        private void InitializeToolTips() {
            _positionToolTipItemModel = ToolTipItemModelFactory.CreateToolTipDataItemModel();
            _valToolTipItemModel = ToolTipItemModelFactory.CreateToolTipDataItemModel();

            _positionToolTipItem = ToolTipItemFactory.CreateIToolTipDataItem();
            _valToolTipItem = ToolTipItemFactory.CreateIToolTipDataItem();

            _positionToolTipItemModel.ToolTipItem = _positionToolTipItem;
            _valToolTipItemModel.ToolTipItem = _valToolTipItem;

            _positionToolTipItem.KeyName = LanguageService.Current?.FindResourceString(Constants.ToolTipKey_Offset);
            _valToolTipItem.KeyName = LanguageService.Current?.FindResourceString(Constants.ToolTipKey_Value);

            ToolTipItemModels.Add(_positionToolTipItemModel);
            ToolTipItemModels.Add(_valToolTipItemModel);
        }

        private IToolTipItemModel _valToolTipItemModel;
        private IToolTipItemModel _positionToolTipItemModel;
        private IToolTipDataItem _valToolTipItem;
        private IToolTipDataItem _positionToolTipItem;

        private long _hoverPosition;
        public long HoverPosition {
            get => _hoverPosition;
            set {
                SetProperty(ref _hoverPosition, value);
                UpdateToolTipItems();
            }
        }

        public ObservableCollection<IToolTipItemModel> ToolTipItemModels { get; set; } = new ObservableCollection<IToolTipItemModel>();

        /// <summary>
        /// These properties make the tool tip more extensible;
        /// </summary>
        public ICollection<(long position, long size, IToolTipDataItem toolTipDataItem)> CustomDataToolTipItems = new List<(long position, long size, IToolTipDataItem toolTipDataItem)>();
        public ICollection<(long position, long size, IToolTipObjectItem toolTipObjectItem)> CustomObjectToolTipItems = new List<(long position, long size, IToolTipObjectItem toolTipObjectItem)>();
        /// <summary>
        /// This is for better performance,reducing frequency of the building IToolTipDataItem;
        /// </summary>
        private List<IToolTipItemModel> _cachedToolTipDataItems = new List<IToolTipItemModel>();
        IToolTipItemModel GetOrCreateDataItem(int index) {
            if(_cachedToolTipDataItems.Count < index +1) {
                var sub = index + 1 - _cachedToolTipDataItems.Count;
                for (int i = 0; i < sub; i++) {
                    _cachedToolTipDataItems.Add(ToolTipItemModelFactory.CreateToolTipDataItemModel());
                }
            }
            return _cachedToolTipDataItems[index];
        }

        private void UpdateToolTipItems() {
            if (!(Stream?.CanRead ?? false)) {
                return;
            }

            if (HoverPosition >= Stream.Length) {
                return;
            }

            ToolTipItemModels.Clear();

            Stream.Position = HoverPosition;
            _positionToolTipItem.Value = HoverPosition.ToString();
            _valToolTipItem.Value = Stream.ReadByte().ToString();
            _positionToolTipItemModel.ToolTipItem = _positionToolTipItem;
            _valToolTipItemModel.ToolTipItem = _valToolTipItem;

            ToolTipItemModels.Add(_positionToolTipItemModel);
            ToolTipItemModels.Add(_valToolTipItemModel);
            
            //Update  Custom ToolDataTips;
            var dataToolTipIndex = 0;
            foreach ((long position, long size, IToolTipDataItem tooltipDataItem) in CustomDataToolTipItems) {
                if (!(HoverPosition >= position && HoverPosition < size + position)) {
                    continue;
                }
                
                var tooltipDataItemModel = GetOrCreateDataItem(dataToolTipIndex);
                tooltipDataItemModel.ToolTipItem = tooltipDataItem;
                ToolTipItemModels.Add(tooltipDataItemModel);
                dataToolTipIndex++;
            }

            foreach ((long position, long size, IToolTipObjectItem toolTipObjectItem) in CustomObjectToolTipItems) {
                if (!(HoverPosition >= position && HoverPosition < size + position)) {
                    continue;
                }
                var toolTipObjectItemModel = ToolTipItemModelFactory.CreateToolTipObjectItemModel();
                toolTipObjectItemModel.ToolTipItem = toolTipObjectItem;
                ToolTipItemModels.Add(toolTipObjectItemModel);
            }
        }

        private IToolTipItemModel _selectedToolTipItemModel;
        public IToolTipItemModel SelectedToolTipItemModel {
            get => _selectedToolTipItemModel;
            set => SetProperty(ref _selectedToolTipItemModel, value);
        }


        private DelegateCommand _copyKeyCommand;
        public DelegateCommand CopyKeyCommand => _copyKeyCommand ??
            (_copyKeyCommand = new DelegateCommand(
                () => {
                    if (SelectedToolTipItemModel.ToolTipItem is IToolTipDataItem toolTipDataItem) {
                        ClipBoardService.SetText(toolTipDataItem.KeyName);
                    }
                },
                () => SelectedToolTipItemModel != null
            )).ObservesProperty(() => SelectedToolTipItemModel);


        private DelegateCommand _copyValueCommand;
        public DelegateCommand CopyValueCommand => _copyValueCommand ??
            (_copyValueCommand = new DelegateCommand(
                () => {
                    if (SelectedToolTipItemModel.ToolTipItem is IToolTipDataItem toolTipDataItem) {
                        ClipBoardService.SetText(toolTipDataItem.Value);
                    }
                },
                () => SelectedToolTipItemModel != null
            )).ObservesProperty(() => SelectedToolTipItemModel);

        private DelegateCommand _copyExpressionCommand;
        public DelegateCommand CopyExpressionCommand => _copyExpressionCommand ??
            (_copyExpressionCommand = new DelegateCommand(
                () => {
                    if (SelectedToolTipItemModel.ToolTipItem is IToolTipDataItem toolTipDataItem) {
                        ClipBoardService.SetText($"{toolTipDataItem.KeyName}:{toolTipDataItem.Value}");
                    }
                },
                () => SelectedToolTipItemModel != null
            )).ObservesProperty(() => SelectedToolTipItemModel);

        /// <summary>
        /// 右键菜单项;
        /// </summary>
        public ObservableCollection<ICommandItem> ContextCommands { get; set; } = new ObservableCollection<ICommandItem>();
    }

    public partial class HexViewViewModel {
        private WpfHexaEditor.Core.Interfaces.IBytesToCharEncoding _bytesToCharEncoding;
        public WpfHexaEditor.Core.Interfaces.IBytesToCharEncoding BytesToCharEncoding {
            get => _bytesToCharEncoding;
            set => SetProperty(ref _bytesToCharEncoding, value);
        }

    }
}
