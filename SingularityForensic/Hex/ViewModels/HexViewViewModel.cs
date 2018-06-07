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
using System.Windows;
using System.Windows.Media;
using WpfHexaEditor.Core.Interfaces;

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

        public ObservableCollection<WpfHexaEditor.Core.Interfaces.ICustomBackgroundBlock> CustomBackgroundBlocks { get; set; } 
            = new ObservableCollection<WpfHexaEditor.Core.Interfaces.ICustomBackgroundBlock>();
        
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
            _positionToolTip = ToolTipItemFactory.CreateIToolTipDataItem();
            _valToolTip = ToolTipItemFactory.CreateIToolTipDataItem();

            _positionToolTip.KeyName = LanguageService.Current?.FindResourceString(Constants.ToolTipKey_Offset); 
            _valToolTip.KeyName = LanguageService.Current?.FindResourceString(Constants.ToolTipKey_Value);

            DataToolTips.Add(_positionToolTip);
            DataToolTips.Add(_valToolTip);
        }

        private IToolTipDataItem _valToolTip;
        private IToolTipDataItem _positionToolTip;

        private long _hoverPosition;
        public long HoverPosition {
            get => _hoverPosition;
            set {
                SetProperty(ref _hoverPosition, value);
                if (Stream?.CanRead ?? false) {
                    if (_hoverPosition >= Stream.Length) {
                        return;
                    }
                    Stream.Position = _hoverPosition;
                    _positionToolTip.Value = value.ToString();
                    _valToolTip.Value = Stream.ReadByte().ToString();
                }
                UpdateToolTipItems();

            }
        }

        public ObservableCollection<IToolTipItem> DataToolTips { get; set; } = new ObservableCollection<IToolTipItem>();

        /// <summary>
        /// These properties make the tool tip more extensible;
        /// </summary>
        public ICollection<(long position, long size, string key, string value)> CustomDataToolTipItems = new List<(long position, long size, string key, string value)>();
        public ICollection<(long position, long size, IToolTipObjectItem toolTipObjectItem)> CustomObjectToolTipItems = new List<(long position, long size, IToolTipObjectItem toolTipObjectItem)>();

        /// <summary>
        /// This is for better performance,reducing frequency of the building IToolTipDataItem;
        /// </summary>
        private List<IToolTipDataItem> _cachedToolTipDataItems = new List<IToolTipDataItem>();
        private void UpdateToolTipItems() {
            if (!(Stream?.CanRead ?? false)) {
                return;
            }

            if (HoverPosition >= Stream.Length) {
                return;
            }

            DataToolTips.Clear();

            Stream.Position = HoverPosition;
            _positionToolTip.Value = HoverPosition.ToString();
            _valToolTip.Value = Stream.ReadByte().ToString();

            DataToolTips.Add(_positionToolTip);
            DataToolTips.Add(_valToolTip);

            if (_cachedToolTipDataItems.Count < CustomDataToolTipItems.Count) {
                var sub = CustomDataToolTipItems.Count - _cachedToolTipDataItems.Count;
                for (int i = 0; i < sub; i++) {
                    _cachedToolTipDataItems.Add(ToolTipItemFactory.CreateIToolTipDataItem());
                }
            }


            //Update  Custom ToolDataTips;
            var dataToolTipIndex = 0;
            foreach ((long position, long size, string key, string value) in CustomDataToolTipItems) {
                if (!(HoverPosition >= position && HoverPosition < size + position)) {
                    continue;
                }

                var tooltipDataItem = _cachedToolTipDataItems[dataToolTipIndex];
                tooltipDataItem.KeyName = key;
                tooltipDataItem.Value = value;
                DataToolTips.Add(tooltipDataItem);

                dataToolTipIndex++;
            }

            foreach ((long position, long size, IToolTipObjectItem toolTipObjectItem) in CustomObjectToolTipItems) {
                if (!(HoverPosition >= position && HoverPosition < size + position)) {
                    continue;
                }

                DataToolTips.Add(toolTipObjectItem);
            }
        }

        private IToolTipItem _selectedToolTipItem;
        public IToolTipItem SelectedToolTipItem {
            get => _selectedToolTipItem;
            set => SetProperty(ref _selectedToolTipItem, value);
        }


        private DelegateCommand _copyKeyCommand;
        public DelegateCommand CopyKeyCommand => _copyKeyCommand ??
            (_copyKeyCommand = new DelegateCommand(
                () => {
                    if(SelectedToolTipItem is IToolTipDataItem toolTipDataItem) {
                        Clipboard.SetText(toolTipDataItem.KeyName);
                    }
                },
                () => SelectedToolTipItem != null
            )).ObservesProperty(() => SelectedToolTipItem);


        private DelegateCommand _copyValueCommand;
        public DelegateCommand CopyValueCommand => _copyValueCommand ??
            (_copyValueCommand = new DelegateCommand(
                () => {
                    if (SelectedToolTipItem is IToolTipDataItem toolTipDataItem) {
                        Clipboard.SetText(toolTipDataItem.Value);
                    }
                },
                () => SelectedToolTipItem != null
            )).ObservesProperty(() => SelectedToolTipItem);

        private DelegateCommand _copyExpressionCommand;
        public DelegateCommand CopyExpressionCommand => _copyExpressionCommand ??
            (_copyExpressionCommand = new DelegateCommand(
                () => {
                    if (SelectedToolTipItem is IToolTipDataItem toolTipDataItem) {
                        Clipboard.SetText($"{toolTipDataItem.KeyName}:{toolTipDataItem.Value}");
                    }
                },
                () => SelectedToolTipItem != null
            )).ObservesProperty(() => SelectedToolTipItem);

        /// <summary>
        /// 右键菜单项;
        /// </summary>
        public ObservableCollection<ICommandItem> ContextCommands { get; set; } = new ObservableCollection<ICommandItem>();
    }
}
