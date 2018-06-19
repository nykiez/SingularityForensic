using SingularityForensic.Contracts.Hex;
using System.Windows.Media;

namespace SingularityForensic.Hex {
    public class BrushBlock : IBrushBlock {
        public long StartOffset { get ; set ; }
        public long Length { get ; set ; }
        public Brush Background { get ; set; }
    }
}
