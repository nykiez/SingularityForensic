using SingularityForensic.Contracts.Hex;
using System.Windows.Media;

namespace SingularityForensic.Hex.Models {
    class BrushBlockWrapper : WpfHexaEditor.Core.Interfaces.IBrushBlock {
        public BrushBlockWrapper(IBrushBlock customBackgroundBlock) {
            this._customBackgroundBlock = customBackgroundBlock;
        }
        
        private IBrushBlock _customBackgroundBlock;
        public long StartOffset {
            get => _customBackgroundBlock.StartOffset;
            set => _customBackgroundBlock.StartOffset = value;
        }

        public long Length {
            get => _customBackgroundBlock.Length;
            set => _customBackgroundBlock.Length = value;
        }

        public Brush Brush {
            get => _customBackgroundBlock.Background;
            set => _customBackgroundBlock.Background = value;
        }
    }
}
