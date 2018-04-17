using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Hex.ViewModels;
using SingularityForensic.Hex.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SingularityForensic.Hex {
    class HexDataContext : ExtensibleBindableBase,IHexDataContext {
        public HexDataContext(Stream stream) {
            _vm.Stream = stream;
            _view = ViewProvider.GetView(Contracts.Hex.Constants.HexView) as FrameworkElement;
            if(_view != null) {
                _view.DataContext = _vm;
            }
        }
        private HexStreamEditorViewModel _vm = new HexStreamEditorViewModel();
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
        public long Position {
            get => _vm.Position;
            set => _vm.Position = value;
        }

        public long FocusPosition {
            get => _vm.FocusPosition;
            set => _vm.FocusPosition = value;
        }

        public IList<(long index, long length, Brush background)> CustomBackgroundBlocks {
            get => _vm.CustomBackgroundBlocks;
        }

        public object Tag { get ; set ; }

        private FrameworkElement _view;
        public object UIObject => _view;
    }
}
