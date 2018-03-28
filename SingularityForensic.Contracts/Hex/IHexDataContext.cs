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
    

    public interface IHexDataContext {
        bool ReadOnlyMode { get; }
        Stream Stream { get; set; }
        long SelectionStart { get; set; }
        long SelectionStop { get; set; }
        long Position { get; set; }
        long FocusPosition { get; set; }
        ObservableCollection<(long index, long length, Brush background)> CustomBackgroundBlocks { get; set; }
        object Tag { get; set; }
    }

    
}
