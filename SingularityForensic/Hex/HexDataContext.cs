using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Controls;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Hex.Models;
using SingularityForensic.Hex.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;

namespace SingularityForensic.Hex {
    partial class HexDataContext : ExtensibleBindableBase, IHexDataContext {
        public HexDataContext(Stream stream) {
            _vm.Stream = stream;
            StackGrid.AddChild(
                UIObjectProviderFactory.CreateNew(ViewProvider.CreateView(Contracts.Hex.Constants.HexView, _vm)), 
                GridChildLength.Auto
            );
            
            _vm.FocusPositionChanged += delegate {
                FocusPositionChanged?.Invoke(this, EventArgs.Empty);
            };

            _vm.SelectionStateChanged += delegate {
                SelectionStateChanged?.Invoke(this, EventArgs.Empty);
            };

            _vm.FocusPositionChanged += delegate {
                //LostFocus?.Invoke(this, EventArgs.Empty);
            };

            _vm.SelectedToolTipItemModelChanged += delegate {
                SelectedToolTipItemChanged?.Invoke(this, EventArgs.Empty);
            };

            _customBackgroundBlocksWrapper = new CollectionWrapper<
                IBrushBlock, 
                WpfHexaEditor.Core.Interfaces.IBrushBlock
            >(p => new BrushBlockWrapper(p), _vm.CustomBackgroundBlocks);
        }

        public bool ReadOnlyMode { get; set; }

        public Stream Stream {
            get => _vm.Stream;
            set => _vm.Stream = value;
        }

        public long SelectionStart {
            get => _vm.SelectionStart;
            set => _vm.SelectionStart = value;
        }

        public long SelectionLength {
            get => _vm.SelectionLength;
            set => _vm.SelectionLength = value;
        }

        public event EventHandler SelectionStateChanged;
        public event EventHandler FocusPositionChanged;
        

        //public event EventHandler LostFocus;

        public long Position {
            get => _vm.Position;
            set => _vm.Position = value;
        }

        public long FocusPosition {
            get => _vm.FocusPosition;
            set => _vm.FocusPosition = value;
        }

        public ICollection<IBrushBlock> CustomBackgroundBlocks => _customBackgroundBlocksWrapper;

        private CollectionWrapper<IBrushBlock, WpfHexaEditor.Core.Interfaces.IBrushBlock> _customBackgroundBlocksWrapper;
        
        private HexViewViewModel _vm = new HexViewViewModel();

        public object UIObject => StackGrid.UIObject;

        public IBytesToCharEncoding BytesToCharEncoding {
            get => (_vm.BytesToCharEncoding as BytesToCharEncodingWrapper)?.Encoding;
            set => _vm.BytesToCharEncoding = new BytesToCharEncodingWrapper(value);  
        }
        public IStackGrid<IUIObjectProvider> StackGrid { get; } = StackGridFactory.CreateNew<IUIObjectProvider>();

        public void UpdateCustomBackgroundBlocks() {
            _vm.UpdateCustomBackgroundBlocks();
        }

        public void AddKeyBinding(ICommand command, Key key, ModifierKeys modifier = ModifierKeys.None) {
            
            InputBindingExtensions.AddKeyBinding(UIObject,command, key, modifier);
        }

        public int BytePerLine {
            get => _vm.BytePerLine;
            set => _vm.BytePerLine = value;
        }
    }

    partial class HexDataContext {
        public IEnumerable<ICommandItem> ContextCommands => _vm.ContextCommands;

        public ICollection<(long position, long size, IToolTipDataItem toolTipDataItem)> CustomDataToolTipItems => _vm.CustomDataToolTipItems;

        public ICollection<(long position, long size, IToolTipObjectItem toolTipObjectItem)> CustomObjectToolTipItems =>
            _vm.CustomObjectToolTipItems;

        public void AddContextCommand(ICommandItem commandItem) => _vm.ContextCommands.AddOrderBy(commandItem,ci => ci.Sort);

        public void RemoveContextCommand(ICommandItem commandItem) => _vm.ContextCommands.Remove(commandItem);

        public event EventHandler SelectedToolTipItemChanged;
        public IToolTipItem SelectedToolTipItem => _vm.SelectedToolTipItemModel?.ToolTipItem;

#if DEBUG
        ~HexDataContext() {

        }
#endif
    }
}
