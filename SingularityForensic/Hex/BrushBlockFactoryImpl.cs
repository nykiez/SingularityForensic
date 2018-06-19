using SingularityForensic.Contracts.Hex;
using System.ComponentModel.Composition;
using System.Windows.Media;

namespace SingularityForensic.Hex {
    [Export(typeof(IBrushBlockFactory))]
    class BrushBlockFactoryImpl : IBrushBlockFactory {
        public IBrushBlock CreateNewBackgroundBlock() => new BrushBlock();

        public IBrushBlock CreateNewBackgroundBlock(long startOffset, long length, Brush background) =>
            new BrushBlock {
                StartOffset = startOffset,
                Length = length,
                Background = background
            };

        public Brush FirstBrush => Brushes.Chocolate;
        public Brush SecondBrush => Brushes.DarkGray;

        public Brush HighLightBrush => Brushes.Yellow;
    }
}
