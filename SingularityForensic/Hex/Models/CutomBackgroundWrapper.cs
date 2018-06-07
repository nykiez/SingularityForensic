using SingularityForensic.Contracts.Hex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SingularityForensic.Hex.Models {
    public class CutomBackgroundBlockWrapper :  WpfHexaEditor.Core.Interfaces.ICustomBackgroundBlock {
        public CutomBackgroundBlockWrapper(ICustomBackgroundBlock customBackgroundBlock) {
            this._customBackgroundBlock = customBackgroundBlock;
        }

        private ICustomBackgroundBlock _customBackgroundBlock;
        public long StartOffset {
            get => _customBackgroundBlock.StartOffset;
            set => _customBackgroundBlock.StartOffset = value;
        }

        public long Length {
            get => _customBackgroundBlock.Length;
            set => _customBackgroundBlock.Length = value;
        }
        public Brush Background {
            get => _customBackgroundBlock.Background;
            set => _customBackgroundBlock.Background = value;
        }
    }
}
