using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SingularityForensic.Contracts.Hex {
    public interface ICustomBackgroundBlock {
        long StartOffset { get; set; }
        long Length { get; set; }
        Brush Background { get; set; }
    }
}
