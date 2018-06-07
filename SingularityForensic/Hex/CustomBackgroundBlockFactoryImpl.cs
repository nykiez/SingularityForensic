using SingularityForensic.Contracts.Hex;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SingularityForensic.Hex {
    [Export(typeof(ICustomBackgroundBlockFactory))]
    class CustomBackgroundBlockFactoryImpl : ICustomBackgroundBlockFactory {
        public ICustomBackgroundBlock CreateNewBackgroundBlock() => new CustomBackgroundBlock();

        public ICustomBackgroundBlock CreateNewBackgroundBlock(long startOffset, long length, Brush background) =>
            new CustomBackgroundBlock {
                StartOffset = startOffset,
                Length = length,
                Background = background
            };

        public Brush FirstBrush => Brushes.Chocolate;
        public Brush SecondBrush => Brushes.DarkGray;
    }
}
