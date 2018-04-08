using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SingularityForensic.Contracts.Hex {
    //搜寻方法;
    public enum FindMethod {
        Text,                                                               //文字检索;
        Hex                                                                 //十六进制检索;
    }
    

    public interface IHexDataContext:IUIObjectProvider2 {
        bool ReadOnlyMode { get; set; }
        Stream Stream { get; set; }
        long SelectionStart { get; set; }
        long SelectionLength { get; set; }
        long Position { get; set; }
        long FocusPosition { get; set; }
        IList<(long index, long length, Brush background)> CustomBackgroundBlocks { get; }
        object Tag { get; set; }
    }

    
}
