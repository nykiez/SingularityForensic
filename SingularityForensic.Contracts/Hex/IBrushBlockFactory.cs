using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SingularityForensic.Contracts.Hex {
    public interface IBrushBlockFactory {
        IBrushBlock CreateNewBackgroundBlock(long startOffset,long length,Brush background);
        IBrushBlock CreateNewBackgroundBlock();

        //区分相邻块的两种颜色;
        Brush FirstBrush { get; }
        Brush SecondBrush { get; }
    }

    public class BrushBlockFactory : GenericServiceStaticInstance<IBrushBlockFactory> {
        public static IBrushBlock CreateNewBackgroundBlock(long startOffset, long length, Brush background)
            => Current.CreateNewBackgroundBlock(startOffset, length, background);
        public static IBrushBlock CreateNewBackgroundBlock() => Current.CreateNewBackgroundBlock();

        public static Brush FirstBrush => Current.FirstBrush;
        public static Brush SecondBrush => Current.SecondBrush;
    }
}
