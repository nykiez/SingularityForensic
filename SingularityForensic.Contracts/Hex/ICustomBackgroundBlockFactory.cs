using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SingularityForensic.Contracts.Hex {
    public interface ICustomBackgroundBlockFactory {
        ICustomBackgroundBlock CreateNewBackgroundBlock(long startOffset,long length,Brush background);
        ICustomBackgroundBlock CreateNewBackgroundBlock();

        //区分相邻块的两种颜色;
        Brush FirstBrush { get; }
        Brush SecondBrush { get; }
    }

    public class CustomBackgroundBlockFactory : GenericServiceStaticInstance<ICustomBackgroundBlockFactory> {
        public static ICustomBackgroundBlock CreateNewBackgroundBlock(long startOffset, long length, Brush background)
            => Current.CreateNewBackgroundBlock(startOffset, length, background);
        public static ICustomBackgroundBlock CreateNewBackgroundBlock() => Current.CreateNewBackgroundBlock();

        public static Brush FirstBrush => Current.FirstBrush;
        public static Brush SecondBrush => Current.SecondBrush;
    }
}
