﻿using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Hex.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace SingularityForensic.Hex {
    partial class HexDataContext : ExtensibleBindableBase, IHexDataContext {
        public HexDataContext(Stream stream) {
            _vm.Stream = stream;
            UIObject = ViewProvider.CreateView(Contracts.Hex.Constants.HexView, _vm);
            _vm.FocusPositionChanged += delegate {
                FocusPositionChanged?.Invoke(this, EventArgs.Empty);
            };

            _vm.SelectionStateChanged += delegate {
                SelectionStateChanged?.Invoke(this, EventArgs.Empty);
            };

            _vm.FocusPositionChanged += delegate {
                LostFocus?.Invoke(this, EventArgs.Empty);
            };
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
        public event EventHandler LostFocus;

        public long Position {
            get => _vm.Position;
            set => _vm.Position = value;
        }

        public long FocusPosition {
            get => _vm.FocusPosition;
            set => _vm.FocusPosition = value;
        }

        public ICollection<(long index, long length, Brush background)> CustomBackgroundBlocks {
            get => _vm.CustomBackgroundBlocks;
        }

        private HexViewViewModel _vm = new HexViewViewModel();
        
        public object UIObject { get; }
    }

    partial class HexDataContext {
        public IEnumerable<ICommandItem> ContextCommands => _vm.ContextCommands;

        public ICollection<(long position, long size, string key, string value)> CustomDataToolTipItems => _vm.CustomDataToolTipItems;

        public ICollection<(long position, long size, IToolTipObjectItem toolTipObjectItem)> CustomObjectToolTipItems =>
            _vm.CustomObjectToolTipItems;

        public void AddContextCommand(ICommandItem commandItem) => _vm.ContextCommands.AddOrderBy(commandItem,ci => ci.Sort);

        public void RemoveContextCommand(ICommandItem commandItem) => _vm.ContextCommands.Remove(commandItem);
    }
}
